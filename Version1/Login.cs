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
    public partial class Login : Form
    {
        Socket server;
        Thread atender;
        Register r = new Register();
        bool cerrando = false;
        string[] mensaje;
        string id;
        string socket;
        string usuario;
        string[] registro;
        int entro = 0;
        string existe = "false";
        bool login = false;
        public Login()
        {
            InitializeComponent();
            this.CenterToScreen(); 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
        public void RegisterRequest (string[] Request)
        {
            this.registro = Request;

        }
        public string GetRegistro()
        {
            return registro[1];

        }
        public void SetRegister (Register forms)
        {
            this.r = forms;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            r.ShowDialog();
            this.CenterToScreen();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.CenterToScreen();
            this.MaximizeBox = false;
        }
        public void SetServer(Socket servidor)
        {
            this.server = servidor;
        }
        public void SetMensaje(String[] message)
        {
            this.mensaje = message; 

        }
        public void SetLogin(bool log)
        {
            this.login = log;
        }
        public bool GetLogin()
        {
            return this.login;
        }
        public string GetId()
        {
            return this.id;
        }
        public string GetSocket()
        {
            return this.socket; 
        }
        public int GetDesconectando()
        {
            return this.entro; 
        }
        public string GetUser()
        {
            return this.usuario;
        }
        public void SetThread(Thread thread)
        {
            this.atender = thread;
        }
        public void SetExiste(string bo)
        {
            this.existe = bo;
        }
        private void enviar_server (string mensaje)
        {
 
            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            usuario = textBox1.Text;
            
            enviar_server("4/" + usuario);
            
            string pwd = textBox2.Text;
            

            if ((usuario != "") && (pwd != ""))
            {

                Thread.Sleep(300);
                enviar_server("6/" + textBox1.Text);
                Thread.Sleep(300);
                string[] message = mensaje; //El split sirve para quedarme solo con el string que quiero
                string contraseña = message[1];                                                         //lo demás se considera basura
                if ((pwd == contraseña) && (existe=="false")) // SI LA CONTRASEÑA COINCIDE AVANZAMOS
                {
                    login = true;
                    MessageBox.Show("Log in Correcto");

                    entro = 1;
                    id = message[2];
                    //socket = message[3];
                    
                    this.Hide();      
                    textBox2.Text = "";


                }
                if (pwd != contraseña)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                    textBox2.Text = "";

                }
                if (existe == "true")
                {
                    MessageBox.Show("El usuario ya esta conectado en otro dispositivo");
                }

            }
            else
                MessageBox.Show("Introduzca su usuario y contraseña");
                textBox2.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (textBox2.PasswordChar == Convert.ToChar("*"))
                {
                    textBox2.PasswordChar = Convert.ToChar("\0");
                    

                }
            }
            else
            {
                textBox2.PasswordChar = Convert.ToChar("*");
               
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
            textBox2.PasswordChar = Convert.ToChar("*");

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                usuario = textBox1.Text;

                enviar_server("4/" + usuario);

                string pwd = textBox2.Text;


                if ((usuario != "") && (pwd != ""))
                {

                    Thread.Sleep(300);
                    enviar_server("6/" + textBox1.Text);
                    Thread.Sleep(300);
                    string[] message = mensaje; //El split sirve para quedarme solo con el string que quiero
                    string contraseña = message[1];                                                         //lo demás se considera basura
                    if ((pwd == contraseña) && (existe == "false")) // SI LA CONTRASEÑA COINCIDE AVANZAMOS
                    {
                        MessageBox.Show("Log in Correcto");

                        entro = 1;
                        id = message[2];
                        //socket = message[3];

                        this.Hide();
                        textBox2.Text = "";


                    }
                    if (pwd != contraseña)
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                        textBox2.Text = "";

                    }
                    if (existe == "true")
                    {
                        MessageBox.Show("El usuario ya esta conectado en otro dispositivo");
                    }

                }
                else
                    MessageBox.Show("Introduzca su usuario y contraseña");
                textBox2.Text = "";
            }
            
        }
    }
}
