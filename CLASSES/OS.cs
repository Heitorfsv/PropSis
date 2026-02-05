using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        public string aviso_oleo {  get; set; }
        public string aviso_revisao { get; set; }
        public int pago { get; set; }
        public string metodo { get; set; }

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
                    cmd.CommandText = "SELECT MAX(controle) FROM os";
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
                    cmd.CommandText = "SELECT MAX(controle) FROM os";
                    var res = cmd.ExecuteScalar();
                    indexRemoto = (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
                }
                catch { indexRemoto = 0; }
            }

            // 3. Define o index como o maior valor encontrado + 1 (ou apenas o maior se você somar depois)
            // Mantendo sua lógica de apenas retornar o valor do último controle encontrado:
            index = Math.Max(indexLocal, indexRemoto);
        }

        public void cadastrar_os()
        {
            // 1. SEMPRE grava no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Adicionamos a coluna sync = 0 (Pendente)
                    cmdLocal.CommandText = @"INSERT INTO os (controle, placa, cliente, doc, km, observacao, descricao, total, dt_cadastro, dt_saida, aviso_oleo, aviso_revisao, pago, metodo_pag, sync) 
                                    values (@controle,@placa,@cliente,@doc,@km,@observacao,@descricao,@total,@dt_cadastro,@dt_saida,@aviso_oleo,@aviso_revisao,@pago,@metodo_pag, 0)";

                    PreencherParametrosOS(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar OS localmente: " + ex.Message);
                    return; // Interrompe para não tentar o remoto sem ter salvo o local
                }
            }

            // 2. Agora tenta enviar para o MySQL
            using (var conRemoto = new MySqlConnection(static_class.strConexao))
            {
                try
                {
                    conRemoto.Open();
                    var cmdRemoto = conRemoto.CreateCommand();

                    cmdRemoto.CommandText = @"INSERT INTO os (controle, placa, cliente, doc, km, observacao, descricao, total, dt_cadastro, dt_saida, aviso_oleo, aviso_revisao, pago, metodo_pag) 
                                    values (@controle,@placa,@cliente,@doc,@km,@observacao,@descricao,@total,@dt_cadastro,@dt_saida,@aviso_oleo,@aviso_revisao,@pago,@metodo_pag)";

                    PreencherParametrosOS(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se funcionou no MySQL, marcamos como sincronizado (1)
                    static_class.AtualizarStatusSync("os", index, 1);

                    MessageBox.Show("Ordem de Serviço cadastrada e sincronizada!", "JCMotorsport");
                }
                catch (Exception)
                {
                    // Se falhar o MySQL, o sync continua 0 e o sistema segue a vida
                    MessageBox.Show("OS salva localmente. Sincronização pendente com o servidor.", "Modo Offline");
                }
            }
        }

        public void alterar_os()
        {
            // 1. SEMPRE altera no Local (SQLite) primeiro
            using (var conLocal = new SQLiteConnection(static_class.strLocal))
            {
                try
                {
                    conLocal.Open();
                    var cmdLocal = conLocal.CreateCommand();

                    // Atualiza os dados e reseta o sync para 0 (Pendente)
                    cmdLocal.CommandText = @"UPDATE os SET 
                                    placa = @placa, km = @km, cliente = @cliente, 
                                    observacao = @observacao, descricao = @descricao, 
                                    total = @total, dt_cadastro = @dt_cadastro, 
                                    aviso_oleo = @aviso_oleo, aviso_revisao = @aviso_revisao, 
                                    dt_saida = @dt_saida, pago = @pago, 
                                    metodo_pag = @metodo_pag, sync = 0 
                                    WHERE controle = @controle";

                    PreencherParametrosOS(cmdLocal);
                    cmdLocal.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar OS localmente: " + ex.Message);
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

                    cmdRemoto.CommandText = @"UPDATE os SET 
                                    placa = @placa, km = @km, cliente = @cliente, 
                                    observacao = @observacao, descricao = @descricao, 
                                    total = @total, dt_cadastro = @dt_cadastro, 
                                    aviso_oleo = @aviso_oleo, aviso_revisao = @aviso_revisao, 
                                    dt_saida = @dt_saida, pago = @pago, 
                                    metodo_pag = @metodo_pag 
                                    WHERE controle = @controle";

                    PreencherParametrosOS(cmdRemoto);
                    cmdRemoto.ExecuteNonQuery();

                    // 3. Se deu certo no MySQL, voltamos o sync para 1 (Sincronizado)
                    static_class.AtualizarStatusSync("os", index, 1);

                    MessageBox.Show("Ordem de Serviço atualizada e sincronizada!", "Sucesso");
                }
                catch (Exception)
                {
                    // Servidor offline: o dado está salvo com sync=0 no SQLite
                    MessageBox.Show("Alteração salva localmente. O servidor será atualizado na próxima sincronização.", "Modo Offline");
                }
            }
        }

        private void PreencherParametrosOS(System.Data.Common.DbCommand cmd)
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
            Add("@km", km);
            Add("@cliente", cliente);
            Add("@doc", doc);
            Add("@observacao", observacao);
            Add("@descricao", descricao);
            Add("@total", total);
            Add("@dt_cadastro", dt_cadastro);
            Add("@aviso_oleo", aviso_oleo);
            Add("@aviso_revisao", aviso_revisao);
            Add("@dt_saida", dt_saida);
            Add("@pago", pago);
            Add("@metodo_pag", metodo); 
        }
    }
}
