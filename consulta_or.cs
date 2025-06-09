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

                        soma_servico += (reader.GetDecimal("valor") * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
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

                        soma_peca += (reader.GetDecimal("valor") * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
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
    }
}
