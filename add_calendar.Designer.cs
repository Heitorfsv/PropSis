namespace PrototipoSistema
{
    partial class add_calendar
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
            this.txt_evento = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_horario = new System.Windows.Forms.CheckBox();
            this.txt_horario1 = new System.Windows.Forms.TextBox();
            this.txt_horario2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bnt_add = new System.Windows.Forms.Button();
            this.txt_data = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_evento
            // 
            this.txt_evento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_evento.Location = new System.Drawing.Point(12, 70);
            this.txt_evento.Name = "txt_evento";
            this.txt_evento.Size = new System.Drawing.Size(319, 21);
            this.txt_evento.TabIndex = 0;
            this.txt_evento.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Evento";
            // 
            // cb_horario
            // 
            this.cb_horario.AutoSize = true;
            this.cb_horario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_horario.Location = new System.Drawing.Point(16, 110);
            this.cb_horario.Name = "cb_horario";
            this.cb_horario.Size = new System.Drawing.Size(126, 19);
            this.cb_horario.TabIndex = 2;
            this.cb_horario.Text = "Adicionar horário?";
            this.cb_horario.UseVisualStyleBackColor = true;
            this.cb_horario.CheckedChanged += new System.EventHandler(this.cb_horario_CheckedChanged);
            // 
            // txt_horario1
            // 
            this.txt_horario1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_horario1.Location = new System.Drawing.Point(148, 107);
            this.txt_horario1.Name = "txt_horario1";
            this.txt_horario1.Size = new System.Drawing.Size(71, 21);
            this.txt_horario1.TabIndex = 3;
            // 
            // txt_horario2
            // 
            this.txt_horario2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_horario2.Location = new System.Drawing.Point(255, 108);
            this.txt_horario2.Name = "txt_horario2";
            this.txt_horario2.Size = new System.Drawing.Size(76, 21);
            this.txt_horario2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(225, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "até";
            // 
            // bnt_add
            // 
            this.bnt_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_add.Location = new System.Drawing.Point(249, 153);
            this.bnt_add.Name = "bnt_add";
            this.bnt_add.Size = new System.Drawing.Size(82, 23);
            this.bnt_add.TabIndex = 6;
            this.bnt_add.Text = "Adicionar";
            this.bnt_add.UseVisualStyleBackColor = true;
            this.bnt_add.Click += new System.EventHandler(this.bnt_add_Click);
            // 
            // txt_data
            // 
            this.txt_data.Enabled = false;
            this.txt_data.Location = new System.Drawing.Point(12, 12);
            this.txt_data.Name = "txt_data";
            this.txt_data.Size = new System.Drawing.Size(71, 20);
            this.txt_data.TabIndex = 7;
            // 
            // add_calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 188);
            this.Controls.Add(this.txt_data);
            this.Controls.Add(this.bnt_add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_horario2);
            this.Controls.Add(this.txt_horario1);
            this.Controls.Add(this.cb_horario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_evento);
            this.Name = "add_calendar";
            this.Text = "Adicionar evento";
            this.Load += new System.EventHandler(this.add_calendar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_evento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_horario;
        private System.Windows.Forms.TextBox txt_horario1;
        private System.Windows.Forms.TextBox txt_horario2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bnt_add;
        private System.Windows.Forms.TextBox txt_data;
    }
}