using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrototipoSistema
{
    public partial class consulta_os : Form
    {
        // Configurações de Conexão (Ajuste os caminhos conforme sua necessidade)
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public List<int> lista_os = new List<int>();
        List<string> lista_doc = new List<string>();

        string order = "DESC";
        string campoOrdenacao = "dt_cadastro";

        public consulta_os()
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
            listView1.Columns.Add("Cadastro", 90);
            listView1.Columns.Add("Saída", 90);
            listView1.Columns.Add("Telefone", 110);
            listView1.Columns.Add("Marca", 120);
            listView1.Columns.Add("Modelo", 120);
            listView1.Columns.Add("Peças (R$)", 90);
            listView1.Columns.Add("Serviços (R$)", 100);
            listView1.Columns.Add("Total (R$)", 90);
        }

        // --- LÓGICA PRINCIPAL DE CARREGAMENTO ---
        private void CarregarDadosOS(string filtroSql = "")
        {
            listView1.Items.Clear();
            lista_os.Clear();
            lista_doc.Clear();

            decimal totalGeralPecas = 0;
            decimal totalGeralServicos = 0;

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

                    // SQL com Alias 'o' para evitar ambiguidade no SQLite e COALESCE para os cálculos
                    string sql = $@"
                SELECT DISTINCT
                    o.*, 
                    c.telefone, 
                    m.marca, 
                    m.modelo,
                    (SELECT COALESCE(SUM((valor * qtd) - desco), 0) FROM servicos_os WHERE os = o.controle) as total_serv_calc,
                    (SELECT COALESCE(SUM((valor * qtd) - desco), 0) FROM pecas_os WHERE os = o.controle) as total_peca_calc
                FROM os o
                LEFT JOIN clientes c ON o.doc = c.doc
                LEFT JOIN motos m ON o.placa = m.placa
                WHERE 1=1 {filtroSql}
                ORDER BY o.controle DESC";

                    cmd.CommandText = sql;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 1. Validação do ID de controle
                            int controle = reader["controle"] != DBNull.Value ? Convert.ToInt32(reader["controle"]) : 0;
                            if (controle == 0 || lista_os.Contains(controle)) continue;

                            lista_os.Add(controle);
                            lista_doc.Add(reader["doc"]?.ToString() ?? "");

                            // 2. Criação do item com strings seguras (uso de ?? para evitar null)
                            var item = new ListViewItem(reader["placa"]?.ToString() ?? "");
                            item.SubItems.Add(reader["cliente"]?.ToString() ?? "");
                            item.SubItems.Add(FormatData(reader["dt_cadastro"]?.ToString() ?? ""));
                            item.SubItems.Add(FormatData(reader["dt_saida"]?.ToString() ?? ""));
                            item.SubItems.Add(reader["telefone"]?.ToString() ?? "");
                            item.SubItems.Add(reader["marca"]?.ToString() ?? "");
                            item.SubItems.Add(reader["modelo"]?.ToString() ?? "");

                            // 3. BLINDAGEM CONTRA DBNULL: Conversão segura de decimais
                            decimal vPeca = reader["total_peca_calc"] != DBNull.Value ? Convert.ToDecimal(reader["total_peca_calc"]) : 0;
                            decimal vServ = reader["total_serv_calc"] != DBNull.Value ? Convert.ToDecimal(reader["total_serv_calc"]) : 0;

                            item.SubItems.Add(vPeca.ToString("N2"));
                            item.SubItems.Add(vServ.ToString("N2"));
                            item.SubItems.Add(reader["total"]?.ToString() ?? "0,00");

                            // 4. Validação do campo Pago
                            int pago = reader["pago"] != DBNull.Value ? Convert.ToInt32(reader["pago"]) : 0;
                            if (pago == 0) item.ForeColor = Color.Red;

                            listView1.Items.Add(item);

                            totalGeralPecas += vPeca;
                            totalGeralServicos += vServ;
                        }
                    }
                }

                // Atualização dos totais na tela
                txt_total_pecas.Text = totalGeralPecas.ToString("N2");
                txt_total_servicos.Text = totalGeralServicos.ToString("N2");
                txt_total.Text = (totalGeralPecas + totalGeralServicos).ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar OS: " + ex.Message);
            }
        }

        public void CarregarGrafico(string filtroExtra = "")
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("Main"));

            Series sServ = new Series("Serviços") { ChartType = SeriesChartType.Line, Color = Color.Purple, BorderWidth = 3 };
            Series sPec = new Series("Peças") { ChartType = SeriesChartType.Line, Color = Color.Green, BorderWidth = 3 };

            System.Data.IDbConnection conexao;
            bool isSqlite = false;

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

                    // Ajuste de função de data conforme o banco conectado
                    string funcMes = isSqlite ? "strftime('%m', dt_cadastro)" : "MONTH(STR_TO_DATE(dt_cadastro, '%d/%m/%Y'))";

                    cmd.CommandText = $@"
                SELECT 
                    {funcMes} as mes,
                    SUM((SELECT COALESCE(SUM((valor * qtd) - desco), 0) FROM servicos_os WHERE os = os.controle)) as soma_s,
                    SUM((SELECT COALESCE(SUM((valor * qtd) - desco), 0) FROM pecas_os WHERE os = os.controle)) as soma_p
                FROM os 
                WHERE 1=1 {filtroExtra}
                GROUP BY mes ORDER BY mes ASC";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int numMes = Convert.ToInt32(reader["mes"]);
                            string mesNome = GetNomeMes(numMes);

                            sServ.Points.AddXY(mesNome, reader["soma_s"] != DBNull.Value ? reader["soma_s"] : 0);
                            sPec.Points.AddXY(mesNome, reader["soma_p"] != DBNull.Value ? reader["soma_p"] : 0);
                        }
                    }
                }
                chart1.Series.Add(sServ);
                chart1.Series.Add(sPec);
            }
            catch { /* Silencioso */ }
        }

        // --- EVENTOS ---
        private void consulta_os_Load(object sender, EventArgs e)
        {
            CarregarDadosOS();
            CarregarGrafico("");
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            string col = cmb_consulta.Text;
            string pesq = txt_pesquisa.Text.Replace(" ", "%");

            // Se pesquisar por marca/modelo que está em outra tabela
            if (col == "marca" || col == "modelo")
                CarregarDadosOS($" AND m.{col} LIKE '%{pesq}%'");
            else
                CarregarDadosOS($" AND os.{col} LIKE '%{pesq}%'");

            CarregarGrafico(col != "marca" && col != "modelo" ? $" AND {col} LIKE '%{pesq}%'" : "");
        }

        private void bnt_pesquisar_ps_Click(object sender, EventArgs e)
        {
            // 1. Identifica qual tabela de itens pesquisar
            string tabelaItens = cmb_ps.Text == "Serviços" ? "servicos_os" : "pecas_os";

            // 2. Formata o termo de pesquisa (Troca Oleo -> Troca%Oleo)
            string termo = txt_ps.Text.Replace(" ", "%");

            // 3. Criamos uma lista com os IDs das OSs que estão atualmente na tela
            // Se a lista_os estiver vazia, não há o que pesquisar
            if (lista_os.Count == 0) return;

            string idsAtuais = string.Join(",", lista_os);

            // 4. Chamamos o carregamento principal passando o filtro de "Subpesquisa"
            // O comando "IN" garante que só buscaremos dentro do que já estava na tela
            // O "EXISTS" verifica se dentro daquela OS existe o item pesquisado
            string subFiltro = $@"
        AND os.controle IN ({idsAtuais})
        AND EXISTS (SELECT 1 FROM {tabelaItens} i 
                    WHERE i.os = os.controle 
                    AND i.nome LIKE '%{termo}%')";

            CarregarDadosOS(subFiltro);

            // 5. Atualiza o gráfico para refletir apenas essas OSs encontradas
            CarregarGrafico(subFiltro);
        }

        private void lbl_order_Click(object sender, EventArgs e)
        {
            order = (order == "DESC") ? "ASC" : "DESC";
            lbl_order.Text = (order == "DESC") ? "↑" : "↓";
            CarregarDadosOS();
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            campoOrdenacao = "dt_cadastro";
            CarregarDadosOS();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                static_class.controle = lista_os[listView1.SelectedIndices[0]];
                new edicao_os { Text = "Edição OS" }.Show();
            }
        }

        // --- MÉTODOS AUXILIARES ---

        private string FormatData(string data)
        {
            if (string.IsNullOrEmpty(data)) return "";
            try { return DateTime.Parse(data).ToString("dd/MM/yyyy"); } catch { return data; }
        }

        private string GetNomeMes(int mes)
        {
            string[] meses = { "", "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            return meses[mes];
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_os os = new edicao_os();
            os.Text = "Cadastro OS";
            os.Show();
        }

        private void bnt_pag_Click(object sender, EventArgs e)
        {
            // Filtra apenas onde pago é igual a 0 (pendente)
            // Se quiser alternar, podemos usar a variável 'order' ou 'filtro'
            string filtroPagamento = " AND os.pago = 0";

            CarregarDadosOS(filtroPagamento);

            // Atualiza o gráfico para mostrar o faturamento apenas das OS pendentes (opcional)
            CarregarGrafico(filtroPagamento);
        }
    }
}