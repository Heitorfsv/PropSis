using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoSistema.classes
{
    public class pecas
    {
        public int index { get; set; }
        public string nome { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public decimal valor_pago { get; set; }
        public decimal impostos { get; set; }
        public decimal valor_sugerido { get; set; }
        public string fornecedor { get; set; }
        public string contato { get; set; }
        public string local {  get; set; }
        public string estoque { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM pecas ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();
        }

        public void cadastrar_pecas()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("INSERT INTO pecas (controle, nome, marca, modelo, valor_pago, impostos, valor_sugerido, fornecedor, contato, local, estoque) values (@controle,@nome,@marca,@modelo,@valor_pago,@impostos,@valor_sugerido,@fornecedor,@contato,@local,@estoque)", conexao);
            cmd.Parameters.AddWithValue("@controle", index);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@marca", marca);
            cmd.Parameters.AddWithValue("@modelo", modelo);
            cmd.Parameters.AddWithValue("@valor_pago", valor_pago);
            cmd.Parameters.AddWithValue("@impostos", impostos);
            cmd.Parameters.AddWithValue("@valor_sugerido", valor_sugerido);
            cmd.Parameters.AddWithValue("@fornecedor", fornecedor);
            cmd.Parameters.AddWithValue("@contato", contato);
            cmd.Parameters.AddWithValue("@local", local);
            cmd.Parameters.AddWithValue("@estoque", estoque);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void alterar_pecas()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM pecas WHERE nome = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();

            var cmd2 = new MySqlCommand($"UPDATE pecas SET nome = '{nome}', marca = '{marca}', modelo = '{modelo}', valor_pago = '{valor_pago}', impostos = '{impostos}', valor_sugerido = '{valor_sugerido}', fornecedor = '{fornecedor}', contato = '{contato}', local = '{local}', estoque = '{estoque}' WHERE nome = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd2.ExecuteReader();
            conexao.Close();
        }
    }
}
