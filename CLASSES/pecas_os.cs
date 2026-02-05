using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PrototipoSistema
{
    public class pecas_os
    {
        public int index {  get; set; }
        public int os_or { get; set; }
        public string modo { get; set; } // Modo pode ser "os" ou "orca"
        public string nome { get; set; }
        public string valor { get; set; }
        public string desc { get; set; }
        public decimal qtd { get; set; }
        public int pos { get; set; }

        public void ultimo_index()
        {
            int indexLocal = 0;
            int indexRemoto = 0;

            // 1. Busca o maior ID no SQLite
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmd = conLocal.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM pecas_os";
                    var res = cmd.ExecuteScalar();
                    indexLocal = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexLocal = 0; }
            }

            // 2. Busca o maior ID no MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmd = conRemoto.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM pecas_os";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; }
            }

            // 3. Define o index como o maior valor encontrado + 1 (ou apenas o maior se você somar depois)
            // Mantendo sua lógica de apenas retornar o valor do último controle encontrado:
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_peca_os()
        {
            // 1. SEMPRE Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Adicionamos a coluna sync = 0
                    // Mantive sua lógica de {modo} para o nome da coluna (ex: os ou orcamento)
                    cmdLocal.CommandText = $@"INSERT INTO pecas_os (controle, {modo}, nome, valor, qtd, desco, pos, sync) 
                                    values (@controle, @modo, @nome, @valor, @qtd, @desco, @pos, 0)";

                    PreencherParametrosPecasOS(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar item localmente: " + ex.Message);
                    return;
                }
            }

            // 2. Tenta replicar para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = $@"INSERT INTO pecas_os (controle, {modo}, nome, valor, qtd, desco, pos) 
                                    values (@controle, @modo, @nome, @valor, @qtd, @desco, @pos)";

                    PreencherParametrosPecasOS(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. SE deu certo no MySQL, usa a FUNÇÃO MESTRE pelo ID (controle)
                    static_class.AtualizarStatusSync("pecas_os", index, 1);
                }
                catch
                {
                    // Se falhar, não damos MessageBox aqui para não travar o loop de inserção de itens, 
                    // já que geralmente são várias peças de uma vez.
                    Console.WriteLine("Item da OS salvo offline.");
                }
            }
        }

        private void PreencherParametrosPecasOS(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@modo", os_or);
            Add("@nome", nome);
            Add("@valor", valor);
            Add("@qtd", qtd);
            Add("@desco", desc);
            Add("@pos", pos);
        }
    }
}
