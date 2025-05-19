using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using Org.BouncyCastle.Asn1.Ocsp;
using PrototipoSistema.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Globalization;

namespace PrototipoSistema
{
    public partial class edicao_os : Form
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "CalendarioApp";

        string doc_cliente;
        OS os = new OS();
        public edicao_os()
        {
            InitializeComponent();
        }

        private void edicao_os2_Load(object sender, EventArgs e)
        {
            gb_troca.Visible = false;

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM os WHERE controle = '{static_class.controle_os}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

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
                catch (Exception a)
                {
                    dtp_saida.Enabled = false;
                    cb_saida.Checked = false;
                }

                try
                {
                    dtp_troca_filtro.Value = DateTime.ParseExact(reader.GetString("aviso_filtro_dt"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    cb_filtro.Checked = true;
                }
                catch{}

                try
                {
                    dtp_troca_oleo.Value = DateTime.ParseExact(reader.GetString("aviso_oleo_dt"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    cb_oleo.Checked = true;
                }
                catch{}


                if (reader.GetInt32("pago") == 1)
                { cb_pago.Checked = true; }
                else
                { cb_pago.Checked = false; }
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
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();
            decimal total_servico = 0;
            string total = "";

            while (reader.Read())
            {
                lst_servicos.Items.Add(reader.GetString("nome"));
                lst_servicos_qtd.Items.Add(reader.GetString("qtd"));
                total = reader.GetString("valor");
                lst_servico_total.Items.Add(total);

                string qtd = reader.GetString("qtd");
                qtd = qtd.Replace(".", ",");

                total_servico += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd));
            }
            txt_total_servico.Text = total_servico.ToString("N2");
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();
            decimal total_peca = 0;

            while (reader.Read())
            {
                lst_pecas.Items.Add(reader.GetString("nome"));
                lst_pecas_qtd.Items.Add(reader.GetString("qtd"));
                total = reader.GetString("valor");
                lst_peca_total.Items.Add(total);

                string qtd = reader.GetString("qtd");
                qtd = qtd.Replace(".", ",");

                try
                { total_peca += (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
                catch { }
            }
            txt_total_pecas.Text = total_peca.ToString("N2");
            conexao.Close();

            txt_total.Text = (total_peca + total_servico).ToString("N2");

            /////////////////////////////////////////

            cmd = new MySqlCommand($"SELECT * FROM os WHERE ((aviso_filtro_dt REGEXP '[A-Za-z0-9]' OR aviso_oleo_dt REGEXP '[A-Za-z0-9]') AND placa = '{cmb_placa.Text}') AND controle != {static_class.controle_os} ORDER BY dt_cadastro DESC;", conexao); 

            conexao.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            { 
                txt_trocaoleo.Text = reader.GetString("dt_cadastro");
                txt_trocakm.Text = (int.Parse(txt_km.Text) - int.Parse(reader.GetString("km"))).ToString();
            }
            conexao.Close();
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

                if (cb_pago.Checked == true) os.pago = 1;
                else os.pago = 0;
            }
            else
            { MessageBox.Show("Preencha os dados da moto", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            conexao.Close();

            try
            {
                MessageBox.Show(os.aviso_filtro_dt + "\n" + os.aviso_oleo_dt ); 
                os.alterar_os();

                cliente.doc = txt_doc.Text;
                cliente.quitado();

                MessageBox.Show("OS Alterada!", "JCMotorsport", MessageBoxButtons.OK);
            }
            catch (Exception a) { MessageBox.Show(a.ToString()); }

            conexao_calendario();
        }

        private void bnt_add_peca_Click(object sender, EventArgs e)
        {
            lst_pecas.Items.Clear();

            add_pecas add_pecas = new add_pecas();
            add_pecas.Show();
        }

        private void bnt_add_servico_Click(object sender, EventArgs e)
        {
            lst_servicos.Items.Clear();

            add_servicos add_servicos = new add_servicos();
            add_servicos.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (static_class.close == 1)
            {
                lst_servicos.Items.Clear();
                lst_servicos_qtd.Items.Clear();
                lst_servico_total.Items.Clear();

                lst_pecas.Items.Clear();
                lst_pecas_qtd.Items.Clear();
                lst_peca_total.Items.Clear();

                decimal servico_total = 0;
                decimal peca_total = 0;
                string total = "";

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_servicos.Items.Add(reader.GetString("nome"));
                    lst_servicos_qtd.Items.Add(reader.GetString("qtd"));
                    total = reader.GetString("valor");
                    lst_servico_total.Items.Add(total);

                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".",",");

                    try
                    { servico_total += (decimal.Parse(total) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
                    catch (Exception a) { MessageBox.Show(a.ToString()); }
                }
                conexao.Close();

                cmd = new MySqlCommand($"SELECT * FROM pecas_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_pecas.Items.Add(reader.GetString("nome"));
                    lst_pecas_qtd.Items.Add(reader.GetString("qtd"));
                    total = reader.GetString("valor");
                    lst_peca_total.Items.Add(total);

                    string qtd = reader.GetString("qtd");
                    qtd = qtd.Replace(".", ",");

                    try
                    { peca_total += (decimal.Parse(total) * decimal.Parse(qtd)) - decimal.Parse(reader.GetString("desco")); }
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

            var cmd = new MySqlCommand($"DELETE FROM os WHERE controle = '{static_class.controle_os}'", conexao);

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

                this.Close();
            }
        }
    }
}
