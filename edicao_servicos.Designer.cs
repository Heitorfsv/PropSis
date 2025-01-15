namespace PrototipoSistema
{
    partial class edicao_servicos
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
            this.bnt_editar = new System.Windows.Forms.Button();
            this.txt_nome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_valor = new System.Windows.Forms.TextBox();
            this.bnt_deletar = new System.Windows.Forms.Button();
            this.bnt_historico = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnt_editar
            // 
            this.bnt_editar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_editar.Location = new System.Drawing.Point(201, 108);
            this.bnt_editar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_editar.Name = "bnt_editar";
            this.bnt_editar.Size = new System.Drawing.Size(122, 31);
            this.bnt_editar.TabIndex = 54;
            this.bnt_editar.Text = "Editar cadastro";
            this.bnt_editar.UseVisualStyleBackColor = true;
            this.bnt_editar.Click += new System.EventHandler(this.bnt_editar_Click);
            // 
            // txt_nome
            // 
            this.txt_nome.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nome.Location = new System.Drawing.Point(19, 31);
            this.txt_nome.Margin = new System.Windows.Forms.Padding(2);
            this.txt_nome.Name = "txt_nome";
            this.txt_nome.Size = new System.Drawing.Size(304, 20);
            this.txt_nome.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 56;
            this.label2.Text = "Valor";
            // 
            // txt_valor
            // 
            this.txt_valor.Location = new System.Drawing.Point(18, 75);
            this.txt_valor.Name = "txt_valor";
            this.txt_valor.Size = new System.Drawing.Size(100, 20);
            this.txt_valor.TabIndex = 55;
            // 
            // bnt_deletar
            // 
            this.bnt_deletar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bnt_deletar.Location = new System.Drawing.Point(167, 74);
            this.bnt_deletar.Name = "bnt_deletar";
            this.bnt_deletar.Size = new System.Drawing.Size(75, 20);
            this.bnt_deletar.TabIndex = 57;
            this.bnt_deletar.Text = "Deletar";
            this.bnt_deletar.UseVisualStyleBackColor = true;
            this.bnt_deletar.Click += new System.EventHandler(this.bnt_deletar_Click);
            // 
            // bnt_historico
            // 
            this.bnt_historico.ForeColor = System.Drawing.Color.Black;
            this.bnt_historico.Location = new System.Drawing.Point(248, 75);
            this.bnt_historico.Name = "bnt_historico";
            this.bnt_historico.Size = new System.Drawing.Size(75, 20);
            this.bnt_historico.TabIndex = 108;
            this.bnt_historico.Text = "Histórico";
            this.bnt_historico.UseVisualStyleBackColor = true;
            this.bnt_historico.Click += new System.EventHandler(this.bnt_historico_Click);
            // 
            // edicao_servicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 150);
            this.Controls.Add(this.bnt_historico);
            this.Controls.Add(this.bnt_deletar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_valor);
            this.Controls.Add(this.bnt_editar);
            this.Controls.Add(this.txt_nome);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "edicao_servicos";
            this.Text = "Edição serviços ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.edicao_servicos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bnt_editar;
        private System.Windows.Forms.TextBox txt_nome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_valor;
        private System.Windows.Forms.Button bnt_deletar;
        private System.Windows.Forms.Button bnt_historico;
    }
}