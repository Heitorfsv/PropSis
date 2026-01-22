using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrototipoSistema.classes
{
    public class pecas
    {
        public int index { get; set; }
        public string nome { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public decimal valor_pago { get; set; }
        public decimal impostos { get; set; }
        public decimal valor_sugerido { get; set; }
        public string fornecedor { get; set; }
        public string contato { get; set; }
        public string local { get; set; }
        public string estoque { get; set; }

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
                    cmd.CommandText = "SELECT controle FROM pecas ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_pecas(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = @"INSERT INTO pecas (controle, nome, marca, modelo, valor_pago, impostos, valor_sugerido, fornecedor, contato, local, estoque) 
                                values (@controle, @nome, @marca, @modelo, @valor_pago, @impostos, @valor_sugerido, @fornecedor, @contato, @local, @estoque)";

                    PreencherParametrosPecas(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Peça cadastrada no servidor!");
                }
            }
            catch { if (!usarLocal) cadastrar_pecas(true); }
        }

        public void alterar_pecas(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Usando a variável estática diretamente no WHERE como você solicitou
                    cmd.CommandText = $@"UPDATE pecas SET nome = @nome, marca = @marca, modelo = @modelo, valor_pago = @valor_pago, impostos = @impostos, valor_sugerido = @valor_sugerido, fornecedor = @fornecedor, 
                                contato = @contato, local = @local, estoque = @estoque WHERE nome = '{static_class.doc_consultar}'";

                    PreencherParametrosPecas(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Dados da peça atualizados!");
                }
            }
            catch { if (!usarLocal) alterar_pecas(true); }
        }

        private void PreencherParametrosPecas(System.Data.Common.DbCommand cmd)
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
            Add("@marca", marca);
            Add("@modelo", modelo);
            Add("@valor_pago", valor_pago);
            Add("@impostos", impostos);
            Add("@valor_sugerido", valor_sugerido);
            Add("@fornecedor", fornecedor);
            Add("@contato", contato);
            Add("@local", local);
            Add("@estoque", estoque);
        }
    }
}
