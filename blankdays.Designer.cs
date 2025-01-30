namespace PrototipoSistema
{
    partial class blankdays
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_days = new System.Windows.Forms.Label();
            this.lst_tarefas = new System.Windows.Forms.ListBox();
            this.bnt_add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_days
            // 
            this.lbl_days.AutoSize = true;
            this.lbl_days.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_days.Location = new System.Drawing.Point(3, 0);
            this.lbl_days.Name = "lbl_days";
            this.lbl_days.Size = new System.Drawing.Size(28, 21);
            this.lbl_days.TabIndex = 0;
            this.lbl_days.Text = "00";
            // 
            // lst_tarefas
            // 
            this.lst_tarefas.FormattingEnabled = true;
            this.lst_tarefas.Location = new System.Drawing.Point(3, 24);
            this.lst_tarefas.Name = "lst_tarefas";
            this.lst_tarefas.Size = new System.Drawing.Size(251, 95);
            this.lst_tarefas.TabIndex = 2;
            this.lst_tarefas.DoubleClick += new System.EventHandler(this.lst_tarefas_DoubleClick);
            // 
            // bnt_add
            // 
            this.bnt_add.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_add.Location = new System.Drawing.Point(231, 0);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(23, 23);
            this.bnt_add.TabIndex = 3;
            this.bnt_add.Text = "+";
            this.bnt_add.UseVisualStyleBackColor = true;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // blankdays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bnt_add);
            this.Controls.Add(this.lst_tarefas);
            this.Controls.Add(this.lbl_days);
            this.Name = "blankdays";
            this.Size = new System.Drawing.Size(257, 121);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_days;
        private System.Windows.Forms.ListBox lst_tarefas;
        private System.Windows.Forms.Button bnt_add;
    }
}
