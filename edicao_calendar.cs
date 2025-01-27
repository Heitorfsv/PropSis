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
    public partial class edicao_calendar : Form
    {
        string evento, inicio, fim;
        int year, month, day;

        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API C# App";

        public edicao_calendar()
        {
            InitializeComponent();
        }

        private void consulta_calendar_Load(object sender, EventArgs e)
        {
            txt_horario1.Enabled = false;
            txt_horario2.Enabled = false;
        }

        public void carregar_info(string evento, int year, int month, int day, string inicio, string fim)
        {
            this.evento = evento;
            this.year = year;
            this.month = month;
            this.day = day;
            this.inicio = inicio;
            this.fim = fim;

            txt_evento.Text = evento;
            txt_data.Text = day + "/" + month + "/" + year;
            txt_horario1.Text = inicio;
            txt_horario2.Text = fim;
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
        private void bnt_editar_Click(object sender, EventArgs e)
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
                HttpClientInitializer = credential, ApplicationName = ApplicationName,
            });

            // ID do calendário (use "primary" para o calendário principal)
            string calendarId = "primary";

            //Buscando eventID pra usar depois
            // Nome e data do evento que você quer buscar
            string eventNameToSearch = evento; // Substitua pelo nome do evento
            DateTime eventDate = new DateTime(year, month, day); // Substitua pela data do evento

            // Definir o intervalo de tempo para buscar eventos no dia especificado
            DateTime startTime = eventDate.Date; // Início do dia
            DateTime endTime = eventDate.Date.AddDays(1).AddTicks(-1); // Fim do dia

            // Solicitar eventos no intervalo de tempo
            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = startTime;
            request.TimeMax = endTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();

            var eventItem = events.Items?
                .Where(m => m.Summary != null
                            && m.Summary.Equals(eventNameToSearch, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            // ID do evento a ser editado
            // Substitua pelo ID do evento

            try
                {
                    // Obter o evento existente
                    Event existingEvent = service.Events.Get(calendarId, eventItem.Id).Execute();
                    
                    // Editar nome do evento
                    existingEvent.Summary = txt_evento.Text;
                MessageBox.Show(day + "/" + month + "/" + year + "T" + txt_horario2.Text);
                    // Alterar horário do evento
                    existingEvent.Start = new EventDateTime()
                    {
                        DateTime = DateTime.Parse(year + "/" + month + "/" + day + "," + txt_horario1.Text + ":00"),
                        TimeZone = "America/Sao_Paulo"
                    };
                    existingEvent.End = new EventDateTime()
                    {
                        DateTime = DateTime.Parse(day + "/" + month + "/" + year + "," + txt_horario2.Text + ":00"),
                        TimeZone = "America/Sao_Paulo"
                    };
                    
                    // Enviar a atualização
                    Event updatedEvent = service.Events.Update(existingEvent, calendarId, eventItem.Id).Execute();
                    
                    Close();
                    
                    Console.WriteLine("Evento atualizado com sucesso!");
                    Console.WriteLine($"Título: {updatedEvent.Summary}");
                    Console.WriteLine($"Descrição: {updatedEvent.Description}");
                    Console.WriteLine($"Início: {updatedEvent.Start.DateTime}");
                    Console.WriteLine($"Fim: {updatedEvent.End.DateTime}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar o evento: {ex.Message}");
                }
            
        }

        private void bnt_excluir_Click(object sender, EventArgs e)
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

            // ID do calendário (use "primary" para o calendário principal)
            string calendarId = "primary";

            //Buscando eventID pra usar depois
            // Nome e data do evento que você quer buscar
            string eventNameToSearch = evento; // Substitua pelo nome do evento
            DateTime eventDate = new DateTime(year, month, day); // Substitua pela data do evento

            // Definir o intervalo de tempo para buscar eventos no dia especificado
            DateTime startTime = eventDate.Date; // Início do dia
            DateTime endTime = eventDate.Date.AddDays(1).AddTicks(-1); // Fim do dia

            // Solicitar eventos no intervalo de tempo
            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = startTime;
            request.TimeMax = endTime;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();

            var eventItem = events.Items?
                .Where(m => m.Summary != null
                            && m.Summary.Equals(eventNameToSearch, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            try
            {
                // Chamar o método Delete
                service.Events.Delete(calendarId, eventItem.Id).Execute();
                Console.WriteLine($"Evento com ID '{eventItem.Id}' foi excluído com sucesso.");
                MessageBox.Show($"Evento {txt_evento.Text} foi excluído com sucesso.");
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir o evento: " + ex.Message);
            }
        }
    }
}
