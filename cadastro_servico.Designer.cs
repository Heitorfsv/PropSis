namespace PrototipoSistema
{
    partial class cadastro_servicos
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
            this.txt_nome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bnt_cadastro = new System.Windows.Forms.Button();
            this.txt_valor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_nome
            // 
            this.txt_nome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nome.Location = new System.Drawing.Point(14, 27);
            this.txt_nome.Margin = new System.Windows.Forms.Padding(2);
            this.txt_nome.Name = "txt_nome";
            this.txt_nome.Size = new System.Drawing.Size(304, 20);
            this.txt_nome.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 44;
            this.label1.Text = "Nome";
            // 
            // bnt_cadastro
            // 
            this.bnt_cadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_cadastro.Location = new System.Drawing.Point(195, 60);
            this.bnt_cadastro.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_cadastro.Name = "bnt_cadastro";
            this.bnt_cadastro.Size = new System.Drawing.Size(122, 31);
            this.bnt_cadastro.TabIndex = 48;
            this.bnt_cadastro.Text = "Criar cadastro";
            this.bnt_cadastro.UseVisualStyleBackColor = true;
            this.bnt_cadastro.Click += new System.EventHandler(this.bnt_cadastro_Click);
            // 
            // txt_valor
            // 
            this.txt_valor.Location = new System.Drawing.Point(14, 71);
            this.txt_valor.Name = "txt_valor";
            this.txt_valor.Size = new System.Drawing.Size(100, 20);
            this.txt_valor.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 50;
            this.label2.Text = "Valor";
            // 
            // cadastro_servicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 108);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_valor);
            this.Controls.Add(this.bnt_cadastro);
            this.Controls.Add(this.txt_nome);
            this.Controls.Add(this.label1);
            this.Name = "cadastro_servicos";
            this.Text = "Cadastro de Serviços";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_nome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnt_cadastro;
        private System.Windows.Forms.TextBox txt_valor;
        private System.Windows.Forms.Label label2;
    }
}