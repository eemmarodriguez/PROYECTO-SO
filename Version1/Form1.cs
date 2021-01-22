using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Media;

namespace Version1
{
    public partial class Preguntas : Form
    {
        string pregunta;
        string op1;
        string op2;
        string op3;
        string op4;
        int respuesta;
        int correcta;
        int seg;
        bool cerrar = false;
        bool activado = true;

        SoundPlayer Player = new SoundPlayer();
        public Preguntas()
        {
            InitializeComponent();
        }

        private void Preguntas_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            this.MaximizeBox = false;
            PREGUNTA.Text = pregunta;
            op1Button.Text = op1;
            op2Button.Text = op2;
            op3Button.Text = op3;
            op4Button.Text = op4;

            timer1.Start();
            seg = 15;
            timer1.Enabled = true;

            Player.SoundLocation = "sounds/Preguntas.wav";
            Player.Play();
        }
        public void SetFormPregunta (string pregunta_serv, string op1_serv, string op2_serv, string op3_serv, string op4_serv, string respuesta_serv)
        {
            pregunta = pregunta_serv;
            op1 = op1_serv;
            op2 = op2_serv;
            op3 = op3_serv;
            op4 = op4_serv;
            correcta = Convert.ToInt32(respuesta_serv);
        }
        public void SetTipo(string tipo)
        {
            if (tipo == "DEPORTE")
            {
                this.BackgroundImage = Image.FromFile("Imagenes/deporte.png");
            }
            if (tipo == "ARTE")
            {
                this.BackgroundImage = Image.FromFile("Imagenes/arte.png");
            }
            if (tipo == "GEOGRAFIA")
            {
                this.BackgroundImage = Image.FromFile("Imagenes/geografia.png");
            }
            if (tipo == "ENTRETENIMIENTO")
            {
                this.BackgroundImage = Image.FromFile("Imagenes/entretenimiento.png");
            }
            if (tipo == "HISTORIA")
            {
                this.BackgroundImage = Image.FromFile("Imagenes/historia.png");
            }
            if (tipo == "CIENCIA")
            {
                this.BackgroundImage = Image.FromFile("Imagenes/ciencia.png");
            }
            
        }
        public int GetRespuesta()
        {
            return respuesta;
        }
        private void op1Button_Click(object sender, EventArgs e)
        {
            
            respuesta = 1;
            cerrar = true;
            Player.Stop();
            timer1.Stop();
            if (respuesta == correcta)
            {
                Player.SoundLocation = "sounds/acierto.wav";
                Player.Play();
                op1Button.BackColor = Color.Green;
                MessageBox.Show("Acertaste");               
              //  ContadorLbl.Text = "00";
            }
            else
            {
                Player.SoundLocation = "sounds/error.wav";
                Player.Play();
                op1Button.BackColor = Color.Red;
                MessageBox.Show("Fallaste");               
             //   ContadorLbl.Text = "00";
            }
            this.Close();
        }

        private void op2Button_Click(object sender, EventArgs e)
        {
            respuesta = 2;
            cerrar = true;
            Player.Stop();
            timer1.Stop();
            if (respuesta == correcta)
            {
                Player.SoundLocation = "sounds/acierto.wav";
                Player.Play();
                op2Button.BackColor = Color.Green;
                MessageBox.Show("Acertaste");                
             //   ContadorLbl.Text = "00";
            }
            else
            {
                Player.SoundLocation = "sounds/error.wav";
                Player.Play();
                op2Button.BackColor = Color.Red;
                MessageBox.Show("Fallaste");
             //   ContadorLbl.Text = "00";

            }
            this.Close();
        }

        private void op3Button_Click(object sender, EventArgs e)
        {
            respuesta = 3;
            cerrar = true;
            Player.Stop();
            timer1.Stop();
            if (respuesta == correcta)
            {
                Player.SoundLocation = "sounds/acierto.wav";
                Player.Play();
                op3Button.BackColor = Color.Green;
                MessageBox.Show("Acertaste");
             //   ContadorLbl.Text = "00";
            }
            else
            {
                Player.SoundLocation = "sounds/error.wav";
                Player.Play();
                op3Button.BackColor = Color.Red;
                MessageBox.Show("Fallaste");                
             //   ContadorLbl.Text = "00";

            }
            this.Close();
        }


        private void op4Button_Click(object sender, EventArgs e)
        {
            respuesta = 4;
            cerrar = true;
            Player.Stop();
            timer1.Stop();
            if (respuesta == correcta)
            {
                Player.SoundLocation = "sounds/acierto.wav";
                Player.Play();
                op4Button.BackColor = Color.Green;
                MessageBox.Show("Acertaste");
             //   ContadorLbl.Text = "00";
            }
            else
            {
                Player.SoundLocation = "sounds/error.wav";
                Player.Play();
                op4Button.BackColor = Color.Red;
                MessageBox.Show("Fallaste");
             //   ContadorLbl.Text = "00";


            }
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seg -= 1;

            string segundos = seg.ToString();

           if(seg <= 9)
            {
                segundos = "0" + seg.ToString();
                if (seg <= 5)
                {
                    ContadorLbl.ForeColor = Color.Red;
                }
            }

            if (seg == 0)
            {
                Player.SoundLocation = "sounds/perdida_turno.wav";
                Player.Play();
                timer1.Stop();
                ContadorLbl.Text = "00";
                MessageBox.Show("Te has quedado sin tiempo!");
                cerrar = true;
                this.Close();
            }

            ContadorLbl.Text = segundos;
        }

        private void Preguntas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Player.Stop();
            timer1.Stop();

            if(cerrar == false)
            {
                Player.SoundLocation = "sounds/perdida_turno.wav";
                Player.Play();
                MessageBox.Show("Has perdido tu turno");
            }
            
        }

        private void VolumenBox_Click(object sender, EventArgs e)
        {
            if (activado == true)
            {
                VolumenBox.Image = Image.FromFile("Imagenes/nosonido.jpg");
                Player.Stop();
                activado = false;
            }

            else
            {
                VolumenBox.Image = Image.FromFile("Imagenes/sonido2.jpg");
                Player.Play();
                activado = true;
            }
        }
    }
}
