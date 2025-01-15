using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public class servicos
    {
        public int index { get; set; }
        public string nome { get; set; }
        public decimal valor { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM servicos ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();
        }

        public void cadastrar_servicos()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("INSERT INTO servicos (controle, nome, valor) values (@controle,@nome,@valor)", conexao);
            cmd.Parameters.AddWithValue("@controle", index);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@valor", valor);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void alterar_servico()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"UPDATE servicos SET nome = '{nome}', valor = '{valor}' WHERE nome = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }
    }
}
