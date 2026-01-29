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
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public consulta_servicos()
        {
            InitializeComponent();
        }

        private void consulta_servicos_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Columns.Add("Nome", 350);
            listView1.Columns.Add("Valor", 100);

            CarregarServicos();
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            CarregarServicos(txt_pesquisa.Text);
        }

        private void CarregarServicos(string filtro = "")
        {
            listView1.Items.Clear();
            System.Data.IDbConnection conexao;

            // 1. TENTATIVA HÍBRIDA AUTOMÁTICA
            try
            {
                var mysql = new MySqlConnection(strConexao);
                mysql.Open();
                conexao = mysql;
            }
            catch
            {
                // Fallback para SQLite local caso o servidor esteja offline
                var sqlite = new System.Data.SQLite.SQLiteConnection(strLocal);
                sqlite.Open();
                conexao = sqlite;
            }

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();

                    // 2. DISTINCT para evitar serviços duplicados se houver registros repetidos
                    string sql = "SELECT DISTINCT nome, valor FROM servicos";

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
                        // HashSet para garantir unicidade visual no ListView
                        HashSet<string> servicosAdicionados = new HashSet<string>();

                        while (reader.Read())
                        {
                            string nome = reader["nome"] != DBNull.Value ? reader["nome"].ToString() : "";

                            // Só adiciona se o serviço ainda não estiver na lista (case-insensitive)
                            if (!servicosAdicionados.Contains(nome.ToLower().Trim()))
                            {
                                string valorFormatado = "0,00";
                                if (reader["valor"] != DBNull.Value)
                                {
                                    decimal valorDec = Convert.ToDecimal(reader["valor"]);
                                    valorFormatado = valorDec.ToString("N2");
                                }

                                ListViewItem item = new ListViewItem(nome);
                                item.SubItems.Add(valorFormatado);
                                listView1.Items.Add(item);

                                servicosAdicionados.Add(nome.ToLower().Trim());
                            }
                        }
                    }
                }

                if (listView1.Items.Count > 0)
                    listView1.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar serviços: " + ex.Message, "JCMotorsport");
            }
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
