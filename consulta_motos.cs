using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class consulta_motos : Form
    {
        public consulta_motos()
        {
            InitializeComponent();
        }

        private void consulta_motos_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            listView1.Items.Clear();
            listView1.Columns.Clear();

            // Colunas
            listView1.Columns.Add("Placa", 100);
            listView1.Columns.Add("Marca", 120);
            listView1.Columns.Add("Modelo", 130);
            listView1.Columns.Add("Cor", 100);
            listView1.Columns.Add("Ano", 60);
            listView1.Columns.Add("Proprietário", 300);

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM motos", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<(string placa, string marca, string modelo, string cor, string ano, string doc_dono)> motos = new List <(string placa, string marca, string modelo, string cor, string ano, string doc_dono)>();

            while (reader.Read())
            {
                motos.Add((
                    reader.GetString("placa"),
                    reader.GetString("marca"),
                    reader.GetString("modelo"),
                    reader.GetString("cor"),
                    reader.GetString("ano"),
                    reader.GetString("doc_dono")
                ));
            }
            conexao.Close();

            foreach (var moto in motos)
            {
                string nome = "";
                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{moto.doc_dono}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read()) nome = reader.GetString("nome");
                conexao.Close();

                var item = new ListViewItem(moto.placa);
                item.SubItems.Add(moto.marca);
                item.SubItems.Add(moto.modelo);
                item.SubItems.Add(moto.cor);
                item.SubItems.Add(moto.ano);
                item.SubItems.Add(nome);
                item.Tag = moto.doc_dono;

                listView1.Items.Add(item);
            }

            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<(string placa, string marca, string modelo, string cor, string ano, string doc_dono)> motos = new List<(string placa, string marca, string modelo, string cor, string ano, string doc_dono)> ();

            while (reader.Read())
            {
                motos.Add((
                    reader.GetString("placa"),
                    reader.GetString("marca"),
                    reader.GetString("modelo"),
                    reader.GetString("cor"),
                    reader.GetString("ano"),
                    reader.GetString("doc_dono")
                ));
            }
            conexao.Close();

            foreach (var moto in motos)
            {
                string nome = "";
                cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{moto.doc_dono}'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read()) nome = reader.GetString("nome");
                conexao.Close();

                var item = new ListViewItem(moto.placa);
                item.SubItems.Add(moto.marca);
                item.SubItems.Add(moto.modelo);
                item.SubItems.Add(moto.cor);
                item.SubItems.Add(moto.ano);
                item.SubItems.Add(nome);
                item.Tag = moto.doc_dono;

                listView1.Items.Add(item);
            }
        }

        private void bnt_pesquisar_nome_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

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

            foreach (var doc in doc_list)
            {
                cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono LIKE '%{doc}%' AND {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();

                List <(string placa, string marca, string modelo, string cor, string ano)> motos = new List <(string placa, string marca, string modelo, string cor, string ano)> ();

                while (reader.Read())
                {
                    motos.Add((
                        reader.GetString("placa"),
                        reader.GetString("marca"),
                        reader.GetString("modelo"),
                        reader.GetString("cor"),
                        reader.GetString("ano")
                    ));
                }
                conexao.Close();

                foreach (var moto in motos)
                {
                    string nome = "";
                    cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc = '{doc}'", conexao);
                    conexao.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read()) nome = reader.GetString("nome");
                    conexao.Close();

                    var item = new ListViewItem(moto.placa);
                    item.SubItems.Add(moto.marca);
                    item.SubItems.Add(moto.modelo);
                    item.SubItems.Add(moto.cor);
                    item.SubItems.Add(moto.ano);
                    item.SubItems.Add(nome);
                    item.Tag = doc;

                    listView1.Items.Add(item);
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];

                edicao_motos edicao = new edicao_motos();
                edicao.Text = "Edição motos";

                static_class.doc_consultar = item.Text; // placa
                static_class.doc_dono = item.Tag.ToString(); // doc_dono
                edicao.Show();
            }   
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_motos cadastro = new edicao_motos();
            cadastro.Text = "Cadastro motos";
            cadastro.Show();
        }

    }
}
