using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public class cliente
    {
        public int index { get; set; }
        public string nome { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string email { get; set; }
        public string doc { get; set; }
        public int inscricao { get; set; }
        public string telefone { get; set; }
        public string telefone2 { get; set; }
        public string dt_nascimento { get; set; }
        public string cep { get; set; }
        public DateTime dt_cadastro { get; set; }
        public int sujo { get; set; }

        string pesquisa_doc;

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM clientes ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();

        }

        public void cadastrar_cliente()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT nome FROM clientes WHERE doc LIKE '%{doc}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                pesquisa_doc = reader.GetString("nome");
            }
            conexao.Close();

            if (pesquisa_doc == null)
            {
                var cmd2 = new MySqlCommand("INSERT INTO clientes (controle, nome, doc, inscricao, dt_nascimento, telefone, telefone2, email, rua, bairro, cidade, cep, dt_cadastro, sujo) values (@controle,@nome,@doc,@inscricao,@dt_nascimento,@telefone,@telefone2,@email,@rua,@bairro,@cidade,@cep,@dt_cadastro,@sujo)", conexao);
                cmd2.Parameters.AddWithValue("@controle", index);
                cmd2.Parameters.AddWithValue("@nome", nome);
                cmd2.Parameters.AddWithValue("@doc", doc);
                cmd2.Parameters.AddWithValue("@inscricao", inscricao);
                cmd2.Parameters.AddWithValue("@dt_nascimento", dt_nascimento);
                cmd2.Parameters.AddWithValue("@telefone", telefone);
                cmd2.Parameters.AddWithValue("@telefone2", telefone2);
                cmd2.Parameters.AddWithValue("@email", email);
                cmd2.Parameters.AddWithValue("@rua", rua);
                cmd2.Parameters.AddWithValue("@bairro", bairro);
                cmd2.Parameters.AddWithValue("@cidade", cidade);
                cmd2.Parameters.AddWithValue("@cep", cep);
                cmd2.Parameters.AddWithValue("@dt_cadastro", dt_cadastro);
                cmd2.Parameters.AddWithValue("@sujo", sujo);

                conexao.Open();
                cmd2.ExecuteReader();
                conexao.Close();
            }
            else { MessageBox.Show("Este Documento já está cadastrado", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        public void alterar_cliente()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"UPDATE clientes SET nome = '{nome}', doc = '{doc}', inscricao = '{inscricao}', dt_nascimento = '{dt_nascimento}', telefone = '{telefone}', telefone2 = '{telefone2}', email = '{email}', rua = '{rua}', bairro = '{bairro}', cidade = '{cidade}', cep = '{cep}' WHERE controle = {index}", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void quitado()
        {
            int sujo = 0;
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT pago FROM os WHERE doc = '{doc}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read()) 
            { 
                if (reader.GetInt32("pago") == 0)
                { sujo = 1; }
            }
            conexao.Close();

            cmd = new MySqlCommand($"UPDATE clientes SET sujo = '{sujo}' WHERE doc = '{doc}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }
    }
}
