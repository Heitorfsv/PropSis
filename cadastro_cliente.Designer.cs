namespace PrototipoSistema
{
    partial class cadastro_cliente
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nome = new System.Windows.Forms.TextBox();
            this.lbl_cpf = new System.Windows.Forms.Label();
            this.bnt_cadastro = new System.Windows.Forms.Button();
            this.dtp_nascimento = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rb_juridica = new System.Windows.Forms.RadioButton();
            this.rb_fisica = new System.Windows.Forms.RadioButton();
            this.txt_doc = new System.Windows.Forms.MaskedTextBox();
            this.txt_telefone = new System.Windows.Forms.MaskedTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_rua = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_cep = new System.Windows.Forms.MaskedTextBox();
            this.txt_bairro = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_cidade = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_telefone2 = new System.Windows.Forms.MaskedTextBox();
            this.lbl_inscricao = new System.Windows.Forms.Label();
            this.txt_inscricao = new System.Windows.Forms.MaskedTextBox();
            this.cb_dt_nascimento = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome";
            // 
            // txt_nome
            // 
            this.txt_nome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nome.Location = new System.Drawing.Point(8, 27);
            this.txt_nome.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_nome.Name = "txt_nome";
            this.txt_nome.Size = new System.Drawing.Size(200, 20);
            this.txt_nome.TabIndex = 2;
            // 
            // lbl_cpf
            // 
            this.lbl_cpf.AutoSize = true;
            this.lbl_cpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpf.Location = new System.Drawing.Point(5, 152);
            this.lbl_cpf.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_cpf.Name = "lbl_cpf";
            this.lbl_cpf.Size = new System.Drawing.Size(34, 17);
            this.lbl_cpf.TabIndex = 5;
            this.lbl_cpf.Text = "CPF";
            // 
            // bnt_cadastro
            // 
            this.bnt_cadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_cadastro.Location = new System.Drawing.Point(397, 211);
            this.bnt_cadastro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bnt_cadastro.Name = "bnt_cadastro";
            this.bnt_cadastro.Size = new System.Drawing.Size(122, 31);
            this.bnt_cadastro.TabIndex = 7;
            this.bnt_cadastro.Text = "Criar cadastro";
            this.bnt_cadastro.UseVisualStyleBackColor = true;
            this.bnt_cadastro.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtp_nascimento
            // 
            this.dtp_nascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_nascimento.Location = new System.Drawing.Point(237, 27);
            this.dtp_nascimento.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtp_nascimento.Name = "dtp_nascimento";
            this.dtp_nascimento.Size = new System.Drawing.Size(125, 20);
            this.dtp_nascimento.TabIndex = 8;
            this.dtp_nascimento.Value = new System.DateTime(2024, 1, 9, 17, 3, 25, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(234, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Data de nascimento";
            // 
            // txt_email
            // 
            this.txt_email.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_email.Location = new System.Drawing.Point(8, 121);
            this.txt_email.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(200, 20);
            this.txt_email.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Email";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 192);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Tipo de pessoa";
            // 
            // rb_juridica
            // 
            this.rb_juridica.AutoSize = true;
            this.rb_juridica.Location = new System.Drawing.Point(58, 0);
            this.rb_juridica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb_juridica.Name = "rb_juridica";
            this.rb_juridica.Size = new System.Drawing.Size(63, 17);
            this.rb_juridica.TabIndex = 19;
            this.rb_juridica.Text = "Jurídica";
            this.rb_juridica.UseVisualStyleBackColor = true;
            // 
            // rb_fisica
            // 
            this.rb_fisica.AutoSize = true;
            this.rb_fisica.Checked = true;
            this.rb_fisica.Location = new System.Drawing.Point(0, 0);
            this.rb_fisica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rb_fisica.Name = "rb_fisica";
            this.rb_fisica.Size = new System.Drawing.Size(54, 17);
            this.rb_fisica.TabIndex = 18;
            this.rb_fisica.TabStop = true;
            this.rb_fisica.Text = "Física";
            this.rb_fisica.UseVisualStyleBackColor = true;
            this.rb_fisica.CheckedChanged += new System.EventHandler(this.rb_fisica_CheckedChanged);
            // 
            // txt_doc
            // 
            this.txt_doc.Location = new System.Drawing.Point(8, 170);
            this.txt_doc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_doc.Name = "txt_doc";
            this.txt_doc.Size = new System.Drawing.Size(126, 20);
            this.txt_doc.TabIndex = 21;
            // 
            // txt_telefone
            // 
            this.txt_telefone.Location = new System.Drawing.Point(8, 75);
            this.txt_telefone.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_telefone.Mask = "(00)00000-0000";
            this.txt_telefone.Name = "txt_telefone";
            this.txt_telefone.Size = new System.Drawing.Size(97, 20);
            this.txt_telefone.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_fisica);
            this.panel1.Controls.Add(this.rb_juridica);
            this.panel1.Location = new System.Drawing.Point(8, 207);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(133, 17);
            this.panel1.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(234, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "CEP";
            // 
            // txt_rua
            // 
            this.txt_rua.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_rua.Location = new System.Drawing.Point(297, 75);
            this.txt_rua.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_rua.Name = "txt_rua";
            this.txt_rua.Size = new System.Drawing.Size(216, 20);
            this.txt_rua.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(294, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Rua";
            // 
            // txt_cep
            // 
            this.txt_cep.Location = new System.Drawing.Point(237, 75);
            this.txt_cep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_cep.Mask = "00000-000";
            this.txt_cep.Name = "txt_cep";
            this.txt_cep.Size = new System.Drawing.Size(57, 20);
            this.txt_cep.TabIndex = 33;
            this.txt_cep.TextChanged += new System.EventHandler(this.txt_cep_TextChanged);
            this.txt_cep.Leave += new System.EventHandler(this.txt_cep_Leave);
            // 
            // txt_bairro
            // 
            this.txt_bairro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_bairro.Location = new System.Drawing.Point(237, 122);
            this.txt_bairro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_bairro.Name = "txt_bairro";
            this.txt_bairro.Size = new System.Drawing.Size(131, 20);
            this.txt_bairro.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(234, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 36;
            this.label6.Text = "Bairro";
            // 
            // txt_cidade
            // 
            this.txt_cidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_cidade.Location = new System.Drawing.Point(378, 122);
            this.txt_cidade.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_cidade.Name = "txt_cidade";
            this.txt_cidade.Size = new System.Drawing.Size(131, 20);
            this.txt_cidade.TabIndex = 39;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(375, 104);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 17);
            this.label8.TabIndex = 38;
            this.label8.Text = "Cidade";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 56);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 17);
            this.label9.TabIndex = 40;
            this.label9.Text = "Telefone";
            // 
            // txt_telefone2
            // 
            this.txt_telefone2.Location = new System.Drawing.Point(109, 75);
            this.txt_telefone2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_telefone2.Mask = "(00)00000-0000";
            this.txt_telefone2.Name = "txt_telefone2";
            this.txt_telefone2.Size = new System.Drawing.Size(102, 20);
            this.txt_telefone2.TabIndex = 41;
            // 
            // lbl_inscricao
            // 
            this.lbl_inscricao.AutoSize = true;
            this.lbl_inscricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_inscricao.Location = new System.Drawing.Point(141, 152);
            this.lbl_inscricao.Name = "lbl_inscricao";
            this.lbl_inscricao.Size = new System.Drawing.Size(117, 16);
            this.lbl_inscricao.TabIndex = 42;
            this.lbl_inscricao.Text = "Inscrição Estadual";
            // 
            // txt_inscricao
            // 
            this.txt_inscricao.Location = new System.Drawing.Point(144, 170);
            this.txt_inscricao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_inscricao.Mask = "000000000";
            this.txt_inscricao.Name = "txt_inscricao";
            this.txt_inscricao.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_inscricao.Size = new System.Drawing.Size(126, 20);
            this.txt_inscricao.TabIndex = 43;
            // 
            // cb_dt_nascimento
            // 
            this.cb_dt_nascimento.AutoSize = true;
            this.cb_dt_nascimento.Location = new System.Drawing.Point(367, 30);
            this.cb_dt_nascimento.Name = "cb_dt_nascimento";
            this.cb_dt_nascimento.Size = new System.Drawing.Size(15, 14);
            this.cb_dt_nascimento.TabIndex = 44;
            this.cb_dt_nascimento.UseVisualStyleBackColor = true;
            this.cb_dt_nascimento.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cadastro_cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 439);
            this.Controls.Add(this.cb_dt_nascimento);
            this.Controls.Add(this.txt_inscricao);
            this.Controls.Add(this.lbl_inscricao);
            this.Controls.Add(this.txt_telefone2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_cidade);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_bairro);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_cep);
            this.Controls.Add(this.txt_rua);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt_telefone);
            this.Controls.Add(this.txt_doc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtp_nascimento);
            this.Controls.Add(this.bnt_cadastro);
            this.Controls.Add(this.lbl_cpf);
            this.Controls.Add(this.txt_nome);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "cadastro_cliente";
            this.Text = " Cadastro de cliente";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_nome;
        private System.Windows.Forms.Label lbl_cpf;
        private System.Windows.Forms.Button bnt_cadastro;
        private System.Windows.Forms.DateTimePicker dtp_nascimento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rb_juridica;
        private System.Windows.Forms.RadioButton rb_fisica;
        private System.Windows.Forms.MaskedTextBox txt_doc;
        private System.Windows.Forms.MaskedTextBox txt_telefone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_rua;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txt_cep;
        private System.Windows.Forms.TextBox txt_bairro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_cidade;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox txt_telefone2;
        private System.Windows.Forms.Label lbl_inscricao;
        private System.Windows.Forms.MaskedTextBox txt_inscricao;
        private System.Windows.Forms.CheckBox cb_dt_nascimento;
    }
}

