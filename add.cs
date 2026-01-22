using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class add : Form
    {
        public string modo, table;

        servicos_os servicos_os = new servicos_os();
        pecas_os pecas_os = new pecas_os();

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public add()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Serviço", 170);
            listView1.Columns.Add("Qtd", 40);
            listView1.Columns.Add("Valor", 60);
            listView1.Columns.Add("Desc.", 60);
            listView1.Columns.Add("Total", 60);
        }

        private void add_servicos_Load(object sender, EventArgs e)
        {
            // Lógica das Listas temporárias (Padrão original)
            var lista = (table == "pecas") ? itens_pecas : itens_servicos;

            foreach (ListViewItem item in lista)
            {
                item.BackColor = listView1.BackColor;
                listView1.Items.Add((ListViewItem)item.Clone());
            }

            itens_pecas.Clear();
            itens_servicos.Clear();

            // Título da Janela
            this.Text = (table == "servicos") ? "Adicionar serviços" : "Adicionar peças";

            // Carregar dados (Híbrido)
            CarregarLista(txt_pesquisa.Text.Trim());

            AtualizarTotal();
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            // Carregar dados enquanto digita (Híbrido)
            CarregarLista(txt_pesquisa.Text.Trim());
        }

        private void CarregarLista(string filtro, bool usarLocal = false)
        {
            lst_pesquisa.Items.Clear();

            // Define a conexão (Mesmo padrão das outras classes)
            System.Data.Common.DbConnection conexao;
            if (usarLocal)
                conexao = new SQLiteConnection(strLocal);
            else
                conexao = new MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Monta a query com parâmetro para evitar SQL Injection no filtro
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        cmd.CommandText = $"SELECT nome FROM {table} WHERE nome LIKE @filtro";
                        var p = cmd.CreateParameter();
                        p.ParameterName = "@filtro";
                        p.Value = "%" + filtro + "%";
                        cmd.Parameters.Add(p);
                    }
                    else
                    {
                        cmd.CommandText = $"SELECT nome FROM {table}";
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lst_pesquisa.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Se falhou no MySQL, tenta no banco local
                if (!usarLocal)
                {
                    CarregarLista(filtro, true);
                }
            }
        }

        public List<ListViewItem> itens_pecas { get; private set; } = new List<ListViewItem>();
        public List<ListViewItem> itens_servicos { get; private set; } = new List<ListViewItem>();

        private void lst_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                object nome = lst_pesquisa.SelectedItem;
                if (nome == null) return; // Proteção caso clique no vazio

                string valor = "", desc = "";

                // --- INÍCIO DA BUSCA HÍBRIDA ---
                void BuscarPreco(bool usarLocal = false)
                {
                    System.Data.Common.DbConnection conexao;
                    if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
                    else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

                    try
                    {
                        using (conexao)
                        {
                            conexao.Open();
                            var cmd = conexao.CreateCommand();
                            // Uso de parâmetros para evitar erro com aspas (ex: Pneu 18")
                            cmd.CommandText = $"SELECT * FROM {table} WHERE nome = @nomeItem";

                            var p = cmd.CreateParameter();
                            p.ParameterName = "@nomeItem";
                            p.Value = nome.ToString();
                            cmd.Parameters.Add(p);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (table == "servicos") valor = reader["valor"].ToString();
                                    else valor = reader["valor_sugerido"].ToString();
                                }
                            }
                        }
                    }
                    catch { if (!usarLocal) BuscarPreco(true); }
                }

                BuscarPreco(); // Executa a busca (Online ou Offline)
                               // --- FIM DA BUSCA HÍBRIDA ---

                qtd qtd_tela = new qtd();
                qtd_tela.valor = valor; // Sugere o valor recuperado do banco
                qtd_tela.ShowDialog();

                if (qtd_tela.quantidade > 0)
                {
                    decimal qtd = qtd_tela.quantidade;
                    valor = qtd_tela.valor; // Pode ter sido alterado na tela 'qtd'
                    desc = qtd_tela.desc;

                    string total;
                    decimal totalItem;

                    // Mantendo sua lógica: try -> número | catch -> letras
                    try
                    {
                        totalItem = (decimal.Parse(valor) * qtd) - decimal.Parse(desc);
                        total = totalItem.ToString("N2");
                    }
                    catch { total = valor; }

                    var item = new ListViewItem(nome.ToString());
                    item.SubItems.Add(qtd.ToString());

                    try { item.SubItems.Add(decimal.Parse(valor).ToString("N2")); }
                    catch { item.SubItems.Add(valor); }

                    item.SubItems.Add(desc);
                    item.SubItems.Add(total);
                    listView1.Items.Add(item);

                    AtualizarTotal();
                }
            }
            catch (Exception a) { MessageBox.Show(a.ToString()); }
        }

        private void add_servicos_FormClosing(object sender, FormClosingEventArgs e)
        {
            //copia os itens da lista para adicionar no formulario edicao_os
            foreach (ListViewItem items in listView1.Items)
            {
                if (table == "pecas") itens_pecas.Add((ListViewItem)items);
                else if (table == "servicos") itens_servicos.Add((ListViewItem)items);
            }
        }

        private void bnt_delete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var servicoNome = listView1.SelectedItems[0].Text;

                // Função local para executar o delete onde houver conexão
                void ExecutarDelete(bool usarLocal = false)
                {
                    System.Data.Common.DbConnection conexao;
                    if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
                    else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

                    try
                    {
                        using (conexao)
                        {
                            conexao.Open();
                            var cmd = conexao.CreateCommand();

                            // Usamos parâmetros para o nome e para o ID de controle
                            // O nome da tabela {table}_os e a coluna {modo} permanecem dinâmicos
                            cmd.CommandText = $@"DELETE FROM {table}_os WHERE nome = @nome AND {modo} = @controle";

                            var pNome = cmd.CreateParameter();
                            pNome.ParameterName = "@nome";
                            pNome.Value = servicoNome;
                            cmd.Parameters.Add(pNome);

                            var pControle = cmd.CreateParameter();
                            pControle.ParameterName = "@controle";
                            pControle.Value = static_class.controle;
                            cmd.Parameters.Add(pControle);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        // Se falhar no MySQL, tenta deletar no banco local (SQLite)
                        if (!usarLocal) ExecutarDelete(true);
                    }
                }

                // Tenta deletar no banco de dados
                ExecutarDelete();

                // Remove da interface visual e atualiza o total da tela
                listView1.Items.Remove(listView1.SelectedItems[0]);
                AtualizarTotal();
            }
        }
        private void AtualizarTotal()
        {
            decimal total = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                try { total += decimal.Parse(item.SubItems[4].Text); } catch { }
            }
            txt_total.Text = total.ToString("N2");
        }


        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            int currentIndex = listView1.SelectedItems[0].Index;
            var currentItem = (ListViewItem)listView1.SelectedItems[0].Clone();

            if (e.KeyCode == Keys.Up && currentIndex > 0)
            {
                listView1.Items.RemoveAt(currentIndex);
                listView1.Items.Insert(currentIndex - 1, currentItem);
                listView1.Items[currentIndex - 1].Selected = true;
            }
            else if (e.KeyCode == Keys.Down && currentIndex < listView1.Items.Count - 1)
            {
                listView1.Items.RemoveAt(currentIndex);
                listView1.Items.Insert(currentIndex + 1, currentItem);
                listView1.Items[currentIndex + 1].Selected = true;
            }
        }
    }
}
