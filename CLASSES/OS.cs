using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PrototipoSistema.classes
{
    public class OS
    {
        public int index { get; set; }
        public string placa {  get; set; }
        public string cliente { get; set; }
        public string doc { get; set; }
        public int km { get; set; }
        public string observacao { get; set; }
        public string descricao { get; set; }
        public string total { get; set; }
        public string dt_cadastro { get; set; }
        public string dt_saida { get; set; }
        public string aviso_oleo {  get; set; }
        public string aviso_revisao { get; set; }
        public int pago { get; set; }
        public string metodo { get; set; }

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
                    cmd.CommandText = "SELECT controle FROM os ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_os(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = @"INSERT INTO os (controle, placa, cliente, doc, km, observacao, descricao, total, dt_cadastro, dt_saida, aviso_oleo, aviso_revisao, pago, metodo_pag) 
                                values (@controle,@placa,@cliente,@doc,@km,@observacao,@descricao,@total,@dt_cadastro,@dt_saida,@aviso_oleo,@aviso_revisao,@pago,@metodo_pag)";

                    PreencherParametrosOS(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Ordem de Serviço salva no servidor!");
                }
            }
            catch (Exception w) { MessageBox.Show(w.ToString()); if (!usarLocal) cadastrar_os(true); }
        }

        public void alterar_os(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = @"UPDATE os SET placa = @placa, km = @km, cliente = @cliente, observacao = @observacao, descricao = @descricao, total = @total, dt_cadastro = @dt_cadastro, aviso_oleo = @aviso_oleo, aviso_revisao = @aviso_revisao, 
                                dt_saida = @dt_saida, pago = @pago, metodo_pag = @metodo_pag WHERE controle = @controle";

                    PreencherParametrosOS(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Ordem de Serviço atualizada!");
                }
            }
            catch { if (!usarLocal) alterar_os(true); }
        }

        private void PreencherParametrosOS(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@placa", placa);
            Add("@km", km);
            Add("@cliente", cliente);
            Add("@doc", doc);
            Add("@observacao", observacao);
            Add("@descricao", descricao);
            Add("@total", total);
            Add("@dt_cadastro", dt_cadastro);
            Add("@aviso_oleo", aviso_oleo);
            Add("@aviso_revisao", aviso_revisao);
            Add("@dt_saida", dt_saida);
            Add("@pago", pago);
            Add("@metodo_pag", metodo); 
        }
    }
}
