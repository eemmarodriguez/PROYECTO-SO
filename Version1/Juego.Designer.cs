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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Juego));
            this.PartidaLabel = new System.Windows.Forms.Label();
            this.ChatBOX = new System.Windows.Forms.ListBox();
            this.MensajeBOX = new System.Windows.Forms.TextBox();
            this.SendBUTTON = new System.Windows.Forms.Button();
            this.UsuarioLabel = new System.Windows.Forms.Label();
            this.CategoriaButton = new System.Windows.Forms.Button();
            this.CategoriaLabel = new System.Windows.Forms.Label();
            this.TurnoLabel = new System.Windows.Forms.Label();
            this.PreguntaLabel = new System.Windows.Forms.Label();
            this.ScoreBoardGridView = new System.Windows.Forms.DataGridView();
            this.Jugadores = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Puntuacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deportes = new System.Windows.Forms.DataGridViewImageColumn();
            this.Historia = new System.Windows.Forms.DataGridViewImageColumn();
            this.Ciencia = new System.Windows.Forms.DataGridViewImageColumn();
            this.Arte = new System.Windows.Forms.DataGridViewImageColumn();
            this.Entretenimiento = new System.Windows.Forms.DataGridViewImageColumn();
            this.Geografia = new System.Windows.Forms.DataGridViewImageColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreBoardGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PartidaLabel
            // 
            this.PartidaLabel.AutoSize = true;
            this.PartidaLabel.BackColor = System.Drawing.Color.Transparent;
            this.PartidaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PartidaLabel.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PartidaLabel.ForeColor = System.Drawing.Color.White;
            this.PartidaLabel.Location = new System.Drawing.Point(970, 12);
            this.PartidaLabel.Name = "PartidaLabel";
            this.PartidaLabel.Size = new System.Drawing.Size(97, 13);
            this.PartidaLabel.TabIndex = 0;
            this.PartidaLabel.Text = "NUMERO PARTIDA";
            // 
            // ChatBOX
            // 
            this.ChatBOX.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatBOX.FormattingEnabled = true;
            this.ChatBOX.Location = new System.Drawing.Point(56, 62);
            this.ChatBOX.Name = "ChatBOX";
            this.ChatBOX.Size = new System.Drawing.Size(237, 511);
            this.ChatBOX.TabIndex = 1;
            this.ChatBOX.Tag = "";
            // 
            // MensajeBOX
            // 
            this.MensajeBOX.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MensajeBOX.Location = new System.Drawing.Point(56, 592);
            this.MensajeBOX.Name = "MensajeBOX";
            this.MensajeBOX.Size = new System.Drawing.Size(172, 20);
            this.MensajeBOX.TabIndex = 2;
       
            this.MensajeBOX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MensajeBOX_KeyPress);
            // 
            // SendBUTTON
            // 
            this.SendBUTTON.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendBUTTON.Location = new System.Drawing.Point(234, 589);
            this.SendBUTTON.Name = "SendBUTTON";
            this.SendBUTTON.Size = new System.Drawing.Size(59, 24);
            this.SendBUTTON.TabIndex = 3;
            this.SendBUTTON.Text = "Enviar";
            this.SendBUTTON.UseVisualStyleBackColor = true;
            this.SendBUTTON.Click += new System.EventHandler(this.SendBUTTON_Click);
            // 
            // UsuarioLabel
            // 
            this.UsuarioLabel.AutoSize = true;
            this.UsuarioLabel.BackColor = System.Drawing.Color.Transparent;
            this.UsuarioLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UsuarioLabel.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsuarioLabel.ForeColor = System.Drawing.Color.White;
            this.UsuarioLabel.Location = new System.Drawing.Point(970, 36);
            this.UsuarioLabel.Name = "UsuarioLabel";
            this.UsuarioLabel.Size = new System.Drawing.Size(42, 13);
            this.UsuarioLabel.TabIndex = 4;
            this.UsuarioLabel.Text = "Usuario";
            // 
            // CategoriaButton
            // 
            this.CategoriaButton.BackColor = System.Drawing.Color.White;
            this.CategoriaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoriaButton.Font = new System.Drawing.Font("Berlin Sans FB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoriaButton.Location = new System.Drawing.Point(973, 556);
            this.CategoriaButton.Name = "CategoriaButton";
            this.CategoriaButton.Size = new System.Drawing.Size(242, 46);
            this.CategoriaButton.TabIndex = 6;
            this.CategoriaButton.Text = "TIRAR";
            this.CategoriaButton.UseVisualStyleBackColor = false;
            this.CategoriaButton.Click += new System.EventHandler(this.CategoriaButton_Click);
            // 
            // CategoriaLabel
            // 
            this.CategoriaLabel.AutoSize = true;
            this.CategoriaLabel.Location = new System.Drawing.Point(1256, 453);
            this.CategoriaLabel.Name = "CategoriaLabel";
            this.CategoriaLabel.Size = new System.Drawing.Size(69, 13);
            this.CategoriaLabel.TabIndex = 7;
            this.CategoriaLabel.Text = "CATEGORIA";
            this.CategoriaLabel.Visible = false;
            // 
            // TurnoLabel
            // 
            this.TurnoLabel.AutoSize = true;
            this.TurnoLabel.Location = new System.Drawing.Point(1256, 497);
            this.TurnoLabel.Name = "TurnoLabel";
            this.TurnoLabel.Size = new System.Drawing.Size(46, 13);
            this.TurnoLabel.TabIndex = 8;
            this.TurnoLabel.Text = "TURNO";
            this.TurnoLabel.Visible = false;
            // 
            // PreguntaLabel
            // 
            this.PreguntaLabel.AutoSize = true;
            this.PreguntaLabel.Location = new System.Drawing.Point(1367, 477);
            this.PreguntaLabel.Name = "PreguntaLabel";
            this.PreguntaLabel.Size = new System.Drawing.Size(67, 13);
            this.PreguntaLabel.TabIndex = 9;
            this.PreguntaLabel.Text = "PREGUNTA";
            this.PreguntaLabel.Visible = false;
            // 
            // ScoreBoardGridView
            // 
            this.ScoreBoardGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ScoreBoardGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScoreBoardGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Jugadores,
            this.Puntuacion,
            this.Deportes,
            this.Historia,
            this.Ciencia,
            this.Arte,
            this.Entretenimiento,
            this.Geografia});
            this.ScoreBoardGridView.Location = new System.Drawing.Point(973, 64);
            this.ScoreBoardGridView.Name = "ScoreBoardGridView";
            this.ScoreBoardGridView.Size = new System.Drawing.Size(796, 275);
            this.ScoreBoardGridView.TabIndex = 10;
            // 
            // Jugadores
            // 
            this.Jugadores.HeaderText = "Jugadores";
            this.Jugadores.Name = "Jugadores";
            // 
            // Puntuacion
            // 
            this.Puntuacion.HeaderText = "Puntuación";
            this.Puntuacion.Name = "Puntuacion";
            // 
            // Deportes
            // 
            this.Deportes.HeaderText = "Deportes";
            this.Deportes.Name = "Deportes";
            // 
            // Historia
            // 
            this.Historia.HeaderText = "Historia";
            this.Historia.Name = "Historia";
            // 
            // Ciencia
            // 
            this.Ciencia.HeaderText = "Ciencia";
            this.Ciencia.Name = "Ciencia";
            // 
            // Arte
            // 
            this.Arte.HeaderText = "Arte";
            this.Arte.Name = "Arte";
            // 
            // Entretenimiento
            // 
            this.Entretenimiento.HeaderText = "Entretenimiento";
            this.Entretenimiento.Name = "Entretenimiento";
            // 
            // Geografia
            // 
            this.Geografia.HeaderText = "Geografía";
            this.Geografia.Name = "Geografia";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1259, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "GANAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1451, 589);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(611, 278);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(51, 61);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(336, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1259, 536);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "PUNTUACION 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Berlin Sans FB", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(53, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Chat:";
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1803, 716);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ScoreBoardGridView);
            this.Controls.Add(this.PreguntaLabel);
            this.Controls.Add(this.TurnoLabel);
            this.Controls.Add(this.CategoriaLabel);
            this.Controls.Add(this.CategoriaButton);
            this.Controls.Add(this.UsuarioLabel);
            this.Controls.Add(this.SendBUTTON);
            this.Controls.Add(this.MensajeBOX);
            this.Controls.Add(this.ChatBOX);
            this.Controls.Add(this.PartidaLabel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Juego";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Juego_FormClosing);
            this.Load += new System.EventHandler(this.Juego_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ScoreBoardGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PartidaLabel;
        private System.Windows.Forms.ListBox ChatBOX;
        private System.Windows.Forms.TextBox MensajeBOX;
        private System.Windows.Forms.Button SendBUTTON;
        private System.Windows.Forms.Label UsuarioLabel;
        private System.Windows.Forms.Button CategoriaButton;
        private System.Windows.Forms.Label CategoriaLabel;
        private System.Windows.Forms.Label TurnoLabel;
        private System.Windows.Forms.Label PreguntaLabel;
        private System.Windows.Forms.DataGridView ScoreBoardGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jugadores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Puntuacion;
        private System.Windows.Forms.DataGridViewImageColumn Deportes;
        private System.Windows.Forms.DataGridViewImageColumn Historia;
        private System.Windows.Forms.DataGridViewImageColumn Ciencia;
        private System.Windows.Forms.DataGridViewImageColumn Arte;
        private System.Windows.Forms.DataGridViewImageColumn Entretenimiento;
        private System.Windows.Forms.DataGridViewImageColumn Geografia;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
    }
}