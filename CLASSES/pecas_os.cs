using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PrototipoSistema
{
    public class pecas_os
    {
        public int index {  get; set; }
        public int os_or { get; set; }
        public string modo { get; set; } // Modo pode ser "os" ou "orca"
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
                    cmd.CommandText = "SELECT controle FROM pecas_os ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_peca_os(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Mantive a interpolação apenas na coluna {modo}, pois nomes de colunas não podem ser parâmetros
                    cmd.CommandText = $@"INSERT INTO pecas_os (controle, {modo}, nome, valor, qtd, desco, pos) values (@controle, @modo, @nome, @valor, @qtd, @desco, @pos)";

                    PreencherParametrosPecasOS(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Item adicionado ao servidor!");
                }
            }
            catch { if (!usarLocal) cadastrar_peca_os(true); }
        }

        private void PreencherParametrosPecasOS(System.Data.Common.DbCommand cmd)
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
