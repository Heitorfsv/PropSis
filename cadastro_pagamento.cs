using classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class cadastro_pagamento : Form
    {
        metodo_pag metodo = new metodo_pag();
        List<int> controle = new List<int>();
        public cadastro_pagamento()
        {
            InitializeComponent();
        }

        private void cadastro_pagamento_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Metodo", 100);
            listView1.Columns.Add("Agência", 100);
            listView1.Columns.Add("Parcelas", 100);

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM metodo_pag", conexao);
            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                controle.Add(reader.GetInt32("controle"));
                string metodo = reader.GetString("metodo");
                string agencia = reader.GetString("banco");
                int parcelas = reader.GetInt32("parcelas");

                var item = new ListViewItem(metodo);
                item.SubItems.Add(agencia);
                item.SubItems.Add(parcelas.ToString());
                listView1.Items.Add(item);
            }
            conexao.Close();

            if (this.Text == "Cadastro metodo de pagamento")
            {
                metodo.ultimo_index();
                metodo.index++;
                static_class.controle = metodo.index;
            }
        }

        private void bnt_cadastro_Click(object sender, EventArgs e)
        {
            metodo.metodo = txt_metodo.Text;
            metodo.agencia = txt_agencia.Text;
            metodo.parcelas = Convert.ToInt32(txt_parcelas.Text);

            if (this.Text == "Cadastro metodo de pagamento")
            {
                metodo.cadastrar_metodo();
            }
            else if (this.Text == "Edição metodo de pagamento")
            {
                metodo.alterar_metodo();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;

            int index = listView1.SelectedIndices[0];
            metodo.index = controle[index];

            this.Text = "Edição metodo de pagamento";
            bnt_cadastro.Text = "Salvar";

            ListViewItem itemSelecionado = listView1.Items[index];

            txt_metodo.Text = itemSelecionado.SubItems[0].Text;
            txt_agencia.Text = itemSelecionado.SubItems[1].Text;
            txt_parcelas.Text = itemSelecionado.SubItems[2].Text;
        }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            txt_metodo.Text = "";
            txt_agencia.Text = "";
            txt_parcelas.Text = "";

            this.Text = "Cadastro metodo de pagamento";
            bnt_cadastro.Text = "Cadastrar";
        }
    }
}
