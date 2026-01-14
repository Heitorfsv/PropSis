namespace PrototipoSistema
{
    partial class add_troca
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
            this.gb_troca = new System.Windows.Forms.GroupBox();
            this.dtp_troca_oleo = new System.Windows.Forms.DateTimePicker();
            this.dtp_revisao = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bnt_salvar = new System.Windows.Forms.Button();
            this.gb_troca.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_troca
            // 
            this.gb_troca.Controls.Add(this.dtp_troca_oleo);
            this.gb_troca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_troca.Location = new System.Drawing.Point(13, 12);
            this.gb_troca.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb_troca.Name = "gb_troca";
            this.gb_troca.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gb_troca.Size = new System.Drawing.Size(169, 51);
            this.gb_troca.TabIndex = 71;
            this.gb_troca.TabStop = false;
            this.gb_troca.Text = "Proxima troca de oleo em:";
            // 
            // dtp_troca_oleo
            // 
            this.dtp_troca_oleo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_troca_oleo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_troca_oleo.Location = new System.Drawing.Point(8, 20);
            this.dtp_troca_oleo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtp_troca_oleo.Name = "dtp_troca_oleo";
            this.dtp_troca_oleo.Size = new System.Drawing.Size(116, 22);
            this.dtp_troca_oleo.TabIndex = 60;
            // 
            // dtp_revisao
            // 
            this.dtp_revisao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_revisao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_revisao.Location = new System.Drawing.Point(8, 20);
            this.dtp_revisao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtp_revisao.Name = "dtp_revisao";
            this.dtp_revisao.Size = new System.Drawing.Size(116, 22);
            this.dtp_revisao.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_revisao);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(190, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(150, 51);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proxima revisão em:";
            // 
            // bnt_salvar
            // 
            this.bnt_salvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_salvar.Location = new System.Drawing.Point(265, 71);
            this.bnt_salvar.Name = "bnt_salvar";
            this.bnt_salvar.Size = new System.Drawing.Size(75, 23);
            this.bnt_salvar.TabIndex = 73;
            this.bnt_salvar.Text = "Salvar";
            this.bnt_salvar.UseVisualStyleBackColor = true;
            this.bnt_salvar.Click += new System.EventHandler(this.bnt_salvar_Click);
            // 
            // add_troca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 106);
            this.Controls.Add(this.bnt_salvar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_troca);
            this.Name = "add_troca";
            this.Text = "Definir avisos futuros:";
            this.Load += new System.EventHandler(this.add_troca_Load);
            this.gb_troca.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_troca;
        private System.Windows.Forms.DateTimePicker dtp_troca_oleo;
        private System.Windows.Forms.DateTimePicker dtp_revisao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bnt_salvar;
    }
}