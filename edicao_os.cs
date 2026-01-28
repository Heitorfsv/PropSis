using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using static iText.Svg.SvgConstants;
using static QuestPDF.Helpers.Colors;


namespace PrototipoSistema
{
    public partial class edicao_os : Form
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "CalendarioApp";

        string doc_cliente;
        int troca_oleo, revisao;
        DateTime prox_oleo, prox_revisao;

        //Variaveis para o PDF
        string rua = "";
        string bairro = "";
        string cidade = "";
        string cep = "";
        string cor = "";

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        OS os = new OS();
        public edicao_os()
        {
            InitializeComponent();
        }

        private void edicao_os2_Load(object sender, EventArgs e)
        {
            // 1. Configuração Visual das Listas
            ConfigurarColunasListViews();

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
                // 3. Carregar Métodos de Pagamento
                var cmd = conexao.CreateCommand();
                cmd.CommandText = "SELECT * FROM metodo_pag";
                using (var reader = cmd.ExecuteReader())
                {
                    cmb_pago.Items.Clear();
                    while (reader.Read()) { cmb_pago.Items.Add(reader["metodo"].ToString()); }
                }
                cmb_pago.Visible = false;

                // 4. Lógica de Cadastro ou Edição
                if (this.Text == "Cadastro OS")
                {
                    bnt_editar.Text = "Cadastrar";
                    dtp_saida.Enabled = false;
                    dtp_cadastro.Value = DateTime.Now;

                    os.ultimo_index();
                    os.index++;
                    static_class.controle = os.index;
                }
                else if (this.Text == "Edição OS")
                {
                    bnt_editar.Text = "Salvar";

                    // CARREGAR DADOS DA OS
                    cmd.CommandText = $"SELECT * FROM os WHERE controle = '{static_class.controle}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            os.index = Convert.ToInt32(reader["controle"]);
                            cmb_placa.Text = reader["placa"].ToString();
                            txt_doc.Text = reader["doc"].ToString();
                            txt_km.Text = reader["km"].ToString();
                            dtp_cadastro.Value = DateTime.Parse(reader["dt_cadastro"].ToString());
                            txt_observacao.Text = reader["observacao"].ToString();
                            try { txt_descricao.Text = reader["descricao"].ToString(); } catch { }

                            try
                            {
                                if (reader["dt_saida"] != DBNull.Value && !string.IsNullOrEmpty(reader["dt_saida"].ToString()))
                                {
                                    dtp_saida.Value = DateTime.Parse(reader["dt_saida"].ToString());
                                    dtp_saida.Enabled = true;
                                    cb_saida.Checked = true;
                                }
                            }
                            catch { dtp_saida.Enabled = false; cb_saida.Checked = false; }

                            if (reader["pago"].ToString() == "1")
                            {
                                cb_pago.Checked = true;
                                cmb_pago.Visible = true;
                                try { cmb_pago.Text = reader["metodo_pag"].ToString(); } catch { }
                            }
                        }
                    }

