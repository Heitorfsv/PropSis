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
        public edicao_servicos()
        {
            InitializeComponent();
        }

        private void edicao_servicos_Load(object sender, EventArgs e)
        {

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

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            servicos.nome = txt_nome.Text;
            servicos.valor = long.Parse(txt_valor.Text);

            try
            {
                MessageBox.Show("Serviço Alterado!", "JCMotorsport", MessageBoxButtons.OK);
                servicos.alterar_servico();
            }
            catch { }
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM servicos WHERE nome = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
            Close();
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
