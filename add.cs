using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class add : Form
    {
        public string modo;
        public string table;

        servicos_os servicos_os = new servicos_os();
        pecas_os pecas_os = new pecas_os();

        public add()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Serviço", 150);
            listView1.Columns.Add("Qtd", 50);
            listView1.Columns.Add("Valor", 80);
            listView1.Columns.Add("Total", 80);
        }

        private void add_servicos_Load(object sender, EventArgs e)
        {
            if (table == "servicos") this.Text = "Adicionar serviços";
            else this.Text = "Adicionar peças";

            decimal total = 0;
            lst_pesquisa.Items.Clear();

            using (var conexao = new MySqlConnection("server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport"))
            {
                // Carregar lista de serviços disponíveis
                var cmd = new MySqlCommand($"SELECT * FROM {table}", conexao);
                conexao.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lst_pesquisa.Items.Add(reader.GetString("nome"));
                }
                conexao.Close();

                // Carregar serviços da OS/Orçamento
                cmd = new MySqlCommand($"SELECT * FROM {table}_os WHERE {modo} = '{static_class.controle}' ORDER BY pos ASC", conexao);
                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtdStr = reader.GetString("qtd").Replace(".", ",");
                    string valorStr = reader.GetString("valor");
                    decimal qtd = decimal.Parse(qtdStr);
                    decimal valor = decimal.Parse(valorStr);
                    decimal totalItem = valor * qtd;

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd.ToString());
                    item.SubItems.Add(valor.ToString("N2"));
                    item.SubItems.Add(totalItem.ToString("N2"));
                    listView1.Items.Add(item);
                }
                conexao.Close();
            }

            AtualizarTotal();
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            lst_pesquisa.Items.Clear();
            string filtro = txt_pesquisa.Text.Trim();

            using (var conexao = new MySqlConnection("server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport"))
            {
                MySqlCommand cmd;
                if (!string.IsNullOrEmpty(filtro))
                    cmd = new MySqlCommand($"SELECT * FROM {table} WHERE nome LIKE '%{filtro}%'", conexao);
                else
                    cmd = new MySqlCommand($"SELECT * FROM {table}", conexao);

                conexao.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lst_pesquisa.Items.Add(reader.GetString("nome"));
                }
                conexao.Close();
            }
        }

        private void lst_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                object servico = lst_pesquisa.SelectedItem;
                string valor = "";
                string desc = "";

                using (var conexao = new MySqlConnection("server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport"))
                {
                    var cmd = new MySqlCommand($"SELECT * FROM {table} WHERE nome = '{servico}'", conexao);
                    conexao.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (table == "servicos") valor = reader.GetString("valor");
                        else valor = reader.GetString("valor_sugerido");
                    }
                    conexao.Close();
                }

                qtd qtd_tela = new qtd();
                qtd_tela.valor = valor;
                qtd_tela.ShowDialog();

                if (qtd_tela.quantidade > 0)
                {
                    decimal qtd = qtd_tela.quantidade;
                    valor = qtd_tela.valor;
                    desc = qtd_tela.desc;

                    decimal totalItem = (decimal.Parse(valor) * qtd) - decimal.Parse(desc);

                    var item = new ListViewItem(servico.ToString());
                    item.SubItems.Add(qtd.ToString());
                    item.SubItems.Add(valor);
                    item.SubItems.Add(totalItem.ToString("N2"));
                    listView1.Items.Add(item);

                    AtualizarTotal();

                    if (table == "servicos")
                    {
                        servicos_os.ultimo_index();
                        servicos_os.index++;
                        servicos_os.modo = modo;

                        //os serve tanto pra orçamento quando pra ordem de serviço nesse contexto
                        servicos_os.os_or = static_class.controle;

                        servicos_os.nome = servico.ToString();
                        servicos_os.valor = valor;
                        servicos_os.qtd = qtd;
                        servicos_os.desc = desc;
                        servicos_os.pos = listView1.Items.Count - 1;
                        servicos_os.cadastrar_servico_os();
                    }
                    else if (table == "pecas")
                    {
                        pecas_os.ultimo_index();
                        pecas_os.index++;
                        pecas_os.modo = modo;

                        //os serve tanto pra orçamento quando pra ordem de serviço nesse contexto
                        pecas_os.os_or = static_class.controle;

                        pecas_os.nome = servico.ToString();
                        pecas_os.valor = valor;
                        pecas_os.qtd = qtd;
                        pecas_os.desc = desc;
                        pecas_os.pos = listView1.Items.Count - 1;
                        pecas_os.cadastrar_peca_os();
                    }
                }
            }
            catch (Exception a) { MessageBox.Show(a.ToString()); }
        }

        private void add_servicos_FormClosing(object sender, FormClosingEventArgs e)
        {
            static_class.close = 1;
            try
            {
                using (var conexao = new MySqlConnection("server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport"))
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        var cmd = new MySqlCommand(
                            $"UPDATE {table}_os SET pos = '{i}' WHERE {modo} = {static_class.controle} AND nome = '{listView1.Items[i].Text}'",
                            conexao);
                        conexao.Open();
                        cmd.ExecuteNonQuery();
                        conexao.Close();
                    }
                }
            }
            catch { }
        }

        private void bnt_delete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var servicoNome = listView1.SelectedItems[0].Text;

                using (var conexao = new MySqlConnection("server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport"))
                {
                    var cmd = new MySqlCommand(
                        $"DELETE FROM {table}_os WHERE nome = '{servicoNome}' AND {modo} = {static_class.controle}",
                        conexao);
                    conexao.Open();
                    cmd.ExecuteNonQuery();
                    conexao.Close();
                }

                listView1.Items.Remove(listView1.SelectedItems[0]);
                AtualizarTotal();
            }
        }
        private void AtualizarTotal()
        {
            decimal total = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                total += decimal.Parse(item.SubItems[3].Text);
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
