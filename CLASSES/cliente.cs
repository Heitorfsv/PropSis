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

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public void ultimo_index(bool usarLocal = false)
        {
            // Define qual conexão usar
            System.Data.Common.DbConnection conexao;

            if (usarLocal)
                conexao = new SQLiteConnection(strLocal);
            else
                conexao = new MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT controle FROM clientes ORDER BY controle DESC LIMIT 1";

                    var resultado = cmd.ExecuteScalar();

                    // Se o banco estiver vazio, o resultado é null. 
                    // Tratamos isso para não dar erro no Convert.
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        index = Convert.ToInt32(resultado);
                    }
                    else
                    {
                        index = 0; // Primeiro registro
                    }
                }
            }
            catch (Exception)
            {
                // Se falhou no MySQL e ainda não tentamos o local, dispara a tentativa local
                if (!usarLocal)
                {
                    ultimo_index(true);
                }
                else
                {
                    index = 0; // Fallback final caso ambos falhem
                }
            }
        }

        public void cadastrar_cliente(bool usarLocal = false)
        {
            pesquisa_doc = null;
            // Define qual conexão e comando usar com base no parâmetro
            System.Data.Common.DbConnection conexao;

            if (usarLocal)
                conexao = new SQLiteConnection(strLocal);
            else
                conexao = new MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    // 1. Verificação de duplicidade
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT nome FROM clientes WHERE doc = @doc";
                    var pDoc = cmd.CreateParameter();
                    pDoc.ParameterName = "@doc";
                    pDoc.Value = doc;
                    cmd.Parameters.Add(pDoc);

                    conexao.Open();
                    pesquisa_doc = cmd.ExecuteScalar()?.ToString();

                    if (pesquisa_doc == null)
                    {
                        // 2. Inserção
                        var cmd2 = conexao.CreateCommand();
                        cmd2.CommandText = @"INSERT INTO clientes (controle, nome, nome_fantasia, doc, inscricao, dt_nascimento, telefone, telefone2, email, rua, bairro, cidade, cep, dt_cadastro, sujo) 
                                    values (@controle,@nome,@fantasia,@doc,@inscricao,@dt_nascimento,@telefone,@telefone2,@email,@rua,@bairro,@cidade,@cep,@dt_cadastro,@sujo)";

                        // Preenche os parâmetros (usando o auxiliar abaixo para não repetir código)
                        PreencherParametros(cmd2);

                        cmd2.ExecuteNonQuery();
                        conexao.Close();

                        string msg = usarLocal ? "Salvo localmente (Servidor Offline)!" : "Cadastrado com sucesso!";
                        MessageBox.Show(msg, "JCMotorsport");
                    }
                    else
                    {
                        MessageBox.Show("Este Documento já está cadastrado", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception)
            {
                // Se falhou no MySQL e ainda não tentamos o local, tenta o local agora
                if (!usarLocal)
                {
                    cadastrar_cliente(true);
                }
                else
                {
                    MessageBox.Show("Erro crítico: Nem o banco local está disponível.");
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

        public void alterar_cliente(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;

            if (usarLocal)
                conexao = new SQLiteConnection(strLocal);
            else
                conexao = new MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Usando parâmetros para evitar erros de sintaxe com aspas
                    cmd.CommandText = @"UPDATE clientes SET nome = @nome, nome_fantasia = @fantasia, doc = @doc, inscricao = @inscricao, dt_nascimento = @dt_nascimento, telefone = @telefone, telefone2 = @telefone2, email = @email, 
                                rua = @rua, bairro = @bairro, cidade = @cidade, cep = @cep 
                                WHERE controle = @controle";

                    PreencherParametros(cmd); // Reaproveita os parâmetros que você já tem

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                if (!usarLocal)
                {
                    alterar_cliente(true); // Se falhar o online, tenta o offline
                }
                else
                {
                    MessageBox.Show("Erro ao alterar cliente no banco local.");
                }
            }
        }

        public void quitado(bool usarLocal = false)
        {
            int sujoLocal = 0;
            System.Data.Common.DbConnection conexao;

            if (usarLocal)
                conexao = new SQLiteConnection(strLocal);
            else
                conexao = new MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();

                    // 1. Verificar se há OS pendente
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT pago FROM os WHERE doc = @doc";

                    var pDoc = cmd.CreateParameter();
                    pDoc.ParameterName = "@doc";
                    pDoc.Value = doc;
                    cmd.Parameters.Add(pDoc);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["pago"]) == 0)
                            {
                                sujoLocal = 1;
                            }
                        }
                    }

                    // 2. Atualizar o status 'sujo' do cliente
                    var cmdUpdate = conexao.CreateCommand();
                    cmdUpdate.CommandText = "UPDATE clientes SET sujo = @sujo WHERE doc = @doc";

                    var pSujo = cmdUpdate.CreateParameter();
                    pSujo.ParameterName = "@sujo";
                    pSujo.Value = sujoLocal;
                    cmdUpdate.Parameters.Add(pSujo);

                    var pDoc2 = cmdUpdate.CreateParameter();
                    pDoc2.ParameterName = "@doc";
                    pDoc2.Value = doc;
                    cmdUpdate.Parameters.Add(pDoc2);

                    cmdUpdate.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                if (!usarLocal)
                {
                    quitado(true);
                }
                else
                {
                    MessageBox.Show("Erro ao processar status de quitação localmente.");
                }
            }
        }
    }
}
