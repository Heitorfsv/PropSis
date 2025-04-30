namespace PrototipoSistema
{
    partial class add_pecas
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
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.lst_pesquisa = new System.Windows.Forms.ListBox();
            this.lst_qtd = new System.Windows.Forms.ListBox();
            this.lst_pecas = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_valor = new System.Windows.Forms.ListBox();
            this.bnt_delete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lst_total = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Location = new System.Drawing.Point(7, 14);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(251, 20);
            this.txt_pesquisa.TabIndex = 7;
            this.txt_pesquisa.TextChanged += new System.EventHandler(this.txt_pesquisa_TextChanged);
            // 
            // lst_pesquisa
            // 
            this.lst_pesquisa.FormattingEnabled = true;
            this.lst_pesquisa.Location = new System.Drawing.Point(7, 38);
            this.lst_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.lst_pesquisa.Name = "lst_pesquisa";
            this.lst_pesquisa.Size = new System.Drawing.Size(251, 225);
            this.lst_pesquisa.TabIndex = 5;
            this.lst_pesquisa.DoubleClick += new System.EventHandler(this.lst_pesquisa_DoubleClick);
            // 
            // lst_qtd
            // 
            this.lst_qtd.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lst_qtd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_qtd.FormattingEnabled = true;
            this.lst_qtd.Location = new System.Drawing.Point(552, 38);
            this.lst_qtd.Margin = new System.Windows.Forms.Padding(2);
            this.lst_qtd.Name = "lst_qtd";
            this.lst_qtd.Size = new System.Drawing.Size(39, 225);
            this.lst_qtd.TabIndex = 11;
            // 
            // lst_pecas
            // 
            this.lst_pecas.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lst_pecas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_pecas.FormattingEnabled = true;
            this.lst_pecas.Location = new System.Drawing.Point(285, 38);
            this.lst_pecas.Margin = new System.Windows.Forms.Padding(2);
            this.lst_pecas.Name = "lst_pecas";
            this.lst_pecas.Size = new System.Drawing.Size(205, 225);
            this.lst_pecas.TabIndex = 10;
            this.lst_pecas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst_pecas_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(549, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Qtd.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Peça";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(491, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Valor";
            // 
            // lst_valor
            // 
            this.lst_valor.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lst_valor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_valor.FormattingEnabled = true;
            this.lst_valor.Location = new System.Drawing.Point(494, 38);
            this.lst_valor.Margin = new System.Windows.Forms.Padding(2);
            this.lst_valor.Name = "lst_valor";
            this.lst_valor.Size = new System.Drawing.Size(54, 225);
            this.lst_valor.TabIndex = 14;
            // 
            // bnt_delete
            // 
            this.bnt_delete.ForeColor = System.Drawing.Color.DarkRed;
            this.bnt_delete.Location = new System.Drawing.Point(7, 275);
            this.bnt_delete.Name = "bnt_delete";
            this.bnt_delete.Size = new System.Drawing.Size(60, 20);
            this.bnt_delete.TabIndex = 16;
            this.bnt_delete.Text = "Delete";
            this.bnt_delete.UseVisualStyleBackColor = true;
            this.bnt_delete.Click += new System.EventHandler(this.bnt_delete_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(592, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Subtotal";
            // 
            // lst_total
            // 
            this.lst_total.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lst_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_total.FormattingEnabled = true;
            this.lst_total.Location = new System.Drawing.Point(595, 38);
            this.lst_total.Margin = new System.Windows.Forms.Padding(2);
            this.lst_total.Name = "lst_total";
            this.lst_total.Size = new System.Drawing.Size(54, 225);
            this.lst_total.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(546, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Total";
            // 
            // txt_total
            // 
            this.txt_total.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txt_total.Enabled = false;
            this.txt_total.Location = new System.Drawing.Point(549, 307);
            this.txt_total.Name = "txt_total";
            this.txt_total.Size = new System.Drawing.Size(100, 20);
            this.txt_total.TabIndex = 20;
            // 
            // add_pecas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 339);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lst_total);
            this.Controls.Add(this.bnt_delete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lst_valor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lst_qtd);
            this.Controls.Add(this.lst_pecas);
            this.Controls.Add(this.txt_pesquisa);
            this.Controls.Add(this.lst_pesquisa);
            this.Name = "add_pecas";
            this.Text = "Adicionar peças";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.add_pecas_FormClosing);
            this.Load += new System.EventHandler(this.add_pecas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.ListBox lst_pesquisa;
        private System.Windows.Forms.ListBox lst_qtd;
        private System.Windows.Forms.ListBox lst_pecas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lst_valor;
        private System.Windows.Forms.Button bnt_delete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lst_total;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_total;
    }
}