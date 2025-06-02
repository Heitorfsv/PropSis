using classes;
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
    public partial class cadastro_or : Form
    {
        orcamento orcamento = new orcamento();
        public cadastro_or()
        {
            InitializeComponent();
        }

        private void cadastro_or_Load(object sender, EventArgs e)
        {
            dtp_cadastro.Value = DateTime.Now;

            orcamento.ultimo_index();
            orcamento.index++;
            static_class.controle_os = orcamento.index;
        }

        private void bnt_cadastro_Click(object sender, EventArgs e)
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

        private void bnt_editar_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add_servicos add_servicos = new add_servicos();
            add_servicos.modo = "or";
            add_servicos.Show();
        }

        private void bnt_editar_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();

            add_pecas add_pecas = new add_pecas();
            add_pecas.modo = "or";
            add_pecas.Show();
        }

        private void cadastro_or_FormClosing(object sender, FormClosingEventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT controle FROM orcamentos WHERE controle = '{orcamento.index}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            { }
            else
            {
                cmd = new MySqlCommand($"DELETE FROM pecas_os WHERE or = '{orcamento.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();

                cmd = new MySqlCommand($"DELETE FROM servicos_os WHERE or = '{orcamento.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();
            }
            conexao.Close();
        }
    }
}
