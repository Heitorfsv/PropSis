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
    public partial class edicao_servicos : Form
    {
        servicos servicos = new servicos();

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public edicao_servicos()
        {
            InitializeComponent();
        }

        private void edicao_servicos_Load(object sender, EventArgs e)
        {
            if (this.Text == "Edição serviços")
            {
                bnt_deletar.Visible = true;
                bnt_historico.Visible = true;
                bnt_editar.Text = "Salvar";

                CarregarDadosServico();
            }
            else if (this.Text == "Cadastro serviços")
            {
                bnt_deletar.Visible = false;
                bnt_historico.Visible = false;
                bnt_editar.Text = "Cadastrar";
            }
        }

        private void CarregarDadosServico(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Uso de parâmetros para evitar erros com serviços como "Mão d'obra"
                    cmd.CommandText = "SELECT * FROM servicos WHERE nome = @nome LIMIT 1";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@nome";
                    p.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(p);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_nome.Text = reader["nome"].ToString();

                            // Convert.ToDecimal é mais seguro para alternar entre MySQL e SQLite
                            if (reader["valor"] != DBNull.Value)
                            {
                                txt_valor.Text = Convert.ToDecimal(reader["valor"]).ToString("N2");
                            }
                        }
                    }
                }
            }
            catch
            {
                // Se falhar no MySQL (servidor), tenta buscar a definição do serviço no SQLite (local)
                if (!usarLocal) CarregarDadosServico(true);
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
                // 1. Validação de Entrada
            if (string.IsNullOrWhiteSpace(txt_nome.Text))
            {
                MessageBox.Show("Preencha o campo Nome", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txt_valor.Text, out decimal valorServico))
            {
                MessageBox.Show("Preencha o campo Valor com caracteres numéricos (ex: 150,00)", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Atribuição ao objeto
            servicos.nome = txt_nome.Text;
            servicos.valor = valorServico;

            if (this.Text == "Edição serviços")
            {
                servicos.alterar_servico();
            }
            else if (this.Text == "Cadastro serviços")
            {
                // 3. Verificação de existência Híbrida
                if (VerificarServicoExistente(txt_nome.Text))
                {
                    MessageBox.Show("Serviço já cadastrado no sistema", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    servicos.ultimo_index();
                    servicos.index++;
                    servicos.cadastrar_servicos();
                }
            }
        }

        // Método para checar duplicidade em ambos os bancos
        private bool VerificarServicoExistente(string nomeServico, bool usarLocal = false)
        {
            bool existe = false;
            System.Data.Common.DbConnection conexao;

            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM servicos WHERE nome = @nome";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@nome";
                    p.Value = nomeServico;
                    cmd.Parameters.Add(p);

                    existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch
            {
                // Se falhar no MySQL, tenta no SQLite local
                if (!usarLocal) return VerificarServicoExistente(nomeServico, true);
            }
            return existe;
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            // Confirmação para segurança do usuário
            if (MessageBox.Show($"Deseja excluir permanentemente o serviço '{static_class.doc_consultar}'?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ExecutarDeleteServico();
                Close();
            }
        }

        private void ExecutarDeleteServico(bool usarLocal = false)
        {
            // Seleciona o driver de conexão (MySQL ou SQLite)
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

                    // Comando parametrizado para evitar erros com aspas simples no nome
                    cmd.CommandText = "DELETE FROM servicos WHERE nome = @nomeServico";

                    var pNome = cmd.CreateParameter();
                    pNome.ParameterName = "@nomeServico";
                    pNome.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(pNome);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Se falhar no servidor MySQL, tenta deletar no banco local para manter a consistência
                if (!usarLocal)
                    ExecutarDeleteServico(true);
            }
        }

        private void bnt_historico_Click(object sender, EventArgs e)
        {
            try
            {
                historico historico = new historico();
                static_class.historico = "servicos_os";
                historico.Show();
            }
            catch { }
        }
    }
}
