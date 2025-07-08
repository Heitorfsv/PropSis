using MySql.Data.MySqlClient;
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
    public partial class consulta_motos : Form
    {
        List<string> doc_dono = new List<string>();
        public consulta_motos()
        {
            InitializeComponent();
        }

        private void consulta_motos_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            lst_placa.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();
            lst_cor.Items.Clear();
            lst_ano.Items.Clear();
            lst_nome.Items.Clear();
            doc_dono.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM motos", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_placa.Items.Add(reader.GetString("placa"));
                lst_marca.Items.Add(reader.GetString("marca"));
                lst_modelo.Items.Add(reader.GetString("modelo"));
                lst_cor.Items.Add(reader.GetString("cor"));
                lst_ano.Items.Add(reader.GetString("ano"));
                doc_dono.Add(reader.GetString("doc_dono"));
            }
            conexao.Close();

            int count = 0;
            while (count < doc_dono.Count)
            {
                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{doc_dono[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                { lst_nome.Items.Add(reader.GetString("nome")); }
                count++;

                conexao.Close();
            }

            try
            { lst_placa.SelectedIndex = 0; }
            catch { }
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_motos_Load(sender, e);
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            lst_placa.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();
            lst_cor.Items.Clear();
            lst_ano.Items.Clear();
            lst_nome.Items.Clear();
            doc_dono.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_placa.Items.Add(reader.GetString("placa"));
                lst_marca.Items.Add(reader.GetString("marca"));
                lst_modelo.Items.Add(reader.GetString("modelo"));
                lst_cor.Items.Add(reader.GetString("cor"));
                lst_ano.Items.Add(reader.GetString("ano"));
                doc_dono.Add(reader.GetString("doc_dono"));
            }
            conexao.Close();

            int count = 0;
            while (count < doc_dono.Count)
            {
                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{doc_dono[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                { lst_nome.Items.Add(reader.GetString("nome")); }
                count++;

                conexao.Close();
            }
        }

        private void lst_placa_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_marca.SelectedIndex = lst_placa.SelectedIndex;
            lst_modelo.SelectedIndex = lst_placa.SelectedIndex;
            lst_cor.SelectedIndex = lst_placa.SelectedIndex;
            lst_ano.SelectedIndex = lst_placa.SelectedIndex;
            lst_nome.SelectedIndex = lst_placa.SelectedIndex;
        }

        private void lst_marca_Click(object sender, EventArgs e)
        {
            lst_placa.SelectedIndex = lst_marca.SelectedIndex;
        }

        private void lst_modelo_Click(object sender, EventArgs e)
        {
            lst_placa.SelectedIndex = lst_modelo.SelectedIndex;
        }

        private void lst_cor_Click(object sender, EventArgs e)
        {
            lst_placa.SelectedIndex = lst_cor.SelectedIndex;
        }

        private void lst_ano_Click(object sender, EventArgs e)
        {
            lst_placa.SelectedIndex = lst_ano.SelectedIndex;
        }

        private void bnt_pesquisar_nome_Click(object sender, EventArgs e)
        {
            lst_placa.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();
            lst_cor.Items.Clear();
            lst_ano.Items.Clear();
            lst_nome.Items.Clear();
            doc_dono.Clear();

            List<string> doc_list = new List<string>();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome LIKE '%{txt_pesquisar_nome.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                doc_list.Add(reader.GetString("doc"));
            }
            conexao.Close();

            int count = 0;

            while (count < doc_list.Count)
            {
                cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono LIKE '%{doc_list[count]}%'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_placa.Items.Add(reader.GetString("placa"));
                    lst_marca.Items.Add(reader.GetString("marca"));
                    lst_modelo.Items.Add(reader.GetString("modelo"));
                    lst_cor.Items.Add(reader.GetString("cor"));
                    lst_ano.Items.Add(reader.GetString("ano"));
                    doc_dono.Add(reader.GetString("doc_dono"));
                }
                count++;
                conexao.Close();
            }

            count = 0;
            while (count < doc_dono.Count)
            {
                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{doc_dono[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                { lst_nome.Items.Add(reader.GetString("nome")); }
                count++;

                conexao.Close();
            }
        }

        private void lst_nome_Click(object sender, EventArgs e)
        {
            lst_placa.SelectedIndex = lst_nome.SelectedIndex;
        }

        private void lst_nome_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_motos edicao_motos = new edicao_motos();
                edicao_motos.Text = "Edição motos";

                static_class.doc_consultar = lst_placa.SelectedItem.ToString();
                static_class.doc_dono = doc_dono[lst_placa.SelectedIndex];
                edicao_motos.Show();
            }
            catch { }
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_motos cadastro = new edicao_motos();
            cadastro.Text = "Cadastro motos";
            cadastro.Show();
        }
    }
}