                    // CARREGAR MOTO
                    cmd.CommandText = $"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_marca.Text = reader["marca"].ToString();
                            txt_modelo.Text = reader["modelo"].ToString();
                            txt_ano.Text = reader["ano"].ToString();
                            cor = reader["cor"].ToString();
                            txt_chassi.Text = reader["chassi"].ToString();
                            doc_cliente = reader["doc_dono"].ToString();
                        }
                    }

                    // CARREGAR CLIENTE
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

                    // CARREGAR LISTAS (Serviços e Peças)
                    decimal s_total = PreencherLista(conexao, "servicos_os", lst_servicos, txt_total_servico);
                    decimal p_total = PreencherLista(conexao, "pecas_os", lst_pecas, txt_total_pecas);
                    txt_total.Text = (s_total + p_total).ToString("N2");

                    // CARREGAR HISTÓRICOS (Óleo e Revisão)
                    CarregarAlertas(conexao);
                }
            }
        }

        // --- FUNÇÕES DE SUPORTE ---

        private void ConfigurarColunasListViews()
        {
            if (lst_servicos.Columns.Count > 0) return;

            lst_servicos.View = View.Details;
            lst_servicos.Columns.Add("Nome", 230); lst_servicos.Columns.Add("Qtd", 50);
            lst_servicos.Columns.Add("Valor", 50); lst_servicos.Columns.Add("Desc.", 50); lst_servicos.Columns.Add("Total", 80);

            lst_pecas.View = View.Details;
            lst_pecas.Columns.Add("Nome", 230); lst_pecas.Columns.Add("Qtd", 50);
            lst_pecas.Columns.Add("Valor", 50); lst_pecas.Columns.Add("Desc.", 50); lst_pecas.Columns.Add("Total", 80);
        }

        private decimal PreencherLista(System.Data.IDbConnection conn, string tabela, ListView lista, TextBox txtSub)
        {
            lista.Items.Clear();
            decimal subtotal = 0;
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM {tabela} WHERE os = '{static_class.controle}' ORDER BY pos ASC";

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

        private void CarregarAlertas(System.Data.IDbConnection conn)
        {
            var cmd = conn.CreateCommand();
            // Óleo
            cmd.CommandText = $"SELECT * FROM os WHERE (aviso_oleo IS NOT NULL AND placa = '{cmb_placa.Text}') AND controle < {static_class.controle} ORDER BY controle DESC";
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    try { txt_oleo_dt.Text = reader["dt_saida"].ToString(); } catch { }
                    try { txt_oleo_km.Text = (int.Parse(txt_km.Text) - int.Parse(reader["km"].ToString())).ToString(); } catch { }
                }
            }
            // Revisão
            cmd.CommandText = $"SELECT * FROM os WHERE (aviso_revisao IS NOT NULL AND placa = '{cmb_placa.Text}') AND controle < {static_class.controle} ORDER BY controle DESC";
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    try { txt_revisao_dt.Text = reader["dt_saida"].ToString(); } catch { }
                    try { txt_revisao.Text = (int.Parse(txt_km.Text) - int.Parse(reader["km"].ToString())).ToString(); } catch { }
                }
            }
        }

        private void cmb_placa_TextChanged(object sender, EventArgs e)
        {
            // Limpeza rápida se o campo estiver vazio
            if (string.IsNullOrWhiteSpace(cmb_placa.Text))
            {
                txt_marca.Text = ""; txt_modelo.Text = ""; txt_ano.Text = "";
                doc_cliente = ""; txt_doc.Text = ""; txt_cliente.Text = "";
                txt_telefone.Text = "";
                return;
            }

            string placa_formata = cmb_placa.Text.Replace(" ", "%");
            System.Data.IDbConnection conexao;

            // Lógica Híbrida: Tenta MySQL, se falhar vai SQLite
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

                // 1. BUSCA MOTO
                cmd.CommandText = $"SELECT * FROM motos WHERE placa LIKE '%{placa_formata}%'";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmb_placa.Items.Add(reader["placa"].ToString());
                        txt_marca.Text = reader["marca"].ToString();
                        txt_modelo.Text = reader["modelo"].ToString();
                        txt_ano.Text = reader["ano"].ToString();
                        txt_doc.Text = reader["doc_dono"].ToString();
                        doc_cliente = reader["doc_dono"].ToString();
                    }
                }

                // 2. BUSCA CLIENTE (Baseado no doc_cliente encontrado acima)
                if (!string.IsNullOrEmpty(doc_cliente))
                {
                    cmd.CommandText = $"SELECT * FROM clientes WHERE doc LIKE '%{doc_cliente}%'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txt_cliente.Text = reader["nome"].ToString();
                            txt_telefone.Text = reader["telefone"].ToString();
                        }
                    }
                    CarregarAlertas(conexao);
                }
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            os.placa = cmb_placa.Text;
            os.cliente = txt_cliente.Text;
            os.doc = txt_doc.Text;
            os.km = int.TryParse(txt_km.Text, out int resultKm) ? resultKm : 0;
            os.observacao = txt_observacao.Text;
            os.descricao = txt_descricao.Text;
            os.dt_cadastro = dtp_cadastro.Value.ToString();

            if (cb_pago.Checked)
            {
                os.pago = 1;
                os.metodo = cmb_pago.Text;
            }
            else { os.pago = 0; os.metodo = ""; }

            os.dt_saida = dtp_saida.Enabled ? dtp_saida.Value.ToString("dd/MM/yyyy") : null;

            // Conexão Híbrida para verificação
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
                // Verifica troca de óleo nas peças
                foreach (ListViewItem item in lst_pecas.Items)
                {
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM pecas WHERE nome = '{item.Text}' AND troca_oleo = 1";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) troca_oleo = 1;
                    }
                }

                // Verifica revisão nos serviços
                foreach (ListViewItem item in lst_servicos.Items)
                {
                    if (item.Text.ToUpper().Contains("REVISÃO")) revisao = 1;
                }

                if (troca_oleo == 1 || revisao == 1)
                {
                    add_troca troca = new add_troca();
                    troca.ShowDialog();
                    prox_oleo = troca.oleo;
                    os.aviso_oleo = prox_oleo.ToString();
                    prox_revisao = troca.revisao;
                    os.aviso_revisao = prox_revisao.ToString();
                }

                cliente clienteObj = new cliente();

                if (this.Text == "Cadastro OS")
                {
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Não foi possível achar a placa digitada", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    os.total = !string.IsNullOrEmpty(txt_total.Text) ? txt_total.Text : "0,00";
                    os.cadastrar_os();
                    verificar_itens(); // Chama a função híbrida abaixo

                    clienteObj.doc = txt_doc.Text;
                    clienteObj.quitado();
                    os.index++;
                }
                else if (this.Text == "Edição OS")
                {
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = $"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) os.total = txt_total.Text;
                        else MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    try
                    {
                        os.alterar_os();
                        verificar_itens();
                        clienteObj.doc = txt_doc.Text;
                        clienteObj.quitado();
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

                // 1. LIMPA TUDO QUE JÁ EXISTE DESSA OS NO BANCO
                // Fazemos isso primeiro para evitar duplicidade e limpar itens que você removeu da lista
                cmd.CommandText = $"DELETE FROM pecas_os WHERE os = {static_class.controle}";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DELETE FROM servicos_os WHERE os = {static_class.controle}";
                cmd.ExecuteNonQuery();

                // 2. REINSERE AS PEÇAS
                foreach (ListViewItem item in lst_pecas.Items)
                {
                    objPeca.ultimo_index();
                    objPeca.index++;
                    objPeca.modo = "os";
                    objPeca.os_or = static_class.controle;
                    objPeca.nome = item.Text;
                    objPeca.qtd = decimal.Parse(item.SubItems[1].Text);
                    objPeca.valor = item.SubItems[2].Text;
                    objPeca.desc = item.SubItems[3].Text;
                    objPeca.pos = lst_pecas.Items.IndexOf(item); // Posição atual na lista

                    objPeca.cadastrar_peca_os();
                }

                // 3. REINSERE OS SERVIÇOS
                foreach (ListViewItem item in lst_servicos.Items)
                {
                    objServico.ultimo_index();
                    objServico.index++;
                    objServico.modo = "os";
                    objServico.os_or = static_class.controle;
                    objServico.nome = item.Text;
                    objServico.qtd = decimal.Parse(item.SubItems[1].Text);
                    objServico.valor = item.SubItems[2].Text;
                    objServico.desc = item.SubItems[3].Text;
                    objServico.pos = lst_servicos.Items.IndexOf(item); // Posição atual na lista

                    objServico.cadastrar_servico_os();
                }
            }
        }

        private void bnt_add_peca_Click(object sender, EventArgs e)
        {
            add add_pecas = new add();

            foreach (ListViewItem item in lst_pecas.Items)
            {
                // Clona o item antes de adicionar (evita referência duplicada)
                add_pecas.itens_pecas.Add((ListViewItem)item.Clone());
            }
            lst_pecas.Items.Clear();

            add_pecas.table = "pecas";
            add_pecas.modo = "os";
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

        private void bnt_add_servico_Click(object sender, EventArgs e)
        {
            add add_servicos = new add();

            foreach (ListViewItem item in lst_servicos.Items)
            {
                // Clona o item antes de adicionar (evita referência duplicada)
                add_servicos.itens_servicos.Add((ListViewItem)item.Clone());
            }
            lst_servicos.Items.Clear();

            add_servicos.table = "servicos";
            add_servicos.modo = "os";
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

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            // Confirmação para evitar exclusão acidental
            if (MessageBox.Show("Tem certeza que deseja excluir esta Ordem de Serviço permanentemente?",
                "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                ExecutarDeleteOS();
                Close();
            }
        }

        private void ExecutarDeleteOS(bool usarLocal = false)
        {
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

                    conexao.Open();
                    cmd.ExecuteReader();
                    conexao.Close();
                    Close();
                    // Comando parametrizado para deletar a OS pelo controle
                    cmd.CommandText = "DELETE FROM os WHERE controle = @controle";

                    var pControle = cmd.CreateParameter();
                    pControle.ParameterName = "@controle";
                    pControle.Value = static_class.controle;
                    cmd.Parameters.Add(pControle);

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM servicos_os WHERE os = @controle2";

                    var pControle2 = cmd.CreateParameter();
                    pControle2.ParameterName = "@controle2";
                    pControle2.Value = static_class.controle;
                    cmd.Parameters.Add(pControle2);

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM pecas_os WHERE os = @controle3";

                    var pControle3 = cmd.CreateParameter();
                    pControle3.ParameterName = "@controle3";
                    pControle3.Value = static_class.controle;
                    cmd.Parameters.Add(pControle3);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao deletar OS: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se o servidor MySQL estiver offline, deleta do banco de dados local
                if (!usarLocal)
                    ExecutarDeleteOS(true);
            }
        }

        private void cb_saida_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_saida.Checked == true) dtp_saida.Enabled = true;
            else dtp_saida.Enabled = false;
        }

        public void conexao_calendario()
        {
            UserCredential credential;

            using (var stream = new FileStream("C:\\credentials\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                // Path to the token storage directory.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define the new event
            if (troca_oleo == 1)
            {
                Event event_troca = new Event()
                {
                    Summary = "Próxima troca de óleo " + txt_cliente.Text,
                    Description = "...",
                    Start = new EventDateTime()
                    {
                        DateTime = prox_oleo,
                        TimeZone = "America/Sao_Paulo",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = prox_oleo,
                        TimeZone = "America/Sao_Paulo",
                    },
                    // Attendees = new EventAttendee[] { new EventAttendee() { Email = "jcmotors2020@gmail.com" } },
                };

                // Insert the event into the user's calendar
                EventsResource.InsertRequest request = service.Events.Insert(event_troca, "956ab706496289c001ca9563d240163c1f4c1f4383cd54fc48b28a0db742a186@group.calendar.google.com");
                Event createdEvent = request.Execute();
        }

            if (revisao == 1)
            {
                Event event_revisao = new Event()
                {
                    Summary = "Próxima revisão " + txt_cliente.Text,
                    Description = "...",
                    Start = new EventDateTime()
                    {
                        DateTime = prox_revisao,
                        TimeZone = "America/Sao_Paulo",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = prox_revisao,
                        TimeZone = "America/Sao_Paulo",
                    },
                    //Attendees = new EventAttendee[] { new EventAttendee() { Email = "jcmotors2020@gmail.com" } },
                };

                // Insert the event into the user's calendar
                EventsResource.InsertRequest request = service.Events.Insert(event_revisao, "58e9d765620bb76b062a943bc01eae7de7b49875dd31e8e944557d05f30b1c86@group.calendar.google.com");
                Event createdEvent = request.Execute();
            }
            this.Close();
        }

        private osPdf CriarDocumento()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return new osPdf
            {
                tipo = "O.S: ",
                Cliente = txt_cliente.Text,
                Documento = doc_cliente,
                Telefone = txt_telefone.Text,
                Placa = cmb_placa.Text,
                Marca = txt_marca.Text,
                Modelo = txt_modelo.Text,
                Ano = txt_ano.Text,
                Cor = cor,
                Km = txt_km.Text,
                Rua = rua,
                Bairro = bairro,
                Cidade = cidade,
                CEP = cep,
                Observacao = txt_observacao.Text,
                DtCadastro = dtp_cadastro.Value,
                DtSaida = dtp_saida.Value,
                Total = decimal.Parse(txt_total.Text),
                TotalPecas = decimal.Parse(txt_total_pecas.Text),
                TotalServicos = decimal.Parse(txt_total_servico.Text),
                Pecas = new List<(string, string, string)>(),
                Servicos = new List<(string, string, string)>()
            };
        }

        private void visualizarImpressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            try
            {
                var doc = CriarDocumento();

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

        private void txt_km_TextChanged(object sender, EventArgs e)
        {
            // 1. Só avança se o KM for um número válido
            if (!int.TryParse(txt_km.Text, out int kmDigitado)) return;

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
                // 2. Busca o KM mais alto já registrado para esta moto na tabela OS
                cmd.CommandText = $"SELECT MAX(km) FROM os WHERE placa = '{cmb_placa.Text}'";

                var resultado = cmd.ExecuteScalar();
                int ultimoKmGravado = 0;

                if (resultado != DBNull.Value && resultado != null) ultimoKmGravado = Convert.ToInt32(resultado);
                

                // 3. TRAVA: Só carrega alertas se o KM que você está digitando 
                // for maior ou igual ao último que já estava no banco.
                if (kmDigitado >= ultimoKmGravado) CarregarAlertas(conexao);
                    else txt_oleo_km.Text = ""; txt_revisao.Text = "";               
            }
        }

        private void imprimirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            try
            {
                var doc = CriarDocumento();

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

        private void cb_pago_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_pago.Checked) cmb_pago.Visible = true;
            else cmb_pago.Visible = false;
        }
    }
}
