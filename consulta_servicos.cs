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
    public partial class consulta_servicos : Form
    {
        public consulta_servicos()
        {
            InitializeComponent();
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            lst_nome.Items.Clear();
            lst_valor.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM servicos WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

             while (reader.Read())
            {
                lst_nome.Items.Add(reader.GetString("nome"));
                lst_valor.Items.Add(reader.GetDecimal("valor"));    

            }

            conexao.Close();
        }

        private void consulta_servicos_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            lst_nome.Items.Clear();
            lst_valor.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM servicos", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_nome.Items.Add(reader.GetString("nome"));
                decimal valor = reader.GetDecimal("valor");
                lst_valor.Items.Add(valor.ToString());
            }

            conexao.Close();

            lst_nome.SelectedIndex = 0;
        }

        private void lst_nome_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_valor.SelectedIndex = lst_nome.SelectedIndex;
        }

        private void lst_nome_DoubleClick(object sender, EventArgs e)
        {
            edicao_servicos tela_servico = new edicao_servicos();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_servico.Show();
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_servicos_Load(sender, e);
        }

        private void lst_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome.SelectedIndex = lst_valor.SelectedIndex;
        }

        private void lst_status_DoubleClick(object sender, EventArgs e)
        {
            edicao_servicos tela_servico = new edicao_servicos();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_servico.Show();
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            cadastro_servicos cadastro = new cadastro_servicos();
            cadastro.Show();
        }
    }
}
