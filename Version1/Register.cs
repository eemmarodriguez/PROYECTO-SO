using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Version1
{

    public partial class Register : Form
    {
        Socket server;
        string[] m;
        bool contraprimera = false;
        bool comprovationprimera = false;
        bool userprimera = false;
        string mensaje;
        public Register()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }
        public void SetRegister (string[] register)
        {
            this.m = register;
    
        }
        public void SetServerRegister(Socket servidor)
        {
            this.server = servidor;
        }
       
        private void enviar_server(string mensaje)
        {

            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);


        }
        private void SaveButton_Click_1(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor
            //al que deseamos conectarnos

            if ((Comprovation.Text != "") && (PasswordBox.Text != "") && (UsernameBox.Text != ""))
            {
                if (PasswordBox.Text == Comprovation.Text)
                {

                    enviar_server("5/" + UsernameBox.Text + "/" + PasswordBox.Text);
                    Thread.Sleep(300);

                    mensaje = m[1];

                    if (mensaje == "REGISTRADO")
                    {
                        PasswordBox.Text = "";
                        UsernameBox.Text = "";
                        Comprovation.Text = "";
                        MessageBox.Show("Usuario registrado correctamente");
                        this.Close();
                    }
                    else if (mensaje == "NO")
                    {
                        PasswordBox.Text = "";
                        UsernameBox.Text = "";
                        Comprovation.Text = "";
                        MessageBox.Show("Usuario ya registrado.");
                    }




                }
                else
                {
                    PasswordBox.Text = "";
                    Comprovation.Text = "";
                    MessageBox.Show("The passwords don't match");
                }


            }
            else
            {
                PasswordBox.Text = "";
                UsernameBox.Text = "";
                Comprovation.Text = "";
                MessageBox.Show("Introduzca un usuario y contraseña");
            }
        }
        
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (PasswordBox.PasswordChar == Convert.ToChar("*"))
                {
                    PasswordBox.PasswordChar = Convert.ToChar("\0");
                    Comprovation.PasswordChar = Convert.ToChar("\0");

                }
            }
            else
            {
                PasswordBox.PasswordChar = Convert.ToChar("*");
                Comprovation.PasswordChar = Convert.ToChar("*");

            }
            
            
        }


        private void Comprovation_Enter(object sender, EventArgs e)
        {
            Comprovation.Text = "";
            Comprovation.ForeColor = Color.Black;

            Comprovation.PasswordChar = Convert.ToChar("*");
            comprovationprimera = true;
        }

        private void PasswordBox_Enter(object sender, EventArgs e)
        {
            PasswordBox.Text = "";
            PasswordBox.ForeColor = Color.Black;
            PasswordBox.PasswordChar = Convert.ToChar("*");
            contraprimera = true;
        }

        private void UsernameBox_Enter(object sender, EventArgs e)
        {
            UsernameBox.Text = "";
            UsernameBox.ForeColor = Color.Black;
            userprimera = true;
        }
    }
}
