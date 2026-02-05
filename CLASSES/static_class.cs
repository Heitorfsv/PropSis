using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoSistema
{
    public static class static_class
    {
        public static string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        public static string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public static string doc_consultar { get; set; }
        public static string doc_dono { get; set; }
        public static string historico { get; set; }
        public static int controle {  get; set; }

        public static void AtualizarStatusSync(string tabela, int idControle, int novoStatus)
        {
            using (var conLocal = new System.Data.SQLite.SQLiteConnection(strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmd = conLocal.CreateCommand();

                    // Usamos interpolação para o nome da tabela e parâmetros para os valores
                    cmd.CommandText = $"UPDATE {tabela} SET sync = @status WHERE controle = @id";
                    cmd.Parameters.AddWithValue("@status", novoStatus);
                    cmd.Parameters.AddWithValue("@id", idControle);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Como é uma função de fundo, apenas logamos o erro
                    System.Diagnostics.Debug.WriteLine("Erro de sincronia: " + ex.Message);
                }
            }
        }

        public static void ApagarRegistroLocal(string tabela, int idControle)
        {
            using (var conLocal = new System.Data.SQLite.SQLiteConnection(strLocal))
            {
                conLocal.Open();
                var cmd = conLocal.CreateCommand();
                cmd.CommandText = $"DELETE FROM {tabela} WHERE controle = @id";
                cmd.Parameters.AddWithValue("@id", idControle);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
