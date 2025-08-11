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
            listView1.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM servicos WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader.GetString("nome")); // 1ª coluna
                item.SubItems.Add(reader.GetString("valor")); // 2ª coluna
                listView1.Items.Add(item);
            }

            conexao.Close();
        }

        private void consulta_servicos_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Colunas do ListView
            listView1.Columns.Add("Nome", 350);
            listView1.Columns.Add("Valor", 100);

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM servicos", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader.GetString("nome")); // 1ª coluna
                item.SubItems.Add(reader.GetString("valor")); // 2ª coluna
                listView1.Items.Add(item);
            }

            conexao.Close();

            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_servicos_Load(sender, e);
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_servicos cadastro = new edicao_servicos();
            cadastro.Text = "Cadastro serviços";
            cadastro.Show();
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                edicao_servicos tela_servico = new edicao_servicos();
                tela_servico.Text = "Edição serviços";

                static_class.doc_consultar = listView1.SelectedItems[0].Text; // Nome (1ª coluna)
                tela_servico.Show();
            }
        }
    }
}
