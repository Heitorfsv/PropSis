using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class trocas : Form
    {
        private List<int> lista_oleo = new List<int>();
        private List<int> lista_Oatrasado = new List<int>();

        private List<int> lista_revisao = new List<int>();
        private List<int> lista_Ratrasado = new List<int>();

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public trocas()
        {
            InitializeComponent();
        }

        private void trocas_Load(object sender, EventArgs e)
        {
            ConfigurarListView(lst_oleo);
            ConfigurarListView(lst_oleo_atrasado);
            ConfigurarListView(lst_filtro);
            ConfigurarListView(lst_filtro_atrasado);

            CarregarDados();
        }

        private void ConfigurarListView(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.Columns.Clear();
            lv.Columns.Add("Cliente", 170);
            lv.Columns.Add("Placa", 100);
            lv.Columns.Add("Marca", 100);
            lv.Columns.Add("Modelo", 100);
            lv.Columns.Add("KM", 70);
            lv.Columns.Add("Data de saída", 100);
            lv.Columns.Add("Previsão", 150);
        }

        private void CarregarDados()
        {
            // 1. Limpeza total de componentes visuais e listas de IDs
            lst_oleo.Items.Clear();
            lst_oleo_atrasado.Items.Clear();
            lst_filtro.Items.Clear();
            lst_filtro_atrasado.Items.Clear();

            lista_oleo.Clear();
            lista_Oatrasado.Clear();
            lista_revisao.Clear();
            lista_Ratrasado.Clear();

            System.Data.IDbConnection conexao;

            // 2. Lógica Híbrida: Tenta Remoto, se falhar vai para Local
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
                // SQL Otimizado com LEFT JOIN para buscar tudo em uma única viagem ao banco
                string sql = @"
            SELECT o.*, m.marca, m.modelo 
            FROM os o
            LEFT JOIN motos m ON o.placa = m.placa
            WHERE (o.aviso_oleo IS NOT NULL AND o.aviso_oleo != '') 
               OR (o.aviso_revisao IS NOT NULL AND o.aviso_revisao != '')
            ORDER BY o.controle DESC";

                var cmd = conexao.CreateCommand();
                cmd.CommandText = sql;

                using (var reader = cmd.ExecuteReader())
                {
                    HashSet<string> placasOleoVistas = new HashSet<string>();
                    HashSet<string> placasRevVistas = new HashSet<string>();

                    while (reader.Read())
                    {
                        // Captura de dados com tratamento de Nulos
                        string placa = reader["placa"]?.ToString() ?? "";
                        string cliente = reader["cliente"]?.ToString() ?? "N/A";
                        string marca = reader["marca"]?.ToString() ?? "N/A";
                        string modelo = reader["modelo"]?.ToString() ?? "N/A";
                        string km = reader["km"]?.ToString() ?? "0";
                        string dt_saida = reader["dt_saida"]?.ToString() ?? "";
                        int controle = Convert.ToInt32(reader["controle"]);

                        // Formata a data de saída para tirar a hora, se houver
                        if (DateTime.TryParse(dt_saida, out DateTime saidaAux))
                            dt_saida = saidaAux.ToString("dd/MM/yyyy");

                        // --- LÓGICA: PRÓXIMA TROCA DE ÓLEO ---
                        if (reader["aviso_oleo"] != DBNull.Value && !placasOleoVistas.Contains(placa))
                        {
                            if (DateTime.TryParse(reader["aviso_oleo"].ToString(), out DateTime dtOleo))
                            {
                                placasOleoVistas.Add(placa);
                                // Novo array de colunas: Cliente, Placa, Marca, Modelo, KM, Saída, Próx. Troca
                                var item = new ListViewItem(new[] { cliente, placa, marca, modelo, km, dt_saida, dtOleo.ToString("dd/MM/yyyy") });

                                if (dtOleo > DateTime.Now)
                                {
                                    lista_oleo.Add(controle);
                                    lst_oleo.Items.Add(item);
                                }
                                else
                                {
                                    lista_Oatrasado.Add(controle);
                                    lst_oleo_atrasado.Items.Add(item);
                                }
                            }
                        }

                        // --- LÓGICA: PREVISÃO PARA REVISÃO ---
                        if (reader["aviso_revisao"] != DBNull.Value && !placasRevVistas.Contains(placa))
                        {
                            if (DateTime.TryParse(reader["aviso_revisao"].ToString(), out DateTime dtRev))
                            {
                                placasRevVistas.Add(placa);
                                // Novo array de colunas seguindo o mesmo padrão
                                var item = new ListViewItem(new[] { cliente, placa, marca, modelo, km, dt_saida, dtRev.ToString("dd/MM/yyyy") });

                                if (dtRev > DateTime.Now)
                                {
                                    lista_revisao.Add(controle);
                                    lst_filtro.Items.Add(item);
                                }
                                else
                                {
                                    lista_Ratrasado.Add(controle);
                                    lst_filtro_atrasado.Items.Add(item);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void lst_oleo_atrasado_DoubleClick(object sender, EventArgs e)
        {
            if (lst_oleo_atrasado.SelectedIndices.Count == 0)
                return;

            int index = lst_oleo_atrasado.SelectedIndices[0];

            try
            {
                edicao_os os = new edicao_os();
                os.Text = "Edição OS";

                static_class.controle = lista_Oatrasado[index];

                os.Show();
            }
            catch { }
        }

        private void lst_oleo_DoubleClick(object sender, EventArgs e)
        {
            if (lst_oleo.SelectedIndices.Count == 0)
                return;

            int index = lst_oleo.SelectedIndices[0];

            try
            {
                edicao_os os = new edicao_os();
                os.Text = "Edição OS";

                static_class.controle = lista_oleo[index];

                os.Show();
            }
            catch { }
        }

        private void lst_filtro_DoubleClick(object sender, EventArgs e)
        {
            if (lst_filtro.SelectedIndices.Count == 0)
                return;

            int index = lst_filtro.SelectedIndices[0];

            try
            {
                edicao_os os = new edicao_os();
                os.Text = "Edição OS";

                static_class.controle = lista_revisao[index];

                os.Show();
            }
            catch { }
        }

        private void lst_filtro_atrasado_DoubleClick(object sender, EventArgs e)
        {
            if (lst_filtro_atrasado.SelectedIndices.Count == 0)
                return;

            int index = lst_filtro_atrasado.SelectedIndices[0];

            try
            {
                edicao_os os = new edicao_os();
                os.Text = "Edição OS";

                static_class.controle = lista_Ratrasado[index];

                os.Show();
            }
            catch { }
        }
    }
}
