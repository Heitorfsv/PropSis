using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class blankdays : UserControl
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API C# App";

        int day, month, year;

        public blankdays()
        {
            InitializeComponent();
        }

        public void days(int numday, int month, int year, List<string> tarefa)
        {
            int count = 0;

            lbl_days.Text = numday+"";
            day = numday;
            this.month = month;
            this.year = year;

            if (tarefa != null)
            {
                while (count < tarefa.Count)
                {
                    lst_tarefas.Items.Add(tarefa[count]);
                    count++;
                }
            }
        }
        private void bnt_add_Click(object sender, EventArgs e)
        {
            edicao_calendar add_calendar = new edicao_calendar();
            add_calendar.Text = "Cadastro de evento";
            add_calendar.carregar_data(day, month, year);

            add_calendar.FormClosed += (s, args) =>
            {
                AtualizarListaTarefas(); // Método para atualizar as tarefas
            };

            add_calendar.Show();
        }

        private void lst_tarefas_DoubleClick(object sender, EventArgs e)
        {
            UserCredential credential;

            // Carregar credenciais
            using (var stream = new FileStream("C:\\credentials\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result; Console.WriteLine("Credenciais salvas em: " + credPath);
            }

            // Criar o serviço do Google Calendar
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Definir a data específica
            DateTime specificDate = new DateTime(year, month, day); // Exemplo: 15 de janeiro de 2025
            DateTime startTime = specificDate.Date; // Início do dia
            DateTime endTime = specificDate.Date.AddDays(1).AddTicks(-1); // Fim do dia

            // Solicitar eventos no intervalo de tempo
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = startTime;
            request.TimeMax = endTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            if (lst_tarefas.SelectedIndex != -1)
            {
                // Executar a consulta
                Events events = request.Execute();
                // Filtro: Nome do evento (opcional)
                string eventNameToSearch = lst_tarefas.SelectedItem.ToString(); // Nome do evento específico


                Console.WriteLine($"Eventos em {specificDate.ToShortDateString()}:");

                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        // Verificar se o evento corresponde ao nome específico
                        if (eventItem.Summary != null && eventItem.Summary.Contains(eventNameToSearch))
                        {
                            string inicio = eventItem.Start.DateTime.HasValue
                                ? eventItem.Start.DateTime.Value.ToString("t")
                                : eventItem.Start.Date;

                            string fim = eventItem.End.DateTime.HasValue
                                ? eventItem.End.DateTime.Value.ToString("t")
                                : eventItem.End.Date;

                            edicao_calendar edicao_calendar = new edicao_calendar();
                            edicao_calendar.Text = "Consulta de evento";
                            edicao_calendar.carregar_info(eventItem.Summary, year, month, day, inicio, fim);

                            edicao_calendar.FormClosed += (s, args) =>
                            {
                                AtualizarListaTarefas(); // Método para atualizar as tarefas
                            };

                            edicao_calendar.Show();
                        }
                    }
                }
            }
        }

        private async void AtualizarListaTarefas()
        {
            try
            {
                lst_tarefas.Items.Clear(); // Limpa a lista atual

                // Configurar autenticação
                UserCredential credential;
                using (var stream = new FileStream("C:\\credentials\\credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true));
                }

                // Criar o serviço do Google Calendar
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Buscar eventos no intervalo de tempo
                DateTime specificDate = new DateTime(year, month, day);
                DateTime startTime = specificDate.Date;
                DateTime endTime = specificDate.Date.AddDays(1).AddTicks(-1);

                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = startTime;
                request.TimeMax = endTime;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                Events events = await request.ExecuteAsync();

                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        if (!string.IsNullOrEmpty(eventItem.Summary))
                        {
                            lst_tarefas.Items.Add(eventItem.Summary);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar tarefas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
