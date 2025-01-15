using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class consulta_os : Form
    {
        List<bool> pagoStatus = new List<bool>();
        OS os = new OS();
        List<String> lista_doc = new List<String>();
        public List<int> lista_os = new List<int>();
        int count;
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

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM os", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            count = 0;

            while (reader.Read())
            {
                lista_os.Add(reader.GetInt32("controle"));
                lst_placa.Items.Add(reader.GetString("placa"));
                lst_cliente.Items.Add(reader.GetString("cliente"));
                lst_dt.Items.Add(reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy"));
                lst_total.Items.Add(reader.GetDecimal("total").ToString()); 
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

            while (count <= lst_placa.Items.Count -1)
            {
                lst_placa.SelectedIndex = count;

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
                    { soma_servico += reader.GetDecimal("valor") * reader.GetDecimal("qtd"); }
                    catch { }
                }
                lst_preco_servico.Items.Add(soma_servico.ToString("N2"));
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{lista_os[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader(); 
                decimal soma_peca = 0;

                while (reader.Read())
                {
                    try
                    { soma_peca += reader.GetDecimal("valor") * reader.GetDecimal("qtd"); }
                    catch { }
                }
                lst_preco_peca.Items.Add(soma_peca.ToString("N2"));
                conexao.Close();
                count++;
            }

            lst_cliente.DrawMode = DrawMode.OwnerDrawFixed;
            lst_cliente.DrawItem += new DrawItemEventHandler(lst_DrawItem);

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

        private void lst_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_marca.SelectedIndex = lst_cliente.SelectedIndex;
            lst_modelo.SelectedIndex = lst_cliente.SelectedIndex;
            lst_placa.SelectedIndex = lst_cliente.SelectedIndex;
            lst_preco_peca.SelectedIndex = lst_cliente.SelectedIndex;
            lst_preco_servico.SelectedIndex = lst_cliente.SelectedIndex;
            lst_total.SelectedIndex = lst_cliente.SelectedIndex;
            lst_dt.SelectedIndex = lst_cliente.SelectedIndex;   
            lst_dt_saida.SelectedIndex = lst_cliente.SelectedIndex;  

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

            List<int> list_index = new List<int>();

            if (cmb_consulta.Text == "dt_cadastro" || cmb_consulta.Text == "placa" || cmb_consulta.Text == "cliente")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao); 


                var cmd = new MySqlCommand($"SELECT * FROM os WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> placa = new List<string>();
                List<string> doc = new List<string>();

                while (reader.Read())
                {
                    lista_os.Add(reader.GetInt32("controle"));
                    lst_dt.Items.Add(reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy"));
                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    placa.Add(reader.GetString("placa"));
                    lst_placa.Items.Add(placa.Last());
                    lst_total.Items.Add(reader.GetDecimal("total").ToString());
                    lst_dt_saida.Items.Add(reader.GetString("dt_saida"));
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
                        doc.Add(reader.GetString("doc_dono"));
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
            else if (cmb_consulta.Text == "marca" || cmb_consulta.Text == "modelo")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE {cmb_consulta.Text} LIKE '%{txt_pequisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                List<string> placa = new List<string>();
                List<string> doc = new List<string>();

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
                        doc.Add(reader.GetString("doc"));
                    }
                    conexao.Close();

                    count++;
                }

                count = 0;

                while (count < doc.Count)
                {
                    cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{doc[count]}'", conexao);

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
                    lst_preco_servico.Items.Add(valor);
                    conexao.Close();

                    cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{lista_os[count]}'", conexao);

                    conexao.Open();
                    reader = cmd.ExecuteReader();

                    valor = 0;

                    while (reader.Read())
                    {
                        valor += reader.GetDecimal("valor") * reader.GetDecimal("qtd");
                    }
                    lst_preco_peca.Items.Add(valor);

                    conexao.Close();

                    count++;
                }
            }   
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

            while (count < consulta_os.Count)
            {
                var cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = {consulta_os[count]}", conexao);
                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_cliente.Items.Add(reader.GetString("cliente"));
                    lst_placa.Items.Add(reader.GetString("placa"));
                    lst_dt.Items.Add(reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy"));
                    lst_dt_saida.Items.Add(reader.GetString("dt_saida"));   
                    lst_total.Items.Add(reader.GetDecimal("total").ToString());
                    doc_dono.Add(reader.GetString("doc"));
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
                    soma_servico += reader.GetDecimal("valor") * reader.GetDecimal("qtd");
                }
                lst_preco_servico.Items.Add(soma_servico);
                conexao.Close();

                decimal soma_peca = 0;

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{consulta_os[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    soma_peca += reader.GetDecimal("valor") * reader.GetDecimal("qtd");
                }
                lst_preco_peca.Items.Add(soma_peca);
                conexao.Close();
                count++;
            }
        }


        public void clear_lists()
        {
            lst_cliente.Items.Clear();
            lst_placa.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();
            lst_preco_peca.Items.Clear();
            lst_preco_servico.Items.Clear();
            lst_total.Items.Clear();
            lst_dt.Items.Clear();
            lst_dt_saida.Items.Clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lst_preco_peca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lst_preco_servico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
