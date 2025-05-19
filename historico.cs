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

            int count = 0;
            List<string> lista_placa = new List<string>();

            while (count < lista_os.Count)
            {
                cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = '{lista_os[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    try { lst_dt_saida.Items.Add(reader.GetString("dt_saida")); }
                    catch { }

                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    lista_placa.Add(reader.GetString("placa"));
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{lista_placa[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_marca.Items.Add(reader.GetString("marca"));
                    lst_modelo.Items.Add(reader.GetString("modelo"));
                }
                conexao.Close();

                count++;
            }
        }

        private void lst_dt_saida_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_dt_saida.SelectedIndex;
        }

        private void lst_cliente_Click(object sender, EventArgs e)
        {
            try { lst_dt_saida.SelectedIndex = lst_cliente.SelectedIndex; } catch { }
            lst_marca.SelectedIndex = lst_cliente.SelectedIndex;
            lst_modelo.SelectedIndex = lst_cliente.SelectedIndex;
        }

        private void lst_marca_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_marca.SelectedIndex;
        }

        private void lst_modelo_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_modelo.SelectedIndex;
        }

        private void lst_dt_saida_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_cliente.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_cliente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_cliente.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_marca_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_cliente.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_modelo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_cliente.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }
    }
}
