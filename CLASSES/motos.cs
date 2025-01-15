using Microsoft.Office.Interop.Excel;
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

namespace PrototipoSistema.classes
{
    public class motos
    {
        public int index { get; set; }
        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string cor {  get; set; }
        public string ano { get; set; }
        public string chassi { get; set; }
        public DateTime dt_registro { get; set; }
        public string doc_dono { get; set; }
        public string observacao { get; set; }

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM motos ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();

        }

        public void cadastrar_moto()
        {

            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);
               
            var cmd = new MySqlCommand("INSERT INTO motos (controle, placa, marca, modelo, cor, ano, chassi, dt_registro, doc_dono, observacao) values (@controle,@placa,@marca,@modelo,@cor,@ano,@chassi,@dt_registro,@doc_dono,@observacao)", conexao); 
            
            cmd.Parameters.AddWithValue("@controle", index);  
            cmd.Parameters.AddWithValue("@placa", placa);
            cmd.Parameters.AddWithValue("@marca", marca);
            cmd.Parameters.AddWithValue("@modelo", modelo);
            cmd.Parameters.AddWithValue("@cor", cor);
            cmd.Parameters.AddWithValue("@ano", ano);
            cmd.Parameters.AddWithValue("@chassi", chassi);
            cmd.Parameters.AddWithValue("@dt_registro", dt_registro);
            cmd.Parameters.AddWithValue("@doc_dono", doc_dono);
            cmd.Parameters.AddWithValue("@observacao", observacao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void alterar_moto()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"UPDATE motos SET placa = '{placa}', marca = '{marca}', modelo = '{modelo}', cor = '{cor}', ano = '{ano}', chassi = '{chassi}', observacao = '{observacao}' WHERE placa = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }
    }
}
