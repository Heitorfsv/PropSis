using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class historico : Form
    {
        List<int> lista_os = new List<int>();
        public historico()
        {
            InitializeComponent();

            listView1.Columns.Clear();
            listView1.Columns.Add("Data Saída", 100);
            listView1.Columns.Add("Cliente", 150);
            listView1.Columns.Add("Placa", 80);
            listView1.Columns.Add("Marca", 120);
            listView1.Columns.Add("Modelo", 150);
        }

        private void historico_Load(object sender, EventArgs e)
        {
            if (static_class.historico == "pecas_os") lbl_titulo.Text = "Faturamento da peça";
            else lbl_titulo.Text = "Faturamento do serviço";
            //////////////////////

            decimal total = 0;

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM {static_class.historico} WHERE nome = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista_os.Add(reader.GetInt32("os"));

                string qtd = reader.GetString("qtd");
                qtd = qtd.Replace(".", ",");
                total += decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd);
            }

            txt_total.Text = total.ToString("N2");
            conexao.Close();

            List<string> lista_placa = new List<string>();

            for (int i = 0; i < lista_os.Count; i++)
            {
                string dt_saida = "";
                string cliente = "";
                string placa = "";
                string marca = "";
                string modelo = "";

                cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = '{lista_os[i]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    try { dt_saida = reader.GetString("dt_saida"); } catch { }

                    cliente = reader.GetString("cliente");
                    placa = reader.GetString("placa");
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{placa}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    marca = reader.GetString("marca");
                    modelo = reader.GetString("modelo");
                }
                conexao.Close();

                var item = new ListViewItem(dt_saida);
                item.SubItems.Add(cliente);
                item.SubItems.Add(placa);
                item.SubItems.Add(marca);
                item.SubItems.Add(modelo);

                listView1.Items.Add(item);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            int index = listView1.SelectedIndices[0];
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle = lista_os[index];
                edicao_os.Text = "Edição OS";
                edicao_os.Show();
            }
            catch { }
        }
    }
}
