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
                    cmd.CommandText = "SELECT controle FROM servicos ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_servicos(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "INSERT INTO servicos (controle, nome, valor) values (@controle, @nome, @valor)";

                    PreencherParametrosServico(cmd);
                    cmd.ExecuteNonQuery();

                    //if (!usarLocal) MessageBox.Show("Serviço cadastrado com sucesso!");
                }
            }
            catch { if (!usarLocal) cadastrar_servicos(true); }
        }

        public void alterar_servico(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Usando a variável estática no WHERE e parâmetros no SET
                    cmd.CommandText = $@"UPDATE servicos SET nome = @nome, valor = @valor WHERE nome = '{static_class.doc_consultar}'";

                    PreencherParametrosServico(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Serviço atualizado com sucesso!");
                }
            }
            catch { if (!usarLocal) alterar_servico(true); }
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
