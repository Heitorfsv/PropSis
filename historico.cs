using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class historico : Form
    {
        List<int> lista_os = new List<int>();
        List<decimal> lista_total = new List<decimal>();

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public historico()
        {
            InitializeComponent();

            listView1.Columns.Clear();
            listView1.Columns.Add("Data Saída", 100);
            listView1.Columns.Add("Cliente", 150);
            listView1.Columns.Add("Placa", 80);
            listView1.Columns.Add("Marca", 120);
            listView1.Columns.Add("Modelo", 150);
        }

        private void historico_Load(object sender, EventArgs e)
        {
            // Configuração visual
            lbl_titulo.Text = (static_class.historico == "pecas_os") ? "Faturamento da peça" : "Faturamento do serviço";
            if (listView1.Columns.Count == 1) // Evita duplicar colunas se o load rodar de novo
            {
                listView1.Columns.Add("Data Saída", 100);
                listView1.Columns.Add("Cliente", 150);
                listView1.Columns.Add("Placa", 80);
                listView1.Columns.Add("Marca", 100);
                listView1.Columns.Add("Modelo", 100);
                listView1.Columns.Add("Total Item", 100);
            }

            CarregarHistoricoHibrido();
        }

        private void CarregarHistoricoHibrido(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    decimal totalGeral = 0;
                    listView1.Items.Clear();
                    lista_os.Clear();
                    lista_total.Clear();

                    // 1. Busca os registros de uso da peça/serviço
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM {static_class.historico} WHERE nome = @nome ORDER BY os ASC";

                    var pNome = cmd.CreateParameter();
                    pNome.ParameterName = "@nome";
                    pNome.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(pNome);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idOS = Convert.ToInt32(reader["os"]);
                            decimal valor = Convert.ToDecimal(reader["valor"]);
                            decimal qtd = Convert.ToDecimal(reader["qtd"].ToString().Replace(".", ","));
                            decimal totalItem = valor * qtd;

                            lista_os.Add(idOS);
                            lista_total.Add(totalItem);
                            totalGeral += totalItem;
                        }
                    }

                    // 2. Busca os detalhes de cada OS vinculada
                    for (int i = 0; i < lista_os.Count; i++)
                    {
                        string dt_saida = "", cliente = "", placa = "", marca = "", modelo = "";

                        // Busca dados da OS
                        var cmdOS = conexao.CreateCommand();
                        cmdOS.CommandText = "SELECT dt_saida, cliente, placa FROM os WHERE controle = @idOS";
                        var pId = cmdOS.CreateParameter();
                        pId.ParameterName = "@idOS";
                        pId.Value = lista_os[i];
                        cmdOS.Parameters.Add(pId);

                        using (var rOS = cmdOS.ExecuteReader())
                        {
                            if (rOS.Read())
                            {
                                dt_saida = rOS["dt_saida"]?.ToString() ?? "";
                                cliente = rOS["cliente"]?.ToString() ?? "";
                                placa = rOS["placa"]?.ToString() ?? "";
                            }
                        }

                        // Busca dados da Moto (usando a placa obtida na OS)
                        if (!string.IsNullOrEmpty(placa))
                        {
                            var cmdMoto = conexao.CreateCommand();
                            cmdMoto.CommandText = "SELECT marca, modelo FROM motos WHERE placa = @placa LIMIT 1";
                            var pPlaca = cmdMoto.CreateParameter();
                            pPlaca.ParameterName = "@placa";
                            pPlaca.Value = placa;
                            cmdMoto.Parameters.Add(pPlaca);

                            using (var rMoto = cmdMoto.ExecuteReader())
                            {
                                if (rMoto.Read())
                                {
                                    marca = rMoto["marca"]?.ToString() ?? "";
                                    modelo = rMoto["modelo"]?.ToString() ?? "";
                                }
                            }
                        }

                        // Adiciona ao ListView
                        var lvi = new ListViewItem(dt_saida);
                        lvi.SubItems.Add(cliente);
                        lvi.SubItems.Add(placa);
                        lvi.SubItems.Add(marca);
                        lvi.SubItems.Add(modelo);
                        lvi.SubItems.Add(lista_total[i].ToString("N2"));
                        listView1.Items.Add(lvi);
                    }

                    txt_total.Text = totalGeral.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!usarLocal) CarregarHistoricoHibrido(true);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            int index = listView1.SelectedIndices[0];
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle = lista_os[index];
                edicao_os.Text = "Edição OS";
                edicao_os.Show();
            }
            catch { }
        }
    }
}
