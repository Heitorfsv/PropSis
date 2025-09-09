using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Diagnostics;
using static QuestPDF.Helpers.Colors;


namespace PrototipoSistema
{
    public partial class edicao_os : Form
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "CalendarioApp";

        string doc_cliente;

        //Variaveis para o PDF
        string rua = "";
        string bairro = "";
        string cidade = "";
        string cep = "";
        string cor = "";

        OS os = new OS();
        public edicao_os()
        {
            InitializeComponent();
        }

        private void edicao_os2_Load(object sender, EventArgs e)
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

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM metodo_pag", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            { cmb_pago.Items.Add(reader.GetString("metodo"));}

            conexao.Close();

            cmb_pago.Visible = false;

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

                gb_troca.Visible = false;

                cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = '{static_class.controle}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    os.index = reader.GetInt32("controle");
                    cmb_placa.Text = reader.GetString("placa");
                    txt_km.Text = reader.GetInt32("km").ToString();
                    dtp_cadastro.Value = DateTime.Parse(reader.GetString("dt_cadastro"));
                    txt_observacao.Text = reader.GetString("observacao");
                    txt_doc.Text = reader.GetString("doc");

                    try
                    {
                        dtp_saida.Value = DateTime.Parse(reader.GetString("dt_saida"));
                        dtp_saida.Enabled = true;
                        cb_saida.Checked = true;
                    }
                    catch
                    {
                        dtp_saida.Enabled = false;
                        cb_saida.Checked = false;
                    }

