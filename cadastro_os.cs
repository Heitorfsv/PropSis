using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class cadastro_os : Form
    {
        OS os = new OS();
        string doc_cliente;

        public cadastro_os()
        {
            InitializeComponent();
        }
        private void cadastro_os_Load(object sender, EventArgs e)
        {
            dtp_saida.Enabled = false;
            dtp_cadastro.Value = DateTime.Now;

            os.ultimo_index();
            os.index++;
            static_class.controle_os = os.index;
        }

        private void bnt_add_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();

            add_pecas add_pecas = new add_pecas();
            add_pecas.Show();
        }

        private void bnt_cadastrar_Click(object sender, EventArgs e)
        {


            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            cliente cliente = new cliente();

            if (reader.Read())
            { 
                os.placa = cmb_placa.Text;
                os.km = int.Parse(txt_km.Text);
                os.cliente = txt_cliente.Text;
                os.doc = doc_cliente;
                os.observacao = txt_observacao.Text;

                if (txt_total.Text != "")
                { os.total = txt_total.Text; }
                else
                { os.total = "0,00"; }

                if (cb_pago.Checked == true)
                { os.pago = 1; }
                else
                { os.pago = 0; }

                os.dt_cadastro = dtp_cadastro.Value;

                if (cb_saida.Checked == true)
                { os.dt_saida = dtp_saida.Value.ToString("dd/MM/yyyy"); }
                else
                { os.dt_saida = null; }

                os.cadastrar_os();

                cliente.doc = txt_doc.Text;
                cliente.quitado();

                os.index++;
            }
            else
            { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            conexao.Close();
        }

        private void cadastro_os_FormClosing(object sender, FormClosingEventArgs e)
        {
            int delete = 0;
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT controle FROM os WHERE controle = '{os.index}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            { }
            else
            { delete = 1; }
            conexao.Close();

            if (delete == 1)
            {
                cmd = new MySqlCommand($"DELETE FROM pecas_os WHERE os = '{os.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();

                cmd = new MySqlCommand($"DELETE FROM servicos_os WHERE os = '{os.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();
            }
        }

        private void bnt_add_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add_servicos add_servicos = new add_servicos();
            add_servicos.Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (static_class.close == 1)
            {
                lst_servicos.Items.Clear();
                lst_servicos_qtd.Items.Clear();
                lst_servico_total.Items.Clear();

                lst_pecas.Items.Clear();
                lst_pecas_qtd.Items.Clear();
                lst_peca_total.Items.Clear();

                decimal servico_total = 0;
                decimal peca_total = 0;
                string total = "";

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_servicos.Items.Add(reader.GetString("nome"));
                    lst_servicos_qtd.Items.Add(reader.GetString("qtd"));
                    total = reader.GetString("valor");
                    lst_servico_total.Items.Add(total);

                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { servico_total += decimal.Parse(qtd) * decimal.Parse(reader.GetString("valor")); }
                    catch { }
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_pecas.Items.Add(reader.GetString("nome"));
                    lst_pecas_qtd.Items.Add(reader.GetString("qtd"));
                    total = reader.GetString("valor");
                    lst_peca_total.Items.Add(total);

                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { peca_total += decimal.Parse(qtd) * decimal.Parse(reader.GetString("valor")); }
                    catch { }
                }
                conexao.Close();

                txt_total_servico.Text = servico_total.ToString("N2");
                txt_total_pecas.Text = peca_total.ToString("N2");
                txt_total.Text = (peca_total + servico_total).ToString("N2");

                static_class.close = 0;
            }
        }

        private void lst_pecas_MouseDown(object sender, MouseEventArgs e)
        {
            object index = lst_pecas.SelectedIndex;
        }

        private void cb_saida_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_saida.Checked == true)
            { dtp_saida.Enabled = true; }
            else
            {  dtp_saida.Enabled = false;}
        }

        private void cmb_placa_TextChanged_1(object sender, EventArgs e)
        {
            // cmb_placa.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa LIKE '%{cmb_placa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_placa.Items.Add(reader.GetString("placa"));
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_ano.Text = reader.GetString("ano");
                doc_cliente = reader.GetString("doc_dono");
                txt_doc.Text = doc_cliente;
            }

            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{doc_cliente}'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_cliente.Text = reader.GetString("nome");
                txt_telefone.Text = reader.GetString("telefone");
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM os WHERE aviso_oleo_km = '*' AND placa = '{cmb_placa.Text}' ORDER BY dt_cadastro DESC", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txt_trocaoleo.Text = reader.GetString("dt_saida");
                decimal km_antigo = reader.GetInt64("km");




            }
            conexao.Close();

            if (cmb_placa.Text == "" || cmb_placa.Text == " ")
            {
                txt_marca.Text = "";
                txt_modelo.Text = "";
                txt_ano.Text = "";
                doc_cliente = "";
                txt_doc.Text = "";
                txt_cliente.Text = "";
                txt_telefone.Text = "";
            }
        }
    }
}
