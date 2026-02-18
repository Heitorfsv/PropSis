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
        public DateTime prox_oleo, prox_revisao;
        public int troca_oleo, revisao;
        public add_troca(int troca, int revisao)
        {
            InitializeComponent();
            troca_oleo = troca;
            this.revisao = revisao;
        }

        private void add_troca_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            if (troca_oleo == 1) gb_troca.Enabled = true;
            else gb_troca.Enabled = false;

            if (revisao == 1) groupBox1.Enabled = true;
            else groupBox1.Enabled = false;

        }

        private void bnt_salvar_Click(object sender, EventArgs e)
        {
            if (troca_oleo == 1) prox_oleo = dtp_troca_oleo.Value;
            if (revisao == 1) prox_revisao = dtp_revisao.Value;
            Close();
        }
    }
}
