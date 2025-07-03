namespace PrototipoSistema
{
    partial class aniversarios
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
            this.lst_15dias = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lst_hoje = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Aníversarios dos próximos 15 dias";
            // 
            // lst_15dias
            // 
            this.lst_15dias.FormattingEnabled = true;
            this.lst_15dias.Location = new System.Drawing.Point(287, 26);
            this.lst_15dias.Name = "lst_15dias";
            this.lst_15dias.Size = new System.Drawing.Size(266, 316);
            this.lst_15dias.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Aníversarios de hoje";
            // 
            // lst_hoje
            // 
            this.lst_hoje.FormattingEnabled = true;
            this.lst_hoje.Location = new System.Drawing.Point(15, 26);
            this.lst_hoje.Name = "lst_hoje";
            this.lst_hoje.Size = new System.Drawing.Size(266, 316);
            this.lst_hoje.TabIndex = 3;
            // 
            // aniversarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 361);
            this.Controls.Add(this.lst_hoje);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lst_15dias);
            this.Controls.Add(this.label1);
            this.Name = "aniversarios";
            this.Text = "aniversarios";
            this.Load += new System.EventHandler(this.aniversarios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lst_15dias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lst_hoje;
    }
}