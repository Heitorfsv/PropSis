namespace PrototipoSistema
{
    partial class qtd
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
            this.bnt_ok = new System.Windows.Forms.Button();
            this.txt_qtd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_valor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_desc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bnt_ok
            // 
            this.bnt_ok.Location = new System.Drawing.Point(270, 61);
            this.bnt_ok.Name = "bnt_ok";
            this.bnt_ok.Size = new System.Drawing.Size(75, 23);
            this.bnt_ok.TabIndex = 0;
            this.bnt_ok.Text = "Ok";
            this.bnt_ok.UseVisualStyleBackColor = true;
            this.bnt_ok.Click += new System.EventHandler(this.bnt_ok_Click);
            // 
            // txt_qtd
            // 
            this.txt_qtd.Location = new System.Drawing.Point(73, 38);
            this.txt_qtd.Name = "txt_qtd";
            this.txt_qtd.Size = new System.Drawing.Size(100, 20);
            this.txt_qtd.TabIndex = 1;
            this.txt_qtd.Text = "1";
            this.txt_qtd.TextChanged += new System.EventHandler(this.txt_qtd_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Quantidade";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Valor";
            // 
            // txt_valor
            // 
            this.txt_valor.Location = new System.Drawing.Point(73, 12);
            this.txt_valor.Name = "txt_valor";
            this.txt_valor.Size = new System.Drawing.Size(100, 20);
            this.txt_valor.TabIndex = 3;
            this.txt_valor.TextChanged += new System.EventHandler(this.txt_valor_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Total";
            // 
            // txt_total
            // 
            this.txt_total.Location = new System.Drawing.Point(245, 8);
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(100, 20);
            this.txt_total.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Desc.";
            // 
            // txt_desc
            // 
            this.txt_desc.Location = new System.Drawing.Point(245, 34);
            this.txt_desc.Name = "txt_desc";
            this.txt_desc.Size = new System.Drawing.Size(100, 20);
            this.txt_desc.TabIndex = 7;
            this.txt_desc.Text = "0";
            this.txt_desc.TextChanged += new System.EventHandler(this.txt_desc_TextChanged);
            // 
            // qtd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 97);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_desc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_valor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_qtd);
            this.Controls.Add(this.bnt_ok);
            this.Name = "qtd";
            this.Text = "Quantidade";
            this.Load += new System.EventHandler(this.qtd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnt_ok;
        private System.Windows.Forms.TextBox txt_qtd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_valor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_desc;
    }
}