using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PrototipoSistema.classes;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class edicao_cliente : Form
    {
        cliente cliente = new cliente();

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public edicao_cliente()
        {
            InitializeComponent();
        }

        private void tela_cliente_Load(object sender, EventArgs e)
        {
            if (this.Text == "Edição Cliente")
            {
                ConfigurarTelaEdicao();
                CarregarDadosCliente();
            }
            else if (this.Text == "Cadastro Cliente")
            {
                ConfigurarTelaCadastro();
            }
        }

        private void ConfigurarTelaEdicao()
        {
            bnt_editar.Text = "Salvar";
            txt_dt_registro.Visible = true;
            label15.Visible = true;
            txt_inscricao.Visible = false;
            lbl_inscricao.Visible = false;
            bnt_delete.Visible = true;
        }

        private void ConfigurarTelaCadastro()
        {
            bnt_editar.Text = "Cadastrar";
            txt_doc.Mask = "000.000.000-00";
            txt_dt_registro.Visible = false;
            label15.Visible = false;
            dtp_nascimento.Enabled = false;
            txt_inscricao.Visible = false;
            lbl_inscricao.Visible = false;
            bnt_delete.Visible = false;
        }

        private void CarregarDadosCliente(bool usarLocal = false)
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
                    // Busca usando parâmetro para segurança e precisão
                    cmd.CommandText = "SELECT * FROM clientes WHERE doc = @doc LIMIT 1";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@doc";
                    p.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(p);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Atribui o index (controle) logo de cara
                            cliente.index = Convert.ToInt32(reader["controle"]);

                            txt_nome.Text = reader["nome"].ToString();
                            txt_fantasia.Text = reader["nome_fantasia"]?.ToString() ?? "";

                            string doc = reader["doc"].ToString();

                            // Lógica de Máscara CPF/CNPJ
                            if (doc.Length == 14)
                            {
                                lbl_cpf.Text = "CPF";
                                txt_doc.Mask = "000.000.000-00";
                                rb_fisica.Checked = true;
                            }
                            else
                            {
                                lbl_cpf.Text = "CNPJ";
                                txt_doc.Mask = "00.000.000/0000-00";
                                txt_inscricao.Visible = true;
                                lbl_inscricao.Visible = true;
                                rb_juridica.Checked = true;
                            }
                            txt_doc.Text = doc;

                            // Data de Nascimento
                            try
                            {
                                if (reader["dt_nascimento"] != DBNull.Value)
                                {
                                    cb_dt_nascimento.Checked = true;
                                    dtp_nascimento.Value = DateTime.Parse(reader["dt_nascimento"].ToString());
                                }
                            }
                            catch { cb_dt_nascimento.Checked = false; }

                            txt_inscricao.Text = reader["inscricao"]?.ToString() ?? "";
                            txt_telefone.Text = reader["telefone"]?.ToString() ?? "";
                            txt_telefone2.Text = reader["telefone2"]?.ToString() ?? "";
                            txt_email.Text = reader["email"]?.ToString() ?? "";
                            txt_cep.Text = reader["cep"]?.ToString() ?? "";
                            txt_rua.Text = reader["rua"]?.ToString() ?? "";
                            txt_bairro.Text = reader["bairro"]?.ToString() ?? "";
                            txt_cidade.Text = reader["cidade"]?.ToString() ?? "";

                            // Data de Registro
                            if (reader["dt_cadastro"] != DBNull.Value)
                                txt_dt_registro.Text = Convert.ToDateTime(reader["dt_cadastro"]).ToString("d");
                        }
                    }
                }
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
                // Se falhar no servidor, tenta buscar no banco local
                if (!usarLocal) CarregarDadosCliente(true);
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            cliente.nome = txt_nome.Text;
            cliente.fantasia = txt_fantasia.Text;   
            cliente.rua = txt_rua.Text;
            cliente.bairro = txt_bairro.Text;
            cliente.cidade = txt_cidade.Text;
            cliente.telefone = txt_telefone.Text;
            cliente.telefone2 = txt_telefone2.Text;
            cliente.email = txt_email.Text;
            cliente.cep = txt_cep.Text;
            cliente.doc = txt_doc.Text;
            try { cliente.inscricao = int.Parse(txt_inscricao.Text); } catch { }

            if (cb_dt_nascimento.Checked == true)
            { cliente.dt_nascimento = dtp_nascimento.Value.ToString("dd/MM/yyyy"); }
            else
            { cliente.dt_nascimento = null; }

            if (this.Text == "Edição Cliente")
            {
                bnt_editar.Text = "Salvar";

                try
                {
                    cliente.alterar_cliente();
                    MessageBox.Show("Cliente Alterado!", "JCMotorsport", MessageBoxButtons.OK);
                }
                catch (Exception erro)
                { MessageBox.Show(erro.ToString()); }
            }
            else if (this.Text == "Cadastro Cliente")
            {
                bnt_editar.Text = "Cadastrar";

                cliente.ultimo_index();
                cliente.index++;
                cliente.dt_cadastro = DateTime.Now;
                cliente.sujo = 0;

                if (txt_nome.Text == string.Empty)
                {
                    MessageBox.Show("Preencha o campo Nome", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        cliente.cadastrar_cliente();
                        MessageBox.Show("Cliente cadastrado!", "JCMotorsport", MessageBoxButtons.OK);
                    }
                    catch (Exception erro)
                    { MessageBox.Show(erro.ToString()); }
                }
            }
        }

        private void rb_fisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_fisica.Checked == true)
            {
                lbl_cpf.Text = "CPF";
                txt_doc.Mask = "000.000.000-00";
                txt_inscricao.Visible = false;  
                lbl_inscricao.Visible = false;
            }
            else
            {
                lbl_cpf.Text = "CNPJ";
                txt_doc.Mask = "00.000.000/0000-00";
                txt_inscricao.Visible = true;
                lbl_inscricao.Visible = true;
            }
        }

        private void txt_cep_TextChanged(object sender, EventArgs e)
        {
            if (txt_cep.MaskCompleted == true)
            {
                try
                {
                    string url = string.Format("https://viacep.com.br/ws/{0}/json/", txt_cep.Text);

                    using (HttpClient client = new HttpClient())
                    {
                        var response = client.GetAsync(url).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            ConsultaCep res = JsonConvert.DeserializeObject<ConsultaCep>(result);

                            txt_rua.Text = res.rua;
                            txt_bairro.Text = res.bairro;
                            txt_cidade.Text = res.cidade + ", " + res.UF;
                        }
                    }
                }
                catch (Exception erro)
                { MessageBox.Show(erro.ToString(), "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void bnt_delete_Click(object sender, EventArgs e)
        {
            // Confirmação de segurança
            DialogResult confirmacao = MessageBox.Show($"Deseja realmente excluir o cliente com documento {static_class.doc_consultar}?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                ExecutarDeleteCliente();
                Close();
            }
        }

        private void ExecutarDeleteCliente(bool usarLocal = false)
        {
            // Define se usará MySQL ou SQLite
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

                    // Comando SQL parametrizado
                    cmd.CommandText = "DELETE FROM clientes WHERE doc = @doc";

                    var pDoc = cmd.CreateParameter();
                    pDoc.ParameterName = "@doc";
                    pDoc.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(pDoc);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Se a rede falhar no MySQL, tenta deletar no banco local
                if (!usarLocal)
                    ExecutarDeleteCliente(true);
                else
                    MessageBox.Show("Erro ao tentar excluir o registro localmente.");
            }
        }

        private void cb_dt_nascimento_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_dt_nascimento.Checked == true)
            { dtp_nascimento.Enabled = true; }
            else
            { dtp_nascimento.Enabled = false; }
        }
    }
}
