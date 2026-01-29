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

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public cadastro_pagamento()
        {
            InitializeComponent();
        }

        private void cadastro_pagamento_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Clear(); // Garante que não duplique colunas se o form reabrir
            listView1.Columns.Add("Metodo", 150);
            listView1.Columns.Add("Banco/Agência", 150);
            listView1.Columns.Add("Parcelas", 80);

            System.Data.IDbConnection conexao;

            // 1. TENTATIVA HÍBRIDA (MySQL -> SQLite)
            try
            {
                var mysql = new MySqlConnection(strConexao);
                mysql.Open();
                conexao = mysql;
            }
            catch
            {
                var sqlite = new System.Data.SQLite.SQLiteConnection(strLocal);
                sqlite.Open();
                conexao = sqlite;
            }

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT * FROM metodo_pag";

                    using (var reader = cmd.ExecuteReader())
                    {
                        listView1.Items.Clear();
                        controle.Clear(); // Limpa a lista de IDs estática

                        while (reader.Read())
                        {
                            controle.Add(Convert.ToInt32(reader["controle"]));

                            string metodoNome = reader["metodo"].ToString();

                            // Tratamento para a coluna que pode mudar de nome entre bancos
                            string agenciaOuBanco = "";
                            try { agenciaOuBanco = reader["agencia"].ToString(); }
                            catch { agenciaOuBanco = reader["banco"].ToString(); }

                            string parcelas = reader["parcelas"].ToString();

                            var item = new ListViewItem(metodoNome);
                            item.SubItems.Add(agenciaOuBanco);
                            item.SubItems.Add(parcelas);
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar métodos: " + ex.Message);
            }

            // 2. LÓGICA DE INDEXAÇÃO
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

        private void bnt_excluir_Click(object sender, EventArgs e)
        {
            // Confirmação de segurança
            DialogResult confirmacao = MessageBox.Show($"Deseja realmente excluir o metodo de pagamento?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes) ExecutarDeleteCliente();
            
        }

        private void ExecutarDeleteCliente(bool usarLocal = false)
        {
            // Define se usará MySQL ou SQLite
            System.Data.Common.DbConnection conexao;
            if (usarLocal)
                conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else
                conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Comando SQL parametrizado
                    cmd.CommandText = "DELETE FROM metodo_pag WHERE metodo = @metodo";

                    var pNome = cmd.CreateParameter();
                    pNome.ParameterName = "@metodo";
                    pNome.Value = txt_metodo.Text;
                    cmd.Parameters.Add(pNome);

                    cmd.ExecuteNonQuery();
                }
            }
            catch 
            {
                // Se a rede falhar no MySQL, tenta deletar no banco local
                if (!usarLocal)
                    ExecutarDeleteCliente(true);
                else
                    MessageBox.Show("Erro ao tentar excluir o registro localmente.");
            }
        }
    }
}
