using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
    public partial class consulta_or : Form
    {
        List<String> lista_doc = new List<String>();
        public List<int> lista_or = new List<int>();

        int count;
        string order = "DESC";
        public consulta_or()
        {
            InitializeComponent();
        }

        private void consulta_or_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;
            cmb_ps.SelectedIndex = 0;

            clear_lists();
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
                lst_placa.Items.Add(reader.GetString("placa"));
                lst_cliente.Items.Add(reader.GetString("cliente"));
                lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                lst_total.Items.Add(reader.GetString("total").ToString());
                lista_doc.Add(reader.GetString("doc"));

                count++;
            }
            conexao.Close();

            count = 0;
            decimal total_servicos = 0;
            decimal total_pecas = 0;

            while (count <= lst_placa.Items.Count - 1)
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

                cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{lista_or[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal soma_servico = 0;

                while (reader.Read())
                {
                    try
                    {
                        string qtd = reader.GetString("qtd");
                        qtd = qtd.Replace(".", ",");

                        soma_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                    }
                    catch { }
                }

                total_servicos += soma_servico;
                lst_preco_servico.Items.Add(soma_servico.ToString("N2"));

                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{lista_or[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal soma_peca = 0;

                while (reader.Read())
                {
                    try
                    {
                        string qtd = reader.GetString("qtd");
                        qtd = qtd.Replace(".", ",");

                        soma_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                    }
                    catch { }
                }

                total_pecas += soma_peca;
                lst_preco_peca.Items.Add(soma_peca.ToString("N2"));

                conexao.Close();
                count++;
            }

            //txt_total_pecas.Text = total_pecas.ToString("N2");
            //txt_total_servicos.Text = total_servicos.ToString("N2");
            //txt_total.Text = (total_pecas + total_servicos).ToString("N2");


            if (lst_dt.Items.Count > 0)
            {
                scrollbar.Maximum = lst_dt.Items.Count - 1;
            }
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

        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_or_Load(sender, e);
        }

        private void lbl_order_Click(object sender, EventArgs e)
        {
            if (order == "DESC")
            {
                order = "ASC";
                lbl_order.Text = "↓";
                consulta_or_Load(sender, e);
            }
            else
            {
                order = "DESC";
                lbl_order.Text = "↑";
                consulta_or_Load(sender, e);
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
        }

        private void lst_dt_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_dt.SelectedIndex;
        }

        private void lst_telefone_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_telefone.SelectedIndex;
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

        private void lst_preco_peca_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_preco_peca.SelectedIndex;
        }

        private void lst_preco_servico_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_preco_servico.SelectedIndex;
        }

        private void lst_total_Click(object sender, EventArgs e)
        {
            lst_cliente.SelectedIndex = lst_total.SelectedIndex;
        }

        private void lst_cliente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_dt_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_telefone_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_placa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_marca_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_modelo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_preco_peca_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_preco_servico_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void lst_total_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                cadastro_or cadastro_or = new cadastro_or();
                cadastro_or.Text = "Edição orçamento";

                static_class.controle_os = lista_or[lst_cliente.SelectedIndex];

                cadastro_or.Show();
            }
            catch { }
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            cadastro_or cadastro_or = new cadastro_or();
            cadastro_or.Text = "Cadastro orçamento";
            cadastro_or.Show();
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            clear_lists();
            lista_or.Clear();
            lista_doc.Clear();

            List<int> list_index = new List<int>();

            if (cmb_consulta.Text == "dt_cadastro" || cmb_consulta.Text == "placa" || cmb_consulta.Text == "cliente")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);


                var cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> placa = new List<string>();

                while (reader.Read())
                {
                    lista_or.Add(reader.GetInt32("controle"));
                    lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    lista_doc.Add(reader.GetString("doc"));
                    placa.Add(reader.GetString("placa"));
                    lst_placa.Items.Add(placa.Last());
                    lst_total.Items.Add(reader.GetString("total").ToString());
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

                while (count < lista_or.Count)
                {
                    try
                    {
                        cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{lista_doc[count]}'", conexao);

                        conexao.Open();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            lst_telefone.Items.Add(reader.GetString("telefone"));
                        }
                        conexao.Close();
                    }
                    catch { lst_telefone.Items.Add(" "); }

                    cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{lista_or[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    decimal soma_servico = 0;

                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd");
                            qtd = qtd.Replace(".", ",");

                            soma_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                        }
                        catch { }
                    }
                    total_servicos += soma_servico;
                    lst_preco_servico.Items.Add(soma_servico.ToString("N2"));

                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{lista_or[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    decimal soma_peca = 0;

                    while (reader.Read())
                    {
                        try
                        {
                            string qtd = reader.GetString("qtd");
                            qtd = qtd.Replace(".", ",");

                            soma_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                        }
                        catch { }
                    }
                    total_pecas += soma_peca;
                    lst_preco_peca.Items.Add(soma_peca.ToString("N2"));

                    conexao.Close();

                    count++;
                }

                //txt_total_pecas.Text = total_pecas.ToString("N2");
                //txt_total_servicos.Text = total_servicos.ToString("N2");
                //txt_total.Text = (total_pecas + total_servicos).ToString("N2");
            }
            else if (cmb_consulta.Text == "marca" || cmb_consulta.Text == "modelo")
            {
                List<string> placa = new List<string>();
                lista_or.Clear();
                lista_doc.Clear();

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    placa.Add(reader.GetString("placa"));
                }
                conexao.Close();

                int count = 0;

                while (count < placa.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE placa = '{placa[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista_or.Add(reader.GetInt32("controle"));
                        lista_doc.Add(reader.GetString("doc"));
                        lst_cliente.Items.Add(reader.GetString("cliente"));
                        lst_placa.Items.Add(reader.GetString("placa"));
                        lst_total.Items.Add(reader.GetString("total").ToString());
                        lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    }
                    conexao.Close();
                    count++;
                }

                count = 0;

                while (count < lst_placa.Items.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{lista_doc[count]}' AND placa = '{lst_placa.Items[count]}'", conexao);

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
                while (count < lista_doc.Count)
                {
                    try
                    {
                        cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{lista_doc[count]}'", conexao);

                        conexao.Open();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            lst_telefone.Items.Add(reader.GetString("telefone"));
                        }
                        conexao.Close();
                    }
                    catch (Exception w) { lst_telefone.Items.Add(" "); MessageBox.Show(w.Message); }

                    count++;
                }

                count = 0;

                while (count < lista_or.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{lista_or[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    decimal valor = 0;

                    while (reader.Read())
                    {
                        string qtd = reader.GetString("qtd");
                        qtd = qtd.Replace(".", ",");

                        valor += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                    }
                    lst_preco_servico.Items.Add(valor.ToString("N2"));
                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{lista_or[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    valor = 0;

                    while (reader.Read())
                    {
                        string qtd = reader.GetString("qtd");
                        qtd = qtd.Replace(".", ",");

                        valor += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                    }
                    lst_preco_peca.Items.Add(valor.ToString("N2"));
                    conexao.Close();

                    count++;
                }
            }
        }

        private void bnt_pesquisar_ps_Click(object sender, EventArgs e)
        {
            List<int> consulta_or = new List<int>();
            List<string> doc_dono = new List<string>();

            clear_lists();
            consulta_or.Clear();
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
            while (count < lista_or.Count)
            {
                lista = lista + lista_or[count] + "\n";
                var cmd = new MySqlCommand($"SELECT * FROM {tabela} WHERE orca = {lista_or[count]} AND nome LIKE '%{txt_ps.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                { consulta_or.Add(reader.GetInt32("os")); }
                conexao.Close();

                count++;
            }

            count = 0;
            decimal total_servicos = 0;
            decimal total_pecas = 0;
            lista_or.Clear();

            while (count < consulta_or.Count)
            {
                var cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE controle = {consulta_or[count]}", conexao);
                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lista_or.Add(reader.GetInt32("controle"));
                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    lst_placa.Items.Add(reader.GetString("placa"));
                    lst_dt.Items.Add(DateTime.Parse(reader.GetString("dt_cadastro")).ToString("dd/MM/yyyy"));
                    lst_total.Items.Add(reader.GetString("total").ToString());
                    doc_dono.Add(reader.GetString("doc"));
                }
                conexao.Close();

                count++;
            }

            count = 0;

            while (count < lst_placa.Items.Count)
            {
                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{doc_dono[count]}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_marca.Items.Add(reader.GetString("marca"));
                    lst_modelo.Items.Add(reader.GetString("modelo"));
                }
                conexao.Close();

                count++;
            }

            count = 0;

            while (count < consulta_or.Count)
            {
                try
                {
                    var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{doc_dono[count]}'", conexao);

                    conexao.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lst_telefone.Items.Add(reader.GetString("telefone"));
                    }
                    conexao.Close();
                }
                catch { lst_telefone.Items.Add(" "); }

                count++;
            }

            count = 0;

            while (count < consulta_or.Count)
            {
                var cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{lista_or[count]}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                decimal valor = 0;

                while (reader.Read())
                {
                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    valor += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                }
                lst_preco_servico.Items.Add(valor.ToString("N2"));
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{lista_or[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                valor = 0;

                while (reader.Read())
                {
                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    valor += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                }
                lst_preco_peca.Items.Add(valor.ToString("N2"));
                conexao.Close();

                count++;
            }

            //txt_total_pecas.Text = total_pecas.ToString("N2");
            //txt_total_servicos.Text = total_servicos.ToString("N2");
            //txt_total.Text = (total_pecas + total_servicos).ToString("N2");

            //nome_vermelho();
        }
    }
}
