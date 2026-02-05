using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrototipoSistema.classes
{
    public class pecas
    {
        public int index { get; set; }
        public string nome { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public decimal valor_pago { get; set; }
        public decimal impostos { get; set; }
        public decimal valor_sugerido { get; set; }
        public string fornecedor { get; set; }
        public string contato { get; set; }
        public string local { get; set; }
        public string estoque { get; set; }

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
                    cmd.CommandText = "SELECT MAX(controle) FROM pecas";
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
                    cmd.CommandText = "SELECT MAX(controle) FROM pecas";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; }
            }

            // 3. Define o index como o maior valor encontrado + 1 (ou apenas o maior se você somar depois)
            // Mantendo sua lógica de apenas retornar o valor do último controle encontrado:
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_pecas()
        {
            // 1. SEMPRE grava no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Adicionamos a coluna sync = 0 (Pendente)
                    cmdLocal.CommandText = @"INSERT INTO pecas (controle, nome, marca, modelo, valor_pago, impostos, valor_sugerido, fornecedor, contato, local, estoque, sync) 
                                    values (@controle, @nome, @marca, @modelo, @valor_pago, @impostos, @valor_sugerido, @fornecedor, @contato, @local, @estoque, 0)";

                    PreencherParametrosPecas(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar peça localmente: " + ex.Message);
                    return; // Interrompe para não tentar o remoto sem salvar o local
                }
            }

            // 2. Agora tenta enviar para o MySQL (Servidor)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"INSERT INTO pecas (controle, nome, marca, modelo, valor_pago, impostos, valor_sugerido, fornecedor, contato, local, estoque) 
                                    values (@controle, @nome, @marca, @modelo, @valor_pago, @impostos, @valor_sugerido, @fornecedor, @contato, @local, @estoque)";

                    PreencherParametrosPecas(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, marcamos como sincronizado (1)
                    static_class.AtualizarStatusSync("pecas", index, 1);

                    MessageBox.Show("Peça cadastrada e sincronizada com sucesso!", "Estoque JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o sync continua 0 e o sistema segue offline
                    MessageBox.Show("Peça salva localmente. Sincronização pendente com o servidor.", "Modo Offline");
                }
            }
        }

        public void alterar_pecas()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Atualiza os dados e reseta o sync para 0 (Pendente)
                    // Mantive o WHERE apontando para o nome antigo na static_class
                    cmdLocal.CommandText = $@"UPDATE pecas SET 
                                    nome = @nome, marca = @marca, modelo = @modelo, 
                                    valor_pago = @valor_pago, impostos = @impostos, 
                                    valor_sugerido = @valor_sugerido, fornecedor = @fornecedor, 
                                    contato = @contato, local = @local, estoque = @estoque, 
                                    sync = 0 
                                    WHERE nome = '{static_class.doc_consultar}'";

                    PreencherParametrosPecas(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar peça localmente: " + ex.Message);
                    return;
                }
            }

            // 2. Agora tenta replicar para o MySQL (Servidor)
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = $@"UPDATE pecas SET 
                                    nome = @nome, marca = @marca, modelo = @modelo, 
                                    valor_pago = @valor_pago, impostos = @impostos, 
                                    valor_sugerido = @valor_sugerido, fornecedor = @fornecedor, 
                                    contato = @contato, local = @local, estoque = @estoque 
                                    WHERE nome = '{static_class.doc_consultar}'";

                    PreencherParametrosPecas(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, marcamos como sincronizado (1)
                    // Usamos o 'nome' atual (que pode ter sido alterado) para marcar como ok
                    static_class.AtualizarStatusSync("pecas", index, 1);

                    MessageBox.Show("Peça atualizada e sincronizada!", "Estoque");
                }
                catch (Exception)
                {
                    // Servidor offline: o dado está salvo com sync=0 no SQLite
                    MessageBox.Show("Alteração salva localmente. O servidor será atualizado na próxima sincronização.", "Modo Offline");
                }
            }
        }

        private void PreencherParametrosPecas(System.Data.Common.DbCommand cmd)
        {
            void Add(string n, object v)
            {
                var p = cmd.CreateParameter();
                p.ParameterName = n;
                p.Value = v ?? DBNull.Value;
                cmd.Parameters.Add(p);
            }

            Add("@controle", index);
            Add("@nome", nome);
            Add("@marca", marca);
            Add("@modelo", modelo);
            Add("@valor_pago", valor_pago);
            Add("@impostos", impostos);
            Add("@valor_sugerido", valor_sugerido);
            Add("@fornecedor", fornecedor);
            Add("@contato", contato);
            Add("@local", local);
            Add("@estoque", estoque);
        }
    }
}
