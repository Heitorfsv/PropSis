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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PrototipoSistema
{
    public partial class historico_moto : Form
    {
        public string placa;
        public string marca;
        public string modelo;
        public string ano;

        string strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
        string strLocal = "Data Source=backup_jcmotorsport.db;Version=3;";

        public historico_moto()
        {
            InitializeComponent();

            listView1.Columns.Clear();
            listView1.Columns.Add("Cliente", 200);
            listView1.Columns.Add("Data de Registro", 120);

        }

        private void historico_moto_Load(object sender, EventArgs e)
        {
            // 1. As caixas de texto DEVEM ser preenchidas antes de qualquer banco
            // Se elas não estão preenchendo, verifique se as variáveis 'placa', 'marca', etc. têm valor
            txt_placa.Text = placa;
            txt_marca.Text = marca;
            txt_modelo.Text = modelo;
            txt_ano.Text = ano;

            // 2. Definir conexão (Certifique-se que strLocal ou strConexao estão corretas)
            bool usarLocal = false;
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

                    // SQL ajustado: usando TRIM para evitar erro com espaços e garantindo a ordem das colunas
                    cmd.CommandText = @"SELECT c.nome, m.dt_registro FROM motos m INNER JOIN clientes c ON m.doc_dono = c.doc WHERE TRIM(m.placa) = @placa";

                    var pPlaca = cmd.CreateParameter();
                    pPlaca.ParameterName = "@placa";
                    // Usamos a variável 'placa' (que veio do outro form) em vez do .Text do TextBox
                    pPlaca.Value = placa.Trim();
                    cmd.Parameters.Add(pPlaca);

                    listView1.Items.Clear();

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Se não entrar no While, é porque o SELECT não achou nada
                        if (!reader.HasRows)
                        {
                            // Log interno para você debugar se quiser:
                            // Console.WriteLine("Nenhum histórico encontrado para a placa: " + placa);
                        }

                        while (reader.Read())
                        {
                            string nome = reader["nome"] != DBNull.Value ? reader["nome"].ToString() : "N/D";
                            string dataFormatada = "";

                            if (reader["dt_registro"] != DBNull.Value)
                            {
                                DateTime dt = Convert.ToDateTime(reader["dt_registro"]);
                                dataFormatada = dt.ToString("dd/MM/yyyy");
                            }

                            var item = new ListViewItem(nome);
                            item.SubItems.Add(dataFormatada);
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão/consulta: " + ex.Message);
            }
        }
    }
}
