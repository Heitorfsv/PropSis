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
        public string total { get; set; }
        public string dt_cadastro { get; set; }
        public string dt_saida { get; set; }
        public string aviso_oleo_km { get; set; }
        public string aviso_oleo_dt {  get; set; }
        public string aviso_filtro_km { get; set; }
        public string aviso_filtro_dt { get; set; }
        public int pago { get; set; }

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

                var cmd = new MySqlCommand("INSERT INTO os (controle, placa, km, cliente, doc, observacao, total, dt_cadastro, dt_saida, pago) values (@controle,@placa,@km,@cliente,@doc,@observacao,@total,@dt_cadastro,@dt_saida,@pago)", conexao);
                cmd.Parameters.AddWithValue("@controle", index);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@km", km);
                cmd.Parameters.AddWithValue("@cliente", cliente);
                cmd.Parameters.AddWithValue("@doc", doc);
                cmd.Parameters.AddWithValue("@observacao", observacao);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@dt_cadastro", dt_cadastro);
                cmd.Parameters.AddWithValue("@dt_saida", dt_saida);
                cmd.Parameters.AddWithValue("@pago", pago);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }

        public void alterar_os()
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            MessageBox.Show(dt_cadastro.ToString());

            var cmd = new MySqlCommand($"UPDATE os SET placa = '{placa}', cliente = '{cliente}', km = '{km}', observacao = '{observacao}', total = '{total}', dt_cadastro = '{dt_cadastro}', dt_saida = '{dt_saida}', aviso_oleo_km = '{aviso_oleo_km}', aviso_oleo_dt = '{aviso_oleo_dt}', aviso_filtro_km = '{aviso_filtro_km}', aviso_filtro_dt = '{aviso_oleo_dt}', pago = '{pago}' WHERE controle = {index}", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();
        }
    }
}
