using Org.BouncyCastle.Pqc.Crypto.Falcon;
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
    public partial class qtd : Form
    {
        public decimal quantidade = 0;
        public string valor = "";
        public qtd()
        {
            InitializeComponent();
        }
            
        private void qtd_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            int posx = Screen.PrimaryScreen.Bounds.Size.Width /2;
            int posy = Screen.PrimaryScreen.Bounds.Size.Height /2;
            Location = new Point(posx - Width, posy - Height);


            txt_valor.Text = valor.ToString();
        }

        private void bnt_ok_Click(object sender, EventArgs e)
        {
            if (txt_qtd.Text != null && txt_valor.Text != null)
            {
                if (decimal.TryParse(txt_qtd.Text, out decimal number))
                {
                    quantidade = decimal.Parse(txt_qtd.Text);
                    valor = txt_valor.Text;
                    Close();
                }
                else { MessageBox.Show("Insira um valor numérico na quantide", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else { MessageBox.Show("Insira a quantidade e o valor", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txt_qtd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
