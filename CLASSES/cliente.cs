using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public class cliente
    {
        public int index { get; set; }
        public string nome { get; set; }
        public string fantasia { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string email { get; set; }
        public string doc { get; set; }
        public int inscricao { get; set; }
        public string telefone { get; set; }
        public string telefone2 { get; set; }
        public string dt_nascimento { get; set; }
        public string cep { get; set; }
        public DateTime dt_cadastro { get; set; }
        public int sujo { get; set; }

        string pesquisa_doc;

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
                    cmd.CommandText = "SELECT MAX(controle) FROM clientes";
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
                    cmd.CommandText = "SELECT MAX(controle) FROM clientes";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; } // Se falhar (offline), ignoramos e usamos o local
            }

            // 3. O 'index' global será o maior entre os dois
            // Usamos Math.Max para garantir que pegamos o maior valor absoluto
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_cliente()
        {
            pesquisa_doc = null;

            // 1. SEMPRE tenta gravar no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();

                    // Verificação de duplicidade local
                    var cmdCheck = conLocal.CreateCommand();
                    cmdCheck.CommandText = "SELECT nome FROM clientes WHERE doc = @doc";
                    cmdCheck.Parameters.AddWithValue("@doc", doc);
                    pesquisa_doc = cmdCheck.ExecuteScalar()?.ToString();

                    if (pesquisa_doc != null)
                    {
                        MessageBox.Show("Este Documento já está cadastrado localmente.", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Inserção Local (Inicia com sync = 0)
                    var cmdInsertLocal = conLocal.CreateCommand();
                    cmdInsertLocal.CommandText = @"INSERT INTO clientes (controle, nome, nome_fantasia, doc, inscricao, dt_nascimento, telefone, telefone2, email, rua, bairro, cidade, cep, dt_cadastro, sujo, sync) 
                                           VALUES (@controle,@nome,@fantasia,@doc,@inscricao,@dt_nascimento,@telefone,@telefone2,@email,@rua,@bairro,@cidade,@cep,@dt_cadastro,@sujo, 0)";

                    PreencherParametros(cmdInsertLocal);
                    cmdInsertLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar no banco local: " + ex.Message);
                    return; // Se não gravou nem no local, para tudo.
                }
            }

            // 2. Agora tenta replicar para o MySQL (Servidor Remoto)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();

                    // Tenta inserir no MySQL
                    var cmdRemoto = conRemoto.CreateCommand();
                    cmdRemoto.CommandText = @"INSERT INTO clientes (controle, nome, nome_fantasia, doc, inscricao, dt_nascimento, telefone, telefone2, email, rua, bairro, cidade, cep, dt_cadastro, sujo) 
                                      VALUES (@controle,@nome,@fantasia,@doc,@inscricao,@dt_nascimento,@telefone,@telefone2,@email,@rua,@bairro,@cidade,@cep,@dt_cadastro,@sujo)";

                    PreencherParametros(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, atualiza o status de 'sync' no SQLite para 1
                    static_class.AtualizarStatusSync("clientes", index, 1);

                    MessageBox.Show("Cliente cadastrado e sincronizado com sucesso!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, não fazemos nada. O dado já está no SQLite com sync=0
                    MessageBox.Show("Salvo localmente. O servidor está offline, a sincronização ocorrerá em breve.", "Modo Offline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void PreencherParametros(System.Data.Common.DbCommand cmd)
        {
            void Add(string nomeParam, object valor)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = nomeParam;
                p.Value = valor ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@nome", nome);
            Add("@fantasia", fantasia);
            Add("@doc", doc);
            Add("@inscricao", inscricao);
            Add("@dt_nascimento", dt_nascimento);
            Add("@telefone", telefone);
            Add("@telefone2", telefone2);
            Add("@email", email);
            Add("@rua", rua);
            Add("@bairro", bairro);
            Add("@cidade", cidade);
            Add("@cep", cep);
            Add("@dt_cadastro", dt_cadastro.ToString("yyyy-MM-dd HH:mm:ss"));
            Add("@sujo", sujo);
        }

        public void alterar_cliente()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Ao alterar, voltamos o sync para 0 (Pendente) 
                    // porque o MySQL agora tem uma versão desatualizada.
                    cmdLocal.CommandText = @"UPDATE clientes SET 
                                    nome = @nome, nome_fantasia = @fantasia, doc = @doc, 
                                    inscricao = @inscricao, dt_nascimento = @dt_nascimento, 
                                    telefone = @telefone, telefone2 = @telefone2, email = @email, 
                                    rua = @rua, bairro = @bairro, cidade = @cidade, cep = @cep,
                                    sync = 0 
                                    WHERE controle = @controle";

                    PreencherParametros(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao alterar no banco local: " + ex.Message);
                    return; // Se falhou no local, não tentamos o remoto
                }
            }

            // 2. Tenta replicar a alteração para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"UPDATE clientes SET 
                                    nome = @nome, nome_fantasia = @fantasia, doc = @doc, 
                                    inscricao = @inscricao, dt_nascimento = @dt_nascimento, 
                                    telefone = @telefone, telefone2 = @telefone2, email = @email, 
                                    rua = @rua, bairro = @bairro, cidade = @cidade, cep = @cep 
                                    WHERE controle = @controle";

                    PreencherParametros(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, marcamos como sincronizado (1) no local
                    static_class.AtualizarStatusSync("clientes", index, 1);

                    MessageBox.Show("Cliente atualizado e sincronizado!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o sync continua como 0 no SQLite.
                    // O usuário continua trabalhando normalmente.
                    MessageBox.Show("Alteração salva localmente. Sincronização pendente (servidor offline).", "Modo Offline");
                }
            }
        }

        public void quitado()
        {
            int sujoCalculado = 0;

            // 1. SEMPRE processa a lógica no Local (SQLite) primeiro
            using (var conLocal = new System.Data.SQLite.SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();

                    // A. Verifica OS abertas no SQLite (Prioriza a realidade local)
                    var cmdCheck = conLocal.CreateCommand();
                    cmdCheck.CommandText = "SELECT controle FROM os WHERE doc = @doc AND (pago = 0 OR pago IS NULL) LIMIT 1";
                    cmdCheck.Parameters.AddWithValue("@doc", doc);

                    using (var reader = cmdCheck.ExecuteReader())
                    {
                        sujoCalculado = reader.Read() ? 1 : 0;
                    }

                    // B. Atualiza o status do cliente localmente e marca como pendente (sync = 0)
                    var cmdUpdate = conLocal.CreateCommand();
                    cmdUpdate.CommandText = "UPDATE clientes SET sujo = @sujo, sync = 0 WHERE doc = @doc";
                    cmdUpdate.Parameters.AddWithValue("@sujo", sujoCalculado);
                    cmdUpdate.Parameters.AddWithValue("@doc", doc);
                    cmdUpdate.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao processar status financeiro local: " + ex.Message);
                    return;
                }
            }

            // 2. Tenta replicar a atualização de status para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();
                    cmdRemoto.CommandText = "UPDATE clientes SET sujo = @sujo WHERE doc = @doc";
                    cmdRemoto.Parameters.AddWithValue("@sujo", sujoCalculado);
                    cmdRemoto.Parameters.AddWithValue("@doc", doc);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no servidor, volta no SQLite e marca como sincronizado (1)
                    static_class.AtualizarStatusSync("clientes", index, 1);

                    MessageBox.Show($"Financeiro atualizado!\nStatus: {(sujoCalculado == 1 ? "DÉBITO PENDENTE" : "QUITADO")}", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Servidor offline: O status no SQLite já está correto e com sync=0
                    MessageBox.Show("Status atualizado localmente. O servidor será atualizado quando houver conexão.", "Modo Offline");
                }
            }
        }
    }
}
