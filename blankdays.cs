using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class blankdays : UserControl
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API C# App";

        int day;
        int month;
        int year;

        public blankdays()
        {
            InitializeComponent();
        }

        private void blankdays_Load(object sender, EventArgs e)
        {

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

        private void blankdays_DoubleClick(object sender, EventArgs e)
        {
            add_calendar add_calendar = new add_calendar();
            add_calendar.carregar_data(day, month, year);
            add_calendar.Show();
        }

        private void lst_tarefas_DoubleClick(object sender, EventArgs e)
        {
            UserCredential credential;

            // Carregar credenciais
            using (var stream = new FileStream("C:\\credentials\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credenciais salvas em: " + credPath);
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

                bool found = false;
                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        // Verificar se o evento corresponde ao nome específico
                        if (eventItem.Summary != null && eventItem.Summary.Contains(eventNameToSearch))
                        {
                            string when = eventItem.Start.DateTime.HasValue
                                ? eventItem.Start.DateTime.Value.ToString("g")
                                : eventItem.Start.Date;
                            MessageBox.Show($"Evento encontrado: {eventItem.Summary} - {when}");
                            found = true;
                        }
                    }
                }
            }
        }
    }
}
