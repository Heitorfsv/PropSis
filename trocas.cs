using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class trocas : Form
    {
        List<string> placa = new List<string>();
        List<string> placa_atrasado = new List<string>();

        List<int> lista_os = new List<int>();
        List<int> lista_atrasado = new List<int>();

        public trocas()
        {
            InitializeComponent();
        }

        private void trocas_Load(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM os WHERE aviso_oleo_dt REGEXP '[A-Za-z0-9]'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (DateTime.Parse(reader.GetString("aviso_oleo_dt")) > DateTime.Now)
                {
                    lista_os.Add(reader.GetInt32("controle"));
                    lst_nome_O.Items.Add(reader.GetString("cliente"));
                    lst_oleo.Items.Add(DateTime.Parse(reader.GetString("aviso_oleo_dt")).ToString("dd/MM/yyyy"));
                    placa.Add(reader.GetString("placa"));
                }
                else if (DateTime.Parse(reader.GetString("aviso_oleo_dt")) < DateTime.Now)
                {
                    lista_atrasado.Add(reader.GetInt32("controle"));
                    lst_nome_Oatrasado.Items.Add(reader.GetString("cliente"));
                    lst_oleo_atrasado.Items.Add(DateTime.Parse(reader.GetString("aviso_oleo_dt")).ToString("dd/MM/yyyy"));
                    placa_atrasado.Add(reader.GetString("placa"));
                }
            }
            conexao.Close();

            int count = 0;

            while (count < placa.Count)
            {   
                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{placa[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read()) 
                {
                    lst_marca_O.Items.Add(reader.GetString("marca"));
                    lst_moto_O.Items.Add(reader.GetString("modelo"));
                }

                conexao.Close();
                count++;
            }

            count = 0;

            while (count < placa_atrasado.Count)
            {
                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{placa_atrasado[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_marca_Oatrasado.Items.Add(reader.GetString("marca"));
                    lst_moto_Oatrasado.Items.Add(reader.GetString("modelo"));
                }

                conexao.Close();
                count++;
            }

            placa.Clear();
            placa_atrasado.Clear();

            cmd = new MySqlCommand($"SELECT * FROM os WHERE aviso_filtro_dt REGEXP '[A-Za-z0-9]'", conexao);

            conexao.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (DateTime.Parse(reader.GetString("aviso_filtro_dt")) > DateTime.Now)
                {
                    lst_nome_F.Items.Add(reader.GetString("cliente"));
                    lst_filtro.Items.Add(DateTime.Parse(reader.GetString("aviso_filtro_dt")).ToString("dd/MM/yyyy"));
                    placa.Add(reader.GetString("placa"));
                }
                else if (DateTime.Parse(reader.GetString("aviso_filtro_dt")) < DateTime.Now)
                {
                    lst_nome_Fatrasado.Items.Add(reader.GetString("cliente"));
                    lst_filtro_atrasado.Items.Add(DateTime.Parse(reader.GetString("aviso_filtro_dt")).ToString("dd/MM/yyyy"));
                    placa_atrasado.Add(reader.GetString("placa"));
                }
            }
            conexao.Close();

            count = 0;

            while (count < placa.Count)
            {
                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{placa[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_marca_F.Items.Add(reader.GetString("marca"));
                    lst_moto_F.Items.Add(reader.GetString("modelo"));
                }

                conexao.Close();
                count++;
            }

            count = 0;

            while (count < placa_atrasado.Count)
            {
                cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{placa_atrasado[count]}'", conexao);

                conexao.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lst_marca_Fatrasado.Items.Add(reader.GetString("marca"));
                    lst_moto_Fatrasado.Items.Add(reader.GetString("modelo"));
                }

                conexao.Close();
                count++;
            }
        }

        private void lst_oleo_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome_Oatrasado.SelectedIndex = lst_oleo_atrasado.SelectedIndex;
            lst_marca_Oatrasado.SelectedIndex = lst_oleo_atrasado.SelectedIndex;
            lst_moto_Oatrasado.SelectedIndex = lst_oleo_atrasado.SelectedIndex;
        }

        private void lst_nome_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo_atrasado.SelectedIndex = lst_nome_Oatrasado.SelectedIndex;
        }

        private void lst_moto_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo_atrasado.SelectedIndex = lst_moto_Oatrasado.SelectedIndex;
        }

        private void lst_oleo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome_O.SelectedIndex = lst_oleo.SelectedIndex;
            lst_marca_O.SelectedIndex = lst_oleo.SelectedIndex;
            lst_moto_O.SelectedIndex = lst_oleo.SelectedIndex;
        }

        private void lst_nome_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo.SelectedIndex = lst_nome_O.SelectedIndex;
        }

        private void lst_moto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo.SelectedIndex = lst_moto_O.SelectedIndex;
        }

        private void lst_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo.SelectedIndex = lst_marca_O.SelectedIndex;
        }

        private void lst_marca_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo_atrasado.SelectedIndex = lst_marca_Oatrasado.SelectedIndex;
        }

        private void lst_filtro_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome_Fatrasado.SelectedIndex = lst_filtro_atrasado.SelectedIndex;
            lst_marca_Fatrasado.SelectedIndex = lst_filtro_atrasado.SelectedIndex;
            lst_moto_Fatrasado.SelectedIndex = lst_filtro_atrasado.SelectedIndex;
        }

        private void lst_nome_Fatrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_filtro_atrasado.SelectedIndex = lst_nome_Fatrasado.SelectedIndex;
        }

        private void lst_marca_Fatrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_filtro_atrasado.SelectedIndex = lst_marca_Fatrasado.SelectedIndex;
        }

        private void lst_moto_Fatrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_filtro_atrasado.SelectedIndex = lst_moto_Fatrasado.SelectedIndex;
        }

        private void lst_filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome_F.SelectedIndex = lst_filtro.SelectedIndex;
            lst_marca_F.SelectedIndex = lst_filtro.SelectedIndex;
            lst_moto_F.SelectedIndex = lst_filtro.SelectedIndex;
        }

        private void lst_nome_F_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_filtro.SelectedIndex = lst_nome_F.SelectedIndex;
        }

        private void lst_marca_F_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_filtro.SelectedIndex = lst_marca_F.SelectedIndex;
        }

        private void lst_moto_F_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_filtro.SelectedIndex = lst_moto_F.SelectedIndex;
        }

        private void lst_nome_Oatrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Oatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_marca_Oatrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Oatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_moto_Oatrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Oatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_oleo_atrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Oatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_filtro_atrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Fatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_nome_Fatrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Fatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_marca_Fatrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Fatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_moto_Fatrasado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_atrasado[lst_nome_Fatrasado.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_oleo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_O.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_nome_O_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_O.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_marca_O_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_O.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_moto_O_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_O.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_filtro_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_F.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_nome_F_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_F.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_marca_F_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_F.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }

        private void lst_moto_F_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                edicao_os edicao_os = new edicao_os();

                static_class.controle_os = lista_os[lst_nome_F.SelectedIndex];

                edicao_os.Show();
            }
            catch { }
        }
    }
}
