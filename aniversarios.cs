using MySql.Data.MySqlClient;
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
    public partial class aniversarios : Form
    {
        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";
        public aniversarios()
        {
            InitializeComponent();
        }

        private void aniversarios_Load(object sender, EventArgs e)
        {
            CarregarAniversarios();
        }

        private void CarregarAniversarios(bool usarLocal = false)
        {
            // Define a conexão baseada na disponibilidade
            System.Data.Common.DbConnection conexao;
            if (usarLocal) conexao = new System.Data.SQLite.SQLiteConnection(strLocal);
            else conexao = new MySql.Data.MySqlClient.MySqlConnection(strConexao);

            try
            {
                using (conexao)
                {
                    var cmd = conexao.CreateCommand();
                    cmd.CommandText = "SELECT nome, dt_nascimento FROM clientes";

                    conexao.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Limpa as listas antes de carregar (importante se houver retry)
                        lst_15dias.Items.Clear();
                        lst_hoje.Items.Clear();

                        while (reader.Read())
                        {
                            try
                            {
                                string dataRaw = reader["dt_nascimento"].ToString();
                                if (string.IsNullOrEmpty(dataRaw) || dataRaw.Length < 5) continue;

                                // Mantendo sua lógica de Substring(0,5) para dia e mês
                                DateTime aniversario = DateTime.Parse(dataRaw.Substring(0, 5) + "/" + DateTime.Now.Year);

                                // Ajuste para considerar apenas a data (sem horas) na comparação
                                TimeSpan dif = aniversario.Date - DateTime.Now.Date;

                                // Lógica de 15 dias
                                if (dif.TotalDays <= 15 && dif.TotalDays > 0)
                                {
                                    lst_15dias.Items.Add(reader["nome"].ToString() + " (" + aniversario.ToString("dd/MM") + ")");
                                }

                                // Lógica de hoje
                                if (dif.TotalDays == 0)
                                {
                                    lst_hoje.Items.Add(reader["nome"].ToString() + " (" + aniversario.ToString("dd/MM") + ")");
                                }
                            }
                            catch { /* Ignora registros com datas inválidas */ }
                        }
                    }
                }
                // Seleciona o primeiro item se existir
                if (lst_15dias.Items.Count > 0) lst_15dias.SelectedIndex = 0;
            }
            catch
            {
                // Se falhar no MySQL, tenta buscar no banco local
                if (!usarLocal) CarregarAniversarios(true);
            }
        }
    }
}
