namespace PrototipoSistema
{
    partial class consulta_motos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(consulta_motos));
            this.bnt_atualizar = new System.Windows.Forms.Button();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lst_placa = new System.Windows.Forms.ListBox();
            this.lst_cor = new System.Windows.Forms.ListBox();
            this.lst_modelo = new System.Windows.Forms.ListBox();
            this.lst_marca = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lst_ano = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bnt_pesquisar_nome = new System.Windows.Forms.Button();
            this.txt_pesquisar_nome = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lst_nome = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bnt_atualizar
            // 
            this.bnt_atualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_atualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_atualizar.Image = ((System.Drawing.Image)(resources.GetObject("bnt_atualizar.Image")));
            this.bnt_atualizar.Location = new System.Drawing.Point(711, 10);
            this.bnt_atualizar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_atualizar.Name = "bnt_atualizar";
            this.bnt_atualizar.Size = new System.Drawing.Size(21, 20);
            this.bnt_atualizar.TabIndex = 14;
            this.bnt_atualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_atualizar.UseVisualStyleBackColor = false;
            this.bnt_atualizar.Click += new System.EventHandler(this.bnt_atualizar_Click);
            // 
            // bnt_pesquisar
            // 
            this.bnt_pesquisar.Location = new System.Drawing.Point(226, 11);
            this.bnt_pesquisar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_pesquisar.Name = "bnt_pesquisar";
            this.bnt_pesquisar.Size = new System.Drawing.Size(65, 20);
            this.bnt_pesquisar.TabIndex = 13;
            this.bnt_pesquisar.Text = "Pesquisar";
            this.bnt_pesquisar.UseVisualStyleBackColor = true;
            this.bnt_pesquisar.Click += new System.EventHandler(this.bnt_pesquisar_Click);
            // 
            // cmb_consulta
            // 
            this.cmb_consulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_consulta.FormattingEnabled = true;
            this.cmb_consulta.Items.AddRange(new object[] {
            "placa",
            "marca",
            "modelo",
            "cor",
            "ano"});
            this.cmb_consulta.Location = new System.Drawing.Point(11, 11);
            this.cmb_consulta.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_consulta.Name = "cmb_consulta";
            this.cmb_consulta.Size = new System.Drawing.Size(82, 21);
            this.cmb_consulta.TabIndex = 12;
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Location = new System.Drawing.Point(96, 11);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(128, 20);
            this.txt_pesquisa.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(101, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Placa:";
            // 
            // lst_placa
            // 
            this.lst_placa.AllowDrop = true;
            this.lst_placa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_placa.FormattingEnabled = true;
            this.lst_placa.ItemHeight = 16;
            this.lst_placa.Location = new System.Drawing.Point(104, 64);
            this.lst_placa.Margin = new System.Windows.Forms.Padding(2);
            this.lst_placa.Name = "lst_placa";
            this.lst_placa.Size = new System.Drawing.Size(89, 212);
            this.lst_placa.TabIndex = 21;
            this.lst_placa.SelectedIndexChanged += new System.EventHandler(this.lst_placa_SelectedIndexChanged);
            this.lst_placa.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // lst_cor
            // 
            this.lst_cor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_cor.FormattingEnabled = true;
            this.lst_cor.ItemHeight = 16;
            this.lst_cor.Location = new System.Drawing.Point(383, 64);
            this.lst_cor.Margin = new System.Windows.Forms.Padding(2);
            this.lst_cor.Name = "lst_cor";
            this.lst_cor.Size = new System.Drawing.Size(89, 212);
            this.lst_cor.TabIndex = 17;
            this.lst_cor.Click += new System.EventHandler(this.lst_cor_Click);
            this.lst_cor.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // lst_modelo
            // 
            this.lst_modelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_modelo.FormattingEnabled = true;
            this.lst_modelo.ItemHeight = 16;
            this.lst_modelo.Location = new System.Drawing.Point(290, 64);
            this.lst_modelo.Margin = new System.Windows.Forms.Padding(2);
            this.lst_modelo.Name = "lst_modelo";
            this.lst_modelo.Size = new System.Drawing.Size(89, 212);
            this.lst_modelo.TabIndex = 16;
            this.lst_modelo.Click += new System.EventHandler(this.lst_modelo_Click);
            this.lst_modelo.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // lst_marca
            // 
            this.lst_marca.AllowDrop = true;
            this.lst_marca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_marca.FormattingEnabled = true;
            this.lst_marca.ItemHeight = 16;
            this.lst_marca.Location = new System.Drawing.Point(197, 64);
            this.lst_marca.Margin = new System.Windows.Forms.Padding(2);
            this.lst_marca.Name = "lst_marca";
            this.lst_marca.Size = new System.Drawing.Size(89, 212);
            this.lst_marca.TabIndex = 15;
            this.lst_marca.Click += new System.EventHandler(this.lst_marca_Click);
            this.lst_marca.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(380, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Cor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(287, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Modelo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Marca:";
            // 
            // lst_ano
            // 
            this.lst_ano.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_ano.FormattingEnabled = true;
            this.lst_ano.ItemHeight = 16;
            this.lst_ano.Location = new System.Drawing.Point(476, 64);
            this.lst_ano.Margin = new System.Windows.Forms.Padding(2);
            this.lst_ano.Name = "lst_ano";
            this.lst_ano.Size = new System.Drawing.Size(89, 212);
            this.lst_ano.TabIndex = 23;
            this.lst_ano.Click += new System.EventHandler(this.lst_ano_Click);
            this.lst_ano.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(473, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Ano:";
            // 
            // bnt_pesquisar_nome
            // 
            this.bnt_pesquisar_nome.Location = new System.Drawing.Point(545, 10);
            this.bnt_pesquisar_nome.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_pesquisar_nome.Name = "bnt_pesquisar_nome";
            this.bnt_pesquisar_nome.Size = new System.Drawing.Size(65, 20);
            this.bnt_pesquisar_nome.TabIndex = 26;
            this.bnt_pesquisar_nome.Text = "Pesquisar";
            this.bnt_pesquisar_nome.UseVisualStyleBackColor = true;
            this.bnt_pesquisar_nome.Click += new System.EventHandler(this.bnt_pesquisar_nome_Click);
            // 
            // txt_pesquisar_nome
            // 
            this.txt_pesquisar_nome.Location = new System.Drawing.Point(413, 10);
            this.txt_pesquisar_nome.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisar_nome.Name = "txt_pesquisar_nome";
            this.txt_pesquisar_nome.Size = new System.Drawing.Size(128, 20);
            this.txt_pesquisar_nome.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Cliente";
            // 
            // lst_nome
            // 
            this.lst_nome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_nome.FormattingEnabled = true;
            this.lst_nome.ItemHeight = 16;
            this.lst_nome.Location = new System.Drawing.Point(11, 64);
            this.lst_nome.Margin = new System.Windows.Forms.Padding(2);
            this.lst_nome.Name = "lst_nome";
            this.lst_nome.Size = new System.Drawing.Size(89, 212);
            this.lst_nome.TabIndex = 28;
            this.lst_nome.Click += new System.EventHandler(this.lst_nome_Click);
            this.lst_nome.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 46);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "Cliente:";
            // 
            // consulta_motos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 340);
            this.Controls.Add(this.lst_nome);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bnt_pesquisar_nome);
            this.Controls.Add(this.txt_pesquisar_nome);
            this.Controls.Add(this.lst_ano);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lst_placa);
            this.Controls.Add(this.lst_cor);
            this.Controls.Add(this.lst_modelo);
            this.Controls.Add(this.lst_marca);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnt_atualizar);
            this.Controls.Add(this.bnt_pesquisar);
            this.Controls.Add(this.cmb_consulta);
            this.Controls.Add(this.txt_pesquisa);
            this.Name = "consulta_motos";
            this.Text = "Consulta de motos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.consulta_motos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnt_atualizar;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.ComboBox cmb_consulta;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lst_placa;
        private System.Windows.Forms.ListBox lst_cor;
        private System.Windows.Forms.ListBox lst_modelo;
        private System.Windows.Forms.ListBox lst_marca;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lst_ano;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bnt_pesquisar_nome;
        private System.Windows.Forms.TextBox txt_pesquisar_nome;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lst_nome;
        private System.Windows.Forms.Label label7;
    }
}