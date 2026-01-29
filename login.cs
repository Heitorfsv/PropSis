using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class login : Form
    {
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public login()
        {
            InitializeComponent();
        }

        private void bnt_login_Click(object sender, EventArgs e)
        {
            bool logado = verificar_dados(txt_usuario.Text, txt_senha.Text);

            if (logado)
            {
                this.DialogResult = DialogResult.OK; // Isso fecha o login e retorna OK para o Program.cs
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.");
            }
        }
        public bool verificar_dados(string usuarioDigitado, string senhaDigitada, bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(strLocal)
                                        : (System.Data.Common.DbConnection)new MySqlConnection(strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Buscamos apenas pelo usuário
                    cmd.CommandText = "SELECT senha FROM login WHERE usuario = @usuario";

                    var pUsuario = cmd.CreateParameter();
                    pUsuario.ParameterName = "@usuario";
                    pUsuario.Value = usuarioDigitado;
                    cmd.Parameters.Add(pUsuario);

                    // Executamos a leitura
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Recupera o hash que está gravado no banco
                            string hashDoBanco = reader["senha"].ToString();

                            // O BCrypt descriptografa o salt do hash e verifica a senha
                            bool senhaValida = BCrypt.Net.BCrypt.Verify(senhaDigitada, hashDoBanco);

                            return senhaValida;
                        }
                    }
                }
                return false; // Usuário não encontrado ou erro
            }
            catch 
            { 
                if (!usarLocal) return verificar_dados(txt_usuario.Text, txt_senha.Text, true); 
                else return false; MessageBox.Show("Erro ao conectar ao banco de dados.");
            }
        }
            

        private void bnt_sair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
