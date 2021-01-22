namespace Version1
{
    partial class Preguntas
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preguntas));
            this.PREGUNTA = new System.Windows.Forms.Label();
            this.op1Button = new System.Windows.Forms.Button();
            this.op2Button = new System.Windows.Forms.Button();
            this.op3Button = new System.Windows.Forms.Button();
            this.op4Button = new System.Windows.Forms.Button();
            this.ContadorLbl = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.VolumenBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.VolumenBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PREGUNTA
            // 
            this.PREGUNTA.AutoSize = true;
            this.PREGUNTA.BackColor = System.Drawing.Color.Transparent;
            this.PREGUNTA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PREGUNTA.Location = new System.Drawing.Point(214, 96);
            this.PREGUNTA.Name = "PREGUNTA";
            this.PREGUNTA.Size = new System.Drawing.Size(149, 13);
            this.PREGUNTA.TabIndex = 0;
            this.PREGUNTA.Text = "PREGUNTA QUE HACEMOS";
            this.PREGUNTA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // op1Button
            // 
            this.op1Button.AutoSize = true;
            this.op1Button.ForeColor = System.Drawing.Color.Black;
            this.op1Button.Location = new System.Drawing.Point(280, 142);
            this.op1Button.Name = "op1Button";
            this.op1Button.Size = new System.Drawing.Size(209, 23);
            this.op1Button.TabIndex = 1;
            this.op1Button.Text = "button1";
            this.op1Button.UseVisualStyleBackColor = true;
            this.op1Button.Click += new System.EventHandler(this.op1Button_Click);
            // 
            // op2Button
            // 
            this.op2Button.AutoSize = true;
            this.op2Button.ForeColor = System.Drawing.Color.Black;
            this.op2Button.Location = new System.Drawing.Point(280, 187);
            this.op2Button.Name = "op2Button";
            this.op2Button.Size = new System.Drawing.Size(209, 23);
            this.op2Button.TabIndex = 2;
            this.op2Button.Text = "button2";
            this.op2Button.UseVisualStyleBackColor = true;
            this.op2Button.Click += new System.EventHandler(this.op2Button_Click);
            // 
            // op3Button
            // 
            this.op3Button.AutoSize = true;
            this.op3Button.ForeColor = System.Drawing.Color.Black;
            this.op3Button.Location = new System.Drawing.Point(280, 233);
            this.op3Button.Name = "op3Button";
            this.op3Button.Size = new System.Drawing.Size(209, 23);
            this.op3Button.TabIndex = 3;
            this.op3Button.Text = "button3";
            this.op3Button.UseVisualStyleBackColor = true;
            this.op3Button.Click += new System.EventHandler(this.op3Button_Click);
            // 
            // op4Button
            // 
            this.op4Button.AutoSize = true;
            this.op4Button.ForeColor = System.Drawing.Color.Black;
            this.op4Button.Location = new System.Drawing.Point(280, 279);
            this.op4Button.Name = "op4Button";
            this.op4Button.Size = new System.Drawing.Size(209, 23);
            this.op4Button.TabIndex = 4;
            this.op4Button.Text = "button4";
            this.op4Button.UseVisualStyleBackColor = true;
            this.op4Button.Click += new System.EventHandler(this.op4Button_Click);
            // 
            // ContadorLbl
            // 
            this.ContadorLbl.AutoSize = true;
            this.ContadorLbl.BackColor = System.Drawing.Color.Transparent;
            this.ContadorLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContadorLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContadorLbl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ContadorLbl.Location = new System.Drawing.Point(360, 357);
            this.ContadorLbl.Name = "ContadorLbl";
            this.ContadorLbl.Size = new System.Drawing.Size(39, 29);
            this.ContadorLbl.TabIndex = 0;
            this.ContadorLbl.Text = "15";
            this.ContadorLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VolumenBox
            // 
            this.VolumenBox.Image = ((System.Drawing.Image)(resources.GetObject("VolumenBox.Image")));
            this.VolumenBox.Location = new System.Drawing.Point(746, 12);
            this.VolumenBox.Name = "VolumenBox";
            this.VolumenBox.Size = new System.Drawing.Size(32, 29);
            this.VolumenBox.TabIndex = 5;
            this.VolumenBox.TabStop = false;
            this.VolumenBox.Click += new System.EventHandler(this.VolumenBox_Click);
            // 
            // Preguntas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.VolumenBox);
            this.Controls.Add(this.ContadorLbl);
            this.Controls.Add(this.op4Button);
            this.Controls.Add(this.op3Button);
            this.Controls.Add(this.op2Button);
            this.Controls.Add(this.op1Button);
            this.Controls.Add(this.PREGUNTA);
            this.Name = "Preguntas";
            this.Text = "Preguntas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Preguntas_FormClosing);
            this.Load += new System.EventHandler(this.Preguntas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VolumenBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PREGUNTA;
        private System.Windows.Forms.Button op1Button;
        private System.Windows.Forms.Button op2Button;
        private System.Windows.Forms.Button op3Button;
        private System.Windows.Forms.Button op4Button;
        private System.Windows.Forms.Label ContadorLbl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox VolumenBox;
    }
}