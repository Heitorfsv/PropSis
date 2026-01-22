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

        public void tela_cliente_Load(object sender, EventArgs e)
        {
            if (this.Text == "Edição Cliente")
            {
                bnt_editar.Text = "Salvar";
                txt_dt_registro.Visible = true;
                label15.Visible = true;

                txt_inscricao.Visible = false;
                lbl_inscricao.Visible = false;
                bnt_delete.Visible = true;

                consulta_cliente consulta = new consulta_cliente();

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc LIKE '%{static_class.doc_consultar}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txt_nome.Text = reader.GetString("nome");

                    try { txt_fantasia.Text = reader.GetString("nome_fantasia"); } catch { }

                    string doc = reader.GetString("doc");

                    if (doc.Length == 14)
                    {
                        lbl_cpf.Text = "CPF";
                        txt_doc.Mask = "000.000.000-00";
                        txt_inscricao.Visible = false;
                        lbl_inscricao.Visible = false;
                        rb_fisica.Checked = true;
                        txt_doc.Text = doc;
                    }
                    else
                    {
                        lbl_cpf.Text = "CNPJ";
                        txt_doc.Mask = "00.000.000/0000-00";
                        txt_inscricao.Visible = true;
                        lbl_inscricao.Visible = true;
                        rb_juridica.Checked = true;
                        txt_doc.Text = doc;
                    }

                    try
                    {
                        if (reader.GetString("dt_nascimento") == null)
                        { }
                        else
                        {
                            cb_dt_nascimento.Checked = true;
                            dtp_nascimento.Value = DateTime.Parse(reader.GetString("dt_nascimento"));
                        }
                    }
                    catch
                    {
                        cb_dt_nascimento.Checked = false;
                        dtp_nascimento.Enabled = false;
                    }

                    txt_inscricao.Text = reader.GetInt32("inscricao").ToString();
                    txt_telefone.Text = reader.GetString("telefone");
                    txt_telefone2.Text = reader.GetString("telefone2");
                    txt_email.Text = reader.GetString("email");
                    txt_cep.Text = reader.GetString("cep");
                    txt_rua.Text = reader.GetString("rua");
                    txt_bairro.Text = reader.GetString("bairro");
                    txt_cidade.Text = reader.GetString("cidade");
                    txt_dt_registro.Text = reader.GetDateTime("dt_cadastro").ToString("d");
                }

                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{static_class.doc_consultar}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cliente.index = reader.GetInt32("controle");
                }
                conexao.Close();
            }
            else if (this.Text == "Cadastro Cliente")
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
