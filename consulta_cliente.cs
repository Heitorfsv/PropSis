using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Forms;
using ListBox = System.Windows.Forms.ListBox;

namespace PrototipoSistema
{
    public partial class consulta_cliente : Form
    {
        private List<bool> docSujo = new List<bool>();

        public consulta_cliente()
        {
            InitializeComponent();
        }

        public void consulta_Load(object sender, EventArgs e)
        {
            cmb_consulta.SelectedIndex = 0;

            lst_dt_cadastro.Items.Clear();
            lst_nome.Items.Clear();
            lst_doc.Items.Clear();
            lst_telefone.Items.Clear();
            lst_endereco.Items.Clear();
            lst_dt_nascimento.Items.Clear();
            docSujo.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            // Preenche as ListBox
            var cmd = new MySqlCommand("SELECT * FROM clientes", conexao);
            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_dt_cadastro.Items.Add(reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy"));
                lst_nome.Items.Add(reader.GetString("nome"));
                lst_doc.Items.Add(reader.GetString("doc"));
                lst_telefone.Items.Add(reader.GetString("telefone"));
                lst_endereco.Items.Add(reader.GetString("rua") + ", " + reader.GetString("bairro") + ", " + reader.GetString("cidade"));
                try
                { lst_dt_nascimento.Items.Add(reader.GetString("dt_nascimento")); }
                catch { }

                docSujo.Add(reader.GetInt32("sujo") == 0);
            }
            conexao.Close();

            lst_dt_cadastro.DrawMode = DrawMode.OwnerDrawFixed;
            lst_dt_cadastro.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_nome.DrawMode = DrawMode.OwnerDrawFixed;
            lst_nome.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_doc.DrawMode = DrawMode.OwnerDrawFixed;
            lst_doc.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_telefone.DrawMode = DrawMode.OwnerDrawFixed;
            lst_telefone.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_endereco.DrawMode = DrawMode.OwnerDrawFixed;
            lst_endereco.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_dt_nascimento.DrawMode = DrawMode.OwnerDrawFixed;
            lst_dt_nascimento.DrawItem += new DrawItemEventHandler(lst_DrawItem);
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Verifica se o índice é válido
                if (e.Index < 0) return;

                // Identifica a ListBox sendo desenhada
                ListBox listBox = (ListBox)sender;

                // Define a cor com base no status de "pago"
                bool isPago = !docSujo[e.Index]; // true se pago == 1, false se pago == 0
                Color textColor = isPago ? Color.Red : Color.Black;

                // Preenche o fundo
                e.DrawBackground();

                // Desenha o item na cor correta
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(textColor), e.Bounds);

