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
                    Thread.Sleep(60);
                    string mensaje;
                    mensaje = m[1];

                    if (mensaje == "REGISTRADO")
                    {
                        MessageBox.Show("Usuario registrado");
                    }
                    else if (mensaje == "NO")
                    {
                        MessageBox.Show("Usuario ya registrado.");
                    }

                   

                    
                }
                else
                    MessageBox.Show("The passwords don't match");


            }
            else
                MessageBox.Show("Introduzca un usuario y contraseña"); 
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
    }
}
