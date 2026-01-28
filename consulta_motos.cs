using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class consulta_motos : Form
    {
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public consulta_motos()
        {
            InitializeComponent();
        }

        private void consulta_motos_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Colunas
            listView1.Columns.Add("Placa", 100);
            listView1.Columns.Add("Marca", 120);
            listView1.Columns.Add("Modelo", 130);
            listView1.Columns.Add("Cor", 100);
            listView1.Columns.Add("Ano", 60);
            listView1.Columns.Add("Proprietário", 300);

            // Chamar o método de pesquisa sem filtro para carregar tudo
            CarregarDadosMotos("", "");
        }

        private void CarregarDadosMotos(string campoFiltro, string valorFiltro)
        {
            System.Data.IDbConnection conexao;

            // 1. LÓGICA HÍBRIDA DE CONEXÃO
            try
            {
                var mysql = new MySqlConnection(strConexao);
                mysql.Open();
                conexao = mysql;
            }
            catch
            {
                // Se o MySQL falhar, ele cai aqui e abre o SQLite
                var sqlite = new System.Data.SQLite.SQLiteConnection(strLocal);
                sqlite.Open();
                conexao = sqlite;
            }

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();

                    // SQL Unificado
                    string sql = @"
                SELECT m.placa, m.marca, m.modelo, m.cor, m.ano, m.doc_dono, c.nome 
                FROM motos m 
                LEFT JOIN clientes c ON m.doc_dono = c.doc";

                    // Aplica filtro se necessário
                    if (!string.IsNullOrEmpty(campoFiltro) && !string.IsNullOrEmpty(valorFiltro))
                    {
                        sql += $" WHERE m.{campoFiltro} LIKE @pesquisa";

                        // Parâmetro genérico que funciona em ambos os bancos
                        var pPesquisa = cmd.CreateParameter();
                        pPesquisa.ParameterName = "@pesquisa";
                        pPesquisa.Value = "%" + valorFiltro.Replace(" ", "%") + "%";
                        cmd.Parameters.Add(pPesquisa);
                    }

                    cmd.CommandText = sql;
                    listView1.Items.Clear();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ListViewItem(reader["placa"].ToString());
                            item.SubItems.Add(reader["marca"].ToString());
                            item.SubItems.Add(reader["modelo"].ToString());
                            item.SubItems.Add(reader["cor"].ToString());
                            item.SubItems.Add(reader["ano"].ToString());

                            string nomeDono = reader["nome"] != DBNull.Value ? reader["nome"].ToString() : "Não cadastrado";
                            item.SubItems.Add(nomeDono);

                            item.Tag = reader["doc_dono"].ToString();
                            listView1.Items.Add(item);
                        }
                    }
                }

                if (listView1.Items.Count > 0)
                    listView1.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                // Este catch agora só serve para erros de SQL (digitação errada, por exemplo)
                MessageBox.Show("Erro ao processar dados: " + ex.Message);
            }
        }

        // Botão Pesquisar por campo (Placa, Marca, etc)
        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            CarregarDadosMotos(cmb_consulta.Text, txt_pesquisa.Text);
        }

        private void bnt_pesquisar_nome_Click(object sender, EventArgs e)
        {
            // 1. Limpa a lista IMEDIATAMENTE para garantir que não reste nada da pesquisa anterior
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

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();

                    // 2. Adicionei o DISTINCT para garantir que o SQL não retorne linhas repetidas
                    // se houver alguma confusão nos IDs ou documentos.
                    cmd.CommandText = $@"
                SELECT DISTINCT m.placa, m.marca, m.modelo, m.cor, m.ano, m.doc_dono, c.nome 
                FROM motos m 
                INNER JOIN clientes c ON m.doc_dono = c.doc 
                WHERE c.nome LIKE @nomePesquisa AND m.{cmb_consulta.Text} LIKE @consulta";

                    var pNome = cmd.CreateParameter();
                    pNome.ParameterName = "@nomePesquisa";
                    pNome.Value = "%" + txt_pesquisar_nome.Text.Replace(" ", "%") + "%";
                    cmd.Parameters.Add(pNome);

                    var pConsulta = cmd.CreateParameter();
                    pConsulta.ParameterName = "@consulta";
                    pConsulta.Value = "%" + txt_pesquisa.Text.Replace(" ", "%") + "%";
                    cmd.Parameters.Add(pConsulta);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Criamos um HashSet para uma segunda camada de proteção no C# (evita duplicar no loop)
                        HashSet<string> placasAdicionadas = new HashSet<string>();

                        while (reader.Read())
                        {
                            string placa = reader["placa"].ToString();

                            // 3. Só adiciona se a placa ainda não estiver na lista
                            if (!placasAdicionadas.Contains(placa))
                            {
                                var item = new ListViewItem(placa);
                                item.SubItems.Add(reader["marca"].ToString());
                                item.SubItems.Add(reader["modelo"].ToString());
                                item.SubItems.Add(reader["cor"].ToString());
                                item.SubItems.Add(reader["ano"].ToString());
                                item.SubItems.Add(reader["nome"].ToString());
                                item.Tag = reader["doc_dono"].ToString();

                                listView1.Items.Add(item);
                                placasAdicionadas.Add(placa);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na pesquisa: " + ex.Message);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];

                edicao_motos edicao = new edicao_motos();
                edicao.Text = "Edição motos";

                static_class.doc_consultar = item.Text; // placa
                static_class.doc_dono = item.Tag.ToString(); // doc_dono
                edicao.Show();
            }   
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_motos cadastro = new edicao_motos();
            cadastro.Text = "Cadastro motos";
            cadastro.Show();
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_motos_Load(sender, e);
        }
    }
}
