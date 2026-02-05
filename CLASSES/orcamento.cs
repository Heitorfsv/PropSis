using MySql.Data.MySqlClient;
using PrototipoSistema;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace classes
{
    internal class orcamento
    {
        public int index { get; set; }
        public string placa { get; set; }
        public int km { get; set; }
        public string cliente { get; set; }
        public string doc { get; set; }
        public string total { get; set; }
        public string dt_cadastro { get; set; }
        public string observacao { get; set; }

        public void ultimo_index()
        {
            int indexLocal = 0;
            int indexRemoto = 0;

            // 1. Busca o maior ID no banco Local (SQLite)
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmd = conLocal.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM orcamentos";
                    var res = cmd.ExecuteScalar();
                    indexLocal = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexLocal = 0; }
            }

            // 2. Busca o maior ID no banco Remoto (MySQL)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmd = conRemoto.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM orcamentos";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; } // Se falhar (offline), assume 0 e usa o local como base
            }

            // 3. O índice final será o maior valor encontrado entre os dois bancos
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_or()
        {
            // 1. SEMPRE grava no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Incluímos a coluna sync = 0
                    cmdLocal.CommandText = @"INSERT INTO orcamentos (controle, cliente, doc, km, placa, dt_cadastro, total, observacao, sync) 
                                    VALUES (@controle, @cliente, @doc, @km, @placa, @dt_cadastro, @total, @observacao, 0)";

                    PreencherParametrosOrcamento(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar orçamento localmente: " + ex.Message);
                    return; // Se não salvou no PC, para aqui para não perder os dados.
                }
            }

            // 2. Tenta replicar para o MySQL (Servidor)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"INSERT INTO orcamentos (controle, cliente, doc, km, placa, dt_cadastro, total, observacao) 
                                    VALUES (@controle, @cliente, @doc, @km, @placa, @dt_cadastro, @total, @observacao)";

                    PreencherParametrosOrcamento(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, marcamos como sincronizado (1)
                    static_class.AtualizarStatusSync("orcamentos", index, 1);

                    MessageBox.Show("Orçamento cadastrado e sincronizado com sucesso!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o dado já está no SQLite seguro com sync=0
                    MessageBox.Show("Orçamento salvo localmente (Modo Offline). A sincronização ocorrerá em breve.", "Informação");
                }
            }
        }

        public void alterar_or()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Atualiza os dados e reseta o sync para 0 (Pendente)
                    cmdLocal.CommandText = @"UPDATE orcamentos SET 
                                    cliente = @cliente, doc = @doc, km = @km, 
                                    placa = @placa, dt_cadastro = @dt_cadastro, 
                                    total = @total, observacao = @observacao, 
                                    sync = 0 
                                    WHERE controle = @controle";

                    PreencherParametrosOrcamento(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar orçamento localmente: " + ex.Message);
                    return;
                }
            }

            // 2. Agora tenta replicar para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"UPDATE orcamentos SET 
                                    cliente = @cliente, doc = @doc, km = @km, 
                                    placa = @placa, dt_cadastro = @dt_cadastro, 
                                    total = @total, observacao = @observacao 
                                    WHERE controle = @controle";

                    PreencherParametrosOrcamento(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, volta o sync para 1
                    static_class.AtualizarStatusSync("orcamentos", index, 1);

                    MessageBox.Show("Orçamento atualizado e sincronizado!", "Sucesso");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o dado está salvo com sync=0 no SQLite
                    MessageBox.Show("Alteração salva localmente. O servidor será atualizado na próxima sincronização.", "Modo Offline");
                }
            }
        }

        private void PreencherParametrosOrcamento(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@cliente", cliente);
            Add("@doc", doc);
            Add("@km", km);
            Add("@placa", placa);
            Add("@dt_cadastro", dt_cadastro);
            Add("@total", total);
            Add("@observacao", observacao);
        }
    }
}
