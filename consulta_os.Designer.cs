namespace PrototipoSistema
{
    partial class consulta_os
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(consulta_os));
            this.lst_cliente = new System.Windows.Forms.ListBox();
            this.lst_placa = new System.Windows.Forms.ListBox();
            this.lst_marca = new System.Windows.Forms.ListBox();
            this.lst_modelo = new System.Windows.Forms.ListBox();
            this.lst_dt = new System.Windows.Forms.ListBox();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.txt_pequisa = new System.Windows.Forms.TextBox();
            this.lst_preco_servico = new System.Windows.Forms.ListBox();
            this.lst_preco_peca = new System.Windows.Forms.ListBox();
            this.lst_total = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bnt_atualizar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_ps = new System.Windows.Forms.TextBox();
            this.cmb_ps = new System.Windows.Forms.ComboBox();
            this.bnt_pesquisar_ps = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.lst_dt_saida = new System.Windows.Forms.ListBox();
            this.bnt_add = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lst_telefone = new System.Windows.Forms.ListBox();
            this.lbl_order = new System.Windows.Forms.Label();
            this.scrollbar = new System.Windows.Forms.VScrollBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_total_servicos = new System.Windows.Forms.TextBox();
            this.txt_total_pecas = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lst_cliente
            // 
            this.lst_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_cliente.FormattingEnabled = true;
            this.lst_cliente.ItemHeight = 16;
            this.lst_cliente.Location = new System.Drawing.Point(136, 59);
            this.lst_cliente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_cliente.Name = "lst_cliente";
            this.lst_cliente.Size = new System.Drawing.Size(123, 500);
            this.lst_cliente.TabIndex = 0;
            this.lst_cliente.SelectedIndexChanged += new System.EventHandler(this.lst_cliente_SelectedIndexChanged);
            this.lst_cliente.DoubleClick += new System.EventHandler(this.lst_cliente_DoubleClick);
            // 
            // lst_placa
            // 
            this.lst_placa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_placa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lst_placa.FormattingEnabled = true;
            this.lst_placa.ItemHeight = 16;
            this.lst_placa.Location = new System.Drawing.Point(394, 59);
            this.lst_placa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_placa.Name = "lst_placa";
            this.lst_placa.Size = new System.Drawing.Size(123, 500);
            this.lst_placa.TabIndex = 1;
            this.lst_placa.Click += new System.EventHandler(this.lst_placa_Click);
            this.lst_placa.DoubleClick += new System.EventHandler(this.lst_placa_DoubleClick);
            // 
            // lst_marca
            // 
            this.lst_marca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_marca.FormattingEnabled = true;
            this.lst_marca.ItemHeight = 16;
            this.lst_marca.Location = new System.Drawing.Point(523, 59);
            this.lst_marca.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_marca.Name = "lst_marca";
            this.lst_marca.Size = new System.Drawing.Size(123, 500);
            this.lst_marca.TabIndex = 2;
            this.lst_marca.Click += new System.EventHandler(this.lst_marca_Click);
            this.lst_marca.DoubleClick += new System.EventHandler(this.lst_marca_DoubleClick);
            // 
            // lst_modelo
            // 
            this.lst_modelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_modelo.FormattingEnabled = true;
            this.lst_modelo.ItemHeight = 16;
            this.lst_modelo.Location = new System.Drawing.Point(652, 59);
            this.lst_modelo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_modelo.Name = "lst_modelo";
            this.lst_modelo.Size = new System.Drawing.Size(123, 500);
            this.lst_modelo.TabIndex = 3;
            this.lst_modelo.Click += new System.EventHandler(this.lst_modelo_Click);
            this.lst_modelo.DoubleClick += new System.EventHandler(this.lst_modelo_DoubleClick);
            // 
            // lst_dt
            // 
            this.lst_dt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_dt.FormattingEnabled = true;
            this.lst_dt.ItemHeight = 16;
            this.lst_dt.Location = new System.Drawing.Point(7, 59);
            this.lst_dt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_dt.Name = "lst_dt";
            this.lst_dt.Size = new System.Drawing.Size(123, 500);
            this.lst_dt.TabIndex = 4;
            this.lst_dt.SelectedIndexChanged += new System.EventHandler(this.lst_dt_SelectedIndexChanged);
            this.lst_dt.DoubleClick += new System.EventHandler(this.lst_dt_DoubleClick);
            // 
            // cmb_consulta
            // 
            this.cmb_consulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_consulta.FormattingEnabled = true;
            this.cmb_consulta.Items.AddRange(new object[] {
            "dt_cadastro",
            "cliente",
            "placa",
            "marca",
            "modelo"});
            this.cmb_consulta.Location = new System.Drawing.Point(12, 10);
            this.cmb_consulta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_consulta.Name = "cmb_consulta";
            this.cmb_consulta.Size = new System.Drawing.Size(108, 24);
            this.cmb_consulta.TabIndex = 5;
            // 
            // bnt_pesquisar
            // 
            this.bnt_pesquisar.Location = new System.Drawing.Point(249, 10);
            this.bnt_pesquisar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnt_pesquisar.Name = "bnt_pesquisar";
            this.bnt_pesquisar.Size = new System.Drawing.Size(87, 25);
            this.bnt_pesquisar.TabIndex = 6;
            this.bnt_pesquisar.Text = "Pesquisar";
            this.bnt_pesquisar.UseVisualStyleBackColor = true;
            this.bnt_pesquisar.Click += new System.EventHandler(this.bnt_pesquisar_Click);
            // 
            // txt_pequisa
            // 
            this.txt_pequisa.Location = new System.Drawing.Point(125, 10);
            this.txt_pequisa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_pequisa.Name = "txt_pequisa";
            this.txt_pequisa.Size = new System.Drawing.Size(119, 22);
            this.txt_pequisa.TabIndex = 7;
            // 
            // lst_preco_servico
            // 
            this.lst_preco_servico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_preco_servico.FormattingEnabled = true;
            this.lst_preco_servico.ItemHeight = 16;
            this.lst_preco_servico.Location = new System.Drawing.Point(911, 59);
            this.lst_preco_servico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_preco_servico.Name = "lst_preco_servico";
            this.lst_preco_servico.Size = new System.Drawing.Size(123, 500);
            this.lst_preco_servico.TabIndex = 8;
            this.lst_preco_servico.Click += new System.EventHandler(this.lst_preco_servico_Click);
            this.lst_preco_servico.DoubleClick += new System.EventHandler(this.lst_preco_servico_DoubleClick);
            // 
            // lst_preco_peca
            // 
            this.lst_preco_peca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_preco_peca.FormattingEnabled = true;
            this.lst_preco_peca.ItemHeight = 16;
            this.lst_preco_peca.Location = new System.Drawing.Point(781, 59);
            this.lst_preco_peca.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_preco_peca.Name = "lst_preco_peca";
            this.lst_preco_peca.Size = new System.Drawing.Size(123, 500);
            this.lst_preco_peca.TabIndex = 9;
            this.lst_preco_peca.Click += new System.EventHandler(this.lst_preco_peca_Click);
            this.lst_preco_peca.DoubleClick += new System.EventHandler(this.lst_preco_peca_DoubleClick);
            // 
            // lst_total
            // 
            this.lst_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_total.FormattingEnabled = true;
            this.lst_total.ItemHeight = 16;
            this.lst_total.Location = new System.Drawing.Point(1040, 59);
            this.lst_total.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_total.Name = "lst_total";
            this.lst_total.Size = new System.Drawing.Size(123, 500);
            this.lst_total.TabIndex = 10;
            this.lst_total.Click += new System.EventHandler(this.lst_total_Click);
            this.lst_total.DoubleClick += new System.EventHandler(this.lst_total_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(392, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Placa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(521, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Marca";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(649, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Modelo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Dt. entrada";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(909, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Valor Serviços";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(779, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Valor Peças";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1037, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Total ";
            // 
            // bnt_atualizar
            // 
            this.bnt_atualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_atualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_atualizar.Image = ((System.Drawing.Image)(resources.GetObject("bnt_atualizar.Image")));
            this.bnt_atualizar.Location = new System.Drawing.Point(1271, 10);
            this.bnt_atualizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnt_atualizar.Name = "bnt_atualizar";
            this.bnt_atualizar.Size = new System.Drawing.Size(21, 20);
            this.bnt_atualizar.TabIndex = 28;
            this.bnt_atualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_atualizar.UseVisualStyleBackColor = false;
            this.bnt_atualizar.Click += new System.EventHandler(this.bnt_atualizar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(569, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 16);
            this.label9.TabIndex = 29;
            this.label9.Text = "Peças / Seviços";
            // 
            // txt_ps
            // 
            this.txt_ps.Location = new System.Drawing.Point(804, 10);
            this.txt_ps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ps.Name = "txt_ps";
            this.txt_ps.Size = new System.Drawing.Size(119, 22);
            this.txt_ps.TabIndex = 31;
            // 
            // cmb_ps
            // 
            this.cmb_ps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ps.FormattingEnabled = true;
            this.cmb_ps.Items.AddRange(new object[] {
            "Peças",
            "Serviços"});
            this.cmb_ps.Location = new System.Drawing.Point(691, 10);
            this.cmb_ps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ps.Name = "cmb_ps";
            this.cmb_ps.Size = new System.Drawing.Size(108, 24);
            this.cmb_ps.TabIndex = 30;
            // 
            // bnt_pesquisar_ps
            // 
            this.bnt_pesquisar_ps.Location = new System.Drawing.Point(929, 10);
            this.bnt_pesquisar_ps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnt_pesquisar_ps.Name = "bnt_pesquisar_ps";
            this.bnt_pesquisar_ps.Size = new System.Drawing.Size(87, 25);
            this.bnt_pesquisar_ps.TabIndex = 32;
            this.bnt_pesquisar_ps.Text = "Pesquisar";
            this.bnt_pesquisar_ps.UseVisualStyleBackColor = true;
            this.bnt_pesquisar_ps.Click += new System.EventHandler(this.bnt_pesquisar_ps_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1167, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 16);
            this.label10.TabIndex = 34;
            this.label10.Text = "Dt. saída";
            // 
            // lst_dt_saida
            // 
            this.lst_dt_saida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_dt_saida.FormattingEnabled = true;
            this.lst_dt_saida.ItemHeight = 16;
            this.lst_dt_saida.Location = new System.Drawing.Point(1169, 59);
            this.lst_dt_saida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_dt_saida.Name = "lst_dt_saida";
            this.lst_dt_saida.Size = new System.Drawing.Size(123, 500);
            this.lst_dt_saida.TabIndex = 33;
            this.lst_dt_saida.Click += new System.EventHandler(this.lst_dt_saida_Click);
            this.lst_dt_saida.DoubleClick += new System.EventHandler(this.lst_dt_saida_DoubleClick);
            // 
            // bnt_add
            // 
            this.bnt_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_add.Image = ((System.Drawing.Image)(resources.GetObject("bnt_add.Image")));
            this.bnt_add.Location = new System.Drawing.Point(1245, 10);
            this.bnt_add.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(21, 20);
            this.bnt_add.TabIndex = 35;
            this.bnt_add.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_add.UseVisualStyleBackColor = false;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(262, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 16);
            this.label11.TabIndex = 37;
            this.label11.Text = "Telefone";
            // 
            // lst_telefone
            // 
            this.lst_telefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_telefone.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lst_telefone.FormattingEnabled = true;
            this.lst_telefone.ItemHeight = 16;
            this.lst_telefone.Location = new System.Drawing.Point(265, 59);
            this.lst_telefone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lst_telefone.Name = "lst_telefone";
            this.lst_telefone.Size = new System.Drawing.Size(123, 500);
            this.lst_telefone.TabIndex = 36;
            this.lst_telefone.Click += new System.EventHandler(this.lst_telefone_Click);
            this.lst_telefone.DoubleClick += new System.EventHandler(this.lst_telefone_DoubleClick);
            // 
            // lbl_order
            // 
            this.lbl_order.AutoSize = true;
            this.lbl_order.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_order.Location = new System.Drawing.Point(1299, 59);
            this.lbl_order.Name = "lbl_order";
            this.lbl_order.Size = new System.Drawing.Size(18, 20);
            this.lbl_order.TabIndex = 38;
            this.lbl_order.Text = "↑";
            this.lbl_order.Click += new System.EventHandler(this.lbl_order_Click);
            // 
            // scrollbar
            // 
            this.scrollbar.Location = new System.Drawing.Point(1320, 59);
            this.scrollbar.Name = "scrollbar";
            this.scrollbar.Size = new System.Drawing.Size(17, 500);
            this.scrollbar.TabIndex = 39;
            this.scrollbar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollbar_Scroll);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1077, 649);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 16);
            this.label12.TabIndex = 45;
            this.label12.Text = "Faturamento serviços:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1090, 675);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 16);
            this.label13.TabIndex = 44;
            this.label13.Text = "Faturamento peças:";
            // 
            // txt_total_servicos
            // 
            this.txt_total_servicos.Location = new System.Drawing.Point(1222, 646);
            this.txt_total_servicos.Name = "txt_total_servicos";
            this.txt_total_servicos.ReadOnly = true;
            this.txt_total_servicos.Size = new System.Drawing.Size(115, 22);
            this.txt_total_servicos.TabIndex = 43;
            // 
            // txt_total_pecas
            // 
            this.txt_total_pecas.Location = new System.Drawing.Point(1222, 672);
            this.txt_total_pecas.Name = "txt_total_pecas";
            this.txt_total_pecas.ReadOnly = true;
            this.txt_total_pecas.Size = new System.Drawing.Size(115, 22);
            this.txt_total_pecas.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1103, 701);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 16);
            this.label14.TabIndex = 41;
            this.label14.Text = "Faturamento total:";
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(1222, 698);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(115, 22);
            this.txt_total.TabIndex = 40;
            // 
            // consulta_os
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 914);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_total_servicos);
            this.Controls.Add(this.txt_total_pecas);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.scrollbar);
            this.Controls.Add(this.lbl_order);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lst_telefone);
            this.Controls.Add(this.bnt_add);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lst_dt_saida);
            this.Controls.Add(this.bnt_pesquisar_ps);
            this.Controls.Add(this.txt_ps);
            this.Controls.Add(this.cmb_ps);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bnt_atualizar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lst_total);
            this.Controls.Add(this.lst_preco_peca);
            this.Controls.Add(this.lst_preco_servico);
            this.Controls.Add(this.txt_pequisa);
            this.Controls.Add(this.bnt_pesquisar);
            this.Controls.Add(this.cmb_consulta);
            this.Controls.Add(this.lst_dt);
            this.Controls.Add(this.lst_modelo);
            this.Controls.Add(this.lst_marca);
            this.Controls.Add(this.lst_placa);
            this.Controls.Add(this.lst_cliente);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "consulta_os";
            this.Text = "Consultar OS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.consulta_os_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_cliente;
        private System.Windows.Forms.ListBox lst_placa;
        private System.Windows.Forms.ListBox lst_marca;
        private System.Windows.Forms.ListBox lst_modelo;
        private System.Windows.Forms.ListBox lst_dt;
        private System.Windows.Forms.ComboBox cmb_consulta;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.TextBox txt_pequisa;
        private System.Windows.Forms.ListBox lst_preco_servico;
        private System.Windows.Forms.ListBox lst_preco_peca;
        private System.Windows.Forms.ListBox lst_total;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bnt_atualizar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_ps;
        private System.Windows.Forms.ComboBox cmb_ps;
        private System.Windows.Forms.Button bnt_pesquisar_ps;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lst_dt_saida;
        private System.Windows.Forms.Button bnt_add;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lst_telefone;
        private System.Windows.Forms.Label lbl_order;
        private System.Windows.Forms.VScrollBar scrollbar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_total_servicos;
        private System.Windows.Forms.TextBox txt_total_pecas;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_total;
    }
}