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
            if (this.Text == "Edição motos")
            {
                bnt_historico.Visible = true;
                bnt_deletar.Visible = true;
                cmb_dono.Enabled = false;
                bnt_editar.Text = "Salvar";

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
                    cmb_dono.Text = reader.GetString("nome");
                }
                conexao.Close();
            }
            else if (this.Text == "Cadastro motos")
            {
                bnt_historico.Visible = false;
                bnt_deletar.Visible = false;
                cmb_dono.Enabled = true;
                bnt_editar.Text = "Cadastrar";
            }
        }

        private void bnt_editar_Click(object sender, EventArgs e)
        {
            if (this.Text == "Edição motos")
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
            else if (this.Text == "Cadastro motos")
            {
                var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
                var conexao = new MySqlConnection(strConexao);

                var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome = '{cmb_dono.Text}'", conexao);

                conexao.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    motos.doc_dono = reader.GetString("doc");
                }
                conexao.Close();

                if (motos.doc_dono != null)
                {
                    motos.ultimo_index();
                    motos.index++;

                    motos.placa = txt_placa.Text;
                    motos.marca = txt_marca.Text;
                    motos.cor = txt_cor.Text;
                    motos.ano = txt_ano.Text;
                    motos.modelo = txt_modelo.Text;
                    motos.chassi = txt_chassi.Text;
                    motos.dt_registro = DateTime.Now;
                    motos.observacao = txt_observacao.Text;

                    try
                    {
                        motos.cadastrar_moto();
                        MessageBox.Show("Moto cadastrada!", "JCMotorsport", MessageBoxButtons.OK);
                    }
                    catch { }
                }
                else
                {
                    MessageBox.Show("O cliente não é válido", "JCMotorsport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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

        private void cmb_dono_TextChanged(object sender, EventArgs e)
        {
            var strConexao = "server=192.168.15.10;uid=heitor;pwd=Vitoria1;database=db_jcmotorsport";
            var conexao = new MySqlConnection(strConexao);

            var cmd = new MySqlCommand($"SELECT * FROM clientes WHERE nome LIKE '%{cmb_dono.Text}%'", conexao);

            conexao.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cmb_dono.Items.Add(reader.GetString("nome"));
            }

            conexao.Close();
        }
    }
}
