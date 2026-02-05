using MySql.Data.MySqlClient;
using PrototipoSistema;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace classes
{
    public class metodo_pag
    {
        public int index { get; set; }
        public string metodo { get; set; }
        public string agencia { get; set; }
        public int parcelas { get; set; }

        public void ultimo_index()
        {
            int indexLocal = 0;
            int indexRemoto = 0;

            // 1. Busca o maior ID no SQLite
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmd = conLocal.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM metodo_pag";
                    var res = cmd.ExecuteScalar();
                    indexLocal = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexLocal = 0; }
            }

            // 2. Busca o maior ID no MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmd = conRemoto.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM metodo_pag";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; }
            }

            // 3. Define o index como o maior valor encontrado + 1 (ou apenas o maior se você somar depois)
            // Mantendo sua lógica de apenas retornar o valor do último controle encontrado:
            index = Math.Max(indexLocal, indexRemoto);
        }

        // 1. O CADASTRO (Adaptado para Local-First)
        public void cadastrar_metodo()
        {
            // SEMPRE Local primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Inicia com sync = 0 (Pendente)
                    cmdLocal.CommandText = @"INSERT INTO metodo_pag (controle, metodo, agencia, parcelas, sync) 
                                    VALUES (@controle, @metodo, @agencia, @parcelas, 0)";

                    PreencherParametrosMetodo(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro local: " + ex.Message);
                    return;
                }
            }

            // Tenta o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();
                    cmdRemoto.CommandText = @"INSERT INTO metodo_pag (controle, metodo, agencia, parcelas) 
                                    VALUES (@controle, @metodo, @agencia, @parcelas)";

                    PreencherParametrosMetodo(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // Se funcionou, marca como sincronizado (1)
                    static_class.AtualizarStatusSync("metodo_pag", index, 1);

                    MessageBox.Show("Método cadastrado e sincronizado!");
                }
                catch
                {
                    MessageBox.Show("Salvo localmente. Sincronização pendente.");
                }
            }
        }

        public void alterar_metodo()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Ao alterar, voltamos o sync para 0 (Pendente)
                    cmdLocal.CommandText = @"UPDATE metodo_pag SET 
                                    metodo = @metodo, 
                                    banco = @agencia, 
                                    parcelas = @parcelas,
                                    sync = 0 
                                    WHERE controle = @controle";

                    PreencherParametrosMetodo(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao alterar método localmente: " + ex.Message);
                    return;
                }
            }

            // 2. Tenta replicar para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"UPDATE metodo_pag SET 
                                    metodo = @metodo, 
                                    banco = @agencia, 
                                    parcelas = @parcelas 
                                    WHERE controle = @controle";

                    PreencherParametrosMetodo(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, marca como sincronizado (1) no local
                    static_class.AtualizarStatusSync("metodo_pag", index, 1);

                    MessageBox.Show("Método de pagamento alterado e sincronizado!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o sync continua 0 no SQLite
                    MessageBox.Show("Alteração salva localmente. Sincronização pendente com o servidor.", "Modo Offline");
                }
            }
        }

        private void PreencherParametrosMetodo(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@metodo", metodo);
            Add("@agencia", agencia); 
            Add("@parcelas", parcelas);
        }
    }
}
