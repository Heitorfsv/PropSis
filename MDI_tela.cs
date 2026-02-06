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
            timer_sincronia.Interval = 60000; // 60 segundos
            timer_sincronia.Enabled = true;
            timer_sincronia.Start();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            try
            {
                string lista_aniversarios_futuros = "";
                string lista_aniversarios = "";

                var conexao = new MySqlConnection(static_class.strConexao);

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

        private async void timer_sincronia_Tick(object sender, EventArgs e)
        {
            // 1. Para o timer para não encavalar uma sincronia na outra
            timer_sincronia.Stop();

            // 2. Cria uma variável para controlar se deu erro
            bool sucesso = false;

            // 3. O Task.Run joga a carga pesada para outra linha de processamento (Thread)
            await Task.Run(() =>
            {
                try
                {
                    // Chama sua função mestre da classe estática
                    ExecutarSincronizacaoGlobal();
                    sucesso = true;
                }
                catch (Exception ex)
                {
                    // Aqui você pode logar o erro, mas não exiba MessageBox (pode travar o timer)
                    Console.WriteLine("Erro na sincronia automática: " + ex.Message);
                    sucesso = false;
                }
            });

            // 4. Se você tiver uma Label de status no MDI, pode atualizar assim:
            if (sucesso)
            {
                lbl_status_sync.Text = "Sincronizado: " + DateTime.Now.ToString("HH:mm");
                lbl_status_sync.ForeColor = Color.Green;
            }
            else
            {
                lbl_status_sync.Text = "Aguardando Conexão...";
                lbl_status_sync.ForeColor = Color.Red;
            }

            // 5. Reinicia o timer para a próxima volta
            timer_sincronia.Start();
        }

        public static void ExecutarSincronizacaoGlobal()
        {
            // --- DEFINIÇÃO DAS COLUNAS (BATENDO COM A INFO FORNECIDA) ---

            string[] colunasClientes = {
        "controle", "dt_cadastro", "nome", "nome_fantasia", "doc",
        "inscricao", "dt_nascimento", "telefone", "telefone2",
        "email", "rua", "bairro", "cidade", "cep", "sujo"
    };

            string[] colunasMotos = {
        "controle", "placa", "marca", "modelo", "cor",
        "ano", "chassi", "dt_registro", "doc_dono", "observacao"
    };

            string[] colunasMetodo = { "controle", "metodo", "agencia", "parcelas" };

            string[] colunasPecas = {
        "controle", "nome", "marca", "modelo", "valor_pago",
        "impostos", "valor_sugerido", "fornecedor", "contato",
        "local", "estoque", "troca_oleo"
    };

            string[] colunasServicos = { "controle", "nome", "valor" };

            string[] colunasOS = {
        "controle", "placa", "km", "cliente", "doc", "observacao",
        "descricao", "total", "dt_cadastro", "aviso_oleo",
        "aviso_revisao", "dt_saida", "pago", "metodo_pag"
    };

            string[] colunasOrc = {
        "controle", "cliente", "doc", "km", "placa",
        "dt_cadastro", "total", "observacao"
    };

            // Note: Usei "orca" como campo de vínculo conforme sua estrutura
            string[] colunasItensOS = {
        "controle", "os", "orca", "nome", "valor", "qtd", "desco", "pos"
    };

            // --- 1º PASSO: CADASTROS INDEPENDENTES ---
            static_class.SincronizarTabelaUniversal("clientes", colunasClientes);
            static_class.SincronizarTabelaUniversal("metodo_pag", colunasMetodo);
            static_class.SincronizarTabelaUniversal("pecas", colunasPecas);
            static_class.SincronizarTabelaUniversal("servicos", colunasServicos);

            // --- 2º PASSO: MOTOS (Dependem de Clientes) ---
            static_class.SincronizarTabelaUniversal("motos", colunasMotos);

            // --- 3º PASSO: ORÇAMENTOS E OS (Pais) ---
            // Se o ID mudar, a função re-vincula os filhos usando a coluna "orca" ou "os"
            static_class.SincronizarTabelaUniversal("orcamentos", colunasOrc, "orca");
            static_class.SincronizarTabelaUniversal("os", colunasOS, "os");

            // --- 4º PASSO: ITENS (Filhos) ---
            static_class.SincronizarTabelaUniversal("pecas_os", colunasItensOS);
            static_class.SincronizarTabelaUniversal("servicos_os", colunasItensOS);
        }
    }
}
