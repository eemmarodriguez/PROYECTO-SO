namespace Version1
{
    partial class Consultas
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
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.GamesWonButton = new System.Windows.Forms.RadioButton();
            this.MoreGamesButton = new System.Windows.Forms.RadioButton();
            this.PlayersListButton = new System.Windows.Forms.RadioButton();
            this.SendButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_player = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Online_users = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.IdLabel = new System.Windows.Forms.Label();
            this.UsuarioLabel = new System.Windows.Forms.Label();
            this.InvitarButton = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.Partida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JugarButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username of the player you want to know about:\r\n(Only necessary for the first con" +
    "sultation)";
            // 
            // UsernameBox
            // 
            this.UsernameBox.AccessibleDescription = "";
            this.UsernameBox.BackColor = System.Drawing.SystemColors.Window;
            this.UsernameBox.ForeColor = System.Drawing.Color.Black;
            this.UsernameBox.Location = new System.Drawing.Point(442, 87);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(183, 20);
            this.UsernameBox.TabIndex = 1;
            this.UsernameBox.Click += new System.EventHandler(this.UsernameBox_Click);
            this.UsernameBox.TextChanged += new System.EventHandler(this.UsernameBox_TextChanged);
            // 
            // GamesWonButton
            // 
            this.GamesWonButton.AutoSize = true;
            this.GamesWonButton.Location = new System.Drawing.Point(476, 64);
            this.GamesWonButton.Name = "GamesWonButton";
            this.GamesWonButton.Size = new System.Drawing.Size(84, 17);
            this.GamesWonButton.TabIndex = 2;
            this.GamesWonButton.TabStop = true;
            this.GamesWonButton.Text = "Games won ";
            this.GamesWonButton.UseVisualStyleBackColor = true;
            this.GamesWonButton.CheckedChanged += new System.EventHandler(this.GamesWonButton_CheckedChanged);
            // 
            // MoreGamesButton
            // 
            this.MoreGamesButton.AutoSize = true;
            this.MoreGamesButton.Location = new System.Drawing.Point(380, 168);
            this.MoreGamesButton.Name = "MoreGamesButton";
            this.MoreGamesButton.Size = new System.Drawing.Size(202, 17);
            this.MoreGamesButton.TabIndex = 3;
            this.MoreGamesButton.TabStop = true;
            this.MoreGamesButton.Text = "Day that more games have been won";
            this.MoreGamesButton.UseVisualStyleBackColor = true;
            this.MoreGamesButton.CheckedChanged += new System.EventHandler(this.MoreGamesButton_CheckedChanged);
            // 
            // PlayersListButton
            // 
            this.PlayersListButton.AutoSize = true;
            this.PlayersListButton.Location = new System.Drawing.Point(380, 192);
            this.PlayersListButton.Name = "PlayersListButton";
            this.PlayersListButton.Size = new System.Drawing.Size(191, 17);
            this.PlayersListButton.TabIndex = 4;
            this.PlayersListButton.TabStop = true;
            this.PlayersListButton.Text = "Players registered in the application";
            this.PlayersListButton.UseVisualStyleBackColor = true;
            this.PlayersListButton.CheckedChanged += new System.EventHandler(this.PlayersListButton_CheckedChanged);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(485, 113);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 5;
            this.SendButton.Text = "CHECK";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(627, 255);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(149, 23);
            this.DisconnectButton.TabIndex = 7;
            this.DisconnectButton.Text = "CERRAR SESIÓN";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Username,
            this.id_player});
            this.dataGridView1.Location = new System.Drawing.Point(152, 128);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(203, 150);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Username
            // 
            this.Username.HeaderText = "Username";
            this.Username.Name = "Username";
            // 
            // id_player
            // 
            this.id_player.HeaderText = "Id";
            this.id_player.Name = "id_player";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Online_users});
            this.dataGridView2.Location = new System.Drawing.Point(649, 87);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(103, 150);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // Online_users
            // 
            this.Online_users.HeaderText = "Online users";
            this.Online_users.Name = "Online_users";
            this.Online_users.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(895, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 11;
            // 
            // IdLabel
            // 
            this.IdLabel.AutoSize = true;
            this.IdLabel.Location = new System.Drawing.Point(895, 30);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(18, 13);
            this.IdLabel.TabIndex = 13;
            this.IdLabel.Text = "ID";
            // 
            // UsuarioLabel
            // 
            this.UsuarioLabel.AutoSize = true;
            this.UsuarioLabel.Location = new System.Drawing.Point(895, 6);
            this.UsuarioLabel.Name = "UsuarioLabel";
            this.UsuarioLabel.Size = new System.Drawing.Size(56, 13);
            this.UsuarioLabel.TabIndex = 12;
            this.UsuarioLabel.Text = "USUARIO";
            // 
            // InvitarButton
            // 
            this.InvitarButton.Location = new System.Drawing.Point(831, 215);
            this.InvitarButton.Name = "InvitarButton";
            this.InvitarButton.Size = new System.Drawing.Size(75, 23);
            this.InvitarButton.TabIndex = 15;
            this.InvitarButton.Text = "Invitar";
            this.InvitarButton.UseVisualStyleBackColor = true;
            this.InvitarButton.Click += new System.EventHandler(this.InvitarButton_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Partida});
            this.dataGridView3.Location = new System.Drawing.Point(818, 87);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.Size = new System.Drawing.Size(105, 122);
            this.dataGridView3.TabIndex = 16;
            this.dataGridView3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            // 
            // Partida
            // 
            this.Partida.HeaderText = "Partida";
            this.Partida.Name = "Partida";
            this.Partida.ReadOnly = true;
            // 
            // JugarButton
            // 
            this.JugarButton.Location = new System.Drawing.Point(221, 445);
            this.JugarButton.Name = "JugarButton";
            this.JugarButton.Size = new System.Drawing.Size(568, 23);
            this.JugarButton.TabIndex = 17;
            this.JugarButton.Text = "Jugar";
            this.JugarButton.UseVisualStyleBackColor = true;
            this.JugarButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(895, 52);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(50, 13);
            this.StatusLabel.TabIndex = 18;
            this.StatusLabel.Text = "STATUS";
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 489);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.JugarButton);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.InvitarButton);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.UsuarioLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.PlayersListButton);
            this.Controls.Add(this.MoreGamesButton);
            this.Controls.Add(this.GamesWonButton);
            this.Controls.Add(this.UsernameBox);
            this.Controls.Add(this.label1);
            this.Name = "Consultas";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Consultas_FormClosing_1);
            this.Load += new System.EventHandler(this.Consultas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.RadioButton GamesWonButton;
        private System.Windows.Forms.RadioButton MoreGamesButton;
        private System.Windows.Forms.RadioButton PlayersListButton;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Online_users;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_player;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label IdLabel;
        private System.Windows.Forms.Label UsuarioLabel;
        private System.Windows.Forms.Button InvitarButton;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Partida;
        private System.Windows.Forms.Button JugarButton;
        private System.Windows.Forms.Label StatusLabel;
    }
}

