using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class cadastro_moto : Form
    {
        motos motos = new motos();
        public cadastro_moto()
        {
            InitializeComponent();
        }

        private void bnt_cadastrar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome = '{cmb_dono.Text}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                motos.doc_dono = reader.GetString("doc");
            }
            conexao.Close();

            if (motos.doc_dono != null)
            {
                motos.ultimo_index();
                motos.index++;

                motos.placa = txt_placa.Text;
                motos.marca = txt_marca.Text;
                motos.cor = txt_cor.Text;
                motos.ano = txt_ano.Text;
                motos.modelo = txt_modelo.Text;
                motos.chassi = txt_chassi.Text;
                motos.dt_registro = DateTime.Now;
                motos.observacao = txt_observacao.Text;

                try
                {
                    motos.cadastrar_moto();
                    MessageBox.Show("Moto cadastrada!", "JCMotorsport", MessageBoxButtons.OK);
                }
                catch { }
            }
            else
            {
                MessageBox.Show("O cliente não é válido", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmb_dono_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome LIKE '%{cmb_dono.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_dono.Items.Add(reader.GetString("nome"));
            }

            conexao.Close();
        }

        private void cadastro_moto_Load(object sender, EventArgs e)
        {

        }
    }
}
