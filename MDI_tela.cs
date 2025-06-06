using MySql.Data.MySqlClient;
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
    public partial class MDI_tela : Form
    {
        public MDI_tela()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Arquivos de texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Arquivos de texto (*.txt)|*.txt|Todos os arquivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void consultaDeClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_cliente consulta = new consulta_cliente();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void MDI_tela_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            string lista_aniversarios_futuros = "";
            string lista_aniversarios = "";

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM clientes", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            { 
                try
                {
                    DateTime aniversario = DateTime.Parse(reader.GetString("dt_nascimento").Substring(0, 5));
                    TimeSpan dif = aniversario - DateTime.Now;

                    if (dif.TotalDays < 15 && dif.TotalDays > 0) lista_aniversarios_futuros = lista_aniversarios_futuros + "- " + reader.GetString("nome") + " (" + aniversario.ToString("dd/MM/yyyy") + ")" + "\r\n";
                    
                    if (dif.TotalDays > -1 && dif.TotalDays < 0.1) lista_aniversarios = lista_aniversarios + "- " + reader.GetString("nome") + " (" + aniversario.ToString("dd/MM/yyyy") + ")" + "\r\n";
                }
                catch { }
            }
            conexao.Close();

            if (lista_aniversarios_futuros != "") MessageBox.Show("Os aniversários de:\r\n\r\n" + lista_aniversarios_futuros + "\r\nEstão chegando", "Aniversários");

            if (lista_aniversarios != "") MessageBox.Show("Os aniversários de:\r\n\r\n" + lista_aniversarios + "\r\nSão hoje", "Aniversários");

        }

        private void cadastroPeçaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_pecas cadastro_peca = new cadastro_pecas();
            cadastro_peca.MdiParent = this;
            cadastro_peca.Show();
        }

        private void cadastroClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_cliente cadastro_cliente = new cadastro_cliente();
            cadastro_cliente.MdiParent = this;
            cadastro_cliente.Show();
        }
        private void cadastrarServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_servicos cadastro_servico = new cadastro_servicos();
            cadastro_servico.MdiParent = this;
            cadastro_servico.Show();
        }

        private void consultaServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_servicos consulta_servico = new consulta_servicos();
            consulta_servico.MdiParent = this;
            consulta_servico.Show();
        }

        private void consultaPeçasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_pecas consulta_pecas = new consulta_pecas();
            consulta_pecas.MdiParent = this;
            consulta_pecas.Show();
        }

        private void consultarMotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_motos consulta_motos = new consulta_motos();
            consulta_motos.MdiParent = this;
            consulta_motos.Show();
        }

        private void cadastrarMotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_moto cadastro_moto = new cadastro_moto();
            cadastro_moto.MdiParent = this;
            cadastro_moto.Show();
        }

        private void consultaFichasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_os consulta_os = new consulta_os();
            consulta_os.MdiParent = this;
            consulta_os.Show();
        }

        private void cadastrarFichaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_os cadastro_os = new cadastro_os();
            cadastro_os.MdiParent = this;
            cadastro_os.Show();
        }

        private void aniversáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aniversarios aniversarios = new aniversarios();
            aniversarios.MdiParent = this;
            aniversarios.Show();
        }

        private void calendarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar calendar = new calendar();
            calendar.MdiParent = this;
            calendar.Show();
        }

        private void windowsMenu_Click(object sender, EventArgs e)
        {

        }

        private void trocaDeOleoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocas trocas = new trocas();
            trocas.MdiParent = this;
            trocas.Show();
        }

        private void cadastroOrçamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_or cadastro_or = new cadastro_or();
            cadastro_or.MdiParent = this;
            cadastro_or.Show();
        }

        private void consultarOrçamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_or consulta_or = new consulta_or();
            consulta_or.MdiParent = this;
            consulta_or.Show();
        }
    }
}
