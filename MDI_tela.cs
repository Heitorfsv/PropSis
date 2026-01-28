using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            verificar_banco_local();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            try
            {
                string lista_aniversarios_futuros = "";
                string lista_aniversarios = "";

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand("SELECT * FROM clientes WHERE dt_nascimento REGEXP '[A-Za-z0-9]'", conexao);

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
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com o banco de dados reemoto. \r\n\r\n" + ex.Message, "Erro de conexão");
            }

        }

        public void verificar_banco_local()
        {
            string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

            using (var conexao = new System.Data.SQLite.SQLiteConnection(strLocal))
            {
                conexao.Open();
                var cmd = conexao.CreateCommand();

                // 1. Lista de comandos INDIVIDUAIS (Garante que se um falhar, os outros tentam)
                string[] comandosCriacao = {
            "CREATE TABLE IF NOT EXISTS clientes (controle INTEGER PRIMARY KEY, dt_cadastro TEXT, nome TEXT, nome_fantasia TEXT, doc TEXT, inscricao TEXT, dt_nascimento TEXT, telefone TEXT, telefone2 TEXT, email TEXT, rua TEXT, bairro TEXT, cidade TEXT, cep TEXT, sujo INTEGER)",
            "CREATE TABLE IF NOT EXISTS motos (controle INTEGER PRIMARY KEY, placa TEXT, marca TEXT, modelo TEXT, cor TEXT, ano TEXT, chassi TEXT, dt_registro TEXT, doc_dono TEXT, observacao TEXT)",
            "CREATE TABLE IF NOT EXISTS orcamentos (controle INTEGER PRIMARY KEY, cliente TEXT, doc TEXT, km TEXT, placa TEXT, dt_cadastro TEXT, total TEXT, observacao TEXT)",
            "CREATE TABLE IF NOT EXISTS os (controle INTEGER PRIMARY KEY, placa TEXT, km TEXT, cliente TEXT, doc TEXT, observacao TEXT, descricao TEXT, total TEXT, dt_cadastro TEXT, aviso_oleo TEXT, aviso_revisao TEXT, dt_saida TEXT, pago INTEGER, metodo_pag TEXT)",
            "CREATE TABLE IF NOT EXISTS pecas (controle INTEGER PRIMARY KEY, nome TEXT, marca TEXT, modelo TEXT, valor_pago TEXT, impostos TEXT, valor_sugerido TEXT, fornecedor TEXT, contato TEXT, local TEXT, estoque TEXT)",
            "CREATE TABLE IF NOT EXISTS pecas_os (controle INTEGER PRIMARY KEY, os TEXT, orca TEXT, nome TEXT, valor TEXT, qtd TEXT, desco TEXT, pos TEXT)",
            "CREATE TABLE IF NOT EXISTS servicos (controle INTEGER PRIMARY KEY, nome TEXT, valor TEXT)",
            "CREATE TABLE IF NOT EXISTS servicos_os (controle INTEGER PRIMARY KEY, os TEXT, orca TEXT, nome TEXT, valor TEXT, qtd TEXT, desco TEXT, pos TEXT)",
            "CREATE TABLE IF NOT EXISTS metodo_pag (controle INTEGER PRIMARY KEY, metodo TEXT, banco TEXT, parcelas TEXT)"
        };

                foreach (var sql in comandosCriacao)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                // 2. Inserts de teste (Mantido sua lógica)
                string[] inserts = {
            "INSERT OR IGNORE INTO clientes (controle, nome, doc, dt_cadastro, sujo) VALUES (0, 'REGISTRO TESTE', '0', '2024-01-01', 0)",
            "INSERT OR IGNORE INTO motos (controle, placa, doc_dono) VALUES (0, 'TESTE-0000', '0')",
            "INSERT OR IGNORE INTO orcamentos (controle, km, cliente) VALUES (0, '0', 'TESTE')",
            "INSERT OR IGNORE INTO os (controle, placa, km, cliente, doc) VALUES (0, '000-0000', '0', 'TESTE', '0')",
            "INSERT OR IGNORE INTO pecas (controle, nome) VALUES (0, 'PEÇA TESTE')",
            "INSERT OR IGNORE INTO pecas_os (controle, nome, os, orca) VALUES (0, 'ITEM TESTE', '0', '0')",
            "INSERT OR IGNORE INTO servicos (controle, nome) VALUES (0, 'SERVIÇO TESTE')",
            "INSERT OR IGNORE INTO servicos_os (controle, nome, os, orca) VALUES (0, 'ITEM TESTE', '0', '0')",
            "INSERT OR IGNORE INTO metodo_pag (controle, metodo) VALUES (0, 'DINHEIRO')"
        };

                foreach (var sql in inserts)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void cadastroPeçaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edicao_pecas cadastro_peca = new edicao_pecas();
            cadastro_peca.Text = "Cadastro peças";

            cadastro_peca.MdiParent = this;
            cadastro_peca.Show();
        }

        private void cadastroClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edicao_cliente cadastro_cliente = new edicao_cliente();
            cadastro_cliente.Text = "Cadastro Cliente";
            cadastro_cliente.MdiParent = this;
            cadastro_cliente.Show();
        }
        private void cadastrarServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edicao_servicos cadastro_servico = new edicao_servicos();
            cadastro_servico.Text = "Cadastro serviços";
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
            edicao_motos cadastro_moto = new edicao_motos();
            cadastro_moto.Text = "Cadastro Moto";

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
            edicao_os cadastro_os = new edicao_os();
            cadastro_os.Text = "Cadastro OS";

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

        private void trocaDeOleoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trocas trocas = new trocas();
            trocas.MdiParent = this;
            trocas.Show();
        }

        private void cadastroOrçamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_or cadastro_or = new cadastro_or();
            cadastro_or.Text = "Cadastro orçamento";
            cadastro_or.MdiParent = this;
            cadastro_or.Show();
        }

        private void consultarOrçamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta_or consulta_or = new consulta_or();
            consulta_or.MdiParent = this;
            consulta_or.Show();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_pagamento cadastro = new cadastro_pagamento();
            cadastro.Text = "Cadastro metodo de pagamento";
            cadastro.MdiParent = this;
            cadastro.Show();
        }

        private void bnt_os_Click(object sender, EventArgs e)
        {
            consulta_os consulta_os = new consulta_os();
            consulta_os.MdiParent = this;
            consulta_os.Show();
        }

        private void bnt_motos_Click(object sender, EventArgs e)
        {
            consulta_motos consulta_motos = new consulta_motos();
            consulta_motos.MdiParent = this;
            consulta_motos.Show();
        }

        private void bnt_pecas_Click(object sender, EventArgs e)
        {
            consulta_pecas consulta_pecas = new consulta_pecas();
            consulta_pecas.MdiParent = this;
            consulta_pecas.Show();
        }

        private void bnt_servicos_Click(object sender, EventArgs e)
        {
            consulta_servicos consulta_servico = new consulta_servicos();
            consulta_servico.MdiParent = this;
            consulta_servico.Show();
        }

        private void bnt_oleo_Click(object sender, EventArgs e)
        {
            trocas trocas = new trocas();
            trocas.MdiParent = this;
            trocas.Show();
        }

        private void bnt_calendario_Click(object sender, EventArgs e)
        {
            calendar calendar = new calendar();
            calendar.MdiParent = this;
            calendar.Show();
        }

        private void bnt_pag_Click(object sender, EventArgs e)
        {
            cadastro_pagamento cadastro = new cadastro_pagamento();
            cadastro.Text = "Cadastro metodo de pagamento";
            cadastro.MdiParent = this;
            cadastro.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            consulta_cliente consulta = new consulta_cliente();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
