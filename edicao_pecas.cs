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

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public edicao_pecas()
        {
            InitializeComponent();
        }

        private void edicao_pecas_Load(object sender, EventArgs e)
        {
            if (this.Text == "Edição peças")
            {
                bnt_deletar.Visible = true;
                bnt_historico.Visible = true;
                bnt_editar.Text = "Salvar";

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
                    txt_valor.Text = reader.GetDecimal("valor_pago").ToString("N2");
                    txt_impostos.Text = reader.GetDecimal("impostos").ToString("N2");
                    txt_preco.Text = reader.GetDecimal("valor_sugerido").ToString("N2");
                    txt_fornecedor.Text = reader.GetString("fornecedor");
                    txt_contato.Text = reader.GetString("contato");
                    txt_local.Text = reader.GetString("local");
                    try { txt_estoque.Text = reader.GetString("estoque"); } catch { }
                }

                reader.Close();

                if (txt_valor.Text != "" && txt_impostos.Text != "")
                {
                    decimal valor_pago = decimal.Parse(txt_valor.Text);
                    decimal imposto = decimal.Parse(txt_impostos.Text);

                    txt_total.Text = (valor_pago + imposto).ToString("N2");
                }
                else txt_total.Text = ""; 
            }
            else if (this.Text == "Cadastro peças")
            {
                bnt_deletar.Visible = false;
                bnt_historico.Visible = false;
                bnt_editar.Text = "Cadastrar";
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            if (this.Text == "Edição peças")
            {
                pecas.nome = txt_nome.Text;
                pecas.marca = txt_marca.Text;
                pecas.modelo = txt_modelo.Text;

                try
                {
                    pecas.valor_pago = decimal.Parse(txt_valor.Text);
                    pecas.impostos = decimal.Parse(txt_impostos.Text);
                    pecas.valor_sugerido = decimal.Parse(txt_preco.Text);
                    pecas.estoque = decimal.TryParse(txt_estoque.Text.Trim(), out _)? txt_estoque.Text.Trim() : "";
                }
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                pecas.fornecedor = txt_fornecedor.Text;
                pecas.contato = txt_contato.Text;
                pecas.local = txt_local.Text;

                if (txt_nome.Text == string.Empty && txt_nome.Text == " ") MessageBox.Show("Preencha o campo Nome", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    MessageBox.Show("Peça Alterada!", "JC Motorsport", MessageBoxButtons.OK);
                    pecas.alterar_pecas();
                }
            }
            else if (this.Text == "Cadastro peças")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE nome = '{txt_nome.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) MessageBox.Show("Peça já cadastrada", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        pecas.estoque = decimal.TryParse(txt_estoque.Text.Trim(), out _) ? txt_estoque.Text.Trim() : "";
                    }
                    catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    pecas.fornecedor = txt_fornecedor.Text;
                    pecas.contato = txt_contato.Text;
                    pecas.local = txt_local.Text;

                    if (txt_nome.Text == string.Empty) MessageBox.Show("Preencha o campo Nome", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else pecas.cadastrar_pecas();
                }
                conexao.Close();
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
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else txt_total.Text = ""; 
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
                catch (Exception) { MessageBox.Show("Preencha os campos Valor e Impostos com caracteres numéricos", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else txt_total.Text = ""; 
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            // Confirmação para evitar exclusões por clique acidental
            if (MessageBox.Show($"Deseja realmente excluir a peça '{static_class.doc_consultar}' do inventário?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ExecutarDeletePeca();
                Close();
            }
        }

        private void ExecutarDeletePeca(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal)
                conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else
                conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Usando parâmetros para evitar erros com nomes de peças que possuem aspas (ex: Pneu 17")
                    cmd.CommandText = "DELETE FROM pecas WHERE nome = @nomePeca";

                    var pNome = cmd.CreateParameter();
                    pNome.ParameterName = "@nomePeca";
                    pNome.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(pNome);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Se falhar no servidor, tenta deletar no banco local para manter a lista atualizada
                if (!usarLocal)
                    ExecutarDeletePeca(true);
            }
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

