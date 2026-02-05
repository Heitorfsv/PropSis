using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private async void bnt_login_Click(object sender, EventArgs e)
        {
            bool logado = verificar_dados(txt_usuario.Text, txt_senha.Text);

            if (logado)
            {
                verificar_banco_local();
                await EspelharBancoCompleto();

                this.DialogResult = DialogResult.OK; // Isso fecha o login e retorna OK para o Program.cs
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.");
            }
        }
        public bool verificar_dados(string usuarioDigitado, string senhaDigitada, bool usarLocal = false)
        {
            var conexao = usarLocal ? (System.Data.Common.DbConnection)new SQLiteConnection(static_class.strLocal)
                                        : (System.Data.Common.DbConnection)new MySqlConnection(static_class.strConexao);
            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Buscamos apenas pelo usuário
                    cmd.CommandText = "SELECT senha FROM login WHERE usuario = @usuario";

                    var pUsuario = cmd.CreateParameter();
                    pUsuario.ParameterName = "@usuario";
                    pUsuario.Value = usuarioDigitado;
                    cmd.Parameters.Add(pUsuario);

                    // Executamos a leitura
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Recupera o hash que está gravado no banco
                            string hashDoBanco = reader["senha"].ToString();

                            // O BCrypt descriptografa o salt do hash e verifica a senha
                            bool senhaValida = BCrypt.Net.BCrypt.Verify(senhaDigitada, hashDoBanco);

                            return senhaValida;
                        }
                    }
                }
                return false; // Usuário não encontrado ou erro
            }
            catch 
            {
                if (!usarLocal) return verificar_dados(txt_usuario.Text, txt_senha.Text, true);
                else MessageBox.Show("Erro ao conectar ao banco de dados."); return false;
            }
        }

        private async Task EspelharBancoCompleto()
        {
            string[] tabelas = { "login", "clientes", "motos", "metodo_pag", "os", "pecas", "servicos", "pecas_os", "servicos_os", "orcamentos" };

            // Configuração inicial da barra
            pbSincronizacao.Value = 0;
            pbSincronizacao.Maximum = tabelas.Length;
            pbSincronizacao.Visible = true;
            lblStatus.Visible = true;

            try
            {
                using (var connRemota = new MySqlConnection(static_class.strConexao))
                using (var connLocal = new SQLiteConnection(static_class.strLocal))
                {
                    await connRemota.OpenAsync();
                    await connLocal.OpenAsync();

                    for (int t = 0; t < tabelas.Length; t++)
                    {
                        string tabela = tabelas[t];

                        // Atualiza o texto para o usuário
                        lblStatus.Text = $"Sincronizando: {tabela}...";
                        pbSincronizacao.Value = t + 1;

                        try
                        {
                            var cmdBusca = new MySqlCommand($"SELECT * FROM `{tabela}`", connRemota);
                            using (var reader = await cmdBusca.ExecuteReaderAsync())
                            {
                                var colunas = new List<string>();
                                var parametros = new List<string>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string nomeCol = reader.GetName(i);
                                    colunas.Add($"[{nomeCol}]"); // Proteção com colchetes que resolveu o erro
                                    parametros.Add("@" + nomeCol);
                                }

                                string sqlInsert = $"INSERT OR REPLACE INTO [{tabela}] ({string.Join(", ", colunas)}) VALUES ({string.Join(", ", parametros)})";

                                using (var transacao = connLocal.BeginTransaction())
                                {
                                    while (await reader.ReadAsync())
                                    {
                                        using (var cmdInsert = new SQLiteCommand(sqlInsert, connLocal))
                                        {
                                            for (int i = 0; i < reader.FieldCount; i++)
                                            {
                                                cmdInsert.Parameters.AddWithValue("@" + reader.GetName(i), reader.GetValue(i) ?? DBNull.Value);
                                            }
                                            await cmdInsert.ExecuteNonQueryAsync();
                                        }
                                    }
                                    transacao.Commit();
                                }
                            }
                        }
                        catch (Exception exTabela)
                        {
                            // Erro individual por tabela para não parar todo o processo
                            Console.WriteLine($"Erro na tabela [{tabela}]: {exTabela.Message}");
                        }
                    }
                }

                lblStatus.Text = "Sincronização concluída!";
                await Task.Delay(1000); // Pequena pausa para o usuário ver o 100%
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro crítico na sincronização: " + ex.Message);
            }
            finally
            {
                pbSincronizacao.Visible = false;
                lblStatus.Visible = false;
            }
        }

        public void verificar_banco_local()
        {
            using (var conexao = new System.Data.SQLite.SQLiteConnection(static_class.strLocal))
            {
                conexao.Open();
                var cmd = conexao.CreateCommand();

                // 1. Primeiro, garantimos que as tabelas existam (conforme seu código original)
                string[] comandosCriacao = {
            "CREATE TABLE IF NOT EXISTS clientes (controle INTEGER PRIMARY KEY, dt_cadastro TEXT, nome TEXT, nome_fantasia TEXT, doc TEXT, inscricao TEXT, dt_nascimento TEXT, telefone TEXT, telefone2 TEXT, email TEXT, rua TEXT, bairro TEXT, cidade TEXT, cep TEXT, sujo INTEGER, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS motos (controle INTEGER PRIMARY KEY, placa TEXT, marca TEXT, modelo TEXT, cor TEXT, ano TEXT, chassi TEXT, dt_registro TEXT, doc_dono TEXT, observacao TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS orcamentos (controle INTEGER PRIMARY KEY, cliente TEXT, doc TEXT, km TEXT, placa TEXT, dt_cadastro TEXT, total TEXT, observacao TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS os (controle INTEGER PRIMARY KEY, placa TEXT, km TEXT, cliente TEXT, doc TEXT, observacao TEXT, descricao TEXT, total TEXT, dt_cadastro TEXT, aviso_oleo TEXT, aviso_revisao TEXT, dt_saida TEXT, pago INTEGER, metodo_pag TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS pecas (controle INTEGER PRIMARY KEY, nome TEXT, marca TEXT, modelo TEXT, valor_pago TEXT, impostos TEXT, valor_sugerido TEXT, fornecedor TEXT, contato TEXT, local TEXT, estoque TEXT, troca_oleo INTEGER, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS pecas_os (controle INTEGER PRIMARY KEY, os TEXT, orca TEXT, nome TEXT, valor TEXT, qtd TEXT, desco TEXT, pos TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS servicos (controle INTEGER PRIMARY KEY, nome TEXT, valor TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS servicos_os (controle INTEGER PRIMARY KEY, os TEXT, orca TEXT, nome TEXT, valor TEXT, qtd TEXT, desco TEXT, pos TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS metodo_pag (controle INTEGER PRIMARY KEY, metodo TEXT, agencia TEXT, parcelas TEXT, sync INTEGER)",
            "CREATE TABLE IF NOT EXISTS login (controle INTEGER PRIMARY KEY, usuario TEXT, senha TEXT)"
        };

                foreach (var sql in comandosCriacao)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }

                // 2. AGORA O DIAGNÓSTICO: Vamos listar as colunas de TODAS as tabelas no console
                string[] todasAsTabelas = { "login", "clientes", "motos", "metodo_pag", "os", "pecas", "servicos", "pecas_os", "servicos_os", "orcamentos" };

                Console.WriteLine("\n========== CHECK-UP GERAL DO BANCO SQLITE ==========");
                foreach (var tabela in todasAsTabelas) 
                {
                    Console.WriteLine($"\n--- ESTRUTURA DA TABELA: {tabela.ToUpper()} ---");
                    cmd.CommandText = $"PRAGMA table_info({tabela})";

                    using (var reader = cmd.ExecuteReader())
                    {
                        bool temControle = false;
                        while (reader.Read())
                        {
                            string nomeColuna = reader["name"].ToString();
                            Console.WriteLine($"Coluna: {nomeColuna} | Tipo: {reader["type"]}");

                            if (nomeColuna.ToLower() == "controle") temControle = true;
                        }

                        if (!temControle)
                        {
                            Console.WriteLine($">>> 🚨 ATENÇÃO: A TABELA '{tabela}' NÃO TEM A COLUNA CONTROLE!");
                        }
                    }
                }
                Console.WriteLine("\n====================================================");
            }
        }



        private void bnt_sair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
