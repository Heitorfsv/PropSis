namespace PrototipoSistema
{
    partial class consulta_servicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(consulta_servicos));
            this.bnt_atualizar = new System.Windows.Forms.Button();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.bnt_add = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Nome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // bnt_atualizar
            // 
            this.bnt_atualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_atualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_atualizar.Image = ((System.Drawing.Image)(resources.GetObject("bnt_atualizar.Image")));
            this.bnt_atualizar.Location = new System.Drawing.Point(505, 8);
            this.bnt_atualizar.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_atualizar.Name = "bnt_atualizar";
            this.bnt_atualizar.Size = new System.Drawing.Size(21, 20);
            this.bnt_atualizar.TabIndex = 19;
            this.bnt_atualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_atualizar.UseVisualStyleBackColor = false;
            this.bnt_atualizar.Click += new System.EventHandler(this.bnt_atualizar_Click);
            // 
            // bnt_pesquisar
            // 
            this.bnt_pesquisar.Location = new System.Drawing.Point(223, 8);
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
            "nome"});
            this.cmb_consulta.Location = new System.Drawing.Point(9, 8);
            this.cmb_consulta.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_consulta.Name = "cmb_consulta";
            this.cmb_consulta.Size = new System.Drawing.Size(82, 21);
            this.cmb_consulta.TabIndex = 12;
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Location = new System.Drawing.Point(94, 8);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(128, 20);
            this.txt_pesquisa.TabIndex = 11;
            // 
            // bnt_add
            // 
            this.bnt_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bnt_add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bnt_add.Image = ((System.Drawing.Image)(resources.GetObject("bnt_add.Image")));
            this.bnt_add.Location = new System.Drawing.Point(480, 7);
            this.bnt_add.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(21, 20);
            this.bnt_add.TabIndex = 37;
            this.bnt_add.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bnt_add.UseVisualStyleBackColor = false;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Nome,
            this.Valor});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(9, 34);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(517, 334);
            this.listView1.TabIndex = 38;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick_1);
            // 
            // Valor
            // 
            this.Valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // consulta_servicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 380);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.bnt_add);
            this.Controls.Add(this.bnt_atualizar);
            this.Controls.Add(this.bnt_pesquisar);
            this.Controls.Add(this.cmb_consulta);
            this.Controls.Add(this.txt_pesquisa);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "consulta_servicos";
            this.Text = "Consulta de serviços";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.consulta_servicos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnt_atualizar;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.ComboBox cmb_consulta;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.Button bnt_add;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Nome;
        private System.Windows.Forms.ColumnHeader Valor;
    }
}