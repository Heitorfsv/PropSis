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
        OS os = new OS();

        //Variaveis para o PDF
        string rua = "";
        string bairro = "";
        string cidade = "";
        string cep = "";
        string cor = "";

        public cadastro_or()
        {
            InitializeComponent();
        }

        private void cadastro_or_Load(object sender, EventArgs e)
        {
            lst_servicos.View = View.Details;
            lst_servicos.Columns.Add("Nome", 150);
            lst_servicos.Columns.Add("Qtd", 50);
            lst_servicos.Columns.Add("Valor", 50);
            lst_servicos.Columns.Add("Desc.", 50);
            lst_servicos.Columns.Add("Total", 80);

            lst_pecas.View = View.Details;
            lst_pecas.Columns.Add("Nome", 150);
            lst_pecas.Columns.Add("Qtd", 50);
            lst_pecas.Columns.Add("Valor", 50);
            lst_pecas.Columns.Add("Desc.", 50);
            lst_pecas.Columns.Add("Total", 80);

            if (this.Text == "Cadastro orçamento")
            {
                bnt_cadastro.Text = "Cadastrar";
                bnt_deletar.Visible = false;
                dtp_cadastro.Value = DateTime.Now;

                orcamento.ultimo_index();
                orcamento.index++;
                static_class.controle = orcamento.index;
            }
            else if (this.Text == "Edição orçamento")
            {
                bnt_cadastro.Text = "Editar";
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
                    cor = reader.GetString("cor");
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{doc_cliente}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmb_cliente.Text = reader.GetString("nome");
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
                    total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2");

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_servicos.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    total_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd));
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
                    total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2");

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_pecas.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { total_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
                    catch { }
                }
                txt_total_pecas.Text = total_peca.ToString("N2");
                conexao.Close();

                txt_total.Text = (total_peca + total_servico).ToString("N2");
            }
        }

        private void bnt_cadastro_Click(object sender, EventArgs e)
        {
            if (this.Text == "Cadastro orçamento")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{txt_doc.Text}'", conexao);

                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    orcamento.cliente = cmb_cliente.Text;
                    orcamento.doc = txt_doc.Text;
                    orcamento.placa = cmb_placa.Text;
                    orcamento.total = txt_total.Text;
                    orcamento.dt_cadastro = dtp_cadastro.Value.ToString("dd/MM/yyyy");

                    if (txt_total.Text != "")
                    { orcamento.total = txt_total.Text; }
                    else
                    { orcamento.total = "0,00"; }

                    orcamento.cadastrar_or();

                    orcamento.index++;
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
                    orcamento.placa = cmb_placa.Text;
                    orcamento.cliente = cmb_cliente.Text;
                    orcamento.doc = txt_doc.Text;
                    orcamento.total = txt_total.Text;
                    orcamento.dt_cadastro = dtp_cadastro.Value.ToString();
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();

                try
                {
                    orcamento.alterar_or();

                    MessageBox.Show("OS Alterada!", "JCMotorsport", MessageBoxButtons.OK);
                }
                catch (Exception a) { MessageBox.Show(a.ToString()); }
            }
        }

        private void bnt_editar_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add add_servicos = new add();
            add_servicos.table = "servicos";
            add_servicos.modo = "orca";
            add_servicos.Show();
        }

        private void bnt_editar_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();

            add add_pecas = new add();
            add_pecas.table = "pecas";
            add_pecas.modo = "orca";
            add_pecas.Show();
        }

        private void cadastro_or_FormClosing(object sender, FormClosingEventArgs e)
        {
            int delete = 0;
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT controle FROM orcamentos WHERE controle = '{orcamento.index}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            { }
            else
            { delete = 1; }
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

        private void cmb_cliente_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome LIKE '%{cmb_cliente.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_cliente.Items.Add(reader.GetString("nome"));
                txt_doc.Text = reader.GetString("doc");
            }
            conexao.Close();

            cmb_placa.Items.Clear();

            cmd = new MySqlCommand($"SELECT * FROM motos WHERE doc_dono = '{txt_doc.Text}'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_placa.Items.Add(reader.GetString("placa"));
            }

            conexao.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (static_class.close == 1)
            {
                lst_servicos.Items.Clear();
                lst_pecas.Items.Clear();

                decimal servico_total = 0;
                decimal peca_total = 0;
                string total = "";

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE orca = '{static_class.controle}' ORDER BY pos ASC", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    string valor = reader.GetString("valor");
                    string desco = reader.GetString("desco");
                    total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2");

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_servicos.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { servico_total += decimal.Parse(qtd) * decimal.Parse(reader.GetString("valor")); }
                    catch { }
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE orca = '{static_class.controle}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    string valor = reader.GetString("valor");
                    string desco = reader.GetString("desco");
                    total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2");

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_pecas.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { peca_total += decimal.Parse(qtd) * decimal.Parse(reader.GetString("valor")); }
                    catch { }
                }
                conexao.Close();

                txt_total_servico.Text = servico_total.ToString("N2");
                txt_total_pecas.Text = peca_total.ToString("N2");
                txt_total.Text = (peca_total + servico_total).ToString("N2");

                static_class.close = 0;
            }
        }

        private void cmb_placa_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_ano.Text = reader.GetString("ano");
                txt_chassi.Text = reader.GetString("chassi");
            }
            conexao.Close();
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM orcamentos WHERE controle = '{static_class.controle}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
            Close();
        }

        private void visualizarImpressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            try
            {
                var doc = new osPdf
                {
                    tipo = "O.R: ",
                    Cliente = cmb_cliente.Text,
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
                    Cliente = cmb_cliente.Text,
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
    }
}
