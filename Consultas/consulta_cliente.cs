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

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public consulta_cliente()
        {
            InitializeComponent();
        }

        private void consulta_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            // Configura colunas apenas uma vez
            listView1.View = View.Details;
            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Columns.Add("Cadastro", 90);
            listView1.Columns.Add("Nome", 350);
            listView1.Columns.Add("Doc", 110);
            listView1.Columns.Add("Telefone", 120);
            listView1.Columns.Add("Endereço", 320);
            listView1.Columns.Add("Nascimento", 90);

            // Carrega todos os clientes ao abrir
            CarregarDadosClientes("SELECT * FROM clientes");
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            string pesquisa = txt_pesquisa.Text.Replace(" ", "%");
            // Monta a query baseada na escolha do ComboBox
            string query = $"SELECT * FROM clientes WHERE {cmb_consulta.Text} LIKE '%{pesquisa}%'";

            CarregarDadosClientes(query);
        }

        // FUNÇÃO HÍBRIDA ÚNICA PARA POPULAR A LISTA
        private void CarregarDadosClientes(string sql)
        {
            listView1.Items.Clear();
            System.Data.IDbConnection conexao;

            try
            {
                var mysql = new MySqlConnection(strConexao);
                mysql.Open();
                conexao = mysql;
            }
            catch
            {
                var sqlite = new System.Data.SQLite.SQLiteConnection(strLocal);
                sqlite.Open();
                conexao = sqlite;
            }

            using (conexao)
            {
                var cmd = conexao.CreateCommand();
                cmd.CommandText = sql;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Tratamento de data de cadastro
                        string cadastro = "";
                        try
                        {
                            cadastro = Convert.ToDateTime(reader["dt_cadastro"]).ToString("dd/MM/yyyy");
                        }
                        catch { cadastro = reader["dt_cadastro"].ToString(); }

                        string nome = reader["nome"].ToString();
                        string doc = reader["doc"].ToString();
                        string tel = reader["telefone"].ToString();
                        string endereco = $"{reader["rua"]}, {reader["bairro"]}, {reader["cidade"]}";

                        // Tratamento de Nulo no Nascimento
                        string nascimento = reader["dt_nascimento"] == DBNull.Value ? "" : reader["dt_nascimento"].ToString();

                        // Verifica se o cliente está com débito (sujo)
                        bool sujo = reader["sujo"].ToString() == "1";

                        var item = new ListViewItem(cadastro);
                        item.SubItems.Add(nome);
                        item.SubItems.Add(doc);
                        item.SubItems.Add(tel);
                        item.SubItems.Add(endereco);
                        item.SubItems.Add(nascimento);

                        // Se estiver sujo, fica vermelho
                        if (sujo) item.ForeColor = Color.Red;

                        // IMPORTANTE: Armazena o DOC na Tag para você poder abrir a edição depois
                        item.Tag = doc;

                        listView1.Items.Add(item);
                    }
                }
            }
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
