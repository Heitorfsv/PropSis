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

        private List<int> lista_filtro = new List<int>();
        private List<int> lista_Fatrasado = new List<int>();

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
            lv.Columns.Add("Cliente", 150);
            lv.Columns.Add("Placa", 100);
            lv.Columns.Add("Marca", 100);
            lv.Columns.Add("Modelo", 100);
            lv.Columns.Add("Previsão de troca", 150);
        }

        private void CarregarDados()
        {
            string connStr = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM os WHERE aviso_oleo REGEXP '[A-Za-z0-9]' ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') DESC", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    HashSet<string> placasAdicionadas = new HashSet<string>();

                    while (reader.Read())
                    {
                        DateTime data = DateTime.Parse(reader.GetString("aviso_oleo"));
                        string placa = reader.GetString("placa");
                        int controle = reader.GetInt32("controle");
                        string cliente = reader.GetString("cliente");

                        // Verifica se a placa já foi adicionada
                        if (placasAdicionadas.Contains(placa))
                            continue;

                        // Marca essa placa como já adicionada
                        placasAdicionadas.Add(placa);

                        var moto = ObterMoto(placa);
                        ListViewItem item = new ListViewItem(new[] { cliente, moto.placa, moto.marca, moto.modelo, data.ToString("dd/MM/yyyy") });

                        if (data > DateTime.Now)
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

                using (var cmd = new MySqlCommand("SELECT * FROM os WHERE aviso REGEXP '[A-Za-z0-9]' ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') DESC", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    HashSet<string> placasAdicionadas = new HashSet<string>();

                    while (reader.Read())
                    {
                        DateTime data = DateTime.Parse(reader.GetString("aviso_revisao"));
                        string placa = reader.GetString("placa");
                        int controle = reader.GetInt32("controle");
                        string cliente = reader.GetString("cliente");

                        // Verifica se a placa já foi adicionada
                        if (placasAdicionadas.Contains(placa))
                            continue;

                        // Marca essa placa como já adicionada
                        placasAdicionadas.Add(placa);

                        var moto = ObterMoto(placa);
                        ListViewItem item = new ListViewItem(new[] { cliente, moto.placa, moto.marca, moto.modelo, data.ToString("dd/MM/yyyy") });

                        if (data > DateTime.Now)
                        {
                            lista_filtro.Add(controle);
                            lst_filtro.Items.Add(item);
                        }
                        else
                        {
                            lista_Fatrasado.Add(controle);
                            lst_filtro_atrasado.Items.Add(item);
                        }
                    }
                }
            }
        }

        private (string placa, string marca, string modelo) ObterMoto(string placa)
        {
            string connStr = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new MySqlCommand($"SELECT placa, marca, modelo FROM motos WHERE placa = @placa", conn))
                {
                    cmd.Parameters.AddWithValue("@placa", placa);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return (reader.GetString("placa"), reader.GetString("marca"), reader.GetString("modelo"));
                    }
                }
            }
            return ("", "","");
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

                static_class.controle = lista_filtro[index];

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

                static_class.controle = lista_Fatrasado[index];

                os.Show();
            }
            catch { }
        }
    }
}
