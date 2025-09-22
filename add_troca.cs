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
    public partial class add_troca : Form
    {
        public DateTime oleo, revisao;
        public add_troca()
        {
            InitializeComponent();
        }

        private void add_troca_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void bnt_salvar_Click(object sender, EventArgs e)
        {
            oleo = dtp_troca_oleo.Value;
            revisao = dtp_revisao.Value;
            Close();
        }
    }
}
