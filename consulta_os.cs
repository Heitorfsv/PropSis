using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PrototipoSistema
{
    public partial class consulta_os : Form
    {
        List<bool> pagoStatus = new List<bool>();
        List<String> lista_doc = new List<String>();
        public List<int> lista_os = new List<int>();

        int count;
        string order = "DESC";
        
        public consulta_os()
        {
            InitializeComponent();
        }

        private void consulta_os_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            cmb_ps.SelectedIndex = 0;

            clear_lists();
            pagoStatus.Clear();
            lista_os.Clear();
            lista_doc.Clear();
            CarregarGrafico("");

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM os ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') {order}", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            count = 0;

            while (reader.Read())
            {
                lista_os.Add(reader.GetInt32("controle"));
                lst_placa.Items.Add(reader.GetString("placa"));
                lst_cliente.Items.Add(reader.GetString("cliente"));
                lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                lst_total.Items.Add(reader.GetString("total").ToString());
                lista_doc.Add(reader.GetString("doc"));

                pagoStatus.Add(reader.GetInt32("pago") == 0);

                try
                { lst_dt_saida.Items.Add(reader.GetString("dt_saida")); }
                catch
                { lst_dt_saida.Items.Add(""); }
                count++;
            }
            conexao.Close();

            count = 0;
            decimal total_servicos = 0;
            decimal total_pecas = 0;

            while (count <= lst_placa.Items.Count -1)
            {
                lst_placa.SelectedIndex = count;

                cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{lista_doc[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_telefone.Items.Add(reader.GetString("telefone"));
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{lst_placa.SelectedItem}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_marca.Items.Add(reader.GetString("marca"));  
                    lst_modelo.Items.Add(reader.GetString("modelo"));
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{lista_os[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal soma_servico = 0;

                while (reader.Read())
                {
                    try
                    {
                        string qtd = reader.GetString("qtd");
                        qtd = qtd.Replace(".", ",");

                        soma_servico += reader.GetDecimal("valor") * decimal.Parse(qtd);
                    }
                    catch { }
                }

                total_servicos += soma_servico;
                lst_preco_servico.Items.Add(soma_servico.ToString("N2"));

                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{lista_os[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal soma_peca = 0;

                while (reader.Read())
                {
                    try
                    {
                        string qtd = reader.GetString("qtd");
                        qtd = qtd.Replace(".", ",");

                        soma_peca += reader.GetDecimal("valor") * decimal.Parse(qtd); 
                    }
                    catch { }
                }

                total_pecas += soma_peca;
                lst_preco_peca.Items.Add(soma_peca.ToString("N2"));

                conexao.Close();
                count++;
            }

            txt_total_pecas.Text = total_pecas.ToString("N2");
            txt_total_servicos.Text = total_servicos.ToString("N2");
            txt_total.Text = (total_pecas + total_servicos).ToString("N2");

            nome_vermelho();

            if (lst_dt.Items.Count > 0)
            {
                scrollbar.Maximum = lst_dt.Items.Count - 1;
            }
        }

        public void CarregarGrafico(string parametro)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Criar área do gráfico
            ChartArea area = new ChartArea("AreaPrincipal");
            chart1.ChartAreas.Add(area);

            // Criar série (linha do gráfico)
            Series servicos = new Series("Faturamento de serviços");
            servicos.ChartType = SeriesChartType.Line;
            servicos.BorderWidth = 1;
            servicos.Color = System.Drawing.Color.Purple;

            Series pecas = new Series("Faturamento de peças");
            pecas.ChartType = SeriesChartType.Line;
            pecas.BorderWidth = 1;
            pecas.Color = System.Drawing.Color.Green;

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            int mes = 1;
            List<int> os = new List<int>();
            List<string> data = new List<string>();
            count = 0;

            while (mes != 12)
            {
                var cmd = new MySqlCommand($"SELECT * FROM os WHERE MONTH (STR_TO_DATE(dt_cadastro, '%d/%m/%y')) = {mes}{parametro} ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') {order}", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    os.Add(reader.GetInt32("controle"));
                    data.Add(reader.GetString("dt_cadastro").Substring(0, 10));
                }
                conexao.Close();

                while (count < os.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Simulação de dados: Faturamento por mês
                        servicos.Points.AddXY(data[count], (decimal.Parse(reader.GetString("valor")) * decimal.Parse(reader.GetString("qtd"))));
                    }
                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Simulação de dados: Faturamento por mês
                        pecas.Points.AddXY(data[count], (decimal.Parse(reader.GetString("valor")) * decimal.Parse(reader.GetString("qtd"))));
                    }
                    conexao.Close();
                    count++;
                }

                mes++;
            }

            // Adiciona a série ao gráfico
            chart1.Series.Add(servicos);
            chart1.Series.Add(pecas);

            // Título (opcional)
            chart1.Titles.Clear();
            chart1.Titles.Add("Faturamento");
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Verifica se o índice é válido
                if (e.Index < 0) return;

                // Identifica a ListBox sendo desenhada
                ListBox listBox = (ListBox)sender;

                // Define a cor com base no status de "pago"
                bool isPago = !pagoStatus[e.Index]; // true se pago == 1, false se pago == 0
                Color textColor = isPago ? Color.Black : Color.Red;

                // Preenche o fundo
                e.DrawBackground();

                // Desenha o item na cor correta
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(textColor), e.Bounds);

                // Desenha o foco no item se necessário
                e.DrawFocusRectangle();

            }
            catch { }
        }

        private void nome_vermelho()
        {
            lst_cliente.DrawMode = DrawMode.OwnerDrawFixed;
            lst_cliente.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_telefone.DrawMode = DrawMode.OwnerDrawFixed;
            lst_telefone.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_dt.DrawMode = DrawMode.OwnerDrawFixed;
            lst_dt.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_dt_saida.DrawMode = DrawMode.OwnerDrawFixed;
            lst_dt_saida.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_modelo.DrawMode = DrawMode.OwnerDrawFixed;
            lst_modelo.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_marca.DrawMode = DrawMode.OwnerDrawFixed;
            lst_marca.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_preco_servico.DrawMode = DrawMode.OwnerDrawFixed;
            lst_preco_servico.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_preco_peca.DrawMode = DrawMode.OwnerDrawFixed;
            lst_preco_peca.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_placa.DrawMode = DrawMode.OwnerDrawFixed;
            lst_placa.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_total.DrawMode = DrawMode.OwnerDrawFixed;
            lst_total.DrawItem += new DrawItemEventHandler(lst_DrawItem);
        }

        private void lst_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_marca.SelectedIndex = lst_cliente.SelectedIndex;
            lst_modelo.SelectedIndex = lst_cliente.SelectedIndex;
            lst_placa.SelectedIndex = lst_cliente.SelectedIndex;
            lst_preco_peca.SelectedIndex = lst_cliente.SelectedIndex;
            lst_preco_servico.SelectedIndex = lst_cliente.SelectedIndex;
            lst_total.SelectedIndex = lst_cliente.SelectedIndex;
            lst_dt.SelectedIndex = lst_cliente.SelectedIndex;
            lst_telefone.SelectedIndex = lst_cliente.SelectedIndex;

            try
            { lst_dt_saida.SelectedIndex = lst_cliente.SelectedIndex; }
            catch { }
        }

        private void lst_placa_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_placa.SelectedIndex;
        }

        private void lst_marca_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_marca.SelectedIndex;
        }

        private void lst_modelo_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_modelo.SelectedIndex;   
        }

        private void lst_dt_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_dt.SelectedIndex;
        }

        private void lst_preco_servico_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_preco_servico.SelectedIndex;
        }

        private void lst_preco_peca_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_preco_peca.SelectedIndex;
        }

        private void lst_total_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_total.SelectedIndex;
        }

        private void lst_cliente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_dt_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_placa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_marca_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_modelo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_preco_servico_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_preco_peca_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_total_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            clear_lists();
            lista_os.Clear();
            lista_doc.Clear();

            List<int> list_index = new List<int>();

            if (cmb_consulta.Text == "dt_cadastro" || cmb_consulta.Text == "placa" || cmb_consulta.Text == "cliente")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao); 


                var cmd = new MySqlCommand($"SELECT * FROM os WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);
                CarregarGrafico($" AND {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'");

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> placa = new List<string>();

                while (reader.Read())
                {
                    lista_os.Add(reader.GetInt32("controle"));
                    lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    lista_doc.Add(reader.GetString("doc"));
                    placa.Add(reader.GetString("placa"));
                    lst_placa.Items.Add(placa.Last());
                    lst_total.Items.Add(reader.GetDecimal("total").ToString());
                    try
                    { lst_dt_saida.Items.Add(reader.GetString("dt_saida")); }
                    catch { }
                }
                conexao.Close();
                int count = 0;

                while (count < placa.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{placa[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lst_marca.Items.Add(reader.GetString("marca"));
                        lst_modelo.Items.Add(reader.GetString("modelo"));
                    }
                    conexao.Close();
                    count++;
                }
                count = 0;
                decimal total_servicos = 0;
                decimal total_pecas = 0;

                while (count < lista_os.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{lista_doc[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst_telefone.Items.Add(reader.GetString("telefone"));
                    }
                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{lista_os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    decimal soma_servico = 0;

                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd");
                            qtd = qtd.Replace(".", ",");

                            soma_servico += reader.GetDecimal("valor") * decimal.Parse(qtd);
                        }
                        catch { }
                    }
                    total_servicos += soma_servico;
                    lst_preco_servico.Items.Add(soma_servico.ToString("N2"));

                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{lista_os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    decimal soma_peca = 0;

                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd");
                            qtd = qtd.Replace(".", ",");

                            soma_peca += reader.GetDecimal("valor") * decimal.Parse(qtd);
                        }
                        catch { }
                    }
                    total_pecas += soma_peca;
                    lst_preco_peca.Items.Add(soma_peca.ToString("N2"));

                    conexao.Close();

                    count++;
                }

                txt_total_pecas.Text = total_pecas.ToString("N2");
                txt_total_servicos.Text = total_servicos.ToString("N2");
                txt_total.Text = (total_pecas + total_servicos).ToString("N2");
            }
            else if (cmb_consulta.Text == "marca" || cmb_consulta.Text == "modelo")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> placa = new List<string>();
                lista_doc.Clear();

                while (reader.Read())
                {
                    placa.Add(reader.GetString("placa"));
                }
                conexao.Close();

                int count = 0;

                while (count < placa.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM os WHERE placa = '{placa[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista_os.Add(reader.GetInt32("controle"));
                        lst_dt.Items.Add(reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy"));
                        try
                        { lst_dt_saida.Items.Add(reader.GetString("dt_saida")); }
                        catch
                        { lst_dt_saida.Items.Add(" "); }
                        lst_cliente.Items.Add(reader.GetString("cliente"));
                        lst_placa.Items.Add(reader.GetString("placa"));
                        lst_total.Items.Add(reader.GetDecimal("total").ToString());
                        lista_doc.Add(reader.GetString("doc"));
                    }
                    conexao.Close();

                    count++;
                }

                count = 0;

                while (count < lista_doc.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{lista_doc[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst_marca.Items.Add(reader.GetString("marca"));
                        lst_modelo.Items.Add(reader.GetString("modelo"));
                    }
                    conexao.Close();

                    count++;
                }

                count = 0;

                while (count < lista_os.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{lista_os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    decimal valor = 0;

                    while (reader.Read())
                    {
                        valor += reader.GetDecimal("valor") * reader.GetDecimal("qtd");
                    }
                    lst_preco_servico.Items.Add(valor.ToString("N2"));
                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{lista_os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    valor = 0;

                    while (reader.Read())
                    {
                        valor += reader.GetDecimal("valor") * reader.GetDecimal("qtd");
                    }
                    lst_preco_peca.Items.Add(valor.ToString("N2"));
                    conexao.Close();

                    count++;
                }
            }
            nome_vermelho();
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_os_Load(sender, e);
        }

        private void lst_dt_saida_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_dt_saida.SelectedIndex;
        }

        private void lst_dt_saida_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();
                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void bnt_pesquisar_ps_Click(object sender, EventArgs e)
        {
            List<int> consulta_os = new List<int>();
            List<string> doc_dono = new List<string>();

            clear_lists();
            consulta_os.Clear();
            doc_dono.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);


            int count = 0;

            string tabela = "";
            if (cmb_ps.Text == "Serviços")
            { tabela = "servicos_os"; }

            else if (cmb_ps.Text == "Peças")
            { tabela = "pecas_os"; }

            string lista = "";
            while (count < lista_os.Count)
            {
                lista = lista + lista_os[count] + "\n";
                var cmd = new MySqlCommand($"SELECT * FROM {tabela} WHERE os = {lista_os[count]} AND nome LIKE '%{txt_ps.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) 
                { consulta_os.Add(reader.GetInt32("os")); }
                conexao.Close();

                count++;
            }

            count = 0;
            decimal total_servicos = 0;
            decimal total_pecas = 0;

            while (count < consulta_os.Count)
            {
                var cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = {consulta_os[count]}", conexao);
                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    lst_placa.Items.Add(reader.GetString("placa"));
                    lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    lst_dt_saida.Items.Add(reader.GetString("dt_saida"));
                    lst_total.Items.Add(reader.GetDecimal("total").ToString());
                    doc_dono.Add(reader.GetString("doc"));
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{lista_doc[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_telefone.Items.Add(reader.GetString("telefone"));
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{doc_dono[count]}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_marca.Items.Add(reader.GetString("marca"));
                    lst_modelo.Items.Add(reader.GetString("modelo"));
                }
                conexao.Close();

                decimal soma_servico = 0;

                cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = {consulta_os[count]}", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    soma_servico += reader.GetDecimal("valor") * decimal.Parse(qtd);
                }
                total_servicos += soma_servico;
                lst_preco_servico.Items.Add(soma_servico.ToString("N2"));

                conexao.Close();

                decimal soma_peca = 0;

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{consulta_os[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    soma_peca += reader.GetDecimal("valor") * decimal.Parse(qtd);
                }
                total_pecas += soma_peca;
                lst_preco_peca.Items.Add(soma_peca.ToString("N2"));

                conexao.Close();

                count++;
            }

            txt_total_pecas.Text = total_pecas.ToString("N2");
            txt_total_servicos.Text = total_servicos.ToString("N2");
            txt_total.Text = (total_pecas + total_servicos).ToString("N2");

            nome_vermelho();
        }


        public void clear_lists()
        {
            lst_cliente.Items.Clear();
            lst_telefone.Items.Clear();
            lst_placa.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();
            lst_preco_peca.Items.Clear();
            lst_preco_servico.Items.Clear();
            lst_total.Items.Clear();
            lst_dt.Items.Clear();
            lst_dt_saida.Items.Clear();
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            cadastro_os cadastro = new cadastro_os();
            cadastro.Show();
        }

        private void lst_telefone_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_telefone.SelectedIndex;
        }

        private void lst_telefone_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_placa.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }


        private void lbl_order_Click(object sender, EventArgs e)
        {
            if (order == "DESC")
            {
                order = "ASC";
                lbl_order.Text = "↓" ;
                consulta_os_Load(sender, e);
            }
            else 
            {
                order = "DESC";
                lbl_order.Text = "↑";
                consulta_os_Load(sender, e);
            }
        }

        private void scrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            int scrollValue = e.NewValue;

            // Sincronizar todas as listas
            lst_dt.TopIndex = scrollValue;
            lst_cliente.TopIndex = scrollValue;
            lst_telefone.TopIndex = scrollValue;
            lst_placa.TopIndex = scrollValue;
            lst_marca.TopIndex = scrollValue;
            lst_modelo.TopIndex = scrollValue;
            lst_preco_peca.TopIndex = scrollValue;
            lst_preco_servico.TopIndex = scrollValue;
            lst_total.TopIndex = scrollValue;
            lst_dt_saida.TopIndex = scrollValue;
        }
    }
}
