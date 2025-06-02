namespace PrototipoSistema
{
    partial class cadastro_or
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
            this.components = new System.ComponentModel.Container();
            this.dtp_cadastro = new System.Windows.Forms.DateTimePicker();
            this.bnt_cadastro = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lst_servico_total = new System.Windows.Forms.ListBox();
            this.lst_servicos_qtd = new System.Windows.Forms.ListBox();
            this.bnt_editar_servico = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_total_servico = new System.Windows.Forms.TextBox();
            this.lst_servicos = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lst_peca_total = new System.Windows.Forms.ListBox();
            this.lst_pecas_qtd = new System.Windows.Forms.ListBox();
            this.bnt_editar_peca = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_total_pecas = new System.Windows.Forms.TextBox();
            this.lst_pecas = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txt_doc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_telefone = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_placa = new System.Windows.Forms.ComboBox();
            this.txt_ano = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_modelo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_km = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_marca = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_cliente = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp_cadastro
            // 
            this.dtp_cadastro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_cadastro.Location = new System.Drawing.Point(12, 26);
            this.dtp_cadastro.Name = "dtp_cadastro";
            this.dtp_cadastro.Size = new System.Drawing.Size(200, 20);
            this.dtp_cadastro.TabIndex = 75;
            // 
            // bnt_cadastro
            // 
            this.bnt_cadastro.Location = new System.Drawing.Point(753, 523);
            this.bnt_cadastro.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_cadastro.Name = "bnt_cadastro";
            this.bnt_cadastro.Size = new System.Drawing.Size(86, 23);
            this.bnt_cadastro.TabIndex = 70;
            this.bnt_cadastro.Text = "Cadastro";
            this.bnt_cadastro.UseVisualStyleBackColor = true;
            this.bnt_cadastro.Click += new System.EventHandler(this.bnt_cadastro_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(719, 484);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 69;
            this.label9.Text = "Total";
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(722, 499);
            this.txt_total.Margin = new System.Windows.Forms.Padding(2);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(117, 20);
            this.txt_total.TabIndex = 67;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.lst_servico_total);
            this.groupBox6.Controls.Add(this.lst_servicos_qtd);
            this.groupBox6.Controls.Add(this.bnt_editar_servico);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.txt_total_servico);
            this.groupBox6.Controls.Add(this.lst_servicos);
            this.groupBox6.Location = new System.Drawing.Point(437, 170);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(389, 301);
            this.groupBox6.TabIndex = 68;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Serviços";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(219, 28);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Valores";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(160, 28);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Qtd.";
            // 
            // lst_servico_total
            // 
            this.lst_servico_total.FormattingEnabled = true;
            this.lst_servico_total.Location = new System.Drawing.Point(222, 42);
            this.lst_servico_total.Margin = new System.Windows.Forms.Padding(2);
            this.lst_servico_total.Name = "lst_servico_total";
            this.lst_servico_total.Size = new System.Drawing.Size(72, 199);
            this.lst_servico_total.TabIndex = 26;
            // 
            // lst_servicos_qtd
            // 
            this.lst_servicos_qtd.FormattingEnabled = true;
            this.lst_servicos_qtd.Location = new System.Drawing.Point(163, 42);
            this.lst_servicos_qtd.Margin = new System.Windows.Forms.Padding(2);
            this.lst_servicos_qtd.Name = "lst_servicos_qtd";
            this.lst_servicos_qtd.Size = new System.Drawing.Size(55, 199);
            this.lst_servicos_qtd.TabIndex = 25;
            // 
            // bnt_editar_servico
            // 
            this.bnt_editar_servico.Location = new System.Drawing.Point(305, 273);
            this.bnt_editar_servico.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_editar_servico.Name = "bnt_editar_servico";
            this.bnt_editar_servico.Size = new System.Drawing.Size(65, 23);
            this.bnt_editar_servico.TabIndex = 16;
            this.bnt_editar_servico.Text = "Editar";
            this.bnt_editar_servico.UseVisualStyleBackColor = true;
            this.bnt_editar_servico.Click += new System.EventHandler(this.bnt_editar_servico_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(2, 261);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 14;
            this.label21.Text = "Subtotal";
            // 
            // txt_total_servico
            // 
            this.txt_total_servico.Location = new System.Drawing.Point(4, 277);
            this.txt_total_servico.Margin = new System.Windows.Forms.Padding(2);
            this.txt_total_servico.Name = "txt_total_servico";
            this.txt_total_servico.ReadOnly = true;
            this.txt_total_servico.Size = new System.Drawing.Size(103, 20);
            this.txt_total_servico.TabIndex = 13;
            // 
            // lst_servicos
            // 
            this.lst_servicos.FormattingEnabled = true;
            this.lst_servicos.Location = new System.Drawing.Point(4, 42);
            this.lst_servicos.Margin = new System.Windows.Forms.Padding(2);
            this.lst_servicos.Name = "lst_servicos";
            this.lst_servicos.Size = new System.Drawing.Size(155, 199);
            this.lst_servicos.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.lst_peca_total);
            this.groupBox4.Controls.Add(this.lst_pecas_qtd);
            this.groupBox4.Controls.Add(this.bnt_editar_peca);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txt_total_pecas);
            this.groupBox4.Controls.Add(this.lst_pecas);
            this.groupBox4.Location = new System.Drawing.Point(12, 170);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(386, 301);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Peças";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(220, 28);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Valores";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(161, 28);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Qtd.";
            // 
            // lst_peca_total
            // 
            this.lst_peca_total.FormattingEnabled = true;
            this.lst_peca_total.Location = new System.Drawing.Point(222, 42);
            this.lst_peca_total.Margin = new System.Windows.Forms.Padding(2);
            this.lst_peca_total.Name = "lst_peca_total";
            this.lst_peca_total.Size = new System.Drawing.Size(72, 199);
            this.lst_peca_total.TabIndex = 25;
            // 
            // lst_pecas_qtd
            // 
            this.lst_pecas_qtd.FormattingEnabled = true;
            this.lst_pecas_qtd.Location = new System.Drawing.Point(164, 42);
            this.lst_pecas_qtd.Margin = new System.Windows.Forms.Padding(2);
            this.lst_pecas_qtd.Name = "lst_pecas_qtd";
            this.lst_pecas_qtd.Size = new System.Drawing.Size(55, 199);
            this.lst_pecas_qtd.TabIndex = 24;
            // 
            // bnt_editar_peca
            // 
            this.bnt_editar_peca.Location = new System.Drawing.Point(306, 273);
            this.bnt_editar_peca.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_editar_peca.Name = "bnt_editar_peca";
            this.bnt_editar_peca.Size = new System.Drawing.Size(65, 23);
            this.bnt_editar_peca.TabIndex = 16;
            this.bnt_editar_peca.Text = "Editar";
            this.bnt_editar_peca.UseVisualStyleBackColor = true;
            this.bnt_editar_peca.Click += new System.EventHandler(this.bnt_editar_peca_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 261);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Subtotal";
            // 
            // txt_total_pecas
            // 
            this.txt_total_pecas.Location = new System.Drawing.Point(5, 276);
            this.txt_total_pecas.Margin = new System.Windows.Forms.Padding(2);
            this.txt_total_pecas.Name = "txt_total_pecas";
            this.txt_total_pecas.ReadOnly = true;
            this.txt_total_pecas.Size = new System.Drawing.Size(103, 20);
            this.txt_total_pecas.TabIndex = 13;
            // 
            // lst_pecas
            // 
            this.lst_pecas.FormattingEnabled = true;
            this.lst_pecas.Location = new System.Drawing.Point(5, 42);
            this.lst_pecas.Margin = new System.Windows.Forms.Padding(2);
            this.lst_pecas.Name = "lst_pecas";
            this.lst_pecas.Size = new System.Drawing.Size(155, 199);
            this.lst_pecas.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_cliente);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.txt_doc);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txt_telefone);
            this.groupBox2.Location = new System.Drawing.Point(11, 51);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(517, 57);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dados do cliente";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(199, 14);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 13);
            this.label22.TabIndex = 9;
            this.label22.Text = "Documento";
            // 
            // txt_doc
            // 
            this.txt_doc.Location = new System.Drawing.Point(202, 29);
            this.txt_doc.Margin = new System.Windows.Forms.Padding(2);
            this.txt_doc.Name = "txt_doc";
            this.txt_doc.ReadOnly = true;
            this.txt_doc.Size = new System.Drawing.Size(119, 20);
            this.txt_doc.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Cliente";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(322, 14);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Telefone";
            // 
            // txt_telefone
            // 
            this.txt_telefone.Location = new System.Drawing.Point(325, 29);
            this.txt_telefone.Margin = new System.Windows.Forms.Padding(2);
            this.txt_telefone.Name = "txt_telefone";
            this.txt_telefone.ReadOnly = true;
            this.txt_telefone.Size = new System.Drawing.Size(177, 20);
            this.txt_telefone.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_placa);
            this.groupBox1.Controls.Add(this.txt_ano);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_modelo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_km);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_marca);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 112);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(517, 55);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do veículo";
            // 
            // cmb_placa
            // 
            this.cmb_placa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_placa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_placa.FormattingEnabled = true;
            this.cmb_placa.Location = new System.Drawing.Point(7, 28);
            this.cmb_placa.Name = "cmb_placa";
            this.cmb_placa.Size = new System.Drawing.Size(82, 21);
            this.cmb_placa.TabIndex = 13;
            // 
            // txt_ano
            // 
            this.txt_ano.Location = new System.Drawing.Point(434, 29);
            this.txt_ano.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ano.Mask = "00/00";
            this.txt_ano.Name = "txt_ano";
            this.txt_ano.ReadOnly = true;
            this.txt_ano.Size = new System.Drawing.Size(68, 20);
            this.txt_ano.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(431, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ano";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(305, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Modelo";
            // 
            // txt_modelo
            // 
            this.txt_modelo.Location = new System.Drawing.Point(308, 29);
            this.txt_modelo.Margin = new System.Windows.Forms.Padding(2);
            this.txt_modelo.Name = "txt_modelo";
            this.txt_modelo.ReadOnly = true;
            this.txt_modelo.Size = new System.Drawing.Size(109, 20);
            this.txt_modelo.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 14);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "KM";
            // 
            // txt_km
            // 
            this.txt_km.Location = new System.Drawing.Point(103, 29);
            this.txt_km.Margin = new System.Windows.Forms.Padding(2);
            this.txt_km.Name = "txt_km";
            this.txt_km.Size = new System.Drawing.Size(68, 20);
            this.txt_km.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Marca";
            // 
            // txt_marca
            // 
            this.txt_marca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_marca.Location = new System.Drawing.Point(185, 29);
            this.txt_marca.Margin = new System.Windows.Forms.Padding(2);
            this.txt_marca.Name = "txt_marca";
            this.txt_marca.ReadOnly = true;
            this.txt_marca.Size = new System.Drawing.Size(109, 20);
            this.txt_marca.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Placa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Dt. cadastro";
            // 
            // cmb_cliente
            // 
            this.cmb_cliente.FormattingEnabled = true;
            this.cmb_cliente.Location = new System.Drawing.Point(5, 29);
            this.cmb_cliente.Name = "cmb_cliente";
            this.cmb_cliente.Size = new System.Drawing.Size(189, 21);
            this.cmb_cliente.TabIndex = 76;
            // 
            // cadastro_or
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 597);
            this.Controls.Add(this.dtp_cadastro);
            this.Controls.Add(this.bnt_cadastro);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "cadastro_or";
            this.Text = "cadastro_or";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cadastro_or_FormClosing);
            this.Load += new System.EventHandler(this.cadastro_or_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_cadastro;
        private System.Windows.Forms.Button bnt_cadastro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lst_servico_total;
        private System.Windows.Forms.ListBox lst_servicos_qtd;
        private System.Windows.Forms.Button bnt_editar_servico;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_total_servico;
        private System.Windows.Forms.ListBox lst_servicos;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lst_peca_total;
        private System.Windows.Forms.ListBox lst_pecas_qtd;
        private System.Windows.Forms.Button bnt_editar_peca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_total_pecas;
        private System.Windows.Forms.ListBox lst_pecas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_cliente;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_doc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_telefone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_placa;
        private System.Windows.Forms.MaskedTextBox txt_ano;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_modelo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_km;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_marca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}