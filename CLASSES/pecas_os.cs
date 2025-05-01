using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PrototipoSistema
{
    public class pecas_os
    {
        public int index {  get; set; }
        public int os { get; set; }
        public string nome { get; set; }
        public string valor { get; set; }
        public string desc { get; set; }
        public decimal qtd { get; set; }
        public int pos { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM pecas_os ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();

        }

        public void cadastrar_peca_os()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("INSERT INTO pecas_os (controle, os, nome, valor, qtd, desco, pos) values (@controle,@os,@nome,@valor,@qtd,@desco,@pos)", conexao);
            cmd.Parameters.AddWithValue("@controle", index);
            cmd.Parameters.AddWithValue("@os", os);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@valor", valor);
            cmd.Parameters.AddWithValue("@qtd", qtd);
            cmd.Parameters.AddWithValue("@desco", desc);
            cmd.Parameters.AddWithValue("@pos", pos);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

    }
}
