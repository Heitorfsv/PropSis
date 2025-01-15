namespace PrototipoSistema
{
    partial class edicao_motos
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
            this.label8 = new System.Windows.Forms.Label();
            this.txt_observacao = new System.Windows.Forms.TextBox();
            this.txt_modelo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ano = new System.Windows.Forms.MaskedTextBox();
            this.txt_chassi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bnt_editar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_placa = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_marca = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_cliente = new System.Windows.Forms.TextBox();
            this.bnt_deletar = new System.Windows.Forms.Button();
            this.bnt_historico = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(282, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "Observação";
            // 
            // txt_observacao
            // 
            this.txt_observacao.Location = new System.Drawing.Point(285, 108);
            this.txt_observacao.MaxLength = 150;
            this.txt_observacao.Multiline = true;
            this.txt_observacao.Name = "txt_observacao";
            this.txt_observacao.Size = new System.Drawing.Size(207, 66);
            this.txt_observacao.TabIndex = 33;
            // 
            // txt_modelo
            // 
            this.txt_modelo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_modelo.Location = new System.Drawing.Point(7, 65);
            this.txt_modelo.Name = "txt_modelo";
            this.txt_modelo.Size = new System.Drawing.Size(228, 20);
            this.txt_modelo.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 16);
            this.label7.TabIndex = 31;
            this.label7.Text = "Modelo";
            // 
            // txt_ano
            // 
            this.txt_ano.Location = new System.Drawing.Point(166, 108);
            this.txt_ano.Mask = "00/00";
            this.txt_ano.Name = "txt_ano";
            this.txt_ano.Size = new System.Drawing.Size(69, 20);
            this.txt_ano.TabIndex = 30;
            // 
            // txt_chassi
            // 
            this.txt_chassi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_chassi.Location = new System.Drawing.Point(7, 153);
            this.txt_chassi.Name = "txt_chassi";
            this.txt_chassi.Size = new System.Drawing.Size(228, 20);
            this.txt_chassi.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 28;
            this.label6.Text = "Chassi";
            // 
            // bnt_editar
            // 
            this.bnt_editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_editar.Location = new System.Drawing.Point(391, 190);
            this.bnt_editar.Name = "bnt_editar";
            this.bnt_editar.Size = new System.Drawing.Size(101, 32);
            this.bnt_editar.TabIndex = 27;
            this.bnt_editar.Text = "Editar";
            this.bnt_editar.UseVisualStyleBackColor = true;
            this.bnt_editar.Click += new System.EventHandler(this.bnt_editar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(282, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "Cliente";
            // 
            // txt_placa
            // 
            this.txt_placa.Location = new System.Drawing.Point(7, 22);
            this.txt_placa.Mask = "AAA-AAAA";
            this.txt_placa.Name = "txt_placa";
            this.txt_placa.Size = new System.Drawing.Size(91, 20);
            this.txt_placa.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Placa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(162, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Ano";
            // 
            // txt_cor
            // 
            this.txt_cor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_cor.Location = new System.Drawing.Point(7, 108);
            this.txt_cor.Name = "txt_cor";
            this.txt_cor.Size = new System.Drawing.Size(152, 20);
            this.txt_cor.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Cor";
            // 
            // txt_marca
            // 
            this.txt_marca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_marca.Location = new System.Drawing.Point(104, 22);
            this.txt_marca.Name = "txt_marca";
            this.txt_marca.Size = new System.Drawing.Size(131, 20);
            this.txt_marca.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(101, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Marca";
            // 
            // txt_cliente
            // 
            this.txt_cliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_cliente.Location = new System.Drawing.Point(285, 66);
            this.txt_cliente.Name = "txt_cliente";
            this.txt_cliente.ReadOnly = true;
            this.txt_cliente.Size = new System.Drawing.Size(207, 20);
            this.txt_cliente.TabIndex = 35;
            // 
            // bnt_deletar
            // 
            this.bnt_deletar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bnt_deletar.Location = new System.Drawing.Point(285, 22);
            this.bnt_deletar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_deletar.Name = "bnt_deletar";
            this.bnt_deletar.Size = new System.Drawing.Size(65, 20);
            this.bnt_deletar.TabIndex = 36;
            this.bnt_deletar.Text = "Deletar";
            this.bnt_deletar.UseVisualStyleBackColor = true;
            this.bnt_deletar.Click += new System.EventHandler(this.bnt_deletar_Click);
            // 
            // bnt_historico
            // 
            this.bnt_historico.Location = new System.Drawing.Point(355, 22);
            this.bnt_historico.Name = "bnt_historico";
            this.bnt_historico.Size = new System.Drawing.Size(68, 20);
            this.bnt_historico.TabIndex = 37;
            this.bnt_historico.Text = "Histórico";
            this.bnt_historico.UseVisualStyleBackColor = true;
            this.bnt_historico.Click += new System.EventHandler(this.bnt_historico_Click);
            // 
            // edicao_motos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bnt_historico);
            this.Controls.Add(this.bnt_deletar);
            this.Controls.Add(this.txt_cliente);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_observacao);
            this.Controls.Add(this.txt_modelo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_ano);
            this.Controls.Add(this.txt_chassi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bnt_editar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_placa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_cor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_marca);
            this.Controls.Add(this.label1);
            this.Name = "edicao_motos";
            this.Text = "Edição moto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.edicao_motos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_observacao;
        private System.Windows.Forms.TextBox txt_modelo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txt_ano;
        private System.Windows.Forms.TextBox txt_chassi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bnt_editar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txt_placa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_marca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_cliente;
        private System.Windows.Forms.Button bnt_deletar;
        private System.Windows.Forms.Button bnt_historico;
    }
}