using Microsoft.Office.Interop.Excel;
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

namespace PrototipoSistema.classes
{
    public class motos
    {
        public int index { get; set; }
        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string cor {  get; set; }
        public string ano { get; set; }
        public string chassi { get; set; }
        public DateTime dt_registro { get; set; }
        public string doc_dono { get; set; }
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
                    cmd.CommandText = "SELECT controle FROM motos ORDER BY controle DESC LIMIT 1";
                    var res = cmd.ExecuteScalar();
                    index = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
            }
            catch { if (!usarLocal) ultimo_index(true); }
        }

        public void cadastrar_moto(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "INSERT INTO motos (controle, placa, marca, modelo, cor, ano, chassi, dt_registro, doc_dono, observacao) values (@controle,@placa,@marca,@modelo,@cor,@ano,@chassi,@dt_registro,@doc_dono,@observacao)";

                    PreencherParametrosMoto(cmd);
                    cmd.ExecuteNonQuery();

                    if (!usarLocal) MessageBox.Show("Moto cadastrada no servidor!");
                }
            }
            catch { if (!usarLocal) cadastrar_moto(true); }
        }

        public void alterar_moto(bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal) : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    // Aqui usamos a placa original da classe estática como filtro
                    cmd.CommandText = @"UPDATE motos SET placa = @placa, marca = @marca, modelo = @modelo, cor = @cor, ano = @ano, chassi = @chassi, observacao = @observacao WHERE placa = @placa_antiga";

                    PreencherParametrosMoto(cmd);

                    // Parâmetro extra para o WHERE
                    var pExtra = cmd.CreateParameter();
                    pExtra.ParameterName = "@placa_antiga";
                    pExtra.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(pExtra);

                    cmd.ExecuteNonQuery();
                }
            }
            catch { if (!usarLocal) alterar_moto(true); }
        }

        private void PreencherParametrosMoto(System.Data.Common.DbCommand cmd)
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
            Add("@marca", marca);
            Add("@modelo", modelo);
            Add("@cor", cor);
            Add("@ano", ano);
            Add("@chassi", chassi);
            Add("@dt_registro", dt_registro);
            Add("@doc_dono", doc_dono);
            Add("@observacao", observacao);
        }

    }
}
