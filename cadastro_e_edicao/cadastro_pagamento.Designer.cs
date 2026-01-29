namespace PrototipoSistema
{
    partial class cadastro_pagamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cadastro_pagamento));
            this.txt_metodo = new System.Windows.Forms.TextBox();
            this.txt_agencia = new System.Windows.Forms.TextBox();
            this.txt_parcelas = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.bnt_cadastro = new System.Windows.Forms.Button();
            this.bnt_excluir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.bnt_add = new System.Windows.Forms.Button();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_metodo
            // 
            this.txt_metodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_metodo.Location = new System.Drawing.Point(15, 27);
            this.txt_metodo.Name = "txt_metodo";
            this.txt_metodo.Size = new System.Drawing.Size(172, 22);
            this.txt_metodo.TabIndex = 0;
            // 
            // txt_agencia
            // 
            this.txt_agencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_agencia.Location = new System.Drawing.Point(12, 70);
            this.txt_agencia.Name = "txt_agencia";
            this.txt_agencia.Size = new System.Drawing.Size(172, 22);
            this.txt_agencia.TabIndex = 1;
            // 
            // txt_parcelas
            // 
            this.txt_parcelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_parcelas.Location = new System.Drawing.Point(12, 113);
            this.txt_parcelas.Name = "txt_parcelas";
            this.txt_parcelas.Size = new System.Drawing.Size(82, 22);
            this.txt_parcelas.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Metodo de pagamento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Agência:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Parcelas:";
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(281, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(317, 130);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // bnt_cadastro
            // 
            this.bnt_cadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_cadastro.Location = new System.Drawing.Point(12, 170);
            this.bnt_cadastro.Name = "bnt_cadastro";
            this.bnt_cadastro.Size = new System.Drawing.Size(82, 23);
            this.bnt_cadastro.TabIndex = 7;
            this.bnt_cadastro.Text = "Cadastrar";
            this.bnt_cadastro.UseVisualStyleBackColor = true;
            this.bnt_cadastro.Click += new System.EventHandler(this.bnt_cadastro_Click);
            // 
            // bnt_excluir
            // 
            this.bnt_excluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_excluir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bnt_excluir.Location = new System.Drawing.Point(12, 141);
            this.bnt_excluir.Name = "bnt_excluir";
            this.bnt_excluir.Size = new System.Drawing.Size(82, 23);
            this.bnt_excluir.TabIndex = 8;
            this.bnt_excluir.Text = "Deletar";
            this.bnt_excluir.UseVisualStyleBackColor = true;
            this.bnt_excluir.Click += new System.EventHandler(this.bnt_excluir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(278, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Consulta de cadastros:";
            // 
            // bnt_add
            // 
            this.bnt_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_add.Image = ((System.Drawing.Image)(resources.GetObject("bnt_add.Image")));
            this.bnt_add.Location = new System.Drawing.Point(577, 2);
            this.bnt_add.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(21, 20);
            this.bnt_add.TabIndex = 36;
            this.bnt_add.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_add.UseVisualStyleBackColor = false;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pesquisa.Location = new System.Drawing.Point(281, 170);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(151, 22);
            this.txt_pesquisa.TabIndex = 37;
            // 
            // cadastro_pagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 220);
            this.Controls.Add(this.txt_pesquisa);
            this.Controls.Add(this.bnt_add);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bnt_excluir);
            this.Controls.Add(this.bnt_cadastro);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_parcelas);
            this.Controls.Add(this.txt_agencia);
            this.Controls.Add(this.txt_metodo);
            this.Name = "cadastro_pagamento";
            this.Text = "Cadastro metodo de pagamento";
            this.Load += new System.EventHandler(this.cadastro_pagamento_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_metodo;
        private System.Windows.Forms.TextBox txt_agencia;
        private System.Windows.Forms.TextBox txt_parcelas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button bnt_cadastro;
        private System.Windows.Forms.Button bnt_excluir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bnt_add;
        private System.Windows.Forms.TextBox txt_pesquisa;
    }
}