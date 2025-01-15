using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class historico_moto : Form
    {
        public string placa;
        public string marca;
        public string modelo;
        public string ano;
        public historico_moto()
        {
            InitializeComponent();
        }

        private void historico_moto_Load(object sender, EventArgs e)
        {
            List<string> doc = new List<string>();
            List<string> dt_registro = new List<string>();

            txt_placa.Text = placa;
            txt_marca.Text = marca;
            txt_modelo.Text = modelo;
            txt_ano.Text = ano;

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT doc_dono, dt_registro FROM motos WHERE placa = '{txt_placa.Text}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();   

            while (reader.Read())
            {   
                doc.Add(reader.GetString("doc_dono"));
                dt_registro.Add(reader.GetDateTime("dt_registro").ToString("dd,MM,yyyy"));
            }
            conexao.Close();

            int count = 0;

            while (count <  doc.Count)
            {
                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{doc[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_clientes.Items.Add(reader.GetString("nome"));
                    lst_dt_registro.Items.Add(dt_registro[count].ToString());
                }
                conexao.Close();

                count++;
            }
        }
    }
}