                    try
                    {
                        dtp_troca_filtro.Value = DateTime.ParseExact(reader.GetString("aviso_filtro_dt"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        cb_filtro.Checked = true;
                    }
                    catch { }

                    try
                    {
                        dtp_troca_oleo.Value = DateTime.ParseExact(reader.GetString("aviso_oleo_dt"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        cb_oleo.Checked = true;
                    }
                    catch { }

                    if (reader.GetInt32("pago") == 1)
                    {
                        cb_pago.Checked = true;
                        cmb_pago.Visible = true;
                        try
                        { cmb_pago.Text = reader.GetString("metodo_pag"); }
                        catch { MessageBox.Show("Nenhum metodo de pagamento cadastrado"); }
                    }
                    else
                    { 
                        cb_pago.Checked = false;
                        cmb_pago.Visible = false;
                    }
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txt_marca.Text = reader.GetString("marca");
                    txt_modelo.Text = reader.GetString("modelo");
                    txt_ano.Text = reader.GetString("ano");
                    cor = reader.GetString("cor");
                    doc_cliente = reader.GetString("doc_dono");
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

                cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle}' ORDER BY pos ASC", conexao);

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

                    total_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco"));
                }
                txt_total_servico.Text = total_servico.ToString("N2");
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{static_class.controle}' ORDER BY pos ASC", conexao);

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

                /////////////////////////////////////////

                // Verifica se já existe uma OS com aviso de troca de óleo ou filtro para a placa selecionada
                cmd = new MySqlCommand($"SELECT * FROM os WHERE (aviso_oleo_dt REGEXP '[A-Za-z0-9]' AND placa = '{cmb_placa.Text}') AND controle < {static_class.controle} ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') DESC;", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_oleo_dt.Text = reader.GetString("dt_cadastro");
                    txt_oleo_km.Text = (int.Parse(txt_km.Text) - int.Parse(reader.GetString("km"))).ToString();
                }
                conexao.Close();

                // Verifica se já existe uma OS com aviso de troca de óleo ou filtro para a placa selecionada
                cmd = new MySqlCommand($"SELECT * FROM os WHERE (aviso_filtro_dt REGEXP '[A-Za-z0-9]' AND placa = '{cmb_placa.Text}') AND controle < {static_class.controle} ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') DESC;", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_filtro_dt.Text = reader.GetString("dt_cadastro");
                    txt_filtro_km.Text = (int.Parse(txt_km.Text) - int.Parse(reader.GetString("km"))).ToString();
                }
                conexao.Close();
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

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            if (this.Text == "Cadastro OS")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                cliente cliente = new cliente();

                if (reader.Read())
                {
                    os.placa = cmb_placa.Text;
                    os.km = int.Parse(txt_km.Text);
                    os.cliente = txt_cliente.Text;

                    os.doc = doc_cliente;
                    os.observacao = txt_observacao.Text;

                    if (txt_total.Text != "")
                    { os.total = txt_total.Text; }
                    else
                    { os.total = "0,00"; }

                    if (cb_pago.Checked == true)
                    { 
                        os.pago = 1; 
                        os.metodo = cmb_pago.Text; 
                    }
                    else os.pago = 0; os.metodo = "";

                    os.dt_cadastro = dtp_cadastro.Value.ToString();

                    if (cb_saida.Checked == true)
                    { os.dt_saida = dtp_saida.Value.ToString("dd/MM/yyyy"); }
                    else
                    { os.dt_saida = null; }

                    os.cadastrar_os();

                    MessageBox.Show("OS Cadastrada");

                    cliente.doc = txt_doc.Text;
                    cliente.quitado();

                    os.index++;
                }
                else
                { MessageBox.Show("Não foi possível achar a placa digitada", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();
            }
            else if (this.Text == "Edição OS")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                cliente cliente = new cliente();

                if (reader.Read())
                {
                    os.placa = cmb_placa.Text;
                    os.cliente = txt_cliente.Text;
                    os.doc = txt_doc.Text;
                    os.km = int.Parse(txt_km.Text);
                    os.observacao = txt_observacao.Text;
                    os.total = txt_total.Text;
                    os.dt_cadastro = dtp_cadastro.Value.ToString();


                    if (cb_oleo.Checked) os.aviso_oleo_dt = dtp_troca_oleo.Value.ToString();
                    else os.aviso_oleo_dt = "";

                    if (cb_filtro.Checked) os.aviso_filtro_dt = dtp_troca_filtro.Value.ToString();
                    else os.aviso_filtro_dt = "";

                    if (dtp_saida.Enabled == true) os.dt_saida = dtp_saida.Value.ToString("dd/MM/yyyy");
                    else os.dt_saida = null;

                    if (cb_pago.Checked == true)
                    {
                        os.pago = 1;
                        os.metodo = cmb_pago.Text;
                    }
                    else os.pago = 0; os.metodo = "";
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();
                 
                try
                {
                    MessageBox.Show(os.aviso_filtro_dt + "\n" + os.aviso_oleo_dt);
                    os.alterar_os();

                    cliente.doc = txt_doc.Text;
                    cliente.quitado();

                    MessageBox.Show("OS Alterada!", "JCMotorsport", MessageBoxButtons.OK);
                }
                catch (Exception a) { MessageBox.Show(a.ToString()); }

                conexao_calendario();
            }
        }

        private void bnt_add_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();

            add add_pecas = new add();
            add_pecas.table = "pecas";
            add_pecas.modo = "os";
            add_pecas.Show();
        }

        private void bnt_add_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add add_servicos = new add();
            add_servicos.table = "servicos";
            add_servicos.modo = "os";
            add_servicos.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (static_class.close == 1)
            {
                lst_servicos.Items.Clear();
                lst_pecas.Items.Clear();

                decimal servico_total = 0;
                decimal peca_total = 0;

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle}' ORDER BY pos ASC", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    string valor = reader.GetString("valor");
                    string desco = reader.GetString("desco");
                    string total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2");

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_servicos.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".",",");

