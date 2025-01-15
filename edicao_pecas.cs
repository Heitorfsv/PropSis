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
    public partial class edicao_pecas : Form
    {
        pecas pecas = new pecas();
        public edicao_pecas()
        {
            InitializeComponent();
        }

        private void edicao_pecas_Load(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE nome LIKE '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_nome.Text = reader.GetString("nome");
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_valor.Text = reader.GetDecimal("valor_pago").ToString();
                txt_impostos.Text = reader.GetDecimal("impostos").ToString();
                txt_preco.Text = reader.GetDecimal("valor_sugerido").ToString();
                txt_fornecedor.Text = reader.GetString("fornecedor");
                txt_contato.Text = reader.GetString("contato");
                txt_local.Text = reader.GetString("local");
                txt_estoque.Text = reader.GetString("estoque");
            }

            reader.Close();

            if (txt_valor.Text != "" && txt_impostos.Text != "")
            {
                long valor_pago = long.Parse(txt_valor.Text);
                long imposto = long.Parse(txt_impostos.Text);

                txt_total.Text = (valor_pago + imposto).ToString();
            }
            else
            { txt_total.Text = ""; }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
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

            if (txt_nome.Text == string.Empty && txt_nome.Text == " ")
            {
                 MessageBox.Show("Preencha o campo Nome", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Peça Alterada!", "JCMotorsport", MessageBoxButtons.OK);
                pecas.alterar_pecas();
            }
        }

        private void txt_impostos_TextChanged(object sender, EventArgs e)
        {
            if (txt_valor.Text != "" && txt_impostos.Text != "")
            {
                try
                {
                    decimal valor_pago = decimal.Parse(txt_valor.Text);
                    decimal imposto = decimal.Parse(txt_impostos.Text);

                    txt_total.Text = (valor_pago + imposto).ToString("N2");
                }
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            { txt_total.Text = ""; }
        }

        private void txt_valor_TextChanged(object sender, EventArgs e)
        {
            if (txt_valor.Text != "" && txt_impostos.Text != "")
            {
                try
                {
                    decimal valor_pago = decimal.Parse(txt_valor.Text);
                    decimal imposto = decimal.Parse(txt_impostos.Text);

                    txt_total.Text = (valor_pago + imposto).ToString("N2");
                }
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            { txt_total.Text = ""; }    
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM pecas WHERE nome = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
            Close();
        }

        private void bnt_historico_Click_1(object sender, EventArgs e)
        {
            try
            {
                historico historico = new historico();
                static_class.historico = "pecas_os";
                historico.Show();
            }
            catch { }
        }
    }
}

