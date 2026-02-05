using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PrototipoSistema
{
    public class servicos
    {
        public int index { get; set; }
        public string nome { get; set; }
        public decimal valor { get; set; }

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
                    cmd.CommandText = "SELECT MAX(controle) FROM servicos";
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
                    cmd.CommandText = "SELECT MAX(controle) FROM servicos";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; }
            }

            // 3. Define o index como o maior valor encontrado + 1 (ou apenas o maior se você somar depois)
            // Mantendo sua lógica de apenas retornar o valor do último controle encontrado:
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_servicos()
        {
            // 1. SEMPRE Local primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();
                    cmdLocal.CommandText = "INSERT INTO servicos (controle, nome, valor, sync) values (@controle, @nome, @valor, 0)";
                    PreencherParametrosServico(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro local: " + ex.Message);
                    return;
                }
            }

            // 2. Tenta o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();
                    cmdRemoto.CommandText = "INSERT INTO servicos (controle, nome, valor) values (@controle, @nome, @valor)";
                    PreencherParametrosServico(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. SE deu certo no MySQL, usa a FUNÇÃO MESTRE pelo ID (controle)
                    static_class.AtualizarStatusSync("servicos", index, 1);

                    MessageBox.Show("Serviço cadastrado e sincronizado!");
                }
                catch
                {
                    MessageBox.Show("Serviço salvo localmente (Offline).");
                }
            }
        }

        public void alterar_servico()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Usamos o ID (controle) no WHERE para total segurança
                    // E resetamos o sync para 0 (Pendente)
                    cmdLocal.CommandText = @"UPDATE servicos SET 
                                    nome = @nome, 
                                    valor = @valor, 
                                    sync = 0 
                                    WHERE controle = @id";

                    PreencherParametrosServico(cmdLocal);

                    // Adicionamos o parâmetro do ID manualmente se ele não estiver no PreencherParametros
                    cmdLocal.Parameters.AddWithValue("@id", index);

                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar serviço localmente: " + ex.Message);
                    return;
                }
            }

            // 2. Agora tenta replicar para o MySQL (Servidor)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"UPDATE servicos SET 
                                    nome = @nome, 
                                    valor = @valor 
                                    WHERE controle = @id";

                    PreencherParametrosServico(cmdRemoto);
                    cmdRemoto.Parameters.AddWithValue("@id", index);

                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, usamos a FUNÇÃO MESTRE pelo ID
                    static_class.AtualizarStatusSync("servicos", index, 1);

                    MessageBox.Show("Serviço atualizado e sincronizado!", "Sucesso");
                }
                catch (Exception)
                {
                    // Servidor offline: o dado está salvo com sync=0 no SQLite
                    MessageBox.Show("Alteração salva localmente. Sincronização pendente.", "Modo Offline");
                }
            }
        }

        private void PreencherParametrosServico(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@nome", nome);
            Add("@valor", valor);
        }
    }
}