                    try
                    { servico_total += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
                    catch (Exception a) { MessageBox.Show(a.ToString()); }
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{static_class.controle}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    string qtd = reader.GetString("qtd").Replace(".", ",");
                    string valor = reader.GetString("valor");
                    string desco = reader.GetString("desco");
                    string total = ((decimal.Parse(valor) * decimal.Parse(qtd)) - decimal.Parse(desco)).ToString("N2");

                    var item = new ListViewItem(nome);
                    item.SubItems.Add(qtd);
                    item.SubItems.Add(valor);
                    item.SubItems.Add(desco);
                    item.SubItems.Add(total);
                    lst_pecas.Items.Add(item);

                    string qtd_formatado = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { peca_total += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
                    catch { }
                }
                conexao.Close();

                txt_total_servico.Text = servico_total.ToString("N2");
                txt_total_pecas.Text = peca_total.ToString("N2");
                txt_total.Text = (peca_total + servico_total).ToString("N2");

                static_class.close = 0;
            }
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM os WHERE controle = '{static_class.controle}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
            Close();
        }

        private void cb_saida_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_saida.Checked == true)
            { 
                dtp_saida.Enabled = true;
                gb_troca.Visible = true;
                dtp_troca_oleo.Enabled = false;
                dtp_troca_filtro.Enabled = false;
            }
            else
            { 
                dtp_saida.Enabled = false;
                gb_troca.Visible = false;
            }
        }

        private void cb_oleo_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_oleo.Checked == true) dtp_troca_oleo.Enabled = true;
            else dtp_troca_oleo.Enabled = false;
        }

        private void cb_filtro_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_filtro.Checked == true) dtp_troca_filtro.Enabled = true;
            else dtp_troca_filtro.Enabled = false;
        }

        public void conexao_calendario()
        {
            if (cb_oleo.Checked)
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
                if (dtp_troca_oleo.Enabled)
                {
                    Event newEvent = new Event()
                    {
                        Summary = "Próxima troca de óleo " + txt_cliente.Text,
                        Description = "...",
                        Start = new EventDateTime()
                        {
                            DateTime = dtp_troca_oleo.Value,
                            TimeZone = "America/Sao_Paulo",
                        },
                        End = new EventDateTime()
                        {
                            DateTime = dtp_troca_oleo.Value,
                            TimeZone = "America/Sao_Paulo",
                        },
                        Attendees = new EventAttendee[]
                        {
                    new EventAttendee() { Email = "heitorfsv@gmail.com" }
                        },
                    };

                    // Insert the event into the user's calendar
                    EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
                    Event createdEvent = request.Execute();
                }

                if (dtp_troca_filtro.Enabled)
                {
                    Event newEvent = new Event()
                    {
                        Summary = "Próxima troca de filtro " + txt_cliente.Text,
                        Description = "...",
                        Start = new EventDateTime()
                        {
                            DateTime = dtp_troca_filtro.Value,
                            TimeZone = "America/Sao_Paulo",
                        },
                        End = new EventDateTime()
                        {
                            DateTime = dtp_troca_filtro.Value,
                            TimeZone = "America/Sao_Paulo",
                        },
                        Attendees = new EventAttendee[]
                        {
                    new EventAttendee() { Email = "heitorfsv@gmail.com" }
                        },
                    };

                    // Insert the event into the user's calendar
                    EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
                    Event createdEvent = request.Execute();
                }
                this.Close();
            }
        }

        private void visualizarImpressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            try
            { 
                var doc = new osPdf
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

        private void edicao_os_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text == "Cadastro OS")
            {
                int delete = 0;
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT controle FROM os WHERE controle = '{os.index}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                { }
                else
                { delete = 1; }
                conexao.Close();

                if (delete == 1)
                {
                    cmd = new MySqlCommand($"DELETE FROM pecas_os WHERE os = '{os.index}'", conexao);
                    conexao.Open();
                    cmd.ExecuteReader();
                    conexao.Close();

                    cmd = new MySqlCommand($"DELETE FROM servicos_os WHERE os = '{os.index}'", conexao);
                    conexao.Open();
                    cmd.ExecuteReader();
                    conexao.Close();
                }
            }
        }

        private void cb_pago_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_pago.Checked) cmb_pago.Visible = true;
            else cmb_pago.Visible = false;
        }
    }
}
