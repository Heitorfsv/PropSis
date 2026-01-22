using MySql.Data.MySqlClient;
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
                    cmd.CommandText = "SELECT controle FROM orcamentos ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_or(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = @"INSERT INTO orcamentos (controle, cliente, doc, km, placa, dt_cadastro, total, observacao) 
                                VALUES (@controle, @cliente, @doc, @km, @placa, @dt_cadastro, @total, @observacao)";

                    PreencherParametrosOrcamento(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Orçamento salvo no servidor!", "Sucesso");
                }
            }
            catch { if (!usarLocal) cadastrar_or(true); }
        }

        public void alterar_or(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = @"UPDATE orcamentos SET cliente = @cliente, doc = @doc, km = @km, placa = @placa, dt_cadastro = @dt_cadastro, total = @total, observacao = @observacao WHERE controle = @controle";

                    PreencherParametrosOrcamento(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Orçamento atualizado no servidor!", "Sucesso");
                }
            }
            catch { if (!usarLocal) alterar_or(true); }
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
