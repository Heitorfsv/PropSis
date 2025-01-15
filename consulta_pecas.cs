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
    public partial class consulta_pecas : Form
    {
        public consulta_pecas()
        {
            InitializeComponent();
        }

        private void lst_status_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lst_nome_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_marca.SelectedIndex = lst_nome.SelectedIndex;
            lst_modelo.SelectedIndex = lst_nome.SelectedIndex;
            lst_fornecedor.SelectedIndex = lst_nome.SelectedIndex;
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            lst_nome.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();   
            lst_fornecedor.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_nome.Items.Add(reader.GetString("nome"));
                lst_marca.Items.Add(reader.GetString("marca"));
                lst_modelo.Items.Add(reader.GetString("modelo"));
                lst_fornecedor.Items.Add(reader.GetString("fornecedor"));                                  
            }

            conexao.Close();
        }

        private void consulta_pecas_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            lst_nome.Items.Clear();
            lst_marca.Items.Clear();
            lst_modelo.Items.Clear();
            lst_fornecedor.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM pecas", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_nome.Items.Add(reader.GetString("nome"));
                lst_marca.Items.Add(reader.GetString("marca"));
                lst_modelo.Items.Add(reader.GetString("modelo"));
                lst_fornecedor.Items.Add(reader.GetString("fornecedor"));
            }

            conexao.Close();

            lst_nome.SelectedIndex = 0;
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_pecas_Load(sender ,e);
        }

        private void lst_nome_DoubleClick(object sender, EventArgs e)
        {
            edicao_pecas tela_peca = new edicao_pecas();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_peca.Show();
        }

        private void lst_status_DoubleClick(object sender, EventArgs e)
        {
            edicao_pecas tela_peca = new edicao_pecas();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_peca.Show();
        }

        private void lst_modelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome.SelectedIndex = lst_modelo.SelectedIndex;
        }

        private void lst_fornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome.SelectedIndex = lst_fornecedor.SelectedIndex;
        }

        private void lst_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome.SelectedIndex = lst_marca.SelectedIndex;
        }

        private void lst_marca_DoubleClick(object sender, EventArgs e)
        {
            edicao_pecas tela_peca = new edicao_pecas();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_peca.Show();
        }

        private void lst_modelo_DoubleClick(object sender, EventArgs e)
        {
            edicao_pecas tela_peca = new edicao_pecas();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_peca.Show();
        }

        private void lst_fornecedor_DoubleClick(object sender, EventArgs e)
        {
            edicao_pecas tela_peca = new edicao_pecas();

            static_class.doc_consultar = lst_nome.SelectedItem.ToString();
            tela_peca.Show();
        }
    }
}
