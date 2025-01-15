using MySql.Data.MySqlClient;
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
    public partial class cadastro_pecas : Form
    {
        pecas pecas = new pecas();
        public cadastro_pecas()
        {
            InitializeComponent();
        }

        private void bnt_cadastro_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE nome = '{txt_nome.Text}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                MessageBox.Show("Peça já cadastrada", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pecas.ultimo_index();
                pecas.index++;

                pecas.nome = txt_nome.Text;
                pecas.marca = txt_marca.Text;
                pecas.modelo = txt_modelo.Text;

                try
                {
                    pecas.valor_pago = decimal.Parse(txt_valor.Text);
                    pecas.impostos = decimal.Parse(txt_impostos.Text);
                    pecas.valor_sugerido = decimal.Parse(txt_preco.Text);
                }
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                pecas.fornecedor = txt_fornecedor.Text;
                pecas.contato = txt_contato.Text;
                pecas.estoque = txt_estoque.Text.Trim();

                pecas.local = txt_local.Text;

                if (txt_nome.Text == string.Empty)
                {
                    MessageBox.Show("Preencha o campo Nome", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    pecas.cadastrar_pecas();
                }
            }
            conexao.Close();
        }

        private void txt_valor_TextChanged(object sender, EventArgs e)
        {
            if (txt_valor.Text != "" && txt_impostos.Text != "")
            {
                try
                {
                    decimal valor_pago = decimal.Parse(txt_valor.Text);
                    decimal imposto = decimal.Parse(txt_impostos.Text);

                    txt_total.Text = (valor_pago + imposto).ToString();
                }
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else 
            { txt_total.Text = ""; }
        }

        private void txt_impostos_TextChanged(object sender, EventArgs e)
        {
            if (txt_valor.Text != "" && txt_impostos.Text != "")
            {
                try
                {
                    decimal valor_pago = decimal.Parse(txt_valor.Text);
                    decimal imposto = decimal.Parse(txt_impostos.Text);

                    txt_total.Text = (valor_pago + imposto).ToString();
                }
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            { txt_total.Text = ""; }
        }

        private void cadastro_pecas_Load(object sender, EventArgs e)
        {

        }
    }
}
