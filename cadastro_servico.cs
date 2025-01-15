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
    public partial class cadastro_servicos : Form
    {
        servicos servicos = new servicos();
        public cadastro_servicos()
        {
            InitializeComponent();
        }

        private void bnt_cadastro_Click(object sender, EventArgs e)
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
                }
                catch (Exception) { MessageBox.Show("Preencha o campo Valor com caracteres numéricos", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

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
            conexao.Close();
        }
    }
}
