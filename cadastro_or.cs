using classes;
using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class cadastro_or : Form
    {
        orcamento orcamento = new orcamento();
        string doc_cliente;

        //Variaveis para o PDF
        string rua = "";
        string bairro = "";
        string cidade = "";
        string cep = "";
        string cor = "";

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public cadastro_or()
        {
            InitializeComponent();
        }

        private void cadastro_or_Load(object sender, EventArgs e)
        {
            lst_servicos.View = View.Details;
            lst_servicos.Columns.Add("Nome", 230);
            lst_servicos.Columns.Add("Qtd", 50);
            lst_servicos.Columns.Add("Valor", 50);
            lst_servicos.Columns.Add("Desc.", 50);
            lst_servicos.Columns.Add("Total", 80);

            lst_pecas.View = View.Details;
            lst_pecas.Columns.Add("Nome", 230);
            lst_pecas.Columns.Add("Qtd", 50);
            lst_pecas.Columns.Add("Valor", 50);
            lst_pecas.Columns.Add("Desc.", 50);
            lst_pecas.Columns.Add("Total", 80);

            if (this.Text == "Cadastro orçamento")
            {
                bnt_editar.Text = "Cadastrar";
                bnt_deletar.Visible = false;
                dtp_cadastro.Value = DateTime.Now;

                orcamento.ultimo_index();
                orcamento.index++;
                static_class.controle = orcamento.index;
            }
            else if (this.Text == "Edição orçamento")
            {
                bnt_editar.Text = "Editar";
                bnt_deletar.Visible = true;

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM orcamentos WHERE controle = '{static_class.controle}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orcamento.index = reader.GetInt32("controle");
                    cmb_placa.Text = reader.GetString("placa");
                    dtp_cadastro.Value = DateTime.Parse(reader.GetString("dt_cadastro"));
                    txt_doc.Text = reader.GetString("doc");
                    txt_km.Text = reader.GetInt32("km").ToString();
                    try { txt_observacao.Text = reader.GetString("observacao"); } catch { }
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao );

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txt_marca.Text = reader.GetString("marca");
                    txt_modelo.Text = reader.GetString("modelo");
                    txt_ano.Text = reader.GetString("ano");
                    doc_cliente = reader.GetString("doc_dono");
                    txt_chassi.Text = reader.GetString("chassi");
                    cor = reader.GetString("cor");
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{doc_cliente}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txt_cliente.Text = reader.GetString("nome");
                    txt_telefone.Text = reader.GetString("telefone");
                    rua = reader.GetString("rua");
                    bairro = reader.GetString("bairro");
                    cidade = reader.GetString("cidade");
                    cep = reader.GetString("cep");
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{static_class.controle}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal total_servico = 0;
                string total = "";

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    string valor = reader.GetString("valor");
                    string desco = reader.GetString("desco");
                    //try catch para aceitar letras no campo valor ( try -> numero | catch -> letras )
                    try { total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2"); } catch { total = valor; }

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_servicos.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    //try catch para ignorar valores com letras na soma do total
                    try { total_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); } catch { }
                }
                txt_total_servico.Text = total_servico.ToString("N2");
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{static_class.controle}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();
                decimal total_peca = 0;

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    string valor = reader.GetString("valor");
                    string desco = reader.GetString("desco");
                    //try catch para aceitar letras no campo valor ( try -> numero | catch -> letras )
                    try { total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2"); } catch { total = valor; }

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_pecas.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    //try catch para ignorar valores com letras na soma do total 
                    try { total_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); } catch { }
                }
                txt_total_pecas.Text = total_peca.ToString("N2");
                conexao.Close();

                txt_total.Text = (total_peca + total_servico).ToString("N2");

            }
        }

        public void verificar_itens()
        {
            string table = "pecas_os";
            var lista = lst_pecas;
            pecas_os pecas_os = new pecas_os();
            servicos_os servicos_os = new servicos_os();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            for (int i = 0; i <= 1; i++)
            {
                foreach (ListViewItem item in lista.Items)
                {
                    var cmd = new MySqlCommand($"SELECT * FROM {table} WHERE nome = '{item.Text}' AND orca = {static_class.controle}", conexao);
                    conexao.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) { }
                    else
                    {
                        if (table == "pecas_os")
                        {
                            pecas_os.ultimo_index();
                            pecas_os.index++;
                            pecas_os.modo = "orca";

                            //OS serve tanto pra orçamento quando pra ordem de serviço nesse contexto
                            pecas_os.os_or = static_class.controle;

                            pecas_os.nome = item.Text;
                            pecas_os.qtd = decimal.Parse(item.SubItems[1].Text);
                            pecas_os.valor = item.SubItems[2].Text;
                            pecas_os.desc = item.SubItems[3].Text;
                            pecas_os.pos = lista.Items.IndexOf(item) - 1;

                            pecas_os.cadastrar_peca_os();
                        }
                        else if (table == "servicos_os")
                        {
                            servicos_os.ultimo_index();
                            servicos_os.index++;
                            servicos_os.modo = "orca";

                            //OS serve tanto pra orçamento quando pra ordem de serviço nesse contexto
                            servicos_os.os_or = static_class.controle;

                            servicos_os.nome = item.Text;
                            servicos_os.qtd = decimal.Parse(item.SubItems[1].Text);
                            servicos_os.valor = item.SubItems[2].Text;
                            servicos_os.desc = item.SubItems[3].Text;
                            servicos_os.pos = lista.Items.IndexOf(item) - 1;

                            servicos_os.cadastrar_servico_os();
                        }
                    }
                    conexao.Close();
                }
                table = "servicos_os";
                lista = lst_servicos;
            }
        }

        public void atualizar_posicoes()
        {
            try
            {
                using (var conexao = new MySqlConnection("server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport"))
                {
                    for (int i = 0; i < lst_pecas.Items.Count; i++)
                    {
                        var cmd = new MySqlCommand(
                            $"UPDATE pecas_os SET pos = '{i}' WHERE orca = {static_class.controle} AND nome = '{lst_pecas.Items[i].Text}'",
                            conexao);
                        conexao.Open();
                        cmd.ExecuteNonQuery();
                        conexao.Close();
                    }
                    for (int i = 0; i < lst_servicos.Items.Count; i++)
                    {
                        var cmd = new MySqlCommand(
                            $"UPDATE servicos_os SET pos = '{i}' WHERE orca = {static_class.controle} AND nome = '{lst_servicos.Items[i].Text}'",
                            conexao);
                        conexao.Open();
                        cmd.ExecuteNonQuery();
                        conexao.Close();
                    }
                }
            }
            catch { }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            orcamento.cliente = txt_cliente.Text;
            orcamento.doc = txt_doc.Text;
            orcamento.km = int.Parse(txt_km.Text);
            orcamento.placa = cmb_placa.Text;
            orcamento.total = txt_total.Text;
            orcamento.dt_cadastro = dtp_cadastro.Value.ToString("dd/MM/yyyy");
            orcamento.observacao = txt_observacao.Text;

            if (this.Text == "Cadastro orçamento")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    if (txt_total.Text != "") orcamento.total = txt_total.Text; 
                    else orcamento.total = "0,00"; 

                    orcamento.cadastrar_or();

                    orcamento.index++;
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();
            }
            else if (this.Text == "Edição orçamento")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                cliente cliente = new cliente();

                if (reader.Read())
                {
                    orcamento.total = txt_total.Text;
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();

                try
                {
                    orcamento.alterar_or();
                    verificar_itens();

                    cliente.doc = txt_doc.Text;
                    cliente.quitado();

                    MessageBox.Show("OS Alterada!", "JCMotorsport", MessageBoxButtons.OK);
                }
                catch (Exception a) { MessageBox.Show(a.ToString()); }

                //  conexao_calendario();
            }
            atualizar_posicoes();
        }

        private void bnt_editar_servico_Click(object sender, EventArgs e)
        {
            add add_servicos = new add();

            foreach (ListViewItem item in lst_servicos.Items)
            {
                // Clona o item antes de adicionar (evita referência duplicada)
                add_servicos.itens_servicos.Add((ListViewItem)item.Clone());
            }
            lst_servicos.Items.Clear();

            add_servicos.table = "servicos";
            add_servicos.modo = "orca";
            add_servicos.ShowDialog();

            decimal total = 0;
            foreach (ListViewItem item in add_servicos.itens_servicos)
            {
                item.BackColor = lst_servicos.BackColor;
                // Clona o item antes de adicionar (evita referência duplicada)
                lst_servicos.Items.Add((ListViewItem)item.Clone());

                try { total += decimal.Parse(item.SubItems[4].Text); }
                catch { var a = item.SubItems[4]; MessageBox.Show(a.ToString()); }
            }
            txt_total_servico.Text = total.ToString("N2");
            txt_total.Text = (decimal.Parse(txt_total_pecas.Text) + decimal.Parse(txt_total_servico.Text)).ToString("N2");
        }

        private void bnt_editar_peca_Click(object sender, EventArgs e)
        {
            add add_pecas = new add();

            foreach (ListViewItem item in lst_pecas.Items)
            {
                // Clona o item antes de adicionar (evita referência duplicada)
                add_pecas.itens_pecas.Add((ListViewItem)item.Clone());
            }
            lst_pecas.Items.Clear();

            add_pecas.table = "pecas";
            add_pecas.modo = "orca";
            add_pecas.ShowDialog();

            decimal total = 0;
            foreach (ListViewItem item in add_pecas.itens_pecas)
            {
                item.BackColor = lst_pecas.BackColor;
                // Clona o item antes de adicionar (evita referência duplicada)
                lst_pecas.Items.Add((ListViewItem)item.Clone());

                try { total += decimal.Parse(item.SubItems[4].Text); }
                catch { var a = item.SubItems[4]; MessageBox.Show(a.ToString()); }
            }
            txt_total_pecas.Text = total.ToString("N2");
            txt_total.Text = (decimal.Parse(txt_total_pecas.Text) + decimal.Parse(txt_total_servico.Text)).ToString("N2");
        }

        private void cadastro_or_FormClosing(object sender, FormClosingEventArgs e)
        {
            int delete = 0;
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT controle FROM orcamentos WHERE controle = '{orcamento.index}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read()) delete = 1;

            conexao.Close();

            if (delete == 1)
            {
                cmd = new MySqlCommand($"DELETE FROM pecas_os WHERE orca = '{orcamento.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();

                cmd = new MySqlCommand($"DELETE FROM servicos_os WHERE orca = '{orcamento.index}'", conexao);
                conexao.Open();
                cmd.ExecuteReader();
                conexao.Close();
            }
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            // Confirmação para evitar exclusão acidental de orçamentos
            if (MessageBox.Show("Deseja realmente excluir este orçamento permanentemente?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ExecutarDeleteOrcamento();
                Close();
            }
        }

        private void ExecutarDeleteOrcamento(bool usarLocal = false)
        {
            // Seleciona o driver de conexão (MySQL ou SQLite)
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

                    // Comando parametrizado: garante que o ID seja tratado corretamente pelo banco
                    cmd.CommandText = "DELETE FROM orcamentos WHERE controle = @controle";

                    var pControle = cmd.CreateParameter();
                    pControle.ParameterName = "@controle";
                    pControle.Value = static_class.controle;
                    cmd.Parameters.Add(pControle);

                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Se o servidor MySQL falhar, replica a exclusão no banco local
                if (!usarLocal)
                    ExecutarDeleteOrcamento(true);
            }
        }

        private void visualizarImpressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            try
            {
                var doc = new osPdf
                {
                    tipo = "O.R: ",
                    Cliente = txt_cliente.Text,
                    Documento = doc_cliente,
                    Telefone = txt_telefone.Text,
                    Placa = cmb_placa.Text,
                    Marca = txt_marca.Text,
                    Modelo = txt_modelo.Text,
                    Ano = txt_ano.Text,
                    Cor = cor,
                    Rua = rua,
                    Bairro = bairro,
                    Cidade = cidade,
                    CEP = cep,
                    DtCadastro = dtp_cadastro.Value,
                    Total = decimal.Parse(txt_total.Text),
                    TotalPecas = decimal.Parse(txt_total_pecas.Text),
                    TotalServicos = decimal.Parse(txt_total_servico.Text),
                    Pecas = new List<(string, string, string)>(),
                    Servicos = new List<(string, string, string)>()
                };

                foreach (ListViewItem item in lst_pecas.Items)
                {
                    string nome = item.SubItems[0].Text;
                    string qtd = item.SubItems[1].Text.Replace(".", ",");
                    string valor = item.SubItems[2].Text;
                    doc.Pecas.Add((nome, qtd, valor));
                }

                // Adaptado para ListView de serviços
                foreach (ListViewItem item in lst_servicos.Items)
                {
                    string nome = item.SubItems[0].Text;
                    string qtd = item.SubItems[1].Text.Replace(".", ",");
                    string valor = item.SubItems[2].Text;
                    doc.Servicos.Add((nome, qtd, valor));
                }

                // Gerar arquivo temporário
                string tempPath = Path.Combine(Path.GetTempPath(), $"preview_{Guid.NewGuid()}.pdf");

                // Gerar PDF e salvar no arquivo temporário
                doc.GeneratePdf(tempPath);

                // Abrir no visualizador padrão do Windows
                Process.Start(new ProcessStartInfo
                {
                    FileName = tempPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar PDF:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imprimirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            try
            {
                var doc = new osPdf
                {
                    tipo = "O.R: ",
                    Cliente = txt_cliente.Text,
                    Documento = doc_cliente,
                    Telefone = txt_telefone.Text,
                    Placa = cmb_placa.Text,
                    Marca = txt_marca.Text,
                    Modelo = txt_modelo.Text,
                    Ano = txt_ano.Text,
                    Cor = cor,
                    Rua = rua,
                    Bairro = bairro,
                    Cidade = cidade,
                    CEP = cep,
                    DtCadastro = dtp_cadastro.Value,
                    Total = decimal.Parse(txt_total.Text),
                    TotalPecas = decimal.Parse(txt_total_pecas.Text),
                    TotalServicos = decimal.Parse(txt_total_servico.Text),
                    Pecas = new List<(string, string, string)>(),
                    Servicos = new List<(string, string, string)>()
                };

                foreach (ListViewItem item in lst_pecas.Items)
                {
                    string nome = item.SubItems[0].Text;
                    string qtd = item.SubItems[1].Text.Replace(".", ",");
                    string valor = item.SubItems[2].Text;
                    doc.Pecas.Add((nome, qtd, valor));
                }

                // Adaptado para ListView de serviços
                foreach (ListViewItem item in lst_servicos.Items)
                {
                    string nome = item.SubItems[0].Text;
                    string qtd = item.SubItems[1].Text.Replace(".", ",");
                    string valor = item.SubItems[2].Text;
                    doc.Servicos.Add((nome, qtd, valor));
                }

                using (var salvar = new SaveFileDialog())
                {
                    salvar.Filter = "PDF files (*.pdf)|*.pdf";
                    salvar.FileName = $"OS_{static_class.controle}.pdf";

                    if (salvar.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            doc.GeneratePdf(salvar.FileName);
                            MessageBox.Show("PDF gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao gerar PDF:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar PDF:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_placa_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa LIKE '%{cmb_placa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_placa.Items.Add(reader.GetString("placa"));
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_ano.Text = reader.GetString("ano");
                doc_cliente = reader.GetString("doc_dono");
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc LIKE '%{doc_cliente}%'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_cliente.Text = reader.GetString("nome");
                txt_telefone.Text = reader.GetString("telefone");
            }
            conexao.Close();

            if (cmb_placa.Text == "" || cmb_placa.Text == " ")
            {
                txt_marca.Text = "";
                txt_modelo.Text = "";
                txt_ano.Text = "";
                doc_cliente = "";
                txt_doc.Text = "";
                txt_cliente.Text = "";
                txt_telefone.Text = "";
            }
        }
    }
}
