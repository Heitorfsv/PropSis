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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            listView1.Columns.Clear();
            listView1.Columns.Add("Cliente", 200);
            listView1.Columns.Add("Data de Registro", 120);
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

            for (int i = 0; i < doc.Count; i++)
            {
                string nome = "", datas = dt_registro[i];
                

                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{doc[i]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    nome = reader.GetString("nome");
                } 
                conexao.Close();

                var item = new ListViewItem(nome);
                item.SubItems.Add(datas);
                listView1.Items.Add(item);
            }
        }
    }
}
