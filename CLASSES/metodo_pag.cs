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

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public void ultimo_index(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT controle FROM metodo_pag ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_metodo(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "INSERT INTO metodo_pag (controle, metodo, agencia, parcelas) VALUES (@controle, @metodo, @agencia, @parcelas)";

                    PreencherParametrosMetodo(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal)
                        MessageBox.Show("Método de pagamento cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                if (!usarLocal) cadastrar_metodo(true);
                else MessageBox.Show("Erro ao salvar localmente.");
            }
        }

        public void alterar_metodo(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Usando parâmetros para evitar erros de aspas nos nomes dos agencia/métodos
                    cmd.CommandText = @"UPDATE metodo_pag SET 
                                metodo = @metodo, 
                                banco = @agencia, 
                                parcelas = @parcelas 
                                WHERE controle = @controle";

                    PreencherParametrosMetodo(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal)
                        MessageBox.Show("Método de pagamento alterado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                if (!usarLocal) alterar_metodo(true);
                else MessageBox.Show("Erro ao alterar localmente.");
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
