namespace PrototipoSistema
{
    partial class add
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
            this.lst_pesquisa = new System.Windows.Forms.ListBox();
            this.txt_pesquisa = new System.Windows.Forms.TextBox();
            this.bnt_delete = new System.Windows.Forms.Button();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lst_pesquisa
            // 
            this.lst_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_pesquisa.FormattingEnabled = true;
            this.lst_pesquisa.ItemHeight = 15;
            this.lst_pesquisa.Location = new System.Drawing.Point(11, 48);
            this.lst_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.lst_pesquisa.Name = "lst_pesquisa";
            this.lst_pesquisa.Size = new System.Drawing.Size(251, 229);
            this.lst_pesquisa.TabIndex = 0;
            this.lst_pesquisa.DoubleClick += new System.EventHandler(this.lst_pesquisa_DoubleClick);
            // 
            // txt_pesquisa
            // 
            this.txt_pesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pesquisa.Location = new System.Drawing.Point(11, 17);
            this.txt_pesquisa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pesquisa.Name = "txt_pesquisa";
            this.txt_pesquisa.Size = new System.Drawing.Size(251, 21);
            this.txt_pesquisa.TabIndex = 4;
            this.txt_pesquisa.TextChanged += new System.EventHandler(this.txt_pesquisa_TextChanged);
            // 
            // bnt_delete
            // 
            this.bnt_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_delete.ForeColor = System.Drawing.Color.DarkRed;
            this.bnt_delete.Location = new System.Drawing.Point(11, 282);
            this.bnt_delete.Name = "bnt_delete";
            this.bnt_delete.Size = new System.Drawing.Size(66, 22);
            this.bnt_delete.TabIndex = 12;
            this.bnt_delete.Text = "Delete";
            this.bnt_delete.UseVisualStyleBackColor = true;
            this.bnt_delete.Click += new System.EventHandler(this.bnt_delete_Click);
            // 
            // txt_total
            // 
            this.txt_total.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txt_total.Enabled = false;
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.Location = new System.Drawing.Point(545, 307);
            this.txt_total.Name = "txt_total";
            this.txt_total.Size = new System.Drawing.Size(100, 21);
            this.txt_total.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(542, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "Total";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(278, 17);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(367, 260);
            this.listView1.TabIndex = 23;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 339);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.txt_total);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bnt_delete);
            this.Controls.Add(this.txt_pesquisa);
            this.Controls.Add(this.lst_pesquisa);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "add";
            this.Text = "Adicionar serviços";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.add_servicos_FormClosing);
            this.Load += new System.EventHandler(this.add_servicos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_pesquisa;
        private System.Windows.Forms.TextBox txt_pesquisa;
        private System.Windows.Forms.Button bnt_delete;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
    }
}