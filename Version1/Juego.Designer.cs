namespace Version1
{
    partial class Juego
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
            this.PartidaLabel = new System.Windows.Forms.Label();
            this.ChatBOX = new System.Windows.Forms.ListBox();
            this.MensajeBOX = new System.Windows.Forms.TextBox();
            this.SendBUTTON = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PartidaLabel
            // 
            this.PartidaLabel.AutoSize = true;
            this.PartidaLabel.Location = new System.Drawing.Point(53, 40);
            this.PartidaLabel.Name = "PartidaLabel";
            this.PartidaLabel.Size = new System.Drawing.Size(105, 13);
            this.PartidaLabel.TabIndex = 0;
            this.PartidaLabel.Text = "NUMERO PARTIDA";
            // 
            // ChatBOX
            // 
            this.ChatBOX.FormattingEnabled = true;
            this.ChatBOX.Location = new System.Drawing.Point(56, 88);
            this.ChatBOX.Name = "ChatBOX";
            this.ChatBOX.Size = new System.Drawing.Size(237, 251);
            this.ChatBOX.TabIndex = 1;
            this.ChatBOX.Tag = "";
            this.ChatBOX.SelectedIndexChanged += new System.EventHandler(this.ChatBOX_SelectedIndexChanged);
            // 
            // MensajeBOX
            // 
            this.MensajeBOX.Location = new System.Drawing.Point(56, 356);
            this.MensajeBOX.Name = "MensajeBOX";
            this.MensajeBOX.Size = new System.Drawing.Size(172, 20);
            this.MensajeBOX.TabIndex = 2;
            // 
            // SendBUTTON
            // 
            this.SendBUTTON.Location = new System.Drawing.Point(234, 353);
            this.SendBUTTON.Name = "SendBUTTON";
            this.SendBUTTON.Size = new System.Drawing.Size(59, 24);
            this.SendBUTTON.TabIndex = 3;
            this.SendBUTTON.Text = "Send";
            this.SendBUTTON.UseVisualStyleBackColor = true;
            this.SendBUTTON.Click += new System.EventHandler(this.SendBUTTON_Click);
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendBUTTON);
            this.Controls.Add(this.MensajeBOX);
            this.Controls.Add(this.ChatBOX);
            this.Controls.Add(this.PartidaLabel);
            this.Name = "Juego";
            this.Text = "Juego";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Juego_FormClosing);
            this.Load += new System.EventHandler(this.Juego_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PartidaLabel;
        private System.Windows.Forms.ListBox ChatBOX;
        private System.Windows.Forms.TextBox MensajeBOX;
        private System.Windows.Forms.Button SendBUTTON;
    }
}