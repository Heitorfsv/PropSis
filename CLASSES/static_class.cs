using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrototipoSistema
{
    public static class static_class
    {
        public static string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        public static string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public static string doc_consultar { get; set; }
        public static string doc_dono { get; set; }
        public static string historico { get; set; }
        public static int controle {  get; set; }

        public static void AtualizarStatusSync(string tabela, int idControle, int novoStatus)
        {
            using (var conLocal = new System.Data.SQLite.SQLiteConnection(strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmd = conLocal.CreateCommand();

                    // Usamos interpolação para o nome da tabela e parâmetros para os valores
                    cmd.CommandText = $"UPDATE {tabela} SET sync = @status WHERE controle = @id";
                    cmd.Parameters.AddWithValue("@status", novoStatus);
                    cmd.Parameters.AddWithValue("@id", idControle);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Como é uma função de fundo, apenas logamos o erro
                    System.Diagnostics.Debug.WriteLine("Erro de sincronia: " + ex.Message);
                }
            }
        }

        public static void ExecutarDelete(int index, string table)
        {
            int idControle = index;

            // 1. SOFT DELETE no Local (SQLite)
            // Em vez de apagar, marcamos sync = 2. Assim ele some da sua tela (usando o filtro no SELECT)
            // e o sincronizador saberá que precisa apagar isso no servidor depois.
            static_class.AtualizarStatusSync(table, idControle, 2);

            // 2. Tenta o DELETE Real no MySQL (Servidor)
            using (var conRemoto = new MySql.Data.MySqlClient.MySqlConnection(strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();
                    cmdRemoto.CommandText = $"DELETE FROM {table} WHERE controle = @controle";
                    cmdRemoto.Parameters.AddWithValue("@controle", idControle);

                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, podemos apagar FISICAMENTE do SQLite
                    ApagarRegistroLocal(table, idControle);

                    MessageBox.Show($"Registro da tabela {table} excluído com sucesso!");
                }
                catch (Exception)
                {
                    // Se o servidor estiver fora, não tem problema. 
                    // O registro continua no SQLite com sync=2, invisível para o usuário,
                    // aguardando a internet voltar para ser deletado no MySQL.
                    MessageBox.Show("Excluído localmente. O servidor será atualizado assim que houver conexão.", "Modo Offline");
                }
            }
        }

        public static void ApagarRegistroLocal(string tabela, int idControle)
        {
            using (var conLocal = new System.Data.SQLite.SQLiteConnection(strLocal))
            {
                conLocal.Open();
                var cmd = conLocal.CreateCommand();
                cmd.CommandText = $"DELETE FROM {tabela} WHERE controle = @id";
                cmd.Parameters.AddWithValue("@id", idControle);
                cmd.ExecuteNonQuery();
            }
        }

        public static void SincronizarTabelaUniversal(string tabela, string[] colunas, string colunaVinculoItens = "")
        {
            using (var conLocal = new System.Data.SQLite.SQLiteConnection(strLocal))
            {
                conLocal.Open();
                // 1. Busca registros pendentes (sync=0) ou para deletar (sync=2)
                var cmdLocal = new System.Data.SQLite.SQLiteCommand($"SELECT * FROM {tabela} WHERE sync IN (0, 2)", conLocal);

                using (var reader = cmdLocal.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idSQLite = Convert.ToInt32(reader["controle"]);
                        int status = Convert.ToInt32(reader["sync"]);

                        // Se o status for 2, deleta no servidor e limpa local
                        if (status == 2)
                        {
                            ExecutarDelete(idSQLite, tabela); // Função que você já tem ou similar
                            continue;
                        }

                        // 2. Monta o Dicionário de parâmetros dinamicamente baseado nas colunas da tabela
                        var parametros = new Dictionary<string, object>();
                        foreach (string col in colunas)
                        {
                            parametros.Add("@" + col, reader[col]);
                        }

                        // 3. Tenta Inserir no MySQL com ajuste de ID
                        // Passamos o comando INSERT gerado dinamicamente
                        string sqlInsert = $"INSERT INTO {tabela} ({string.Join(",", colunas)}) VALUES (@{string.Join(",@", colunas)})";

                        int idConfirmado = AjusteID(tabela, sqlInsert, parametros, idSQLite);

                        // 4. Se o ID mudou no MySQL, re-vincula os filhos (Peças/Serviços) no SQLite
                        if (idConfirmado != idSQLite && !string.IsNullOrEmpty(colunaVinculoItens))
                        {
                            Re_vincularFilhosNoSQLite(colunaVinculoItens, idSQLite, idConfirmado);
                        }

                        // 5. Marca como sincronizado (sync=1)
                        AtualizarStatusSync(tabela, idSQLite, 1);
                    }
                }
            }
        }

        private static void Re_vincularFilhosNoSQLite(string coluna, int antigo, int novo)
        {
            using (var conLocal = new SQLiteConnection(strLocal))
            {
                conLocal.Open();
                var cmd = new SQLiteCommand($"UPDATE pecas_os SET {coluna} = @n WHERE {coluna} = @a; " +
                                            $"UPDATE servicos_os SET {coluna} = @n WHERE {coluna} = @a;", conLocal);
                cmd.Parameters.AddWithValue("@n", novo);
                cmd.Parameters.AddWithValue("@a", antigo);
                cmd.ExecuteNonQuery();
            }
        }

        public static int AjusteID(string tabela, string comandoSQL, Dictionary<string, object> parametros, int idTentado)
        {
            using (var conRemoto = new MySqlConnection(strConexao))
            {
                conRemoto.Open();
                int idFinal = idTentado;

                try
                {
                    // 1. Tenta inserir com o ID que veio do SQLite
                    var cmd = new MySqlCommand(comandoSQL, conRemoto);
                    foreach (var p in parametros) cmd.Parameters.AddWithValue(p.Key, p.Value);
                    cmd.ExecuteNonQuery();
                    return idFinal; // Sucesso com o ID original
                }
                catch (MySqlException ex) when (ex.Number == 1062) // Erro de Duplicidade (Duplicate Entry)
                {
                    // 2. Se falhar, busca o maior ID da tabela no servidor e soma +1
                    var cmdMax = new MySqlCommand($"SELECT COALESCE(MAX(controle), 0) + 1 FROM {tabela}", conRemoto);
                    idFinal = Convert.ToInt32(cmdMax.ExecuteScalar());

                    // 3. Tenta inserir novamente com o novo ID
                    var cmdRetry = new MySqlCommand(comandoSQL, conRemoto);
                    foreach (var p in parametros)
                    {
                        // Substitui o parâmetro do controle pelo novo ID
                        if (p.Key == "@controle") cmdRetry.Parameters.AddWithValue(p.Key, idFinal);
                        else cmdRetry.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    cmdRetry.ExecuteNonQuery();
                    return idFinal; // Retorna o ID ajustado
                }
            }
        }
    }
}
