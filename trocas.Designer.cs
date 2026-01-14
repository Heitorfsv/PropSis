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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_oleo = new System.Windows.Forms.ListView();
            this.lst_oleo_atrasado = new System.Windows.Forms.ListView();
            this.lst_filtro = new System.Windows.Forms.ListView();
            this.lst_filtro_atrasado = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Próximas trocas de oleo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1022, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Trocas de oleo atrasadas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1022, 459);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Trocas de filtro atrasadas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 459);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Próximas trocas de filtro";
            // 
            // lst_oleo
            // 
            this.lst_oleo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_oleo.HideSelection = false;
            this.lst_oleo.Location = new System.Drawing.Point(14, 30);
            this.lst_oleo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lst_oleo.Name = "lst_oleo";
            this.lst_oleo.Size = new System.Drawing.Size(912, 414);
            this.lst_oleo.TabIndex = 20;
            this.lst_oleo.UseCompatibleStateImageBehavior = false;
            this.lst_oleo.DoubleClick += new System.EventHandler(this.lst_oleo_DoubleClick);
            // 
            // lst_oleo_atrasado
            // 
            this.lst_oleo_atrasado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_oleo_atrasado.HideSelection = false;
            this.lst_oleo_atrasado.Location = new System.Drawing.Point(1025, 30);
            this.lst_oleo_atrasado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lst_oleo_atrasado.Name = "lst_oleo_atrasado";
            this.lst_oleo_atrasado.Size = new System.Drawing.Size(912, 414);
            this.lst_oleo_atrasado.TabIndex = 21;
            this.lst_oleo_atrasado.UseCompatibleStateImageBehavior = false;
            this.lst_oleo_atrasado.DoubleClick += new System.EventHandler(this.lst_oleo_atrasado_DoubleClick);
            // 
            // lst_filtro
            // 
            this.lst_filtro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_filtro.HideSelection = false;
            this.lst_filtro.Location = new System.Drawing.Point(14, 477);
            this.lst_filtro.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lst_filtro.Name = "lst_filtro";
            this.lst_filtro.Size = new System.Drawing.Size(912, 414);
            this.lst_filtro.TabIndex = 22;
            this.lst_filtro.UseCompatibleStateImageBehavior = false;
            this.lst_filtro.DoubleClick += new System.EventHandler(this.lst_filtro_DoubleClick);
            // 
            // lst_filtro_atrasado
            // 
            this.lst_filtro_atrasado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_filtro_atrasado.HideSelection = false;
            this.lst_filtro_atrasado.Location = new System.Drawing.Point(1025, 477);
            this.lst_filtro_atrasado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lst_filtro_atrasado.Name = "lst_filtro_atrasado";
            this.lst_filtro_atrasado.Size = new System.Drawing.Size(912, 414);
            this.lst_filtro_atrasado.TabIndex = 23;
            this.lst_filtro_atrasado.UseCompatibleStateImageBehavior = false;
            this.lst_filtro_atrasado.DoubleClick += new System.EventHandler(this.lst_filtro_atrasado_DoubleClick);
            // 
            // trocas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 935);
            this.Controls.Add(this.lst_filtro_atrasado);
            this.Controls.Add(this.lst_filtro);
            this.Controls.Add(this.lst_oleo_atrasado);
            this.Controls.Add(this.lst_oleo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "trocas";
            this.Text = "trocas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.trocas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lst_oleo;
        private System.Windows.Forms.ListView lst_oleo_atrasado;
        private System.Windows.Forms.ListView lst_filtro;
        private System.Windows.Forms.ListView lst_filtro_atrasado;
    }
}