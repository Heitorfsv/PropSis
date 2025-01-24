namespace PrototipoSistema
{
    partial class edicao_calendar
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
            this.txt_data = new System.Windows.Forms.TextBox();
            this.bnt_editar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_horario2 = new System.Windows.Forms.TextBox();
            this.txt_horario1 = new System.Windows.Forms.TextBox();
            this.cb_horario = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_evento = new System.Windows.Forms.TextBox();
            this.bnt_excluir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_data
            // 
            this.txt_data.Enabled = false;
            this.txt_data.Location = new System.Drawing.Point(12, 12);
            this.txt_data.Name = "txt_data";
            this.txt_data.Size = new System.Drawing.Size(71, 20);
            this.txt_data.TabIndex = 15;
            // 
            // bnt_editar
            // 
            this.bnt_editar.Location = new System.Drawing.Point(234, 152);
            this.bnt_editar.Name = "bnt_editar";
            this.bnt_editar.Size = new System.Drawing.Size(73, 20);
            this.bnt_editar.TabIndex = 14;
            this.bnt_editar.Text = "Editar";
            this.bnt_editar.UseVisualStyleBackColor = true;
            this.bnt_editar.Click += new System.EventHandler(this.bnt_editar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "até";
            // 
            // txt_horario2
            // 
            this.txt_horario2.Location = new System.Drawing.Point(135, 152);
            this.txt_horario2.Name = "txt_horario2";
            this.txt_horario2.Size = new System.Drawing.Size(71, 20);
            this.txt_horario2.TabIndex = 12;
            // 
            // txt_horario1
            // 
            this.txt_horario1.Location = new System.Drawing.Point(12, 152);
            this.txt_horario1.Name = "txt_horario1";
            this.txt_horario1.Size = new System.Drawing.Size(71, 20);
            this.txt_horario1.TabIndex = 11;
            // 
            // cb_horario
            // 
            this.cb_horario.AutoSize = true;
            this.cb_horario.Location = new System.Drawing.Point(12, 110);
            this.cb_horario.Name = "cb_horario";
            this.cb_horario.Size = new System.Drawing.Size(111, 17);
            this.cb_horario.TabIndex = 10;
            this.cb_horario.Text = "Adicionar horário?";
            this.cb_horario.UseVisualStyleBackColor = true;
            this.cb_horario.CheckedChanged += new System.EventHandler(this.cb_horario_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Evento";
            // 
            // txt_evento
            // 
            this.txt_evento.Location = new System.Drawing.Point(12, 70);
            this.txt_evento.Name = "txt_evento";
            this.txt_evento.Size = new System.Drawing.Size(298, 20);
            this.txt_evento.TabIndex = 8;
            this.txt_evento.Text = " ";
            // 
            // bnt_excluir
            // 
            this.bnt_excluir.ForeColor = System.Drawing.Color.Red;
            this.bnt_excluir.Location = new System.Drawing.Point(238, 11);
            this.bnt_excluir.Name = "bnt_excluir";
            this.bnt_excluir.Size = new System.Drawing.Size(73, 20);
            this.bnt_excluir.TabIndex = 16;
            this.bnt_excluir.Text = "Excluir";
            this.bnt_excluir.UseVisualStyleBackColor = true;
            this.bnt_excluir.Click += new System.EventHandler(this.bnt_excluir_Click);
            // 
            // edicao_calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 188);
            this.Controls.Add(this.bnt_excluir);
            this.Controls.Add(this.txt_data);
            this.Controls.Add(this.bnt_editar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_horario2);
            this.Controls.Add(this.txt_horario1);
            this.Controls.Add(this.cb_horario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_evento);
            this.Name = "edicao_calendar";
            this.Text = "consulta_calendar";
            this.Load += new System.EventHandler(this.consulta_calendar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_data;
        private System.Windows.Forms.Button bnt_editar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_horario2;
        private System.Windows.Forms.TextBox txt_horario1;
        private System.Windows.Forms.CheckBox cb_horario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_evento;
        private System.Windows.Forms.Button bnt_excluir;
    }
}