using classes;
using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class cadastro_or : Form
    {
        orcamento orcamento = new orcamento();
        string doc_cliente;
        OS os = new OS();

        public cadastro_or()
        {
            InitializeComponent();
        }

        private void cadastro_or_Load(object sender, EventArgs e)
        {
            if (this.Text == "Cadastro orçamento")
            {
                bnt_cadastro.Text = "Cadastrar";
                dtp_cadastro.Value = DateTime.Now;

                orcamento.ultimo_index();
                orcamento.index++;
                static_class.controle_os = orcamento.index;
            }
            else if (this.Text == "Edição orçamento")
            {
                bnt_cadastro.Text = "Editar";

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE controle = '{static_class.controle_os}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orcamento.index = reader.GetInt32("controle");
                    cmb_placa.Text = reader.GetString("placa");
                    dtp_cadastro.Value = DateTime.Parse(reader.GetString("dt_cadastro"));
                    txt_doc.Text = reader.GetString("doc");
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
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{doc_cliente}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmb_cliente.Text = reader.GetString("nome");
                    txt_telefone.Text = reader.GetString("telefone");
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

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

                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    total_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd));
                }
                txt_total_servico.Text = total_servico.ToString("N2");
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal total_peca = 0;

                while (reader.Read())
                {
                    lst_pecas.Items.Add(reader.GetString("nome"));
                    lst_pecas_qtd.Items.Add(reader.GetString("qtd"));
                    total = reader.GetString("valor");
                    lst_peca_total.Items.Add(total);

                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { total_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
                    catch { }
                }
                txt_total_pecas.Text = total_peca.ToString("N2");
                conexao.Close();

                txt_total.Text = (total_peca + total_servico).ToString("N2");
            }
        }

        private void bnt_cadastro_Click(object sender, EventArgs e)
        {
            if (this.Text == "Cadastro orçamento")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{txt_doc.Text}'", conexao);

                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    orcamento.cliente = cmb_cliente.Text;
                    orcamento.doc = txt_doc.Text;
                    orcamento.placa = cmb_placa.Text;
                    orcamento.total = txt_total.Text;
                    orcamento.dt_cadastro = dtp_cadastro.Value.ToString("dd/MM/yyyy");

                    if (txt_total.Text != "")
                    { orcamento.total = txt_total.Text; }
                    else
                    { orcamento.total = "0,00"; }

                    orcamento.cadastrar_or();

                    orcamento.index++;
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();
            }
            else if (this.Text == "Edição orçamento")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                cliente cliente = new cliente();

                if (reader.Read())
                {
                    orcamento.placa = cmb_placa.Text;
                    orcamento.cliente = cmb_cliente.Text;
                    orcamento.doc = txt_doc.Text;
                    orcamento.total = txt_total.Text;
                    orcamento.dt_cadastro = dtp_cadastro.Value.ToString();
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();

                try
                {
                    orcamento.alterar_or();

                    MessageBox.Show("OS Alterada!", "JCMotorsport", MessageBoxButtons.OK);
                }
                catch (Exception a) { MessageBox.Show(a.ToString()); }
            }
        }

        private void bnt_editar_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add_servicos add_servicos = new add_servicos();
            add_servicos.modo = "orca";
            add_servicos.Show();
        }

        private void bnt_editar_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();

            add_pecas add_pecas = new add_pecas();
            add_pecas.modo = "orca";
            add_pecas.Show();
        }

        private void cadastro_or_FormClosing(object sender, FormClosingEventArgs e)
        {
            int delete = 0;
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT controle FROM orcamentos WHERE controle = '{orcamento.index}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            { }
            else
            { delete = 1; }
            conexao.Close();
            
            if (delete == 1)
            {
                cmd = new MySqlCommand($"DELETE FROM pecas_os WHERE orca = '{orcamento.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();

                cmd = new MySqlCommand($"DELETE FROM servicos_os WHERE orca = '{orcamento.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();
            }
        }

        private void cmb_cliente_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome LIKE '%{cmb_cliente.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_cliente.Items.Add(reader.GetString("nome"));
                txt_doc.Text = reader.GetString("doc");
            }
            conexao.Close();

            cmb_placa.Items.Clear();

            cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{txt_doc.Text}'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_placa.Items.Add(reader.GetString("placa"));
            }

            conexao.Close();
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

                var cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

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

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

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

        private void cmb_placa_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_ano.Text = reader.GetString("ano");
            }
            conexao.Close();
        }
    }
}
