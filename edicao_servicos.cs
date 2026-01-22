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

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos WHERE nome LIKE '{static_class.doc_consultar}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txt_nome.Text = reader.GetString("nome");
                    txt_valor.Text = reader.GetDecimal("valor").ToString("N2");
                }

                conexao.Close();
            }
            else if (this.Text == "Cadastro serviços")
            {
                bnt_deletar.Visible = false;
                bnt_historico.Visible = false;
                bnt_editar.Text = "Cadastrar";
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            if (this.Text == "Edição serviços")
            {
                servicos.nome = txt_nome.Text;
                servicos.valor = decimal.Parse(txt_valor.Text);

                try
                {
                    MessageBox.Show("Serviço Alterado!", "JCMotorsport", MessageBoxButtons.OK);
                    servicos.alterar_servico();
                }
                catch { }
            }
            else if (this.Text == "Cadastro serviços")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos WHERE nome = '{txt_nome.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("Serviço já cadastrada", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    servicos.ultimo_index();
                    servicos.index++;

                    servicos.nome = txt_nome.Text;
                    try
                    {
                        servicos.valor = decimal.Parse(txt_valor.Text);

                        if (txt_nome.Text == string.Empty)
                        {
                            MessageBox.Show("Preencha o campo Nome", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            servicos.cadastrar_servicos();
                            MessageBox.Show("Serviço cadastrado!", "JCMotorsport", MessageBoxButtons.OK);
                        }
                    }
                    catch (Exception) { MessageBox.Show("Preencha o campo Valor com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                conexao.Close();
            }
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
