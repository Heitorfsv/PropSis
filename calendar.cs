using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class calendar : Form
    {

        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "CalendarioApp";

        int month;
        int year;
        string tarefa = "";
        public calendar()
        {
            InitializeComponent();
        }

        private async void calendar_Load(object sender, EventArgs e)
        {
            dayscontainer.Controls.Clear();
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            DisplayDays();
        }

        private async Task DisplayDays()
        {
            bnt_proximo.Enabled = false;
            bnt_anterior.Enabled = false;
            bnt_atualizar.Enabled = false;

            string mes = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbl_mes.Text = mes + " " + year;

            DateTime startmonth = new DateTime(year, month,1);

            int days = DateTime.DaysInMonth(year, month);
            int weekdays = Convert.ToInt32(startmonth.DayOfWeek.ToString("d")) +1;

            for(int i = 1; i < weekdays; i++)
            {
                blank blank = new blank();
                dayscontainer.Controls.Add(blank);
            }

            for (int i = 1; i <= days; i++)
            {
                blankdays blankdays = new blankdays();

                await conexao_calendar(i);
                blankdays.days(i, month, year, tarefa); 

                dayscontainer.Controls.Add(blankdays);
            }

            bnt_proximo.Enabled = true;
            bnt_anterior.Enabled = true;
            bnt_atualizar.Enabled = true;
        }

        private void bnt_proximo_Click(object sender, EventArgs e)
        {
            if (month == 12)
            {
                year++;
                month = 1;
            }
            else
            { month++; }

            dayscontainer.Controls.Clear();

            DisplayDays();
        }
            
        private void bnt_anterior_Click(object sender, EventArgs e)
        {
            if (month == 1)
            {
                year--;
                month = 12;
            }
            else
            { month--; }

            dayscontainer.Controls.Clear();

            DisplayDays();
        }

        public async Task conexao_calendar(int day)
        {
            UserCredential credential;

            using (var stream = new FileStream("C:\\credentials\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true));
            }

            // Cria o serviço da API do Google Calendar
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define o intervalo de tempo para buscar os eventos de hoje
            DateTime startOfDay = new DateTime(year, month, day ,0 ,0 ,0);
            DateTime endOfDay = new DateTime(year, month, day, 23, 59, 59);

            EventsResource.ListRequest request = service.Events.List("jcmotors2020@gmail.com");
            request.TimeMin = startOfDay;
            request.TimeMax = endOfDay;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // Executa a requisição para obter os eventos do dia
            Events events = await request.ExecuteAsync();

            StringBuilder tasks = new StringBuilder();

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string eventName = eventItem.Summary;
                    DateTime? startTime = eventItem.Start.DateTime;
                    DateTime? endTime = eventItem.End.DateTime;

                    tasks.AppendLine(eventName);
                    tasks.AppendLine(new string('-', 30));
                }
            }
            else
            { tasks.AppendLine(" "); }

            tarefa = tasks.ToString();
            //MessageBox.Show(tarefa);
        }

        private void dayscontainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            calendar_Load(sender, e);
        }
    }
}
