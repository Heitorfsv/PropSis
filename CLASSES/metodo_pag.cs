using MySql.Data.MySqlClient;
using PrototipoSistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace classes
{
    public class metodo_pag
    {
        public int index { get; set; }
        public string metodo { get; set; }
        public string agencia { get; set; }
        public int parcelas { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM metodo_pag ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();
        }

        public void cadastrar_metodo()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("INSERT INTO metodo_pag (controle, metodo, banco, parcelas) values (@controle,@metodo,@banco,@parcelas)", conexao);
            cmd.Parameters.AddWithValue("@controle", index);
            cmd.Parameters.AddWithValue("@metodo", metodo);
            cmd.Parameters.AddWithValue("@banco", agencia);
            cmd.Parameters.AddWithValue("@parcelas", parcelas);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();

            MessageBox.Show("Método de pagamento cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void alterar_metodo()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"UPDATE metodo_pag SET metodo = '{metodo}', banco = '{agencia}', parcelas = '{parcelas}' WHERE controle = '{index}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();

            MessageBox.Show("Método de pagamento alterado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
