namespace PrototipoSistema
{
    partial class historico_moto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lst_clientes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_placa = new System.Windows.Forms.TextBox();
            this.txt_modelo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_marca = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ano = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_dt_registro = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lst_clientes
            // 
            this.lst_clientes.FormattingEnabled = true;
            this.lst_clientes.Location = new System.Drawing.Point(12, 98);
            this.lst_clientes.Name = "lst_clientes";
            this.lst_clientes.Size = new System.Drawing.Size(143, 251);
            this.lst_clientes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Placa:";
            // 
            // txt_placa
            // 
            this.txt_placa.Enabled = false;
            this.txt_placa.Location = new System.Drawing.Point(52, 6);
            this.txt_placa.Name = "txt_placa";
            this.txt_placa.Size = new System.Drawing.Size(80, 20);
            this.txt_placa.TabIndex = 2;
            // 
            // txt_modelo
            // 
            this.txt_modelo.Enabled = false;
            this.txt_modelo.Location = new System.Drawing.Point(207, 35);
            this.txt_modelo.Name = "txt_modelo";
            this.txt_modelo.Size = new System.Drawing.Size(97, 20);
            this.txt_modelo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Modelo:";
            // 
            // txt_marca
            // 
            this.txt_marca.Enabled = false;
            this.txt_marca.Location = new System.Drawing.Point(207, 6);
            this.txt_marca.Name = "txt_marca";
            this.txt_marca.Size = new System.Drawing.Size(97, 20);
            this.txt_marca.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Marca:";
            // 
            // txt_ano
            // 
            this.txt_ano.Enabled = false;
            this.txt_ano.Location = new System.Drawing.Point(52, 35);
            this.txt_ano.Name = "txt_ano";
            this.txt_ano.Size = new System.Drawing.Size(80, 20);
            this.txt_ano.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ano:";
            // 
            // lst_dt_registro
            // 
            this.lst_dt_registro.FormattingEnabled = true;
            this.lst_dt_registro.Location = new System.Drawing.Point(161, 98);
            this.lst_dt_registro.Name = "lst_dt_registro";
            this.lst_dt_registro.Size = new System.Drawing.Size(143, 251);
            this.lst_dt_registro.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Histórico de donos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Data de registro";
            // 
            // historico_moto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 361);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lst_dt_registro);
            this.Controls.Add(this.txt_ano);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_marca);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_modelo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_placa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lst_clientes);
            this.Name = "historico_moto";
            this.Text = "Histórico da moto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.historico_moto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_clientes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_placa;
        private System.Windows.Forms.TextBox txt_modelo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_marca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ano;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lst_dt_registro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}