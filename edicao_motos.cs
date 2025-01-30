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
        public edicao_motos()
        {
            InitializeComponent();
        }

        private void edicao_motos_Load(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM motos WHERE placa = '{static_class.doc_consultar}' AND doc_dono = '{static_class.doc_dono}'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            string doc_dono = "";

            while (reader.Read())
            {
                txt_placa.Text = reader.GetString("placa");
                txt_marca.Text = reader.GetString("marca");
                txt_modelo.Text = reader.GetString("modelo");
                txt_cor.Text = reader.GetString("cor");
                txt_ano.Text = reader.GetString("ano");
                txt_chassi.Text = reader.GetString("chassi");
                txt_observacao.Text = reader.GetString("observacao");
                txt_dt_registro.Text = reader.GetDateTime("dt_registro").ToString("d");
                doc_dono = reader.GetString("doc_dono");
            }
            conexao.Close();

            cmd = new MySqlCommand($"SELECT * FROM clientes WHERE doc = '{doc_dono}'", conexao);
            conexao.Open();
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txt_cliente.Text = reader.GetString("nome");
            }
            conexao.Close();
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            motos.placa = txt_placa.Text;
            motos.marca = txt_marca.Text;
            motos.modelo = txt_modelo.Text;
            motos.cor = txt_cor.Text;
            motos.ano = txt_ano.Text;
            motos.chassi = txt_chassi.Text;
            motos.observacao = txt_observacao.Text;

            try
            {
                MessageBox.Show("Moto Alterada!", "JCMotorsport", MessageBoxButtons.OK);
                motos.alterar_moto();
            }
            catch { }
        }

        private void bnt_deletar_Click(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"DELETE FROM motos WHERE placa = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close(); 

            cmd = new MySqlCommand($"DELETE FROM os WHERE placa = '{static_class.doc_consultar}'", conexao);

            conexao.Open();
            cmd.ExecuteReader();
            conexao.Close();

            Close();
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
    }
}
