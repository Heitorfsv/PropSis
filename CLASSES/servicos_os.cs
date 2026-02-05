using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrototipoSistema
{
    public class servicos_os
    {
        public int index { get; set; }
        public int os_or { get; set; }
        public string modo { get; set; }// Modo pode ser "os" ou "orca"
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
                    cmd.CommandText = "SELECT MAX(controle) FROM servicos_os";
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
                    cmd.CommandText = "SELECT MAX(controle) FROM servicos_os";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; }
            }

            // 3. Define o index como o maior valor encontrado + 1 (ou apenas o maior se você somar depois)
            // Mantendo sua lógica de apenas retornar o valor do último controle encontrado:
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_servico_os()
        {
            // 1. SEMPRE Local (SQLite) primeiro para garantir o funcionamento offline
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Adicionamos a coluna sync = 0 e mantemos a lógica do {modo}
                    cmdLocal.CommandText = $@"INSERT INTO servicos_os (controle, {modo}, nome, valor, qtd, desco, pos, sync) 
                                    VALUES (@controle, @modo, @nome, @valor, @qtd, @desco, @pos, 0)";

                    PreencherParametrosServicoOS(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar serviço da OS localmente: " + ex.Message);
                    return; // Se falhar no PC, não tenta o servidor para não causar desalinhamento
                }
            }

            // 2. Agora tenta replicar para o MySQL (Servidor)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = $@"INSERT INTO servicos_os (controle, {modo}, nome, valor, qtd, desco, pos) 
                                    VALUES (@controle, @modo, @nome, @valor, @qtd, @desco, @pos)";

                    PreencherParametrosServicoOS(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. SE funcionou no servidor, usamos a FUNÇÃO MESTRE pelo ID (controle) para marcar como 1
                    static_class.AtualizarStatusSync("servicos_os", index, 1);
                }
                catch
                {
                    // Silencioso no catch remoto para não interromper o fluxo caso haja vários serviços
                    Console.WriteLine("Serviço da OS salvo apenas localmente (Offline).");
                }
            }
        }

        private void PreencherParametrosServicoOS(System.Data.Common.DbCommand cmd)
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
