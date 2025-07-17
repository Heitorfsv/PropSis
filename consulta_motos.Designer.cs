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
            this.bnt_pesquisar_nome = new System.Windows.Forms.Button();
            this.txt_pesquisar_nome = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bnt_add = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Cliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Placa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Marca = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Modelo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ano = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            // bnt_add
            // 
            this.bnt_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_add.Image = ((System.Drawing.Image)(resources.GetObject("bnt_add.Image")));
            this.bnt_add.Location = new System.Drawing.Point(686, 10);
            this.bnt_add.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(21, 20);
            this.bnt_add.TabIndex = 30;
            this.bnt_add.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_add.UseVisualStyleBackColor = false;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Cliente,
            this.Placa,
            this.Marca,
            this.Modelo,
            this.Cor,
            this.Ano});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(11, 37);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(721, 412);
            this.listView1.TabIndex = 40;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // Placa
            // 
            this.Placa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // consulta_motos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 486);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.bnt_add);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bnt_pesquisar_nome);
            this.Controls.Add(this.txt_pesquisar_nome);
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
        private System.Windows.Forms.Button bnt_pesquisar_nome;
        private System.Windows.Forms.TextBox txt_pesquisar_nome;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bnt_add;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Cliente;
        private System.Windows.Forms.ColumnHeader Placa;
        private System.Windows.Forms.ColumnHeader Marca;
        private System.Windows.Forms.ColumnHeader Modelo;
        private System.Windows.Forms.ColumnHeader Cor;
        private System.Windows.Forms.ColumnHeader Ano;
    }
}