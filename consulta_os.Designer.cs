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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.bnt_atualizar = new System.Windows.Forms.Button();
            this.bnt_add = new System.Windows.Forms.Button();
            this.lbl_order = new System.Windows.Forms.Label();
            this.bnt_pag = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_total_servicos = new System.Windows.Forms.TextBox();
            this.txt_total_pecas = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.bnt_pesquisar_ps = new System.Windows.Forms.Button();
            this.txt_ps = new System.Windows.Forms.TextBox();
            this.cmb_ps = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // bnt_atualizar
            // 
            this.bnt_atualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_atualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_atualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_atualizar.Image = ((System.Drawing.Image)(resources.GetObject("bnt_atualizar.Image")));
            this.bnt_atualizar.Location = new System.Drawing.Point(1664, 5);
            this.bnt_atualizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnt_atualizar.Name = "bnt_atualizar";
            this.bnt_atualizar.Size = new System.Drawing.Size(21, 20);
            this.bnt_atualizar.TabIndex = 28;
            this.bnt_atualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_atualizar.UseVisualStyleBackColor = false;
            this.bnt_atualizar.Click += new System.EventHandler(this.bnt_atualizar_Click);
            // 
            // bnt_add
            // 
            this.bnt_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_add.Image = ((System.Drawing.Image)(resources.GetObject("bnt_add.Image")));
            this.bnt_add.Location = new System.Drawing.Point(1638, 5);
            this.bnt_add.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(21, 20);
            this.bnt_add.TabIndex = 35;
            this.bnt_add.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_add.UseVisualStyleBackColor = false;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // lbl_order
            // 
            this.lbl_order.AutoSize = true;
            this.lbl_order.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_order.Location = new System.Drawing.Point(1665, 30);
            this.lbl_order.Name = "lbl_order";
            this.lbl_order.Size = new System.Drawing.Size(18, 20);
            this.lbl_order.TabIndex = 38;
            this.lbl_order.Text = "↑";
            this.lbl_order.Click += new System.EventHandler(this.lbl_order_Click);
            // 
            // bnt_pag
            // 
            this.bnt_pag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_pag.Location = new System.Drawing.Point(1509, 6);
            this.bnt_pag.Name = "bnt_pag";
            this.bnt_pag.Size = new System.Drawing.Size(124, 21);
            this.bnt_pag.TabIndex = 73;
            this.bnt_pag.Text = "Filtrar por pagamento";
            this.bnt_pag.UseVisualStyleBackColor = true;
            this.bnt_pag.Click += new System.EventHandler(this.bnt_pag_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.lbl_order);
            this.panel1.Controls.Add(this.bnt_pag);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Controls.Add(this.bnt_add);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.bnt_atualizar);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txt_total_servicos);
            this.panel1.Controls.Add(this.txt_total_pecas);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txt_total);
            this.panel1.Controls.Add(this.bnt_pesquisar_ps);
            this.panel1.Controls.Add(this.txt_ps);
            this.panel1.Controls.Add(this.cmb_ps);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txt_pesquisa);
            this.panel1.Controls.Add(this.bnt_pesquisar);
            this.panel1.Controls.Add(this.cmb_consulta);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1688, 1017);
            this.panel1.TabIndex = 74;
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 29);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1656, 650);
            this.listView1.TabIndex = 87;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.SteelBlue;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(145, 685);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(828, 211);
            this.chart1.TabIndex = 86;
            this.chart1.Text = "chart1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 689);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 15);
            this.label12.TabIndex = 85;
            this.label12.Text = "Faturamento serviços:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 731);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 15);
            this.label13.TabIndex = 84;
            this.label13.Text = "Faturamento peças:";
            // 
            // txt_total_servicos
            // 
            this.txt_total_servicos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_servicos.Location = new System.Drawing.Point(3, 707);
            this.txt_total_servicos.Name = "txt_total_servicos";
            this.txt_total_servicos.ReadOnly = true;
            this.txt_total_servicos.Size = new System.Drawing.Size(115, 21);
            this.txt_total_servicos.TabIndex = 83;
            // 
            // txt_total_pecas
            // 
            this.txt_total_pecas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_pecas.Location = new System.Drawing.Point(3, 749);
            this.txt_total_pecas.Name = "txt_total_pecas";
            this.txt_total_pecas.ReadOnly = true;
            this.txt_total_pecas.Size = new System.Drawing.Size(115, 21);
            this.txt_total_pecas.TabIndex = 82;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 773);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 15);
            this.label14.TabIndex = 81;
            this.label14.Text = "Faturamento total:";
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.Location = new System.Drawing.Point(4, 791);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(115, 21);
            this.txt_total.TabIndex = 80;
            // 
            // bnt_pesquisar_ps
            // 
            this.bnt_pesquisar_ps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_pesquisar_ps.Location = new System.Drawing.Point(904, 2);
            this.bnt_pesquisar_ps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnt_pesquisar_ps.Name = "bnt_pesquisar_ps";
            this.bnt_pesquisar_ps.Size = new System.Drawing.Size(87, 23);
            this.bnt_pesquisar_ps.TabIndex = 79;
            this.bnt_pesquisar_ps.Text = "Pesquisar";
            this.bnt_pesquisar_ps.UseVisualStyleBackColor = true;
            this.bnt_pesquisar_ps.Click += new System.EventHandler(this.bnt_pesquisar_ps_Click);
            // 
            // txt_ps
            // 
            this.txt_ps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ps.Location = new System.Drawing.Point(779, 3);
            this.txt_ps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ps.Name = "txt_ps";
            this.txt_ps.Size = new System.Drawing.Size(119, 21);
            this.txt_ps.TabIndex = 78;
            // 
            // cmb_ps
            // 
            this.cmb_ps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ps.FormattingEnabled = true;
            this.cmb_ps.Items.AddRange(new object[] {
            "Peças",
            "Serviços"});
            this.cmb_ps.Location = new System.Drawing.Point(671, 3);
            this.cmb_ps.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ps.Name = "cmb_ps";
            this.cmb_ps.Size = new System.Drawing.Size(102, 21);
            this.cmb_ps.TabIndex = 77;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(572, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 15);
            this.label9.TabIndex = 76;
            this.label9.Text = "Peças / Seviços";
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pesquisa.Location = new System.Drawing.Point(141, 3);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(141, 21);
            this.txt_pesquisa.TabIndex = 75;
            // 
            // bnt_pesquisar
            // 
            this.bnt_pesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_pesquisar.Location = new System.Drawing.Point(288, 2);
            this.bnt_pesquisar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnt_pesquisar.Name = "bnt_pesquisar";
            this.bnt_pesquisar.Size = new System.Drawing.Size(87, 23);
            this.bnt_pesquisar.TabIndex = 74;
            this.bnt_pesquisar.Text = "Pesquisar";
            this.bnt_pesquisar.UseVisualStyleBackColor = true;
            this.bnt_pesquisar.Click += new System.EventHandler(this.bnt_pesquisar_Click);
            // 
            // cmb_consulta
            // 
            this.cmb_consulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_consulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_consulta.FormattingEnabled = true;
            this.cmb_consulta.Items.AddRange(new object[] {
            "dt_cadastro",
            "cliente",
            "placa",
            "marca",
            "modelo"});
            this.cmb_consulta.Location = new System.Drawing.Point(3, 3);
            this.cmb_consulta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_consulta.Name = "cmb_consulta";
            this.cmb_consulta.Size = new System.Drawing.Size(132, 21);
            this.cmb_consulta.TabIndex = 73;
            // 
            // consulta_os
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1041);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "consulta_os";
            this.Text = "Consultar OS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.consulta_os_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bnt_atualizar;
        private System.Windows.Forms.Button bnt_add;
        private System.Windows.Forms.Label lbl_order;
        private System.Windows.Forms.Button bnt_pag;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_total_servicos;
        private System.Windows.Forms.TextBox txt_total_pecas;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Button bnt_pesquisar_ps;
        private System.Windows.Forms.TextBox txt_ps;
        private System.Windows.Forms.ComboBox cmb_ps;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.ComboBox cmb_consulta;
    }
}