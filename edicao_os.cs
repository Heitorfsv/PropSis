using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using PrototipoSistema.classes;
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
    public partial class edicao_os : Form
    {
        string doc_cliente;
        OS os = new OS();
        public edicao_os()
        {
            InitializeComponent();
        }

        private void edicao_os2_Load(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = '{static_class.controle_os}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                os.index = reader.GetInt32("controle");
                cmb_placa.Text = reader.GetString("placa");
                txt_km.Text = reader.GetInt32("km").ToString();
                dtp_cadastro.Value = reader.GetDateTime("dt_cadastro");
                txt_observacao.Text = reader.GetString("observacao");
                try
                { 
                    dtp_saida.Value = DateTime.Parse(reader.GetString("dt_saida"));
                    dtp_saida.Enabled = true;
                    cb_saida.Checked = true;
                }
                catch
                { dtp_saida.Enabled = false; }

                if (reader.GetInt32("pago") == 1)
                { cb_pago.Checked = true; }
                else
                { cb_pago.Checked = false; }
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
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

            cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();
            decimal total_servico = 0;
            string total = "";

            while (reader.Read())
            {
                lst_servicos.Items.Add(reader.GetString("nome"));
                lst_servicos_qtd.Items.Add(reader.GetString("qtd"));
                total = reader.GetString("valor");
                lst_servico_total.Items.Add(total);
                total_servico += decimal.Parse(reader.GetString("valor")) * decimal.Parse(reader.GetString("qtd"));
            }
            txt_total_servico.Text = total_servico.ToString("N2");
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();
            decimal total_peca = 0;

            while (reader.Read())
            {
                lst_pecas.Items.Add(reader.GetString("nome"));
                lst_pecas_qtd.Items.Add(reader.GetString("qtd"));
                total = reader.GetString("valor");
                lst_peca_total.Items.Add(total);
          
                try
                { total_peca += decimal.Parse(reader.GetString("valor")) * decimal.Parse(reader.GetString("qtd")); }
                catch { }
            }
            txt_total_pecas.Text = total_peca.ToString("N2");
            conexao.Close();

            txt_total.Text = (total_peca + total_servico).ToString("N2");

            ///////////////////////////////////////
            
            cmd = new MySqlCommand($"SELECT * FROM os WHERE aviso_oleo = '*' AND placa = '{cmb_placa.Text}' ORDER BY dt_cadastro DESC", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txt_trocaoleo.Text = reader.GetString("dt_saida");
                decimal km_antigo = reader.GetInt64("km");

                TimeSpan data_troca = DateTime.Now - DateTime.Parse(txt_trocaoleo.Text);

                if (data_troca.TotalDays >= 180)
                { MessageBox.Show("Troca de oleo a 6 meses"); }
                if (data_troca.TotalDays >= 365)
                { MessageBox.Show("Troca de oleo a 1 ano"); }
                
                if (int.Parse(txt_km.Text) - km_antigo >= 3000)
                {
                    MessageBox.Show("Ultima troca de oleo a " + (int.Parse(txt_km.Text) - km_antigo).ToString() + " KM");
                }
                txt_trocakm.Text = (int.Parse(txt_km.Text) - km_antigo).ToString() + " KM";
            }
            conexao.Close();
        }

        private void cmb_placa_TextChanged(object sender, EventArgs e)
        {
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
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc LIKE '%{doc_cliente}%'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_cliente.Text = reader.GetString("nome");
                txt_telefone.Text = reader.GetString("telefone");
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

        private void bnt_editar_Click(object sender, EventArgs e)
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
                os.cliente = txt_cliente.Text;
                os.doc = txt_doc.Text;
                os.km = int.Parse(txt_km.Text);
                os.observacao = txt_observacao.Text;
                os.total = txt_total.Text;

                if (dtp_saida.Enabled == true)
                { os.dt_saida = dtp_saida.Value.ToString("dd/MM/yyyy"); }
                else 
                { os.dt_saida = null; }

                if (cb_pago.Checked == true)
                { os.pago = 1; }
                else
                { os.pago = 0; }

                if (lst_servicos.Items.Contains("TROCA DE OLEO"))
                { os.aviso_oleo = "*"; }
            }
            else
            { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            conexao.Close();

            try
            {
                os.alterar_os();

                cliente.doc = txt_doc.Text;
                cliente.quitado();

                MessageBox.Show("OS Alterada!", "JCMotorsport", MessageBoxButtons.OK);
            }
            catch { }
        }

        private void bnt_add_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();    

            add_pecas add_pecas = new add_pecas();
            add_pecas.Show();
        }

        private void bnt_add_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add_servicos add_servicos = new add_servicos();
            add_servicos.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
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
                    try
                    { servico_total += decimal.Parse(reader.GetString("qtd")) * decimal.Parse(reader.GetString("valor")); }
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
                    try
                    { peca_total += decimal.Parse(reader.GetString("qtd")) * decimal.Parse(reader.GetString("valor")); }
                    catch { }
                }
                conexao.Close();

                txt_total_servico.Text = servico_total.ToString("N2");
                txt_total_pecas.Text = peca_total.ToString("N2");
                txt_total.Text = (peca_total + servico_total).ToString("N2");

                static_class.close = 0;
            }
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM os WHERE controle = '{static_class.controle_os}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
            Close();
        }

        private void cb_saida_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_saida.Checked == true)
            { dtp_saida.Enabled = true; }
            else
            { dtp_saida.Enabled = false; }
        }
    }
}
