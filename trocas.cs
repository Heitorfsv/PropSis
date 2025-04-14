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
                    lst_nome.Items.Add(reader.GetString("cliente"));
                    lst_oleo.Items.Add(DateTime.Parse(reader.GetString("aviso_oleo_dt")).ToString("dd/MM/yyyy"));
                    placa.Add(reader.GetString("placa"));
                }
                else if (DateTime.Parse(reader.GetString("aviso_oleo_dt")) < DateTime.Now)
                {
                    lst_nome_atrasado.Items.Add(reader.GetString("cliente"));
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
                    lst_marca.Items.Add(reader.GetString("marca"));
                    lst_moto.Items.Add(reader.GetString("modelo"));
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
                    lst_marca_atrasado.Items.Add(reader.GetString("marca"));
                    lst_moto_atrasado.Items.Add(reader.GetString("modelo"));
                }

                conexao.Close();
                count++;
            }
        }

        private void lst_oleo_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome_atrasado.SelectedIndex = lst_oleo_atrasado.SelectedIndex;
            lst_marca_atrasado.SelectedIndex = lst_oleo_atrasado.SelectedIndex;
            lst_moto_atrasado.SelectedIndex = lst_oleo_atrasado.SelectedIndex;
        }

        private void lst_nome_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo_atrasado.SelectedIndex = lst_nome_atrasado.SelectedIndex;
        }

        private void lst_moto_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo_atrasado.SelectedIndex = lst_moto_atrasado.SelectedIndex;
        }

        private void lst_oleo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_nome.SelectedIndex = lst_oleo.SelectedIndex;
            lst_marca.SelectedIndex = lst_oleo.SelectedIndex;
            lst_moto.SelectedIndex = lst_oleo.SelectedIndex;
        }

        private void lst_nome_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo.SelectedIndex = lst_nome_atrasado.SelectedIndex;
        }

        private void lst_moto_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo.SelectedIndex = lst_moto_atrasado.SelectedIndex;
        }

        private void lst_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo.SelectedIndex = lst_marca.SelectedIndex;
        }

        private void lst_marca_atrasado_SelectedIndexChanged(object sender, EventArgs e)
        {
            lst_oleo_atrasado.SelectedIndex = lst_marca_atrasado.SelectedIndex;
        }
    }
}
