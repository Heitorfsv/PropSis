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
        public edicao_cliente()
        {
            InitializeComponent();
        }

        public void tela_cliente_Load(object sender, EventArgs e)
        {
            txt_inscricao.Visible = false;
            lbl_inscricao.Visible = false;

            consulta_cliente consulta = new consulta_cliente();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc LIKE '%{static_class.doc_consultar}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_nome.Text = reader.GetString("nome");

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

                if (reader.GetString("dt_nascimento") == "NULL")
                { cb_dt_nascimento.Checked = false; }
                else
                {
                    cb_dt_nascimento.Checked = true;
                    dtp_nascimento.Value = DateTime.Parse(reader.GetString("dt_nascimento")); 
                }

                txt_inscricao.Text = reader.GetInt32("inscricao").ToString();
                txt_telefone.Text = reader.GetString("telefone");
                txt_telefone2.Text = reader.GetString("telefone2");
                txt_email.Text = reader.GetString("email");
                txt_cep.Text = reader.GetString("cep");
                txt_rua.Text = reader.GetString("rua");
                txt_bairro.Text = reader.GetString("bairro");
                txt_cidade.Text = reader.GetString("cidade");
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

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            cliente.nome = txt_nome.Text;
            cliente.rua = txt_rua.Text;
            cliente.bairro = txt_bairro.Text; 
            cliente.cidade = txt_cidade.Text;
            cliente.doc = txt_doc.Text;
            cliente.inscricao = int.Parse(txt_inscricao.Text);
            cliente.email = txt_email.Text;
            cliente.telefone = txt_telefone.Text;
            cliente.telefone2 = txt_telefone2.Text;
            cliente.cep = txt_cep.Text;

            if (cb_dt_nascimento.Checked == true)
            { cliente.dt_nascimento = dtp_nascimento.Value.ToString("dd/MM/yyyy"); }
            else
            { cliente.dt_nascimento = "NULL"; }

            try
            {
                cliente.alterar_cliente();
                MessageBox.Show("Cliente Alterado!", "JCMotorsport", MessageBoxButtons.OK);
            }
            catch (Exception erro)
            { MessageBox.Show(erro.ToString()); }
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

        private void txt_cep_Leave(object sender, EventArgs e)
        {

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
            MessageBox.Show(static_class.doc_consultar.ToString());
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM clientes WHERE doc = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
            Close();
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
