namespace PrototipoSistema
{
    partial class historico
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
            this.lst_dt_saida = new System.Windows.Forms.ListBox();
            this.lst_cliente = new System.Windows.Forms.ListBox();
            this.lst_marca = new System.Windows.Forms.ListBox();
            this.lst_modelo = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_titulo = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lst_dt_saida
            // 
            this.lst_dt_saida.FormattingEnabled = true;
            this.lst_dt_saida.Location = new System.Drawing.Point(12, 82);
            this.lst_dt_saida.Name = "lst_dt_saida";
            this.lst_dt_saida.Size = new System.Drawing.Size(124, 212);
            this.lst_dt_saida.TabIndex = 0;
            this.lst_dt_saida.SelectedIndexChanged += new System.EventHandler(this.lst_dt_saida_SelectedIndexChanged);
            this.lst_dt_saida.DoubleClick += new System.EventHandler(this.lst_dt_saida_DoubleClick);
            // 
            // lst_cliente
            // 
            this.lst_cliente.FormattingEnabled = true;
            this.lst_cliente.Location = new System.Drawing.Point(142, 82);
            this.lst_cliente.Name = "lst_cliente";
            this.lst_cliente.Size = new System.Drawing.Size(124, 212);
            this.lst_cliente.TabIndex = 1;
            this.lst_cliente.Click += new System.EventHandler(this.lst_cliente_Click);
            this.lst_cliente.DoubleClick += new System.EventHandler(this.lst_cliente_DoubleClick);
            // 
            // lst_marca
            // 
            this.lst_marca.FormattingEnabled = true;
            this.lst_marca.Location = new System.Drawing.Point(272, 82);
            this.lst_marca.Name = "lst_marca";
            this.lst_marca.Size = new System.Drawing.Size(124, 212);
            this.lst_marca.TabIndex = 2;
            this.lst_marca.Click += new System.EventHandler(this.lst_marca_Click);
            this.lst_marca.DoubleClick += new System.EventHandler(this.lst_marca_DoubleClick);
            // 
            // lst_modelo
            // 
            this.lst_modelo.FormattingEnabled = true;
            this.lst_modelo.Location = new System.Drawing.Point(402, 82);
            this.lst_modelo.Name = "lst_modelo";
            this.lst_modelo.Size = new System.Drawing.Size(124, 212);
            this.lst_modelo.TabIndex = 3;
            this.lst_modelo.Click += new System.EventHandler(this.lst_modelo_Click);
            this.lst_modelo.DoubleClick += new System.EventHandler(this.lst_modelo_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data de saída";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(139, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(269, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Marca";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(399, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Modelo";
            // 
            // lbl_titulo
            // 
            this.lbl_titulo.AutoSize = true;
            this.lbl_titulo.Location = new System.Drawing.Point(9, 7);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Size = new System.Drawing.Size(84, 13);
            this.lbl_titulo.TabIndex = 8;
            this.lbl_titulo.Text = "Faturamento da:";
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(12, 23);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(124, 20);
            this.txt_total.TabIndex = 9;
            // 
            // historico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 306);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.lbl_titulo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lst_modelo);
            this.Controls.Add(this.lst_marca);
            this.Controls.Add(this.lst_cliente);
            this.Controls.Add(this.lst_dt_saida);
            this.Name = "historico";
            this.Text = "Histórico";
            this.Load += new System.EventHandler(this.historico_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_dt_saida;
        private System.Windows.Forms.ListBox lst_cliente;
        private System.Windows.Forms.ListBox lst_marca;
        private System.Windows.Forms.ListBox lst_modelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_titulo;
        private System.Windows.Forms.TextBox txt_total;
    }
}