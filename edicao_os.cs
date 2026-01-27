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

                cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = '{static_class.controle}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    os.index = reader.GetInt32("controle");
                    cmb_placa.Text = reader.GetString("placa");
                    txt_doc.Text = reader.GetString("doc");
                    txt_km.Text = reader.GetInt32("km").ToString();
                    dtp_cadastro.Value = DateTime.Parse(reader.GetString("dt_cadastro"));
                    txt_observacao.Text = reader.GetString("observacao");
                    try { txt_descricao.Text = reader.GetString("descricao"); } catch { }

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
                    txt_chassi.Text = reader.GetString("chassi");
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

                /////////////////////////////////////////

                // Verifica se já existe uma OS com aviso de troca de óleo ou filtro para a placa selecionada
                cmd = new MySqlCommand($"SELECT * FROM os WHERE (aviso_oleo REGEXP '[A-Za-z0-9]' AND placa = '{cmb_placa.Text}') AND controle < {static_class.controle} ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') DESC;", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    try { txt_oleo_dt.Text = reader.GetString("dt_saida"); } catch { }
                    txt_oleo_km.Text = (int.Parse(txt_km.Text) - int.Parse(reader.GetString("km"))).ToString();
                }
                conexao.Close();

                // Verifica se já existe uma OS com aviso de troca de óleo ou filtro para a placa selecionada
                cmd = new MySqlCommand($"SELECT * FROM os WHERE (aviso_revisao REGEXP '[A-Za-z0-9]' AND placa = '{cmb_placa.Text}') AND controle < {static_class.controle} ORDER BY STR_TO_DATE(dt_cadastro, '%d/%m/%y') DESC;", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    try { txt_revisao_dt.Text = reader.GetString("dt_saida"); } catch { }
                    txt_revisao.Text = (int.Parse(txt_km.Text) - int.Parse(reader.GetString("km"))).ToString();
                }
                conexao.Close();
            }
        }

        private void cmb_placa_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            string placa_formata = cmb_placa.Text.Replace(" ", "%");

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa LIKE '%{placa_formata}%'", conexao);
            
            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_placa.Items.Add(reader.GetString("placa"));
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_ano.Text = reader.GetString("ano");
                txt_doc.Text = reader.GetString("doc_dono");
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
            os.placa = cmb_placa.Text;
            os.cliente = txt_cliente.Text;
            os.doc = txt_doc.Text;
            os.km = int.Parse(txt_km.Text);
            os.observacao = txt_observacao.Text;
            os.descricao = txt_descricao.Text;
            os.dt_cadastro = dtp_cadastro.Value.ToString();

            if (cb_pago.Checked == true)
            {
                os.pago = 1;
                os.metodo = cmb_pago.Text;
            }
            else os.pago = 0; os.metodo = "";

            if (dtp_saida.Enabled == true) os.dt_saida = dtp_saida.Value.ToString("dd/MM/yyyy");
            else os.dt_saida = null;

            //verifica se foi feita troca de oleo e revisão
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            add_troca troca = new add_troca();
            foreach (ListViewItem item in lst_pecas.Items)
            {
                var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE nome = '{item.Text}' AND troca_oleo = 1", conexao); 

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) troca_oleo = 1;
                conexao.Close();
            }
            foreach (ListViewItem item in lst_servicos.Items)
            {
                if (item.Text.Contains("REVISÃO"))  revisao = 1;
            }
            if (troca_oleo == 1 || revisao == 1)
            {
                troca.ShowDialog();

                prox_oleo = troca.oleo;
                os.aviso_oleo = prox_oleo.ToString();

                prox_revisao = troca.revisao;
                os.aviso_revisao = prox_revisao.ToString();
            }

            if (this.Text == "Cadastro OS")
            {
                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                cliente cliente = new cliente();

                if (!reader.Read()) { MessageBox.Show("Não foi possível achar a placa digitada", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                conexao.Close();

                if (txt_total.Text != "") os.total = txt_total.Text; 
                    else os.total = "0,00"; 

                os.dt_cadastro = dtp_cadastro.Value.ToString();

                os.cadastrar_os();
                verificar_itens();

                cliente.doc = txt_doc.Text;
                cliente.quitado();
                os.index++;
            }
            else if (this.Text == "Edição OS")
            {
                var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{cmb_placa.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                cliente cliente = new cliente();

                if (reader.Read())
                {
                    os.total = txt_total.Text;
                }
                else
                { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                conexao.Close();
                try
                {
                    os.alterar_os();
                    verificar_itens();

                    cliente.doc = txt_doc.Text;
                    cliente.quitado();
                }
                catch (Exception a) { MessageBox.Show(a.ToString()); }

             //  conexao_calendario();
            }
            atualizar_posicoes();
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
                    var cmd = new MySqlCommand($"SELECT * FROM {table} WHERE nome = '{item.Text}' AND os = {static_class.controle}", conexao);
                    conexao.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) { }
                    else
                    {
                        if (table == "pecas_os")
                        {
                            pecas_os.ultimo_index();
                            pecas_os.index++;
                            pecas_os.modo = "os";

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
                            servicos_os.modo = "os";

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
                            $"UPDATE pecas_os SET pos = '{i}' WHERE os = {static_class.controle} AND nome = '{lst_pecas.Items[i].Text}'",
                            conexao);
                        conexao.Open();
                        cmd.ExecuteNonQuery();
                        conexao.Close();
                    }
                    for (int i = 0; i < lst_servicos.Items.Count; i++)
                    {
                        var cmd = new MySqlCommand(
                            $"UPDATE servicos_os SET pos = '{i}' WHERE os = {static_class.controle} AND nome = '{lst_servicos.Items[i].Text}'",
                            conexao);
                        conexao.Open();
                        cmd.ExecuteNonQuery();
                        conexao.Close();
                    }
                }
            }
            catch { }
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

                if (!reader.Read()) delete = 1; 

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
