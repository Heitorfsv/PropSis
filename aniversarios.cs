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
    public partial class aniversarios : Form
    {
        public aniversarios()
        {
            InitializeComponent();
        }

        private void aniversarios_Load(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT * FROM clientes", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime aniversario = DateTime.Parse(reader.GetString("dt_nascimento").Substring(0, 5));
                TimeSpan dif = aniversario - DateTime.Now; 

                if (dif.TotalDays < 15 && dif.TotalDays > 0) 
                { lst_15dias.Items.Add(reader.GetString("nome")); }

                if (dif.TotalDays > -1 && dif.TotalDays < 0.1)
                { lst_hoje.Items.Add(reader.GetString("nome")); }

            }

            try
            {
                lst_15dias.SelectedIndex = 0;
            }
            catch { }

            conexao.Close();
        }
    }
}
