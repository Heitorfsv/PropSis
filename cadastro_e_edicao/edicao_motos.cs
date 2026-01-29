using MySql.Data.MySqlClient;
using PrototipoSistema.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototipoSistema
{
    public partial class edicao_motos : Form
    {
        motos motos = new motos();

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public edicao_motos()
        {
            InitializeComponent();
        }

        private void edicao_motos_Load(object sender, EventArgs e)
        {
            if (this.Text == "Edição motos")
            {
                bnt_historico.Visible = true;
                bnt_deletar.Visible = true;
                cmb_dono.Enabled = false;
                bnt_editar.Text = "Salvar";

                CarregarDadosMoto();
            }
            else if (this.Text == "Cadastro motos")
            {
                bnt_historico.Visible = false;
                bnt_deletar.Visible = false;
                cmb_dono.Enabled = true;
                bnt_editar.Text = "Cadastrar";
            }
        }

        private void CarregarDadosMoto(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // 1. Busca os dados da Moto
                    cmd.CommandText = "SELECT * FROM motos WHERE placa = @placa AND doc_dono = @docDono LIMIT 1";

                    var p1 = cmd.CreateParameter();
                    p1.ParameterName = "@placa";
                    p1.Value = static_class.doc_consultar;
                    cmd.Parameters.Add(p1);

                    var p2 = cmd.CreateParameter();
                    p2.ParameterName = "@docDono";
                    p2.Value = static_class.doc_dono;
                    cmd.Parameters.Add(p2);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txt_placa.Text = reader["placa"].ToString();
                            txt_marca.Text = reader["marca"].ToString();
                            txt_modelo.Text = reader["modelo"].ToString();
                            txt_cor.Text = reader["cor"].ToString();
                            txt_ano.Text = reader["ano"].ToString();
                            txt_chassi.Text = reader["chassi"].ToString();
                            txt_observacao.Text = reader["observacao"].ToString();

                            if (reader["dt_registro"] != DBNull.Value)
                                txt_dt_registro.Text = Convert.ToDateTime(reader["dt_registro"]).ToString("d");
                        }
                    }

                    // 2. Busca o nome do Cliente (Dono)
                    if (!string.IsNullOrEmpty(static_class.doc_dono))
                    {
                        var cmdCliente = conexao.CreateCommand();
                        cmdCliente.CommandText = "SELECT nome FROM clientes WHERE doc = @doc LIMIT 1";

                        var pDoc = cmdCliente.CreateParameter();
                        pDoc.ParameterName = "@doc";
                        pDoc.Value = static_class.doc_dono;
                        cmdCliente.Parameters.Add(pDoc);

                        var nomeCliente = cmdCliente.ExecuteScalar();
                        if (nomeCliente != null)
                        {
                            cmb_dono.Text = nomeCliente.ToString();
                        }
                    }
                }
            }
            catch
            {
                // Se falhou no MySQL, tenta buscar no banco local
                if (!usarLocal) CarregarDadosMoto(true);
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            // Preenche o objeto com os dados da tela
            motos.placa = txt_placa.Text;
            motos.marca = txt_marca.Text;
            motos.cor = txt_cor.Text;
            motos.ano = txt_ano.Text;
            motos.modelo = txt_modelo.Text;
            motos.chassi = txt_chassi.Text;
            motos.observacao = txt_observacao.Text;

            if (this.Text == "Edição motos")
            {
                // Tenta alterar (a lógica de fallback já deve estar dentro do método alterar_moto)
                motos.alterar_moto();
                this.Close();
            }
            else if (this.Text == "Cadastro motos")
            {
                // Busca o documento do dono de forma híbrida
                string docEncontrado = BuscarDocumentoDono(cmb_dono.Text);

                if (!string.IsNullOrEmpty(docEncontrado))
                {
                    motos.doc_dono = docEncontrado;

                    // Busca o próximo ID disponível (Híbrido)
                    motos.ultimo_index();
                    motos.index++;
                    motos.dt_registro = DateTime.Now;

                    // Tenta cadastrar
                    motos.cadastrar_moto();
                    this.Close();
                }
                else { MessageBox.Show("O cliente não foi encontrado no banco de dados.", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        // Método auxiliar para buscar o DOC do cliente em qualquer banco disponível
        private string BuscarDocumentoDono(string nomeCliente, bool usarLocal = false)
        {
            string documento = "";
            System.Data.Common.DbConnection conexao;

            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT doc FROM clientes WHERE nome = @nome LIMIT 1";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@nome";
                    p.Value = nomeCliente;
                    cmd.Parameters.Add(p);

                    var resultado = cmd.ExecuteScalar();
                    if (resultado != null) documento = resultado.ToString(); 
                }
            }
            catch
            {
                if (!usarLocal) return BuscarDocumentoDono(nomeCliente, true);
            }
            return documento;
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            // Pergunta antes de deletar para evitar acidentes
            if (MessageBox.Show("Deseja realmente excluir esta moto e todas as suas ordens de serviço?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ExecutarExclusaoTotal();
                Close();
            }
        }

        private void ExecutarExclusaoTotal(bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal)
                conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else
                conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Usamos a placa como referência para encontrar as OSs vinculadas
                    string placaMoto = static_class.doc_consultar;

                    // 1. Deletar PEÇAS vinculadas às OSs desta moto
                    cmd.CommandText = @"DELETE FROM pecas_os WHERE os IN (SELECT controle FROM os WHERE placa = @placa)";
                    var p1 = cmd.CreateParameter();
                    p1.ParameterName = "@placa";
                    p1.Value = placaMoto;
                    cmd.Parameters.Add(p1);
                    cmd.ExecuteNonQuery();

                    // 2. Deletar SERVIÇOS vinculados às OSs desta moto
                    cmd.CommandText = @"DELETE FROM servicos_os WHERE os IN (SELECT controle FROM os WHERE placa = @placa)";
                    // Reutiliza o parâmetro @placa que já está no cmd
                    cmd.ExecuteNonQuery();

                    // 3. Deletar as OSs da moto
                    cmd.CommandText = "DELETE FROM os WHERE placa = @placa";
                    cmd.ExecuteNonQuery();

                    // 4. Por fim, deletar a MOTO 
                    cmd.CommandText = "DELETE FROM motos WHERE placa = @placa";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Trate o erro conforme sua necessidade
                throw new Exception("Erro ao excluir histórico da moto: " + ex.Message);
            }
        }

        private void bnt_historico_Click(object sender, EventArgs e)
        {
            historico_moto historico = new historico_moto();

            historico.placa = txt_placa.Text;
            historico.marca = txt_marca.Text;
            historico.modelo = txt_modelo.Text; 
            historico.ano = txt_ano.Text;
             
            historico.Show();
        }

        private void cmb_dono_TextChanged(object sender, EventArgs e)
        {
            string filtro = cmb_dono.Text.Trim();

            // Só pesquisa se houver pelo menos 2 caracteres para não sobrecarregar o banco
            if (filtro.Length >= 2)
            {
                BuscarDonoHibrido(filtro);
            }
        }

        private void BuscarDonoHibrido(string filtro, bool usarLocal = false)
        {
            System.Data.Common.DbConnection conexao;
            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    var cmd = conexao.CreateCommand();

                    // Query segura com parâmetros
                    cmd.CommandText = "SELECT nome FROM clientes WHERE nome LIKE @filtro LIMIT 10";

                    var p = cmd.CreateParameter();
                    p.ParameterName = "@filtro";
                    p.Value = "%" + filtro + "%";
                    cmd.Parameters.Add(p);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Evita que a lista fique piscando ou duplicando
                        while (reader.Read())
                        {
                            cmb_dono.Items.Add(reader["nome"].ToString());
                        }
                    }

                    // Mantém o cursor no final do texto para o usuário continuar digitando
                    cmb_dono.SelectionStart = cmb_dono.Text.Length;
                }
            }
            catch
            {
                // Se o servidor MySQL falhar, tenta buscar os clientes salvos localmente
                if (!usarLocal) BuscarDonoHibrido(filtro, true);
            }
        }
    }
}
