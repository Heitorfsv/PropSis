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
    public partial class consulta_pecas : Form
    {
        public consulta_pecas()
        {
            InitializeComponent();
        }

        private void consulta_pecas_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Adiciona colunas ao ListView
            listView1.Columns.Add("Nome", 150);
            listView1.Columns.Add("Marca", 100);
            listView1.Columns.Add("Modelo", 100);
            listView1.Columns.Add("Fornecedor", 150);

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM pecas", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader.GetString("nome")); // Nome
                item.SubItems.Add(reader.GetString("marca"));     // Marca
                item.SubItems.Add(reader.GetString("modelo"));    // Modelo
                item.SubItems.Add(reader.GetString("fornecedor")); // Fornecedor
                listView1.Items.Add(item);
            }

            conexao.Close();

            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader.GetString("nome"));
                item.SubItems.Add(reader.GetString("marca"));
                item.SubItems.Add(reader.GetString("modelo"));
                item.SubItems.Add(reader.GetString("fornecedor"));
                listView1.Items.Add(item);
            }

            conexao.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                edicao_pecas tela_peca = new edicao_pecas();
                tela_peca.Text = "Edição peças";

                static_class.doc_consultar = listView1.SelectedItems[0].Text; // Nome da peça
                tela_peca.Show();
            }
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_pecas_Load(sender, e);
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_pecas cadastro = new edicao_pecas();
            cadastro.Text = "Cadastro peças";
            cadastro.Show();
        }

    }
}
