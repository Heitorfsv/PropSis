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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class add_calendar : Form
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "CalendarioApp";
        public add_calendar()
        {
            InitializeComponent();
        }

        private void add_calendar_Load(object sender, EventArgs e)
        {
            txt_horario1.Enabled = false;
            txt_horario2.Enabled = false;
        }

        public void carregar_data(int day, int month, int year)
        {
            txt_data.Text = day + "/" + month + "/" + year;
        }

        private void cb_horario_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_horario.Checked == true)
            {
                txt_horario1.Enabled = true;
                txt_horario2.Enabled = true;
            }
            else
            {
                txt_horario1.Enabled = false;
                txt_horario2.Enabled = false;
            }
        }

        private void bnt_add_Click(object sender, EventArgs e)
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

            DateTime horario1;
            DateTime horario2;

            // Define the new event
            if (cb_horario.Checked == true)
            {
                horario1 = DateTime.Parse(txt_data.Text + "," + txt_horario1.Text + ":00");
                horario2 = DateTime.Parse(txt_data.Text + "," + txt_horario2.Text + ":00");
            }
            else 
            {
                horario1 = DateTime.Parse(txt_data.Text);
                horario2 = DateTime.Parse(txt_data.Text);
            }

            Event newEvent = new Event()
            {
                Summary = txt_evento.Text,
                Description = "...",
                Start = new EventDateTime()
                {
                    DateTime = horario1,
                    TimeZone = "America/Sao_Paulo",
                },
                End = new EventDateTime()
                {
                    DateTime = horario2,
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
