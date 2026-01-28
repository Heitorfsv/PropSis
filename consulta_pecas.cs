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
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public consulta_pecas()
        {
            InitializeComponent();
        }

        private void consulta_pecas_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Nome", 260);
            listView1.Columns.Add("Marca", 100);
            listView1.Columns.Add("Modelo", 100);
            listView1.Columns.Add("Fornecedor", 150);

            CarregarPecas(); // Carrega tudo ao iniciar
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            CarregarPecas(txt_pesquisa.Text);
        }

        private void CarregarPecas(string filtro = "")
        {
            listView1.Items.Clear();
            System.Data.IDbConnection conexao;

            // 1. TENTATIVA HÍBRIDA (MySQL -> SQLite)
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

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();

                    // 2. DISTINCT para evitar duplicados na lista
                    string sql = "SELECT DISTINCT nome, marca, modelo, fornecedor FROM pecas";

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        sql += $" WHERE {cmb_consulta.Text} LIKE @pesquisa";

                        var pPesquisa = cmd.CreateParameter();
                        pPesquisa.ParameterName = "@pesquisa";
                        pPesquisa.Value = "%" + filtro.Replace(" ", "%") + "%";
                        cmd.Parameters.Add(pPesquisa);
                    }

                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        // HashSet como segunda trava de segurança contra duplicidade no C#
                        HashSet<string> chavesUnicas = new HashSet<string>();

                        while (reader.Read())
                        {
                            // Criamos uma chave combinando nome e marca para verificar se já existe na lista
                            string chave = $"{reader["nome"]}-{reader["marca"]}-{reader["modelo"]}";

                            if (!chavesUnicas.Contains(chave))
                            {
                                var item = new ListViewItem(reader["nome"].ToString());
                                item.SubItems.Add(reader["marca"].ToString());
                                item.SubItems.Add(reader["modelo"].ToString());
                                item.SubItems.Add(reader["fornecedor"].ToString());

                                listView1.Items.Add(item);
                                chavesUnicas.Add(chave);
                            }
                        }
                    }
                }

                if (listView1.Items.Count > 0)
                    listView1.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar peças: " + ex.Message, "JCMotorsport");
            }
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
