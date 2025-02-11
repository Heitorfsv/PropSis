namespace PrototipoSistema
{
    partial class cadastro_moto
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_marca = new System.Windows.Forms.TextBox();
            this.txt_cor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_placa = new System.Windows.Forms.MaskedTextBox();
            this.cmb_dono = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bnt_cadastrar = new System.Windows.Forms.Button();
            this.txt_chassi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_ano = new System.Windows.Forms.MaskedTextBox();
            this.txt_modelo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_observacao = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Marca";
            // 
            // txt_marca
            // 
            this.txt_marca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_marca.Location = new System.Drawing.Point(105, 25);
            this.txt_marca.Name = "txt_marca";
            this.txt_marca.Size = new System.Drawing.Size(131, 20);
            this.txt_marca.TabIndex = 1;
            // 
            // txt_cor
            // 
            this.txt_cor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_cor.Location = new System.Drawing.Point(8, 111);
            this.txt_cor.Name = "txt_cor";
            this.txt_cor.Size = new System.Drawing.Size(152, 20);
            this.txt_cor.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(163, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ano";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Placa";
            // 
            // txt_placa
            // 
            this.txt_placa.Location = new System.Drawing.Point(8, 25);
            this.txt_placa.Mask = "AAA-AAAA";
            this.txt_placa.Name = "txt_placa";
            this.txt_placa.Size = new System.Drawing.Size(91, 20);
            this.txt_placa.TabIndex = 7;
            // 
            // cmb_dono
            // 
            this.cmb_dono.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_dono.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_dono.FormattingEnabled = true;
            this.cmb_dono.Location = new System.Drawing.Point(289, 24);
            this.cmb_dono.Name = "cmb_dono";
            this.cmb_dono.Size = new System.Drawing.Size(207, 21);
            this.cmb_dono.TabIndex = 8;
            this.cmb_dono.TextChanged += new System.EventHandler(this.cmb_dono_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(286, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cliente";
            // 
            // bnt_cadastrar
            // 
            this.bnt_cadastrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_cadastrar.Location = new System.Drawing.Point(395, 149);
            this.bnt_cadastrar.Name = "bnt_cadastrar";
            this.bnt_cadastrar.Size = new System.Drawing.Size(101, 32);
            this.bnt_cadastrar.TabIndex = 10;
            this.bnt_cadastrar.Text = "Cadastrar";
            this.bnt_cadastrar.UseVisualStyleBackColor = true;
            this.bnt_cadastrar.Click += new System.EventHandler(this.bnt_cadastrar_Click);
            // 
            // txt_chassi
            // 
            this.txt_chassi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_chassi.Location = new System.Drawing.Point(8, 156);
            this.txt_chassi.Name = "txt_chassi";
            this.txt_chassi.Size = new System.Drawing.Size(228, 20);
            this.txt_chassi.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Chassi";
            // 
            // txt_ano
            // 
            this.txt_ano.Location = new System.Drawing.Point(167, 111);
            this.txt_ano.Mask = "00/00";
            this.txt_ano.Name = "txt_ano";
            this.txt_ano.Size = new System.Drawing.Size(69, 20);
            this.txt_ano.TabIndex = 13;
            // 
            // txt_modelo
            // 
            this.txt_modelo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_modelo.Location = new System.Drawing.Point(8, 68);
            this.txt_modelo.Name = "txt_modelo";
            this.txt_modelo.Size = new System.Drawing.Size(228, 20);
            this.txt_modelo.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Modelo";
            // 
            // txt_observacao
            // 
            this.txt_observacao.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txt_observacao.Location = new System.Drawing.Point(289, 67);
            this.txt_observacao.MaxLength = 150;
            this.txt_observacao.Multiline = true;
            this.txt_observacao.Name = "txt_observacao";
            this.txt_observacao.Size = new System.Drawing.Size(207, 66);
            this.txt_observacao.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(286, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "Observação";
            // 
            // cadastro_moto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 188);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_observacao);
            this.Controls.Add(this.txt_modelo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_ano);
            this.Controls.Add(this.txt_chassi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bnt_cadastrar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_dono);
            this.Controls.Add(this.txt_placa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_cor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_marca);
            this.Controls.Add(this.label1);
            this.Name = "cadastro_moto";
            this.Text = "Cadastro de moto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_marca;
        private System.Windows.Forms.TextBox txt_cor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txt_placa;
        private System.Windows.Forms.ComboBox cmb_dono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bnt_cadastrar;
        private System.Windows.Forms.TextBox txt_chassi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txt_ano;
        private System.Windows.Forms.TextBox txt_modelo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_observacao;
        private System.Windows.Forms.Label label8;
    }
}