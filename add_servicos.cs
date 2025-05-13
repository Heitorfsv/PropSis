using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class add_servicos : Form
    {
        servicos_os servicos_os = new servicos_os();
        public add_servicos()
        {
            InitializeComponent();
        }

        private void add_servicos_Load(object sender, EventArgs e)
        {
            decimal total = 0;
            string valor = "";
            lst_pesquisa.Items.Clear();

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);
            
            var cmd = new MySqlCommand($"SELECT * FROM servicos", conexao);
            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst_pesquisa.Items.Add(reader.GetString("nome"));
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM servicos_os WHERE os = '{static_class.controle_os}' ORDER BY pos ASC", conexao);
            conexao.Open();
            reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                lst_servicos.Items.Add(reader.GetString("nome"));
                lst_qtd.Items.Add(reader.GetString("qtd"));
                valor = reader.GetString("valor");
                lst_valor.Items.Add(valor);

                string qtd = reader.GetString("qtd");
                qtd = qtd.Replace(".", ",");

                try
                { total = (decimal.Parse(reader.GetString("valor")) * decimal.Parse(qtd)); }
                catch { }
                lst_total.Items.Add(total.ToString("N2"));
            }
            total = 0;
            conexao.Close();

            try
            {
                lst_total.SelectedIndex = 0;
                while (lst_total.SelectedIndex < lst_total.Items.Count - 1)
                {
                    total += decimal.Parse(lst_total.SelectedItem.ToString());
                    lst_total.SelectedIndex += 1;   
                } 
                if (lst_total.SelectedIndex == lst_total.Items.Count - 1)
                {
                    total += decimal.Parse(lst_total.SelectedItem.ToString());
                }
                txt_total.Text = total.ToString("N2");
            }
            catch { }
        }

        private void txt_pesquisa_TextChanged(object sender, EventArgs e)
        {
            lst_pesquisa.Items.Clear();

            if (txt_pesquisa.Text != "")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos WHERE nome LIKE '%{txt_pesquisa.Text}%'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_pesquisa.Items.Add(reader.GetString("nome"));
                }
                conexao.Close();
            }
            else
            {
                lst_pesquisa.Items.Clear();

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lst_pesquisa.Items.Add(reader.GetString("nome"));
                }
                conexao.Close();
            }
        }

        private void lst_pesquisa_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                object servico = lst_pesquisa.SelectedItem;
                string valor = "";
                string desc = "";

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM servicos WHERE nome = '{servico}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                { valor = reader.GetString("valor"); }


                qtd qtd_tela = new qtd();
                qtd_tela.valor = valor;
                qtd_tela.ShowDialog();

                if (qtd_tela.quantidade > 0)
                {
                    decimal total = 0;
                    decimal qtd = qtd_tela.quantidade;
                    valor = qtd_tela.valor;
                    desc = qtd_tela.desc;

                    lst_servicos.Items.Add(servico);
                    lst_qtd.Items.Add(qtd_tela.quantidade);

                    lst_valor.Items.Add(valor);

                    try
                    {
                        string qtd_formatado = qtd.ToString();
                        qtd_formatado = qtd_formatado.Replace(".", ",");
                        lst_total.Items.Add(((decimal.Parse(valor) * qtd) - decimal.Parse(desc)).ToString("N2"));
                    }
                    catch { lst_total.Items.Add(valor); }

                    lst_total.SelectedIndex = 0;
                    while (lst_total.SelectedIndex < lst_total.Items.Count - 1)
                    {
                        total += decimal.Parse(lst_total.SelectedItem.ToString());
                        lst_total.SelectedIndex += 1;
                    }
                    try
                    {
                        if (lst_total.SelectedIndex == lst_total.Items.Count - 1)
                        {
                            total += decimal.Parse(lst_total.SelectedItem.ToString());
                        }
                    }
                    catch { }
                    txt_total.Text = total.ToString("N2");

                    conexao.Close();
                    ///
                    servicos_os.ultimo_index();
                    servicos_os.index++;
                    servicos_os.os = static_class.controle_os;
                    servicos_os.nome = servico.ToString();
                    servicos_os.valor = valor;
                    servicos_os.qtd = qtd_tela.quantidade;
                    servicos_os.desc = desc;
                    servicos_os.pos = lst_servicos.Items.Count - 1;

                    servicos_os.cadastrar_servico_os();
                }
            }
            catch (Exception a) { MessageBox.Show(a.ToString()); }
        }

        private void add_servicos_FormClosing(object sender, FormClosingEventArgs e)
        {
            static_class.close = 1;
            try
            {
                lst_servicos.SelectedIndex = 0;

                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                while (lst_servicos.SelectedIndex < lst_servicos.Items.Count)
                {
                    var cmd = new MySqlCommand($"UPDATE servicos_os SET pos = '{lst_servicos.SelectedIndex}' WHERE os = {static_class.controle_os} AND nome = '{lst_servicos.SelectedItem}'", conexao);
                    conexao.Open();
                    cmd.ExecuteReader();
                    conexao.Close();

                    lst_servicos.SelectedIndex += 1;
                }
            }
            catch { }
        }

        private void bnt_delete_Click(object sender, EventArgs e)
        {
            decimal total = 0;
            if (lst_servicos.SelectedIndex != -1)
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"DELETE FROM servicos_os WHERE nome = '{lst_servicos.SelectedItem}' AND os = {static_class.controle_os}", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                conexao.Close();

                int selected = lst_servicos.SelectedIndex;
                lst_servicos.Items.RemoveAt(selected);
                lst_qtd.Items.RemoveAt(selected);
                lst_valor.Items.RemoveAt(selected);
                lst_total.Items.RemoveAt(selected);
            }

            try
            {
                lst_total.SelectedIndex = 0;
                while (lst_total.SelectedIndex < lst_total.Items.Count - 1)
                {
                    total += decimal.Parse(lst_total.SelectedItem.ToString());
                    lst_total.SelectedIndex += 1;
                }
                if (lst_total.SelectedIndex == lst_total.Items.Count - 1)
                {
                    total += decimal.Parse(lst_total.SelectedItem.ToString());
                }
                txt_total.Text = total.ToString("N2");
            }
            catch { txt_total.Text = "0"; }
        }

        private void lst_servicos_KeyDown(object sender, KeyEventArgs e)
        {
            int current_index = lst_servicos.SelectedIndex;
            lst_qtd.SelectedIndex = current_index;
            lst_valor.SelectedIndex = current_index;
            lst_total.SelectedIndex = current_index;

            object current_servico = lst_servicos.SelectedItem;
            object current_qtd = lst_qtd.SelectedItem;
            object current_value = lst_valor.SelectedItem;
            object current_total = lst_total.SelectedItem;

            if (e.KeyCode == Keys.Up)
            {
                if (lst_servicos.SelectedIndex > 0)
                {
                    lst_servicos.Items.RemoveAt(current_index);
                    lst_qtd.Items.RemoveAt(current_index);
                    lst_valor.Items.RemoveAt(current_index);
                    lst_total.Items.RemoveAt(current_index);

                    lst_servicos.Items.Insert(current_index - 1, current_servico);
                    lst_qtd.Items.Insert(current_index - 1, current_qtd);
                    lst_valor.Items.Insert(current_index - 1, current_value);
                    lst_total.Items.Insert(current_index - 1, current_total);

                    lst_servicos.SelectedIndex = current_index - 1;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (lst_servicos.SelectedIndex < lst_servicos.Items.Count - 1)
                {
                    lst_servicos.Items.RemoveAt(current_index);
                    lst_qtd.Items.RemoveAt(current_index);
                    lst_valor.Items.RemoveAt(current_index);
                    lst_total.Items.RemoveAt(current_index);
                    lst_servicos.Items.Insert(current_index + 1, current_servico);
                    lst_qtd.Items.Insert(current_index + 1, current_qtd);
                    lst_valor.Items.Insert(current_index + 1, current_value);
                    lst_total.Items.Insert(current_index + 1, current_total);

                    lst_servicos.SelectedIndex = current_index + 1;
                }
            }
        }
    }
}
