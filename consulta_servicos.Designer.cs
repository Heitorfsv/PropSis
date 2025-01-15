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
            this.label1 = new System.Windows.Forms.Label();
            this.bnt_pesquisar = new System.Windows.Forms.Button();
            this.cmb_consulta = new System.Windows.Forms.ComboBox();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.lst_nome = new System.Windows.Forms.ListBox();
            this.lst_valor = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Nome:";
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
            // lst_nome
            // 
            this.lst_nome.FormattingEnabled = true;
            this.lst_nome.Location = new System.Drawing.Point(5, 52);
            this.lst_nome.Margin = new System.Windows.Forms.Padding(2);
            this.lst_nome.Name = "lst_nome";
            this.lst_nome.Size = new System.Drawing.Size(282, 225);
            this.lst_nome.TabIndex = 0;
            this.lst_nome.SelectedIndexChanged += new System.EventHandler(this.lst_nome_SelectedIndexChanged);
            this.lst_nome.DoubleClick += new System.EventHandler(this.lst_nome_DoubleClick);
            // 
            // lst_valor
            // 
            this.lst_valor.FormattingEnabled = true;
            this.lst_valor.Location = new System.Drawing.Point(293, 52);
            this.lst_valor.Margin = new System.Windows.Forms.Padding(2);
            this.lst_valor.Name = "lst_valor";
            this.lst_valor.Size = new System.Drawing.Size(234, 225);
            this.lst_valor.TabIndex = 20;
            this.lst_valor.SelectedIndexChanged += new System.EventHandler(this.lst_status_SelectedIndexChanged);
            this.lst_valor.DoubleClick += new System.EventHandler(this.lst_status_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Valor:";
            // 
            // consulta_servicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 292);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lst_valor);
            this.Controls.Add(this.lst_nome);
            this.Controls.Add(this.bnt_atualizar);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnt_pesquisar;
        private System.Windows.Forms.ComboBox cmb_consulta;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.ListBox lst_nome;
        private System.Windows.Forms.ListBox lst_valor;
        private System.Windows.Forms.Label label2;
    }
}