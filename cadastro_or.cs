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
            // 1. Configuração das Colunas (Função que criamos para manter o Load limpo)
            ConfigurarColunasOrcamento();

            // 2. Definição da Conexão Híbrida
            System.Data.IDbConnection conexao;
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

            using (conexao)
            {
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

                    var cmd = conexao.CreateCommand();

                    // CARREGAR DADOS DO ORÇAMENTO
                    cmd.CommandText = $"SELECT * FROM orcamentos WHERE controle = '{static_class.controle}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            orcamento.index = Convert.ToInt32(reader["controle"]);
                            cmb_placa.Text = reader["placa"].ToString();
                            dtp_cadastro.Value = DateTime.Parse(reader["dt_cadastro"].ToString());
                            txt_doc.Text = reader["doc"].ToString();
                            txt_km.Text = reader["km"].ToString();
                            try { txt_observacao.Text = reader["observacao"].ToString(); } catch { }
                        }
                    }

                    // CARREGAR DADOS DA MOTO
                    cmd.CommandText = $"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_marca.Text = reader["marca"].ToString();
                            txt_modelo.Text = reader["modelo"].ToString();
                            txt_ano.Text = reader["ano"].ToString();
                            doc_cliente = reader["doc_dono"].ToString();
                            txt_chassi.Text = reader["chassi"].ToString();
                            cor = reader["cor"].ToString();
                        }
                    }

                    // CARREGAR DADOS DO CLIENTE
                    cmd.CommandText = $"SELECT * FROM clientes WHERE doc = '{doc_cliente}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_cliente.Text = reader["nome"].ToString();
                            txt_telefone.Text = reader["telefone"].ToString();
                            rua = reader["rua"].ToString();
                            bairro = reader["bairro"].ToString();
                            cidade = reader["cidade"].ToString();
                            cep = reader["cep"].ToString();
                        }
                    }

                    // CARREGAR LISTAS (Usando a coluna 'orca' como no seu original)
                    decimal s_total = PreencherListaOrcamento(conexao, "servicos_os", lst_servicos, txt_total_servico);
                    decimal p_total = PreencherListaOrcamento(conexao, "pecas_os", lst_pecas, txt_total_pecas);

                    txt_total.Text = (s_total + p_total).ToString("N2");
                }
            }
        }

        // --- FUNÇÕES DE SUPORTE PARA O ORÇAMENTO ---

        private void ConfigurarColunasOrcamento()
        {
            if (lst_servicos.Columns.Count > 0) return;

            lst_servicos.View = View.Details;
            lst_servicos.Columns.Add("Nome", 230); lst_servicos.Columns.Add("Qtd", 50);
            lst_servicos.Columns.Add("Valor", 50); lst_servicos.Columns.Add("Desc.", 50); lst_servicos.Columns.Add("Total", 80);

            lst_pecas.View = View.Details;
            lst_pecas.Columns.Add("Nome", 230); lst_pecas.Columns.Add("Qtd", 50);
            lst_pecas.Columns.Add("Valor", 50); lst_pecas.Columns.Add("Desc.", 50); lst_pecas.Columns.Add("Total", 80);
        }

        private decimal PreencherListaOrcamento(System.Data.IDbConnection conn, string tabela, ListView lista, TextBox txtSub)
        {
            lista.Items.Clear();
            decimal subtotal = 0;
            var cmd = conn.CreateCommand();
            // Importante: aqui usamos 'orca' em vez de 'os' para orçamentos
            cmd.CommandText = $"SELECT * FROM {tabela} WHERE orca = '{static_class.controle}' ORDER BY pos ASC";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string nome = reader["nome"].ToString();
                    string qtd = reader["qtd"].ToString().Replace(".", ",");
                    string valor = reader["valor"].ToString();
                    string desco = reader["desco"].ToString();
                    string totalStr;

                    try
                    {
                        decimal t = (decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco);
                        totalStr = t.ToString("N2");
                        subtotal += t;
                    }
                    catch { totalStr = valor; }

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd); item.SubItems.Add(valor);
                    item.SubItems.Add(desco); item.SubItems.Add(totalStr);
                    lista.Items.Add(item);
                }
            }
            txtSub.Text = subtotal.ToString("N2");
            return subtotal;
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            orcamento.cliente = txt_cliente.Text;
            orcamento.doc = txt_doc.Text;
            orcamento.km = int.TryParse(txt_km.Text, out int resultKm) ? resultKm : 0;
            orcamento.placa = cmb_placa.Text;
            orcamento.total = string.IsNullOrWhiteSpace(txt_total.Text) ? "0,00" : txt_total.Text;
            orcamento.dt_cadastro = dtp_cadastro.Value.ToString("dd/MM/yyyy");
            orcamento.observacao = txt_observacao.Text;

            System.Data.IDbConnection conexao;
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

            using (conexao)
            {
                // Verifica se a moto existe
                var cmd = conexao.CreateCommand();
                cmd.CommandText = $"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'";
                bool motoExiste;
                using (var reader = cmd.ExecuteReader()) { motoExiste = reader.Read(); }

                if (!motoExiste)
                {
                    MessageBox.Show("Preencha os dados da moto ou verifique a placa", "JC Motorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.Text == "Cadastro orçamento")
                {
                    orcamento.cadastrar_or();
                    verificar_itens(); // Já limpa e cadastra tudo
                    orcamento.index++;
                    MessageBox.Show("Orçamento Cadastrado!", "JCMotorsport");
                }
                else if (this.Text == "Edição orçamento")
                {
                    try
                    {
                        orcamento.alterar_or();
                        verificar_itens(); // Já limpa e cadastra tudo

                        cliente clienteObj = new cliente();
                        clienteObj.doc = txt_doc.Text;
                        clienteObj.quitado();

                        MessageBox.Show("Orçamento Alterado!", "JCMotorsport");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        public void verificar_itens()
        {
            pecas_os objPeca = new pecas_os();
            servicos_os objServico = new servicos_os();

            System.Data.IDbConnection conexao;
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

            using (conexao)
            {
                var cmd = conexao.CreateCommand();

                // 1. LIMPA TUDO QUE JÁ EXISTE PARA ESSE ORÇAMENTO
                cmd.CommandText = $"DELETE FROM pecas_os WHERE orca = {static_class.controle}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DELETE FROM servicos_os WHERE orca = {static_class.controle}";
                cmd.ExecuteNonQuery();

                // 2. REINSERE PEÇAS
                foreach (ListViewItem item in lst_pecas.Items)
                {
                    objPeca.ultimo_index();
                    objPeca.index++;
                    objPeca.modo = "orca";
                    objPeca.os_or = static_class.controle;
                    objPeca.nome = item.Text;
                    objPeca.qtd = decimal.Parse(item.SubItems[1].Text);
                    objPeca.valor = item.SubItems[2].Text;
                    objPeca.desc = item.SubItems[3].Text;
                    objPeca.pos = lst_pecas.Items.IndexOf(item); // Posição atual
                    objPeca.cadastrar_peca_os();
                }

                // 3. REINSERE SERVIÇOS
                foreach (ListViewItem item in lst_servicos.Items)
                {
                    objServico.ultimo_index();
                    objServico.index++;
                    objServico.modo = "orca";
                    objServico.os_or = static_class.controle;
                    objServico.nome = item.Text;
                    objServico.qtd = decimal.Parse(item.SubItems[1].Text);
                    objServico.valor = item.SubItems[2].Text;
                    objServico.desc = item.SubItems[3].Text;
                    objServico.pos = lst_servicos.Items.IndexOf(item); // Posição atual
                    objServico.cadastrar_servico_os();
                }
            }
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
            // Limpeza rápida se o campo for apagado
            if (string.IsNullOrWhiteSpace(cmb_placa.Text))
            {
                txt_marca.Text = ""; txt_modelo.Text = ""; txt_ano.Text = "";
                doc_cliente = ""; txt_doc.Text = ""; txt_cliente.Text = "";
                txt_telefone.Text = "";
                return;
            }

            System.Data.IDbConnection conexao;
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

            using (conexao)
            {
                var cmd = conexao.CreateCommand();
                // Busca moto por aproximação
                cmd.CommandText = $"SELECT * FROM motos WHERE placa LIKE '%{cmb_placa.Text}%'";

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Adiciona ao dropdown e preenche campos da moto
                        cmb_placa.Items.Add(reader["placa"].ToString());
                        txt_marca.Text = reader["marca"].ToString();
                        txt_modelo.Text = reader["modelo"].ToString();
                        txt_ano.Text = reader["ano"].ToString();
                        doc_cliente = reader["doc_dono"].ToString();
                        txt_doc.Text = reader["doc_dono"].ToString();
                    }
                }

                // Se encontrou um dono, busca os dados do cliente
                if (!string.IsNullOrEmpty(doc_cliente))
                {
                    var cmdCliente = conexao.CreateCommand();
                    cmdCliente.CommandText = $"SELECT * FROM clientes WHERE doc LIKE '%{doc_cliente}%'";
                    using (var readerC = cmdCliente.ExecuteReader())
                    {
                        if (readerC.Read())
                        {
                            txt_cliente.Text = readerC["nome"].ToString();
                            txt_telefone.Text = readerC["telefone"].ToString();
                        }
                    }
                }
            }
        }
    }
}
