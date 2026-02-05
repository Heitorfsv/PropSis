using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            int indexLocal = 0;
            int indexRemoto = 0;

            // 1. Busca o maior ID no banco Local (SQLite)
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmd = conLocal.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM motos";
                    var res = cmd.ExecuteScalar();
                    indexLocal = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexLocal = 0; }
            }

            // 2. Busca o maior ID no banco Remoto (MySQL)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmd = conRemoto.CreateCommand();
                    cmd.CommandText = "SELECT MAX(controle) FROM motos";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; } // Se falhar (offline), assume 0 e usa o local como base
            }

            // 3. O índice final será o maior valor encontrado entre os dois bancos
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_moto()
        {
            // 1. SEMPRE tenta gravar no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Adicionamos a coluna sync com valor 0 (Pendente)
                    cmdLocal.CommandText = @"INSERT INTO motos (controle, placa, marca, modelo, cor, ano, chassi, dt_registro, doc_dono, observacao, sync) 
                                    values (@controle, @placa, @marca, @modelo, @cor, @ano, @chassi, @dt_registro, @doc_dono, @observacao, 0)";

                    PreencherParametrosMoto(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar moto localmente: " + ex.Message);
                    return; // Se falhar no local, interrompe para não perder o dado
                }
            }

            // 2. Agora tenta replicar para o MySQL (Servidor Remoto)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"INSERT INTO motos (controle, placa, marca, modelo, cor, ano, chassi, dt_registro, doc_dono, observacao) 
                                    values (@controle, @placa, @marca, @modelo, @cor, @ano, @chassi, @dt_registro, @doc_dono, @observacao)";

                    PreencherParametrosMoto(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, atualiza o status de 'sync' no SQLite para 1
                    static_class.AtualizarStatusSync("motos", index, 1);

                    MessageBox.Show("Moto cadastrada e sincronizada com sucesso!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o dado já está no SQLite com sync=0
                    MessageBox.Show("Moto salva localmente. O servidor está offline, a sincronização ocorrerá automaticamente depois.", "Modo Offline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void alterar_moto()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Ao alterar, o sync volta para 0 para que o servidor seja atualizado depois
                    cmdLocal.CommandText = @"UPDATE motos SET 
                                    placa = @placa, marca = @marca, modelo = @modelo, 
                                    cor = @cor, ano = @ano, chassi = @chassi, 
                                    observacao = @observacao, sync = 0 
                                    WHERE placa = @placa_antiga";

                    PreencherParametrosMoto(cmdLocal);

                    // Parâmetro extra para localizar a moto pela placa antiga
                    var pExtra = cmdLocal.CreateParameter();
                    pExtra.ParameterName = "@placa_antiga";
                    pExtra.Value = static_class.doc_consultar;
                    cmdLocal.Parameters.Add(pExtra);

                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao alterar moto localmente: " + ex.Message);
                    return;
                }
            }

            // 2. Tenta replicar a alteração para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"UPDATE motos SET 
                                    placa = @placa, marca = @marca, modelo = @modelo, 
                                    cor = @cor, ano = @ano, chassi = @chassi, 
                                    observacao = @observacao 
                                    WHERE placa = @placa_antiga";

                    PreencherParametrosMoto(cmdRemoto);

                    var pExtraRemoto = cmdRemoto.CreateParameter();
                    pExtraRemoto.ParameterName = "@placa_antiga";
                    pExtraRemoto.Value = static_class.doc_consultar;
                    cmdRemoto.Parameters.Add(pExtraRemoto);

                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, marcamos como sincronizado (1)
                    static_class.AtualizarStatusSync("motos", index, 1);

                    MessageBox.Show("Dados da moto atualizados e sincronizados!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o sync continua como 0 no SQLite.
                    MessageBox.Show("Alteração salva localmente. Sincronização pendente (servidor offline).", "Modo Offline");
                }
            }
        }

        private void PreencherParametrosMoto(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@placa", placa);
            Add("@marca", marca);
            Add("@modelo", modelo);
            Add("@cor", cor);
            Add("@ano", ano);
            Add("@chassi", chassi);
            Add("@dt_registro", dt_registro);
            Add("@doc_dono", doc_dono);
            Add("@observacao", observacao);
        }

    }
}
