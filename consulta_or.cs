using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class consulta_or : Form
    {
        List<String> lista_doc = new List<String>();
        public List<int> lista_or = new List<int>();

        int count;
        string order = "DESC";

        public consulta_or()
        {
            InitializeComponent();
            SetupListView();
        }

        private void SetupListView()
        {
            // Configurar colunas da ListView (deve estar no modo Details)
            listView1.Columns.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.MultiSelect = false;

            listView1.Columns.Add("Placa", 100);
            listView1.Columns.Add("Cliente", 370);
            listView1.Columns.Add("Data Cadastro", 100);
            listView1.Columns.Add("Telefone", 110);
            listView1.Columns.Add("Marca", 150);
            listView1.Columns.Add("Modelo", 150);
            listView1.Columns.Add("Preço Peça", 90);
            listView1.Columns.Add("Preço Serviço", 100);
            listView1.Columns.Add("Total", 90);
        }

        private void consulta_or_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            cmb_ps.SelectedIndex = 0;

            ClearListView();
            lista_or.Clear();
            lista_doc.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM orcamentos ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') {order}", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            count = 0;

            while (reader.Read())
            {
                lista_or.Add(reader.GetInt32("controle"));
                lista_doc.Add(reader.GetString("doc"));

                // Vamos adicionar uma linha na ListView com as colunas iniciais
                var item = new ListViewItem(reader.GetString("placa"));
                item.SubItems.Add(reader.GetString("cliente"));
                item.SubItems.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                item.SubItems.Add(""); // telefone - preencher depois
                item.SubItems.Add(""); // marca - preencher depois
                item.SubItems.Add(""); // modelo - preencher depois
                item.SubItems.Add(""); // preço peça - preencher depois
                item.SubItems.Add(""); // preço serviço - preencher depois
                item.SubItems.Add(reader.GetString("total").ToString());

                listView1.Items.Add(item);

                count++;
            }
            conexao.Close();

            // Agora preencher as colunas restantes (telefone, marca, modelo, preços)
            decimal total_servicos = 0;
            decimal total_pecas = 0;

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                var placa = listView1.Items[i].SubItems[0].Text;
                var doc = lista_doc[i];
                var orca = lista_or[i];

                // Telefone
                cmd = new MySqlCommand($"SELECT telefone FROM clientes WHERE doc = '{doc}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    listView1.Items[i].SubItems[3].Text = reader.GetString("telefone");

                conexao.Close();

                // Marca e modelo
                cmd = new MySqlCommand($"SELECT marca, modelo FROM motos WHERE placa = '{placa}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    listView1.Items[i].SubItems[4].Text = reader.GetString("marca");
                    listView1.Items[i].SubItems[5].Text = reader.GetString("modelo");
                }
                conexao.Close();

                // Preço serviço
                decimal soma_servico = 0;
                cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM servicos_os WHERE orca = '{orca}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        string qtd = reader.GetString("qtd").Replace(".", ",");
                        soma_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                    }
                    catch { }
                }
                conexao.Close();
                listView1.Items[i].SubItems[7].Text = soma_servico.ToString("N2");
                total_servicos += soma_servico;

                // Preço peça
                decimal soma_peca = 0;
                cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM pecas_os WHERE orca = '{orca}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        string qtd = reader.GetString("qtd").Replace(".", ",");
                        soma_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                    }
                    catch { }
                }
                conexao.Close();
                listView1.Items[i].SubItems[6].Text = soma_peca.ToString("N2");
                total_pecas += soma_peca;
            }
        }

        private void ClearListView()
        {
            listView1.Items.Clear();
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

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_or_Load(sender, e);
        }

        private void lbl_order_Click(object sender, EventArgs e)
        {
            order = (order == "DESC") ? "ASC" : "DESC";
            lbl_order.Text = (order == "DESC") ? "↑" : "↓";
            consulta_or_Load(sender, e);
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            ClearListView();
            lista_or.Clear();
            lista_doc.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            if (cmb_consulta.Text == "dt_cadastro" || cmb_consulta.Text == "placa" || cmb_consulta.Text == "cliente")
            {
                var cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> placas = new List<string>();

                while (reader.Read())
                {
                    lista_or.Add(reader.GetInt32("controle"));
                    lista_doc.Add(reader.GetString("doc"));
                    placas.Add(reader.GetString("placa"));

                    // Criar item com os dados iniciais e colunas vazias para preencher depois
                    var item = new ListViewItem(placas.Last());
                    item.SubItems.Add(reader.GetString("cliente"));
                    item.SubItems.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(""); // telefone
                    item.SubItems.Add(""); // marca
                    item.SubItems.Add(""); // modelo
                    item.SubItems.Add(""); // preço peça
                    item.SubItems.Add(""); // preço serviço
                    item.SubItems.Add(reader.GetString("total").ToString());

                    listView1.Items.Add(item);
                }
                conexao.Close();

                // Agora preenche as colunas que faltam
                for (int i = 0; i < placas.Count; i++)
                {
                    string placa = placas[i];
                    string doc = lista_doc[i];
                    int orca = lista_or[i];

                    // Telefone
                    cmd = new MySqlCommand($"SELECT telefone FROM clientes WHERE doc = '{doc}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                        listView1.Items[i].SubItems[3].Text = reader.GetString("telefone");
                    conexao.Close();

                    // Marca e modelo
                    cmd = new MySqlCommand($"SELECT marca, modelo FROM motos WHERE placa = '{placa}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        listView1.Items[i].SubItems[4].Text = reader.GetString("marca");
                        listView1.Items[i].SubItems[5].Text = reader.GetString("modelo");
                    }
                    conexao.Close();

                    // Preço serviço
                    decimal soma_servico = 0;
                    cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM servicos_os WHERE orca = '{orca}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd").Replace(".", ",");
                            soma_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                        }
                        catch { }
                    }
                    conexao.Close();
                    listView1.Items[i].SubItems[7].Text = soma_servico.ToString("N2");

                    // Preço peça
                    decimal soma_peca = 0;
                    cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM pecas_os WHERE orca = '{orca}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd").Replace(".", ",");
                            soma_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                        }
                        catch { }
                    }
                    conexao.Close();
                    listView1.Items[i].SubItems[6].Text = soma_peca.ToString("N2");
                }
            }
            else if (cmb_consulta.Text == "marca" || cmb_consulta.Text == "modelo")
            {
                List<string> placas = new List<string>();

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    placas.Add(reader.GetString("placa"));
                }
                conexao.Close();

                for (int i = 0; i < placas.Count; i++)
                {
                    string placa = placas[i];

                    cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE placa = '{placa}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista_or.Add(reader.GetInt32("controle"));
                        lista_doc.Add(reader.GetString("doc"));

                        var item = new ListViewItem(reader.GetString("placa"));
                        item.SubItems.Add(reader.GetString("cliente"));
                        item.SubItems.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                        item.SubItems.Add(""); // telefone
                        item.SubItems.Add(""); // marca
                        item.SubItems.Add(""); // modelo
                        item.SubItems.Add(""); // preço peça
                        item.SubItems.Add(""); // preço serviço
                        item.SubItems.Add(reader.GetString("total").ToString());

                        listView1.Items.Add(item);
                    }
                    conexao.Close();
                }

                // Preencher marca, modelo, telefone e preços
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    string placa = listView1.Items[i].SubItems[0].Text;
                    string doc = lista_doc[i];
                    int orca = lista_or[i];

                    // Marca e modelo
                    cmd = new MySqlCommand($"SELECT marca, modelo FROM motos WHERE doc_dono = '{doc}' AND placa = '{placa}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        listView1.Items[i].SubItems[4].Text = reader.GetString("marca");
                        listView1.Items[i].SubItems[5].Text = reader.GetString("modelo");
                    }
                    conexao.Close();

                    // Telefone
                    try
                    {
                        cmd = new MySqlCommand($"SELECT telefone FROM clientes WHERE doc = '{doc}'", conexao);
                        conexao.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            listView1.Items[i].SubItems[3].Text = reader.GetString("telefone");
                        conexao.Close();
                    }
                    catch
                    {
                        listView1.Items[i].SubItems[3].Text = " ";
                    }

                    // Preço serviço
                    decimal soma_servico = 0;
                    cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM servicos_os WHERE orca = '{orca}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd").Replace(".", ",");
                            soma_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                        }
                        catch { }
                    }
                    conexao.Close();
                    listView1.Items[i].SubItems[7].Text = soma_servico.ToString("N2");

                    // Preço peça
                    decimal soma_peca = 0;
                    cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM pecas_os WHERE orca = '{orca}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd").Replace(".", ",");
                            soma_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                        }
                        catch { }
                    }
                    conexao.Close();
                    listView1.Items[i].SubItems[6].Text = soma_peca.ToString("N2");
                }
            }
        }

        private void bnt_pesquisar_ps_Click(object sender, EventArgs e)
        {
            List<int> consulta_or = new List<int>();
            List<string> doc_dono = new List<string>();

            ClearListView();
            consulta_or.Clear();
            doc_dono.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            string tabela = "";
            if (cmb_ps.Text == "Serviços")
                tabela = "servicos_os";
            else if (cmb_ps.Text == "Peças")
                tabela = "pecas_os";

            // Busca dentro da lista_or os orçamentos que possuem o nome pesquisado na tabela selecionada
            for (int i = 0; i < lista_or.Count; i++)
            {
                var cmd = new MySqlCommand($"SELECT * FROM {tabela} WHERE orca = {lista_or[i]} AND nome LIKE '%{txt_ps.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    consulta_or.Add(reader.GetInt32("orca"));

                conexao.Close();
            }

            decimal total_servicos = 0;
            decimal total_pecas = 0;
            lista_or.Clear();

            for (int i = 0; i < consulta_or.Count; i++)
            {
                var cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE controle = {consulta_or[i]}", conexao);
                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lista_or.Add(reader.GetInt32("controle"));

                    var item = new ListViewItem(reader.GetString("placa"));
                    item.SubItems.Add(reader.GetString("cliente"));
                    item.SubItems.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    item.SubItems.Add(""); // telefone
                    item.SubItems.Add(""); // marca
                    item.SubItems.Add(""); // modelo
                    item.SubItems.Add(""); // preço peça
                    item.SubItems.Add(""); // preço serviço
                    item.SubItems.Add(reader.GetString("total").ToString());

                    listView1.Items.Add(item);
                    doc_dono.Add(reader.GetString("doc"));
                }
                conexao.Close();
            }

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string doc = doc_dono[i];
                int orca = lista_or[i];
                string placa = listView1.Items[i].SubItems[0].Text;

                var cmd = new MySqlCommand($"SELECT marca, modelo FROM motos WHERE doc_dono = '{doc}'", conexao);
                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    listView1.Items[i].SubItems[4].Text = reader.GetString("marca");
                    listView1.Items[i].SubItems[5].Text = reader.GetString("modelo");
                }
                conexao.Close();

                try
                {
                    cmd = new MySqlCommand($"SELECT telefone FROM clientes WHERE doc = '{doc}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                        listView1.Items[i].SubItems[3].Text = reader.GetString("telefone");
                    conexao.Close();
                }
                catch
                {
                    listView1.Items[i].SubItems[3].Text = " ";
                }

                cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM servicos_os WHERE orca = '{orca}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal valorServico = 0;
                while (reader.Read())
                {
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    valorServico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                }
                conexao.Close();
                listView1.Items[i].SubItems[7].Text = valorServico.ToString("N2");

                cmd = new MySqlCommand($"SELECT valor, qtd, desco FROM pecas_os WHERE orca = '{orca}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal valorPeca = 0;
                while (reader.Read())
                {
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    valorPeca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                }
                conexao.Close();
                listView1.Items[i].SubItems[6].Text = valorPeca.ToString("N2");
            }
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            cadastro_or cadastro_or = new cadastro_or();
            cadastro_or.Text = "Cadastro orçamento";
            cadastro_or.Show();
        }
    }
}
