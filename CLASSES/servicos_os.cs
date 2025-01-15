using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoSistema
{
    public class servicos_os
    {
        public int index { get; set; }
        public int os { get; set; }
        public string nome { get; set; }
        public string valor { get; set; }
        public decimal qtd { get; set; }
        public int pos { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM servicos_os ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();
        }

        public void cadastrar_peca_os()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("INSERT INTO servicos_os (controle, os, nome, valor, qtd, pos) values (@controle,@os,@nome,@valor,@qtd,@pos)", conexao);
            cmd.Parameters.AddWithValue("@controle", index);
            cmd.Parameters.AddWithValue("@os", os);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@valor", valor);
            cmd.Parameters.AddWithValue("@qtd", qtd);
            cmd.Parameters.AddWithValue("@pos", pos);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }
    }
}
