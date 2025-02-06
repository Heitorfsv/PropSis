using Newtonsoft.Json;
using PrototipoSistema.classes;
using System;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class cadastro_cliente : Form
    {
        cliente cliente = new cliente();
        public cadastro_cliente()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_doc.Mask = "000.000.000-00";
            dtp_nascimento.Enabled = false;
            txt_inscricao.Visible = false;
            lbl_inscricao.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cliente.ultimo_index();
            cliente.index++;

            cliente.nome = txt_nome.Text;
            cliente.rua = txt_rua.Text;
            cliente.bairro = txt_bairro.Text;
            cliente.cidade = txt_cidade.Text;
            cliente.dt_cadastro = DateTime.Now;
            cliente.doc = txt_doc.Text;
            cliente.email = txt_email.Text; 
            cliente.telefone = txt_telefone.Text;
            cliente.telefone2 = txt_telefone2.Text;
            cliente.cep = txt_cep.Text;
            cliente.sujo = 0;

            if (cb_dt_nascimento.Checked == true)
            { cliente.dt_nascimento = dtp_nascimento.Value.ToString("dd/MM/yyyy"); }
            else
            { cliente.dt_nascimento = null; }

            if (txt_inscricao.Text != "")
            { cliente.inscricao = int.Parse(txt_inscricao.Text); }


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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_dt_nascimento.Checked == true)
            { dtp_nascimento.Enabled = true; }
            else
            { dtp_nascimento.Enabled = false; }
        }
    }
}