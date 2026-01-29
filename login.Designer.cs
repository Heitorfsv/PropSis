namespace PrototipoSistema
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.txt_usuario = new System.Windows.Forms.TextBox();
            this.txt_senha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bnt_login = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.bnt_sair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_usuario
            // 
            this.txt_usuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_usuario.Location = new System.Drawing.Point(80, 100);
            this.txt_usuario.Name = "txt_usuario";
            this.txt_usuario.Size = new System.Drawing.Size(191, 22);
            this.txt_usuario.TabIndex = 0;
            // 
            // txt_senha
            // 
            this.txt_senha.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_senha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_senha.Location = new System.Drawing.Point(80, 128);
            this.txt_senha.Name = "txt_senha";
            this.txt_senha.Size = new System.Drawing.Size(191, 22);
            this.txt_senha.TabIndex = 1;
            this.txt_senha.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Usuário:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Senha:";
            // 
            // bnt_login
            // 
            this.bnt_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_login.Image = ((System.Drawing.Image)(resources.GetObject("bnt_login.Image")));
            this.bnt_login.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bnt_login.Location = new System.Drawing.Point(195, 156);
            this.bnt_login.Name = "bnt_login";
            this.bnt_login.Size = new System.Drawing.Size(76, 52);
            this.bnt_login.TabIndex = 4;
            this.bnt_login.Text = "Entrar";
            this.bnt_login.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bnt_login.UseVisualStyleBackColor = true;
            this.bnt_login.Click += new System.EventHandler(this.bnt_login_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(259, 82);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bnt_sair
            // 
            this.bnt_sair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_sair.Image = ((System.Drawing.Image)(resources.GetObject("bnt_sair.Image")));
            this.bnt_sair.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bnt_sair.Location = new System.Drawing.Point(113, 156);
            this.bnt_sair.Name = "bnt_sair";
            this.bnt_sair.Size = new System.Drawing.Size(76, 52);
            this.bnt_sair.TabIndex = 6;
            this.bnt_sair.Text = "Sair";
            this.bnt_sair.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.bnt_sair.UseVisualStyleBackColor = true;
            this.bnt_sair.Click += new System.EventHandler(this.bnt_sair_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 220);
            this.Controls.Add(this.bnt_sair);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bnt_login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_senha);
            this.Controls.Add(this.txt_usuario);
            this.Name = "login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_usuario;
        private System.Windows.Forms.TextBox txt_senha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bnt_login;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bnt_sair;
    }
}