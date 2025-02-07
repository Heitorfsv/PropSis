namespace PrototipoSistema
{
    partial class consulta_cliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(consulta_cliente));
            this.lst_nome = new System.Windows.Forms.ListBox();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.lst_dt_nascimento = new System.Windows.Forms.ListBox();
            this.lst_doc = new System.Windows.Forms.ListBox();
            this.lst_telefone = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bnt_atualizar = new System.Windows.Forms.Button();
            this.lst_dt_cadastro = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lst_endereco = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lst_nome
            // 
            this.lst_nome.AllowDrop = true;
            this.lst_nome.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_nome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_nome.FormattingEnabled = true;
            this.lst_nome.Location = new System.Drawing.Point(104, 60);
            this.lst_nome.Margin = new System.Windows.Forms.Padding(2);
            this.lst_nome.Name = "lst_nome";
            this.lst_nome.Size = new System.Drawing.Size(89, 225);
            this.lst_nome.TabIndex = 0;
            this.lst_nome.SelectedIndexChanged += new System.EventHandler(this.lst_nome_SelectedIndexChanged);
            this.lst_nome.DragEnter += new System.Windows.Forms.DragEventHandler(this.lst_nome_DragEnter);
            this.lst_nome.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Location = new System.Drawing.Point(93, 8);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(128, 20);
            this.txt_pesquisa.TabIndex = 2;
            // 
            // cmb_consulta
            // 
            this.cmb_consulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_consulta.FormattingEnabled = true;
            this.cmb_consulta.Items.AddRange(new object[] {
            "nome",
            "doc",
            "telefone"});
            this.cmb_consulta.Location = new System.Drawing.Point(8, 8);
            this.cmb_consulta.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_consulta.Name = "cmb_consulta";
            this.cmb_consulta.Size = new System.Drawing.Size(82, 21);
            this.cmb_consulta.TabIndex = 3;
            // 
            // bnt_pesquisar
            // 
            this.bnt_pesquisar.Location = new System.Drawing.Point(223, 8);
            this.bnt_pesquisar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_pesquisar.Name = "bnt_pesquisar";
            this.bnt_pesquisar.Size = new System.Drawing.Size(65, 20);
            this.bnt_pesquisar.TabIndex = 4;
            this.bnt_pesquisar.Text = "Pesquisar";
            this.bnt_pesquisar.UseVisualStyleBackColor = true;
            this.bnt_pesquisar.Click += new System.EventHandler(this.bnt_pesquisar_Click);
            // 
            // lst_dt_nascimento
            // 
            this.lst_dt_nascimento.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_dt_nascimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_dt_nascimento.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lst_dt_nascimento.FormattingEnabled = true;
            this.lst_dt_nascimento.Location = new System.Drawing.Point(476, 60);
            this.lst_dt_nascimento.Margin = new System.Windows.Forms.Padding(2);
            this.lst_dt_nascimento.Name = "lst_dt_nascimento";
            this.lst_dt_nascimento.Size = new System.Drawing.Size(89, 225);
            this.lst_dt_nascimento.TabIndex = 3;
            this.lst_dt_nascimento.Click += new System.EventHandler(this.lst_dt_nascimento_Click);
            this.lst_dt_nascimento.DoubleClick += new System.EventHandler(this.lst_dt_nascimento_DoubleClick);
            // 
            // lst_doc
            // 
            this.lst_doc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_doc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_doc.FormattingEnabled = true;
            this.lst_doc.Location = new System.Drawing.Point(197, 60);
            this.lst_doc.Margin = new System.Windows.Forms.Padding(2);
            this.lst_doc.Name = "lst_doc";
            this.lst_doc.Size = new System.Drawing.Size(89, 225);
            this.lst_doc.TabIndex = 1;
            this.lst_doc.Click += new System.EventHandler(this.lst_doc_Click);
            this.lst_doc.DoubleClick += new System.EventHandler(this.lst_doc_DoubleClick);
            // 
            // lst_telefone
            // 
            this.lst_telefone.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_telefone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_telefone.FormattingEnabled = true;
            this.lst_telefone.Location = new System.Drawing.Point(290, 60);
            this.lst_telefone.Margin = new System.Windows.Forms.Padding(2);
            this.lst_telefone.Name = "lst_telefone";
            this.lst_telefone.Size = new System.Drawing.Size(89, 225);
            this.lst_telefone.TabIndex = 2;
            this.lst_telefone.Click += new System.EventHandler(this.lst_telefone_Click);
            this.lst_telefone.DoubleClick += new System.EventHandler(this.lst_telefone_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(101, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nome:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(194, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Documento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(287, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Telefone:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(473, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Data de nascimento:";
            // 
            // bnt_atualizar
            // 
            this.bnt_atualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_atualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_atualizar.Image = ((System.Drawing.Image)(resources.GetObject("bnt_atualizar.Image")));
            this.bnt_atualizar.Location = new System.Drawing.Point(544, 8);
            this.bnt_atualizar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_atualizar.Name = "bnt_atualizar";
            this.bnt_atualizar.Size = new System.Drawing.Size(21, 20);
            this.bnt_atualizar.TabIndex = 10;
            this.bnt_atualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_atualizar.UseVisualStyleBackColor = false;
            this.bnt_atualizar.Click += new System.EventHandler(this.bnt_atualizar_Click);
            // 
            // lst_dt_cadastro
            // 
            this.lst_dt_cadastro.AllowDrop = true;
            this.lst_dt_cadastro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_dt_cadastro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_dt_cadastro.FormattingEnabled = true;
            this.lst_dt_cadastro.Location = new System.Drawing.Point(11, 60);
            this.lst_dt_cadastro.Margin = new System.Windows.Forms.Padding(2);
            this.lst_dt_cadastro.Name = "lst_dt_cadastro";
            this.lst_dt_cadastro.Size = new System.Drawing.Size(89, 225);
            this.lst_dt_cadastro.TabIndex = 11;
            this.lst_dt_cadastro.Click += new System.EventHandler(this.lst_dt_cadastro_Click);
            this.lst_dt_cadastro.DoubleClick += new System.EventHandler(this.lst_dt_cadastro_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Dt cadastro:";
            // 
            // lst_endereco
            // 
            this.lst_endereco.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lst_endereco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_endereco.FormattingEnabled = true;
            this.lst_endereco.Location = new System.Drawing.Point(383, 60);
            this.lst_endereco.Margin = new System.Windows.Forms.Padding(2);
            this.lst_endereco.Name = "lst_endereco";
            this.lst_endereco.Size = new System.Drawing.Size(89, 225);
            this.lst_endereco.TabIndex = 13;
            this.lst_endereco.Click += new System.EventHandler(this.lst_endereco_Click);
            this.lst_endereco.DoubleClick += new System.EventHandler(this.lst_endereco_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(380, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Endereço:";
            // 
            // consulta_cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 333);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lst_endereco);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lst_dt_cadastro);
            this.Controls.Add(this.lst_dt_nascimento);
            this.Controls.Add(this.bnt_atualizar);
            this.Controls.Add(this.lst_telefone);
            this.Controls.Add(this.lst_doc);
            this.Controls.Add(this.lst_nome);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnt_pesquisar);
            this.Controls.Add(this.cmb_consulta);
            this.Controls.Add(this.txt_pesquisa);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "consulta_cliente";
            this.Text = "Consulta de clientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.consulta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_nome;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.ComboBox cmb_consulta;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.ListBox lst_dt_nascimento;
        private System.Windows.Forms.ListBox lst_doc;
        private System.Windows.Forms.ListBox lst_telefone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bnt_atualizar;
        private System.Windows.Forms.ListBox lst_dt_cadastro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lst_endereco;
        private System.Windows.Forms.Label label6;
    }
}