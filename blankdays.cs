using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class blankdays : UserControl
    {
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

        public void days(int numday, int month, int year, String tarefa)
        {
            lbl_days.Text = numday+"";
            day = numday;
            this.month = month;
            this.year = year;
            lbl_tarefas.Text = tarefa;
        }

        private void blankdays_DoubleClick(object sender, EventArgs e)
        {
            add_calendar add_calendar = new add_calendar();
            add_calendar.carregar_data(day, month, year);
            add_calendar.Show();
        }
    }
}
