using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace classes
{
    internal class orcamento
    {
        public int index { get; set; }
        public string placa { get; set; }
        public int km { get; set; }
        public string cliente { get; set; }
        public string doc { get; set; }
        public string total { get; set; }
        public string dt_cadastro { get; set; }
        public string observacao { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM orcamentos ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();
        }

        public void cadastrar_or()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("INSERT INTO orcamentos (controle, cliente, doc, km, placa, dt_cadastro, total, observacao) values (@controle,@cliente,@doc,@km,@placa,@dt_cadastro,@total,@observacao)", conexao);
            cmd.Parameters.AddWithValue("@controle", index);
            cmd.Parameters.AddWithValue("@cliente", cliente);
            cmd.Parameters.AddWithValue("@doc", doc);
            cmd.Parameters.AddWithValue("@km", km);
            cmd.Parameters.AddWithValue("@placa", placa);
            cmd.Parameters.AddWithValue("@dt_cadastro", dt_cadastro);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@observacao", observacao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void alterar_or()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"UPDATE orcamentos SET cliente = '{cliente}', doc = '{doc}', km = '{km}', placa = '{placa}', dt_cadastro = '{dt_cadastro}', total = '{total}', observacao = '{observacao}' WHERE controle = {index}", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

    }
}
