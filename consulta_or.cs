using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrototipoSistema
{
    public partial class consulta_or : Form
    {
        // Configurações de Conexão
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public List<int> lista_or = new List<int>();
        List<string> lista_doc = new List<string>();

        string order = "DESC";
        string campoOrdenacao = "dt_cadastro";

        public consulta_or()
        {
            InitializeComponent();
            SetupListView();
        }

        private void SetupListView()
        {
            listView1.Columns.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("Placa", 100);
            listView1.Columns.Add("Cliente", 250);
            listView1.Columns.Add("Data Cadastro", 100);
            listView1.Columns.Add("Telefone", 110);
            listView1.Columns.Add("Marca", 130);
            listView1.Columns.Add("Modelo", 130);
            listView1.Columns.Add("Peças (R$)", 90);
            listView1.Columns.Add("Serviços (R$)", 100);
            listView1.Columns.Add("Total (R$)", 90);
        }

        // --- LÓGICA PRINCIPAL UNIFICADA ---
        private void CarregarDadosOrcamento(string filtroSql = "")
        {
            listView1.Items.Clear();
            lista_or.Clear();
            lista_doc.Clear();

            decimal totalGeralPecas = 0;
            decimal totalGeralServicos = 0;

            System.Data.IDbConnection conexao;
            bool isSqlite = false;

            // 1. TENTATIVA HÍBRIDA DE CONEXÃO
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
                isSqlite = true;
            }

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();

                    // 2. AJUSTE DE ORDENAÇÃO POR DATA (MySQL vs SQLite)
                    string ordenacao;
                    if (isSqlite)
                        // SQLite geralmente guarda texto, ordenamos direto ou via substr se necessário
                        ordenacao = $"ORDER BY o.{campoOrdenacao} {order}";
                    else
                        // MySQL precisa converter a string de data para ordenar corretamente
                        ordenacao = $"ORDER BY STR_TO_DATE(o.{campoOrdenacao}, '%d/%m/%y') {order}";

                    // Query Otimizada com Subqueries para Totais
                    string sql = $@"
                SELECT DISTINCT
                    o.*, 
                    c.telefone, 
                    m.marca, 
                    m.modelo,
                    (SELECT COALESCE(SUM((valor * qtd) - desco), 0) FROM servicos_os WHERE orca = o.controle) as total_serv_calc,
                    (SELECT COALESCE(SUM((valor * qtd) - desco), 0) FROM pecas_os WHERE orca = o.controle) as total_peca_calc
                FROM orcamentos o
                LEFT JOIN clientes c ON o.doc = c.doc
                LEFT JOIN motos m ON o.placa = m.placa
                WHERE 1=1 {filtroSql}
                {ordenacao}";

                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int controle = Convert.ToInt32(reader["controle"]);

                            // Proteção contra duplicatas no C#
                            if (lista_or.Contains(controle)) continue;

                            lista_or.Add(controle);
                            lista_doc.Add(reader["doc"].ToString());

                            var item = new ListViewItem(reader["placa"].ToString());
                            item.SubItems.Add(reader["cliente"].ToString());
                            item.SubItems.Add(FormatData(reader["dt_cadastro"].ToString()));
                            item.SubItems.Add(reader["telefone"].ToString());
                            item.SubItems.Add(reader["marca"].ToString());
                            item.SubItems.Add(reader["modelo"].ToString());

                            // COALESCE no SQL garante que não venha DBNull
                            decimal vPeca = Convert.ToDecimal(reader["total_peca_calc"]);
                            decimal vServ = Convert.ToDecimal(reader["total_serv_calc"]);

                            item.SubItems.Add(vPeca.ToString("N2"));
                            item.SubItems.Add(vServ.ToString("N2"));
                            item.SubItems.Add(reader["total"].ToString());

                            listView1.Items.Add(item);

                            totalGeralPecas += vPeca;
                            totalGeralServicos += vServ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Orçamentos: " + ex.Message, "JCMotorsport");
            }
        }

        // --- EVENTOS DE BOTÕES ---

        private void consulta_or_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            cmb_ps.SelectedIndex = 0;
            CarregarDadosOrcamento();
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            string col = cmb_consulta.Text;
            string pesq = txt_pequisa.Text.Replace(" ", "%");

            string filtro = (col == "marca" || col == "modelo")
                ? $" AND m.{col} LIKE '%{pesq}%'"
                : $" AND o.{col} LIKE '%{pesq}%'";

            CarregarDadosOrcamento(filtro);
        }

        private void bnt_pesquisar_ps_Click(object sender, EventArgs e)
        {
            if (lista_or.Count == 0) return;

            string tabela = cmb_ps.Text == "Serviços" ? "servicos_os" : "pecas_os";
            string termo = txt_ps.Text.Replace(" ", "%");
            string idsAtuais = string.Join(",", lista_or);

            // Sub-pesquisa: Filtra itens dentro dos orçamentos que já estão na tela
            string subFiltro = $@"
                AND o.controle IN ({idsAtuais})
                AND EXISTS (SELECT 1 FROM {tabela} i 
                            WHERE i.orca = o.controle 
                            AND i.nome LIKE '%{termo}%')";

            CarregarDadosOrcamento(subFiltro);
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            static_class.controle = 0; // Zera para novo cadastro
            cadastro_or frm = new cadastro_or();
            frm.Text = "Cadastro Orçamento";
            frm.Show();
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            CarregarDadosOrcamento();
        }

        private void lbl_order_Click(object sender, EventArgs e)
        {
            order = (order == "DESC") ? "ASC" : "DESC";
            lbl_order.Text = (order == "DESC") ? "↑" : "↓";
            CarregarDadosOrcamento();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            int index = listView1.SelectedIndices[0];

            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle = lista_or[index];

                cadastro_or.Show();
            }
            catch { }
        }

        // --- AUXILIARES ---
        private string FormatData(string data)
        {
            try { return DateTime.Parse(data).ToString("dd/MM/yyyy"); } catch { return data; }
        }
    }
}