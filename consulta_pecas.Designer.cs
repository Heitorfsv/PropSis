namespace PrototipoSistema
{
    partial class consulta_pecas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(consulta_pecas));
            this.lst_nome = new System.Windows.Forms.ListBox();
            this.bnt_atualizar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.lst_marca = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lst_modelo = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_fornecedor = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lst_nome
            // 
            this.lst_nome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_nome.FormattingEnabled = true;
            this.lst_nome.ItemHeight = 16;
            this.lst_nome.Location = new System.Drawing.Point(5, 59);
            this.lst_nome.Margin = new System.Windows.Forms.Padding(2);
            this.lst_nome.Name = "lst_nome";
            this.lst_nome.Size = new System.Drawing.Size(86, 212);
            this.lst_nome.TabIndex = 22;
            this.lst_nome.SelectedIndexChanged += new System.EventHandler(this.lst_nome_SelectedIndexChanged);
            this.lst_nome.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // bnt_atualizar
            // 
            this.bnt_atualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_atualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_atualizar.Image = ((System.Drawing.Image)(resources.GetObject("bnt_atualizar.Image")));
            this.bnt_atualizar.Location = new System.Drawing.Point(336, 11);
            this.bnt_atualizar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_atualizar.Name = "bnt_atualizar";
            this.bnt_atualizar.Size = new System.Drawing.Size(21, 20);
            this.bnt_atualizar.TabIndex = 27;
            this.bnt_atualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_atualizar.UseVisualStyleBackColor = false;
            this.bnt_atualizar.Click += new System.EventHandler(this.bnt_atualizar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Nome:";
            // 
            // bnt_pesquisar
            // 
            this.bnt_pesquisar.Location = new System.Drawing.Point(223, 12);
            this.bnt_pesquisar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_pesquisar.Name = "bnt_pesquisar";
            this.bnt_pesquisar.Size = new System.Drawing.Size(65, 20);
            this.bnt_pesquisar.TabIndex = 25;
            this.bnt_pesquisar.Text = "Pesquisar";
            this.bnt_pesquisar.UseVisualStyleBackColor = true;
            this.bnt_pesquisar.Click += new System.EventHandler(this.bnt_pesquisar_Click);
            // 
            // cmb_consulta
            // 
            this.cmb_consulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_consulta.FormattingEnabled = true;
            this.cmb_consulta.Items.AddRange(new object[] {
            "nome",
            "marca",
            "modelo",
            "fornecedor"});
            this.cmb_consulta.Location = new System.Drawing.Point(9, 12);
            this.cmb_consulta.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_consulta.Name = "cmb_consulta";
            this.cmb_consulta.Size = new System.Drawing.Size(82, 21);
            this.cmb_consulta.TabIndex = 24;
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Location = new System.Drawing.Point(94, 12);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(128, 20);
            this.txt_pesquisa.TabIndex = 23;
            // 
            // lst_marca
            // 
            this.lst_marca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_marca.FormattingEnabled = true;
            this.lst_marca.ItemHeight = 16;
            this.lst_marca.Location = new System.Drawing.Point(94, 59);
            this.lst_marca.Margin = new System.Windows.Forms.Padding(2);
            this.lst_marca.Name = "lst_marca";
            this.lst_marca.Size = new System.Drawing.Size(86, 212);
            this.lst_marca.TabIndex = 30;
            this.lst_marca.SelectedIndexChanged += new System.EventHandler(this.lst_marca_SelectedIndexChanged);
            this.lst_marca.DoubleClick += new System.EventHandler(this.lst_marca_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(95, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "Marca:";
            // 
            // lst_modelo
            // 
            this.lst_modelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_modelo.FormattingEnabled = true;
            this.lst_modelo.ItemHeight = 16;
            this.lst_modelo.Location = new System.Drawing.Point(183, 59);
            this.lst_modelo.Margin = new System.Windows.Forms.Padding(2);
            this.lst_modelo.Name = "lst_modelo";
            this.lst_modelo.Size = new System.Drawing.Size(86, 212);
            this.lst_modelo.TabIndex = 32;
            this.lst_modelo.SelectedIndexChanged += new System.EventHandler(this.lst_modelo_SelectedIndexChanged);
            this.lst_modelo.DoubleClick += new System.EventHandler(this.lst_modelo_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(184, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Modelo:";
            // 
            // lst_fornecedor
            // 
            this.lst_fornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_fornecedor.FormattingEnabled = true;
            this.lst_fornecedor.ItemHeight = 16;
            this.lst_fornecedor.Location = new System.Drawing.Point(271, 59);
            this.lst_fornecedor.Margin = new System.Windows.Forms.Padding(2);
            this.lst_fornecedor.Name = "lst_fornecedor";
            this.lst_fornecedor.Size = new System.Drawing.Size(86, 212);
            this.lst_fornecedor.TabIndex = 34;
            this.lst_fornecedor.SelectedIndexChanged += new System.EventHandler(this.lst_fornecedor_SelectedIndexChanged);
            this.lst_fornecedor.DoubleClick += new System.EventHandler(this.lst_fornecedor_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(273, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 16);
            this.label5.TabIndex = 35;
            this.label5.Text = "Fornecedor:";
            // 
            // consulta_pecas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 285);
            this.Controls.Add(this.lst_fornecedor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lst_modelo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lst_marca);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lst_nome);
            this.Controls.Add(this.bnt_atualizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnt_pesquisar);
            this.Controls.Add(this.cmb_consulta);
            this.Controls.Add(this.txt_pesquisa);
            this.Name = "consulta_pecas";
            this.Text = "Consulta de peças";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.consulta_pecas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lst_nome;
        private System.Windows.Forms.Button bnt_atualizar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.ComboBox cmb_consulta;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.ListBox lst_marca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lst_modelo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lst_fornecedor;
        private System.Windows.Forms.Label label5;
    }
}