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
            if (this.Text == "Edição peças")
            {
                bnt_deletar.Visible = true;
                bnt_historico.Visible = true;
                bnt_editar.Text = "Salvar";

                CarregarDadosPeca();
            }
            else if (this.Text == "Cadastro peças")
            {
                bnt_deletar.Visible = false;
                bnt_historico.Visible = false;
                bnt_editar.Text = "Cadastrar";
            }
        }

        private void CarregarDadosPeca(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(static_class.strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(static_class.strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Uso de parâmetros para evitar erro com peças que tenham aspas (ex: Parafuso 1/2")
                    cmd.CommandText = "SELECT * FROM pecas WHERE nome = @nome LIMIT 1";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@nome";
                    p.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(p);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_nome.Text = reader["nome"].ToString();
                            txt_marca.Text = reader["marca"].ToString();
                            txt_modelo.Text = reader["modelo"].ToString();

                            // Tratamento seguro para valores decimais
                            txt_valor.Text = Convert.ToDecimal(reader["valor_pago"]).ToString("N2");
                            txt_impostos.Text = Convert.ToDecimal(reader["impostos"]).ToString("N2");
                            txt_preco.Text = Convert.ToDecimal(reader["valor_sugerido"]).ToString("N2");

                            txt_fornecedor.Text = reader["fornecedor"].ToString();
                            txt_contato.Text = reader["contato"].ToString();
                            txt_local.Text = reader["local"].ToString();
                            txt_estoque.Text = reader["estoque"]?.ToString() ?? "0";
                        }
                    }
                }

                // Calcula o total após carregar os dados
                CalcularTotalPeca();
            }
            catch
            {
                // Se falhar no servidor, tenta buscar no banco local
                if (!usarLocal) CarregarDadosPeca(true);
            }
        }

        private void CalcularTotalPeca()
        {
            if (decimal.TryParse(txt_valor.Text, out decimal valor_pago) &&
                decimal.TryParse(txt_impostos.Text, out decimal imposto))
            {
                txt_total.Text = (valor_pago + imposto).ToString("N2");
            }
            else
            {
                txt_total.Text = "";
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            // Validação de Nome (evita nomes vazios ou só com espaços)
            if (string.IsNullOrWhiteSpace(txt_nome.Text))
            {
                MessageBox.Show("Preencha o campo Nome", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Atribuição básica
            pecas.nome = txt_nome.Text;
            pecas.marca = txt_marca.Text;
            pecas.modelo = txt_modelo.Text;
            pecas.fornecedor = txt_fornecedor.Text;
            pecas.contato = txt_contato.Text;
            pecas.local = txt_local.Text;

            // Validação Numérica
            try
            {
                pecas.valor_pago = decimal.Parse(txt_valor.Text);
                pecas.impostos = decimal.Parse(txt_impostos.Text);
                pecas.valor_sugerido = decimal.Parse(txt_preco.Text);
                pecas.estoque = decimal.TryParse(txt_estoque.Text.Trim(), out _) ? txt_estoque.Text.Trim() : "0";
            }
            catch (Exception)
            {
                MessageBox.Show("Preencha os campos Valor, Impostos e Preço com caracteres numéricos", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.Text == "Edição peças")
            {
                pecas.alterar_pecas();
            }
            else if (this.Text == "Cadastro peças")
            {
                if (VerificarPecaExistente(txt_nome.Text))
                {
                    MessageBox.Show("Peça já cadastrada no sistema", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    pecas.ultimo_index();
                    pecas.index++;
                    pecas.cadastrar_pecas();
                }
            }
        }

        // Método Híbrido para verificar se a peça já existe
        private bool VerificarPecaExistente(string nomePeca, bool usarLocal = false)
        {
            bool existe = false;
            System.Data.Common.DbConnection conexao;

            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(static_class.strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(static_class.strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM pecas WHERE nome = @nome";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@nome";
                    p.Value = nomePeca;
                    cmd.Parameters.Add(p);

                    existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch
            {
                if (!usarLocal) return VerificarPecaExistente(nomePeca, true);
            }
            return existe;
        }

        private void txt_impostos_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalPeca();
        }

        private void txt_valor_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalPeca();
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            // Confirmação para evitar exclusões por clique acidental
            if (MessageBox.Show($"Deseja realmente excluir a peça '{static_class.doc_consultar}' do inventário?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ExecutarDelete();
                Close();
            }
        }

        private void ExecutarDelete()
        {
            int idControle = static_class.controle;

            // 1. SOFT DELETE no Local (SQLite)
            // Em vez de apagar, marcamos sync = 2. Assim ele some da sua tela (usando o filtro no SELECT)
            // e o sincronizador saberá que precisa apagar isso no servidor depois.
            static_class.AtualizarStatusSync("pecas", idControle, 2);

            // 2. Tenta o DELETE Real no MySQL (Servidor)
            using (var conRemoto = new MySql.Data.MySqlClient.MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();
                    cmdRemoto.CommandText = "DELETE FROM pecas WHERE controle = @controle";
                    cmdRemoto.Parameters.AddWithValue("@controle", idControle);

                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, podemos apagar FISICAMENTE do SQLite
                    static_class.ApagarRegistroLocal("pecas", idControle);

                    MessageBox.Show("Orçamento excluído com sucesso!");
                }
                catch (Exception)
                {
                    // Se o servidor estiver fora, não tem problema. 
                    // O registro continua no SQLite com sync=2, invisível para o usuário,
                    // aguardando a internet voltar para ser deletado no MySQL.
                    MessageBox.Show("Excluído localmente. O servidor será atualizado assim que houver conexão.", "Modo Offline");
                }
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

