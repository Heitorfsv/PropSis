namespace PrototipoSistema
{
    partial class trocas
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
            this.lst_oleo = new System.Windows.Forms.ListBox();
            this.lst_oleo_atrasado = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lst_nome_atrasado = new System.Windows.Forms.ListBox();
            this.lst_moto_atrasado = new System.Windows.Forms.ListBox();
            this.lst_nome = new System.Windows.Forms.ListBox();
            this.lst_moto = new System.Windows.Forms.ListBox();
            this.lst_marca_atrasado = new System.Windows.Forms.ListBox();
            this.lst_marca = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lst_oleo
            // 
            this.lst_oleo.FormattingEnabled = true;
            this.lst_oleo.Location = new System.Drawing.Point(12, 26);
            this.lst_oleo.Name = "lst_oleo";
            this.lst_oleo.Size = new System.Drawing.Size(92, 381);
            this.lst_oleo.TabIndex = 0;
            this.lst_oleo.SelectedIndexChanged += new System.EventHandler(this.lst_oleo_SelectedIndexChanged);
            // 
            // lst_oleo_atrasado
            // 
            this.lst_oleo_atrasado.FormattingEnabled = true;
            this.lst_oleo_atrasado.Location = new System.Drawing.Point(591, 26);
            this.lst_oleo_atrasado.Name = "lst_oleo_atrasado";
            this.lst_oleo_atrasado.Size = new System.Drawing.Size(92, 381);
            this.lst_oleo_atrasado.TabIndex = 1;
            this.lst_oleo_atrasado.SelectedIndexChanged += new System.EventHandler(this.lst_oleo_atrasado_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Próximas trocas de oleo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(588, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Trocas de oleo atrasadas";
            // 
            // lst_nome_atrasado
            // 
            this.lst_nome_atrasado.FormattingEnabled = true;
            this.lst_nome_atrasado.Location = new System.Drawing.Point(689, 25);
            this.lst_nome_atrasado.Name = "lst_nome_atrasado";
            this.lst_nome_atrasado.Size = new System.Drawing.Size(166, 381);
            this.lst_nome_atrasado.TabIndex = 4;
            this.lst_nome_atrasado.SelectedIndexChanged += new System.EventHandler(this.lst_nome_atrasado_SelectedIndexChanged);
            // 
            // lst_moto_atrasado
            // 
            this.lst_moto_atrasado.FormattingEnabled = true;
            this.lst_moto_atrasado.Location = new System.Drawing.Point(1001, 24);
            this.lst_moto_atrasado.Name = "lst_moto_atrasado";
            this.lst_moto_atrasado.Size = new System.Drawing.Size(134, 381);
            this.lst_moto_atrasado.TabIndex = 5;
            this.lst_moto_atrasado.SelectedIndexChanged += new System.EventHandler(this.lst_moto_atrasado_SelectedIndexChanged);
            // 
            // lst_nome
            // 
            this.lst_nome.FormattingEnabled = true;
            this.lst_nome.Location = new System.Drawing.Point(110, 26);
            this.lst_nome.Name = "lst_nome";
            this.lst_nome.Size = new System.Drawing.Size(166, 381);
            this.lst_nome.TabIndex = 6;
            this.lst_nome.SelectedIndexChanged += new System.EventHandler(this.lst_nome_SelectedIndexChanged);
            // 
            // lst_moto
            // 
            this.lst_moto.FormattingEnabled = true;
            this.lst_moto.Location = new System.Drawing.Point(422, 26);
            this.lst_moto.Name = "lst_moto";
            this.lst_moto.Size = new System.Drawing.Size(134, 381);
            this.lst_moto.TabIndex = 7;
            this.lst_moto.SelectedIndexChanged += new System.EventHandler(this.lst_moto_SelectedIndexChanged);
            // 
            // lst_marca_atrasado
            // 
            this.lst_marca_atrasado.FormattingEnabled = true;
            this.lst_marca_atrasado.Location = new System.Drawing.Point(861, 25);
            this.lst_marca_atrasado.Name = "lst_marca_atrasado";
            this.lst_marca_atrasado.Size = new System.Drawing.Size(134, 381);
            this.lst_marca_atrasado.TabIndex = 8;
            this.lst_marca_atrasado.SelectedIndexChanged += new System.EventHandler(this.lst_marca_atrasado_SelectedIndexChanged);
            // 
            // lst_marca
            // 
            this.lst_marca.FormattingEnabled = true;
            this.lst_marca.Location = new System.Drawing.Point(282, 26);
            this.lst_marca.Name = "lst_marca";
            this.lst_marca.Size = new System.Drawing.Size(134, 381);
            this.lst_marca.TabIndex = 9;
            this.lst_marca.SelectedIndexChanged += new System.EventHandler(this.lst_marca_SelectedIndexChanged);
            // 
            // trocas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 486);
            this.Controls.Add(this.lst_marca);
            this.Controls.Add(this.lst_marca_atrasado);
            this.Controls.Add(this.lst_moto);
            this.Controls.Add(this.lst_nome);
            this.Controls.Add(this.lst_moto_atrasado);
            this.Controls.Add(this.lst_nome_atrasado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lst_oleo_atrasado);
            this.Controls.Add(this.lst_oleo);
            this.Name = "trocas";
            this.Text = "trocas";
            this.Load += new System.EventHandler(this.trocas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_oleo;
        private System.Windows.Forms.ListBox lst_oleo_atrasado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lst_nome_atrasado;
        private System.Windows.Forms.ListBox lst_moto_atrasado;
        private System.Windows.Forms.ListBox lst_nome;
        private System.Windows.Forms.ListBox lst_moto;
        private System.Windows.Forms.ListBox lst_marca_atrasado;
        private System.Windows.Forms.ListBox lst_marca;
    }
}