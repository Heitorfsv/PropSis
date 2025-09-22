using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PrototipoSistema.classes
{
    public class OS
    {
        public int index { get; set; }
        public string placa {  get; set; }
        public string cliente { get; set; }
        public string doc { get; set; }
        public int km { get; set; }
        public string observacao { get; set; }
        public string descricao { get; set; }
        public string total { get; set; }
        public string dt_cadastro { get; set; }
        public string dt_saida { get; set; }
        public string aviso_oleo_km { get; set; }
        public string aviso_oleo {  get; set; }
        public string aviso_revisao_km { get; set; }
        public string aviso_revisao { get; set; }
        public int pago { get; set; }
        public string metodo { get; set; } 

        public void ultimo_index()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand("SELECT controle FROM os ORDER BY controle DESC LIMIT 1", conexao);

            conexao.Open();
            index = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Close();

        }

        public void cadastrar_os()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand("INSERT INTO os (controle, placa, km, cliente, doc, observacao, descricao, total, dt_cadastro, aviso_oleo, aviso_revisao, dt_saida, pago, metodo_pag) values (@controle,@placa,@km,@cliente,@doc,@observacao,@descricao,@total,@dt_cadastro,@aviso_oleo,@aviso_revisao,@dt_saida,@pago,@metodo_pag)", conexao);
                cmd.Parameters.AddWithValue("@controle", index);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@km", km);
                cmd.Parameters.AddWithValue("@cliente", cliente);
                cmd.Parameters.AddWithValue("@doc", doc);
                cmd.Parameters.AddWithValue("@observacao", observacao);
                cmd.Parameters.AddWithValue("@descricao", descricao);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@dt_cadastro", dt_cadastro);
                cmd.Parameters.AddWithValue("@aviso_oleo", aviso_oleo);
                cmd.Parameters.AddWithValue("@aviso_revisao", aviso_revisao);
                cmd.Parameters.AddWithValue("@dt_saida", dt_saida);
                cmd.Parameters.AddWithValue("@pago", pago);
                cmd.Parameters.AddWithValue("@metodo_pag", metodo);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void alterar_os()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            MessageBox.Show(dt_cadastro.ToString());

            var cmd = new MySqlCommand($"UPDATE os SET placa = '{placa}', cliente = '{cliente}', km = '{km}', observacao = '{observacao}', descricao = '{descricao}', total = '{total}', dt_cadastro = '{dt_cadastro}', aviso_oleo = '{aviso_oleo}', aviso_revisao = '{aviso_revisao}', dt_saida = '{dt_saida}', aviso_oleo = '{aviso_oleo}', aviso_revisao = '{aviso_revisao}', pago = '{pago}', metodo_pag = '{metodo}' WHERE controle = {index}", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }
    }
}