                // Desenha o foco no item se necessário
                e.DrawFocusRectangle();

            }
            catch { }
        }

        private void bnt_pesquisar_Click(object sender, EventArgs e)
        {
            lst_dt_cadastro.Items.Clear();
            lst_nome.Items.Clear();
            lst_doc.Items.Clear();
            lst_telefone.Items.Clear();
            lst_endereco.Items.Clear();
            lst_dt_nascimento.Items.Clear();
            docSujo.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE {cmb_consulta.Text} LIKE '%{txt_pesquisa.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_dt_cadastro.Items.Add(reader.GetDateTime("dt_cadastro").ToString("dd/MM/yyyy"));
                lst_nome.Items.Add(reader.GetString("nome"));
                lst_doc.Items.Add(reader.GetString("doc"));
                lst_telefone.Items.Add(reader.GetString("telefone"));
                lst_endereco.Items.Add(reader.GetString("rua") + ", " + reader.GetString("bairro") + ", " + reader.GetString("cidade"));
                lst_dt_nascimento.Items.Add(reader.GetString("dt_nascimento"));

                docSujo.Add(reader.GetInt32("sujo") == 0);
            }

            conexao.Close();

            lst_dt_cadastro.DrawMode = DrawMode.OwnerDrawFixed;
            lst_dt_cadastro.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_nome.DrawMode = DrawMode.OwnerDrawFixed;
            lst_nome.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_doc.DrawMode = DrawMode.OwnerDrawFixed;
            lst_doc.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_telefone.DrawMode = DrawMode.OwnerDrawFixed;
            lst_telefone.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_endereco.DrawMode = DrawMode.OwnerDrawFixed;
            lst_endereco.DrawItem += new DrawItemEventHandler(lst_DrawItem);

            lst_dt_nascimento.DrawMode = DrawMode.OwnerDrawFixed;
            lst_dt_nascimento.DrawItem += new DrawItemEventHandler(lst_DrawItem);
        }

        private void bnt_atualizar_Click(object sender, EventArgs e)
        {
            consulta_Load(sender, e);
        }

        private void lst_telefone_DoubleClick(object sender, EventArgs e)
        {
            if (lst_telefone.SelectedIndex != -1)
            {
                edicao_cliente tela_cliente = new edicao_cliente();

                static_class.doc_consultar = lst_doc.SelectedItem.ToString();
                tela_cliente.Show();
            }
        }

        private void lst_nome_DoubleClick(object sender, EventArgs e)
        {
            if (lst_nome.SelectedIndex != -1)
            {
                edicao_cliente tela_cliente = new edicao_cliente();

                static_class.doc_consultar = lst_doc.SelectedItem.ToString();
                tela_cliente.Show();
            }
        }

        private void lst_doc_DoubleClick(object sender, EventArgs e)
        {
            if (lst_doc.SelectedIndex != -1)
            {
                edicao_cliente tela_cliente = new edicao_cliente();

                static_class.doc_consultar = lst_doc.SelectedItem.ToString();
                tela_cliente.Show();
            }
        }

        private void lst_dt_nascimento_DoubleClick(object sender, EventArgs e)
        {
            if (lst_dt_nascimento.SelectedIndex != -1)
            {
                edicao_cliente tela_cliente = new edicao_cliente();

                static_class.doc_consultar = lst_doc.SelectedItem.ToString();
                tela_cliente.Show();
            }
        }

        private void lst_doc_Click(object sender, EventArgs e)
        { lst_nome.SelectedIndex = lst_doc.SelectedIndex; }

        private void lst_telefone_Click(object sender, EventArgs e)
        { lst_nome.SelectedIndex = lst_telefone.SelectedIndex; }

        private void lst_nome_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_dt_cadastro.SelectedIndex = lst_nome.SelectedIndex;
            lst_nome.SelectedIndex = lst_nome.SelectedIndex;
            lst_doc.SelectedIndex = lst_nome.SelectedIndex;
            lst_telefone.SelectedIndex = lst_nome.SelectedIndex;
            lst_endereco.SelectedIndex = lst_nome.SelectedIndex;

            try
            { lst_dt_nascimento.SelectedIndex = lst_nome.SelectedIndex; }
            catch { }
        }

        private void lst_nome_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lst_dt_cadastro_DoubleClick(object sender, EventArgs e)
        {
            if (lst_dt_cadastro.SelectedIndex != -1)
            {
                edicao_cliente tela_cliente = new edicao_cliente();

                static_class.doc_consultar = lst_doc.SelectedItem.ToString();
                tela_cliente.Show();
            }
        }

        private void lst_endereco_DoubleClick(object sender, EventArgs e)
        {
            if (lst_endereco.SelectedIndex != -1)
            {
                edicao_cliente tela_cliente = new edicao_cliente();

                static_class.doc_consultar = lst_doc.SelectedItem.ToString();
                tela_cliente.Show();
            }
        }

        private void lst_dt_cadastro_Click(object sender, EventArgs e)
        { lst_nome.SelectedIndex = lst_dt_cadastro.SelectedIndex; }

        private void lst_endereco_Click(object sender, EventArgs e)
        { lst_nome.SelectedIndex = lst_endereco.SelectedIndex; }

        private void lst_dt_nascimento_Click(object sender, EventArgs e)
        { lst_nome.SelectedIndex = lst_dt_nascimento.SelectedIndex; }

        private void bnt_add_Click(object sender, EventArgs e)
        {
            cadastro_cliente cadastro = new cadastro_cliente();
            cadastro.Show();
        }
    }
}
