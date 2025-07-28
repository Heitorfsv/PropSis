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
            this.bnt_editar_servico = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_total_servico = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bnt_editar_peca = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_total_pecas = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_cliente = new System.Windows.Forms.ComboBox();
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
            this.label3 = new System.Windows.Forms.Label();
            this.txt_marca = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.bnt_deletar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizarImpressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lst_servicos = new System.Windows.Forms.ListView();
            this.lst_pecas = new System.Windows.Forms.ListView();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtp_cadastro
            // 
            this.dtp_cadastro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_cadastro.Location = new System.Drawing.Point(13, 50);
            this.dtp_cadastro.Name = "dtp_cadastro";
            this.dtp_cadastro.Size = new System.Drawing.Size(200, 20);
            this.dtp_cadastro.TabIndex = 75;
            // 
            // bnt_cadastro
            // 
            this.bnt_cadastro.Location = new System.Drawing.Point(831, 543);
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
            this.label9.Location = new System.Drawing.Point(706, 503);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 69;
            this.label9.Text = "Total";
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(709, 518);
            this.txt_total.Margin = new System.Windows.Forms.Padding(2);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(117, 20);
            this.txt_total.TabIndex = 67;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lst_servicos);
            this.groupBox6.Controls.Add(this.bnt_editar_servico);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.txt_total_servico);
            this.groupBox6.Location = new System.Drawing.Point(422, 194);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(404, 301);
            this.groupBox6.TabIndex = 68;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Serviços";
            // 
            // bnt_editar_servico
            // 
            this.bnt_editar_servico.Location = new System.Drawing.Point(309, 273);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lst_pecas);
            this.groupBox4.Controls.Add(this.bnt_editar_peca);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txt_total_pecas);
            this.groupBox4.Location = new System.Drawing.Point(14, 194);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(404, 301);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Peças";
            // 
            // bnt_editar_peca
            // 
            this.bnt_editar_peca.Location = new System.Drawing.Point(309, 273);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_cliente);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.txt_doc);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txt_telefone);
            this.groupBox2.Location = new System.Drawing.Point(13, 75);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(517, 57);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dados do cliente";
            // 
            // cmb_cliente
            // 
            this.cmb_cliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmb_cliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_cliente.FormattingEnabled = true;
            this.cmb_cliente.Location = new System.Drawing.Point(5, 29);
            this.cmb_cliente.Name = "cmb_cliente";
            this.cmb_cliente.Size = new System.Drawing.Size(189, 21);
            this.cmb_cliente.TabIndex = 76;
            this.cmb_cliente.TextChanged += new System.EventHandler(this.cmb_cliente_TextChanged);
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
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_marca);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 136);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(516, 55);
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
            this.cmb_placa.TextChanged += new System.EventHandler(this.cmb_placa_TextChanged);
            // 
            // txt_ano
            // 
            this.txt_ano.Location = new System.Drawing.Point(336, 28);
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
            this.label6.Location = new System.Drawing.Point(333, 13);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ano";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(220, 13);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Modelo";
            // 
            // txt_modelo
            // 
            this.txt_modelo.Location = new System.Drawing.Point(223, 28);
            this.txt_modelo.Margin = new System.Windows.Forms.Padding(2);
            this.txt_modelo.Name = "txt_modelo";
            this.txt_modelo.ReadOnly = true;
            this.txt_modelo.Size = new System.Drawing.Size(109, 20);
            this.txt_modelo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Marca";
            // 
            // txt_marca
            // 
            this.txt_marca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_marca.Location = new System.Drawing.Point(94, 28);
            this.txt_marca.Margin = new System.Windows.Forms.Padding(2);
            this.txt_marca.Name = "txt_marca";
            this.txt_marca.ReadOnly = true;
            this.txt_marca.Size = new System.Drawing.Size(125, 20);
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
            this.label1.Location = new System.Drawing.Point(11, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Dt. cadastro";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bnt_deletar
            // 
            this.bnt_deletar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bnt_deletar.Location = new System.Drawing.Point(831, 517);
            this.bnt_deletar.Name = "bnt_deletar";
            this.bnt_deletar.Size = new System.Drawing.Size(86, 23);
            this.bnt_deletar.TabIndex = 107;
            this.bnt_deletar.Text = "Deletar";
            this.bnt_deletar.UseVisualStyleBackColor = true;
            this.bnt_deletar.Click += new System.EventHandler(this.bnt_deletar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1022, 24);
            this.menuStrip1.TabIndex = 108;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualizarImpressToolStripMenuItem,
            this.imprimirToolStripMenuItem1});
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.imprimirToolStripMenuItem.Text = "Impressão";
            // 
            // visualizarImpressToolStripMenuItem
            // 
            this.visualizarImpressToolStripMenuItem.Name = "visualizarImpressToolStripMenuItem";
            this.visualizarImpressToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.visualizarImpressToolStripMenuItem.Text = "Visualizar impressão";
            this.visualizarImpressToolStripMenuItem.Click += new System.EventHandler(this.visualizarImpressToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem1
            // 
            this.imprimirToolStripMenuItem1.Name = "imprimirToolStripMenuItem1";
            this.imprimirToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.imprimirToolStripMenuItem1.Text = "Salvar ";
            this.imprimirToolStripMenuItem1.Click += new System.EventHandler(this.imprimirToolStripMenuItem1_Click);
            // 
            // lst_servicos
            // 
            this.lst_servicos.FullRowSelect = true;
            this.lst_servicos.HideSelection = false;
            this.lst_servicos.Location = new System.Drawing.Point(4, 18);
            this.lst_servicos.MultiSelect = false;
            this.lst_servicos.Name = "lst_servicos";
            this.lst_servicos.Size = new System.Drawing.Size(369, 240);
            this.lst_servicos.TabIndex = 26;
            this.lst_servicos.UseCompatibleStateImageBehavior = false;
            // 
            // lst_pecas
            // 
            this.lst_pecas.FullRowSelect = true;
            this.lst_pecas.HideSelection = false;
            this.lst_pecas.Location = new System.Drawing.Point(5, 18);
            this.lst_pecas.MultiSelect = false;
            this.lst_pecas.Name = "lst_pecas";
            this.lst_pecas.Size = new System.Drawing.Size(369, 240);
            this.lst_pecas.TabIndex = 27;
            this.lst_pecas.UseCompatibleStateImageBehavior = false;
            // 
            // cadastro_or
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 597);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.bnt_deletar);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtp_cadastro;
        private System.Windows.Forms.Button bnt_cadastro;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button bnt_editar_servico;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_total_servico;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button bnt_editar_peca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_total_pecas;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_marca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button bnt_deletar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizarImpressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem1;
        private System.Windows.Forms.ListView lst_servicos;
        private System.Windows.Forms.ListView lst_pecas;
    }
}