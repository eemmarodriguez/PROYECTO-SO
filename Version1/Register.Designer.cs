namespace Version1
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Comprovation = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // UsernameBox
            // 
            this.UsernameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UsernameBox.ForeColor = System.Drawing.Color.Silver;
            this.UsernameBox.Location = new System.Drawing.Point(119, 144);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(187, 13);
            this.UsernameBox.TabIndex = 1;
            this.UsernameBox.Text = "User.";
            
            this.UsernameBox.Enter += new System.EventHandler(this.UsernameBox_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // PasswordBox
            // 
            this.PasswordBox.BackColor = System.Drawing.Color.White;
            this.PasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordBox.ForeColor = System.Drawing.Color.Silver;
            this.PasswordBox.Location = new System.Drawing.Point(119, 207);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(187, 13);
            this.PasswordBox.TabIndex = 2;
            this.PasswordBox.Text = "Password.";
          
            this.PasswordBox.Enter += new System.EventHandler(this.PasswordBox_Enter);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Location = new System.Drawing.Point(119, 314);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(134, 23);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "REGISTER";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click_1);
            // 
            // Comprovation
            // 
            this.Comprovation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Comprovation.ForeColor = System.Drawing.Color.Silver;
            this.Comprovation.Location = new System.Drawing.Point(119, 271);
            this.Comprovation.Name = "Comprovation";
            this.Comprovation.Size = new System.Drawing.Size(187, 13);
            this.Comprovation.TabIndex = 3;
            this.Comprovation.Text = "Repeat Password.";
  
            this.Comprovation.Enter += new System.EventHandler(this.Comprovation_Enter);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(326, 271);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(382, 359);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Comprovation);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsernameBox);
            this.Name = "Register";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox Comprovation;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}