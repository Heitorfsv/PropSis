using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrototipoSistema
{
    public class servicos_os
    {
        public int index { get; set; }
        public int os_or { get; set; }
        public string modo { get; set; }// Modo pode ser "os" ou "orca"
        public string nome { get; set; }
        public string valor { get; set; }
        public string desc { get; set; }
        public decimal qtd { get; set; }
        public int pos { get; set; }

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
                    cmd.CommandText = "SELECT controle FROM servicos_os ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_servico_os(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Mantemos a coluna {modo} dinâmica via string, os valores via parâmetros
                    cmd.CommandText = $@"INSERT INTO servicos_os (controle, {modo}, nome, valor, qtd, desco, pos) VALUES (@controle, @modo, @nome, @valor, @qtd, @desco, @pos)";

                    PreencherParametrosServicoOS(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Serviço adicionado ao servidor!");
                }
            }
            catch { if (!usarLocal) cadastrar_servico_os(true); }
        }

        private void PreencherParametrosServicoOS(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@modo", os_or);
            Add("@nome", nome);
            Add("@valor", valor);
            Add("@qtd", qtd);
            Add("@desco", desc);
            Add("@pos", pos);
        }
    }
}
