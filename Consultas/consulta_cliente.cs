using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class consulta_cliente : Form
    {

        public consulta_cliente()
        {
            InitializeComponent();
        }

        private void consulta_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Adiciona colunas
            listView1.Columns.Add("Cadastro", 90);
            listView1.Columns.Add("Nome", 350);
            listView1.Columns.Add("Doc", 110);
            listView1.Columns.Add("Telefone", 120);
            listView1.Columns.Add("Endereço", 320);
            listView1.Columns.Add("Nascimento", 90);

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM clientes", conexao);
            conexao.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string cadastro = reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy");
                string nome = reader.GetString("nome");
                string doc = reader.GetString("doc");
                string tel = reader.GetString("telefone");
                string endereco = $"{reader.GetString("rua")}, {reader.GetString("bairro")}, {reader.GetString("cidade")}";
                string nascimento = reader.IsDBNull(reader.GetOrdinal("dt_nascimento")) ? "" : reader.GetString("dt_nascimento");

                bool sujo = reader.GetInt32("sujo") == 1;
                

                var item = new ListViewItem(cadastro);
                item.SubItems.Add(nome);
                item.SubItems.Add(doc);
                item.SubItems.Add(tel);
                item.SubItems.Add(endereco);
                item.SubItems.Add(nascimento);

                if (sujo) item.ForeColor = Color.Red; item.Tag = doc; // armazena o documento para edição

                listView1.Items.Add(item);
            }
            conexao.Close();
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao); 

            string pesquisa = txt_pesquisa.Text.Replace(" ", "%");
            
            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE {cmb_consulta.Text} LIKE '%{pesquisa}%'", conexao);
            conexao.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string cadastro = reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy");
                string nome = reader.GetString("nome");
                string doc = reader.GetString("doc");
                string tel = reader.GetString("telefone");
                string endereco = $"{reader.GetString("rua")}, {reader.GetString("bairro")}, {reader.GetString("cidade")}";
                string nascimento = reader.IsDBNull(reader.GetOrdinal("dt_nascimento"))  ? "" : reader.GetString("dt_nascimento");

                bool sujo = reader.GetInt32("sujo") == 1;

                var item = new ListViewItem(cadastro);
                item.SubItems.Add(nome);
                item.SubItems.Add(doc);
                item.SubItems.Add(tel);
                item.SubItems.Add(endereco);
                item.SubItems.Add(nascimento);

                if (sujo) item.ForeColor = Color.Red; item.Tag = doc;

                listView1.Items.Add(item);
            }
            conexao.Close();                                                                            
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_Load(sender, e);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            string docSelecionado = listView1.SelectedItems[0].SubItems[2].Text;

            var tela_cliente = new edicao_cliente();
            tela_cliente.Text = "Edição Cliente";
            static_class.doc_consultar = docSelecionado;
            tela_cliente.Show();
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            var cadastro = new edicao_cliente();
            cadastro.Text = "Cadastro Cliente";
            cadastro.Show();
        }
    }
}
