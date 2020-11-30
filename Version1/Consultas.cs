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

namespace Version1
{
    public partial class Consultas : Form
    {
        Login l = new Login();
        Socket server;
        bool disconecting = false; 
        string usuario;
        int id;
        bool conectado = false;
        int contador_servicios; 
        public Consultas()
        {
            InitializeComponent();
            if (this.BackColor != Color.Green)
                SendButton.Enabled = false; 
        }
        
        private void Consultas_Load(object sender, EventArgs e)
        {
            
            
            this.CenterToScreen();

            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor
            //al que deseamos conectarnos
            conectado = true;

            UsuarioLabel.Text = "User: "+ this.usuario;
            IdLabel.Text = "ID: " + Convert.ToString(this.id);

            IPAddress direc = IPAddress.Parse("192.168.56.101");//DireccionIP de la Maquina Virtual
            IPEndPoint ipep = new IPEndPoint(direc, 9007); //Le pasamos el acceso y el puerto que asignamos en el codigo del servidor

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//Parámetros estándard
            try
            {
                server.Connect(ipep); //Intentamos conectar el socket
                this.BackColor = Color.Green;
                SendButton.Enabled = true;
                //MessageBox.Show("conectado");
            }
            catch (SocketException ex)
            {
                //Si hay excepción imprimimos error y salimos del programa con return
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;
            }

            //PREGUNTAR QUE SOCKET ME CORRESPONDE

            string socket = consulta_server("6/" + "SOCKET");

            //NOS VAMOS A PONER COMO ONLINE

            int calculo = Convert.ToInt32(socket);
            calculo++;
            socket = Convert.ToString(calculo);
            string respuesta = consulta_server("7/" + socket + "/" + this.id);

        }
        public void SetUsername (string user)  //RECIBIMOS EL USUARIO CONECTADO
        { 
            this.usuario = user;
        }
        public void SetId(string identifier) // RECIBIMOS EL ID DEL USUARIO CONECTADO
        {
            int id_1 = Convert.ToInt32(identifier);
            this.id = id_1;

        }
        private string consulta_server(string mensaje)  //UNA FUNCION PARA SIMPLIFICAR EL ENVIAR DATOS AL SERVER
        {

            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            //Recibimos un vector de bytes y lo convertimos a string
            byte[] msg2 = new byte[30];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            return mensaje;
        }

        private void ConnectButton_Click(object sender, EventArgs e)  //NOS CONECTAMOS AL SERVIDOR 
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {

            if(GamesWonButton.Checked)
            {
                string mensaje = consulta_server("1/" + UsernameBox.Text);
 
                MessageBox.Show("El numero de partidas ganadas por el usuario son: " + mensaje);
            }
            if (MoreGamesButton.Checked)
            {
                //if(MoreGamesButton.Checked)
                //{

                //    string mensaje = consulta_server("2/ALGO");

                //    MessageBox.Show("El dia que se han jugado mas partidas es: " + mensaje);

                //}

                //if(PlayersListButton.Checked)
                //{

                //    // RESETEAMOS LA GRID PARA LOS DATOS

                //    dataGridView1.Rows.Clear();
                //    dataGridView1.Refresh();

                //    //CONSULTAMOS AL SERVIDOR SOBRE EL NUMERO DE JUGADORES
                //    //PREGUNTANDO EL ID MÁS GRANDE

                //    string mensaje = consulta_server("3/IDMAX");


                //    int i = 2;

                //    while (i <= Convert.ToInt32(mensaje))
                //    {
                //        string istr = Convert.ToString(i);

                //        //PREGUNTAMOS A LA CONSULTA 15 A QUIEN CORRESPONDE EL ID 
                //        //IGUAL A LA ITERACION I. 

                //        string message = consulta_server("15/" + istr);

                //        //IMPRIMIMOS LOS RESULTADOS EN LA GRID DEL FORMS
                //        int n = dataGridView1.Rows.Add();
                //        dataGridView1.Rows[n].Cells[0].Value = message;
                //        dataGridView1.Rows[n].Cells[1].Value = istr;
                //        dataGridView1.Refresh();

                //        i++;

                //    }

                //}
                //if (OnlinePlayersBUTTON.Checked)
                //{
                //    dataGridView2.Rows.Clear();
                //    dataGridView2.Refresh();

                //    string index = consulta_server("9/index"); //LE PREGUNTAMOS CUANTO ES EL VALOR MAXIMO DEL IDENTIFICADOR
                //    int total = Convert.ToInt32(index);
                //    int i = 2;
                //    while (i <= total)
                //    {
                //        string username = consulta_server("10/" + Convert.ToString(i));
                //        if (username != "NO") //SI NOS DEVUELVE NO ESE USUARIO NO ESTA CONECTADO
                //        { 
                //            int n = dataGridView2.Rows.Add();
                //            dataGridView2.Rows[n].Cells[0].Value = username;
                //            dataGridView1.Refresh();
                //        }
                //        i++;
                //    }

                //}
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            disconecting = true; 

            UsuarioLabel.Text = "";
            UsuarioLabel.Text = "";

            // NOS QUITAMOS DEL MODO ONLINE
            
            conectado = false;
            string respuesta = consulta_server("8/" + Convert.ToString(id));

            //NOS DESCONECTAMOS DEL SOCKET
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Nos desconectamos
            this.BackColor = Color.Gray;
            SendButton.Enabled = false;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            this.Close();
            l.ShowDialog();

            
            
        }

        private void Consultas_FormClosing_1(object sender, FormClosingEventArgs e)
        {

            // El formulario se está cerrando. Llamamos al evento
            // Click del control Button1.
            //Mensaje de desconexión
            if (disconecting == false)
            {
                if (MessageBox.Show("Desea salir del programa", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (conectado == true)
                    {
                        string respuesta = consulta_server("8/" + Convert.ToString(id));

                        string mensaje = "0/";

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Nos desconectamos
                        this.BackColor = Color.Gray;
                        SendButton.Enabled = false;
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                         
                    }
                    disconecting = true; 
                    Application.Exit();
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //FUNCIONES PARA QUITAR Y PONER LA BARRA DE TEXTO Y LAS TABLAS DE DATOS
        private void GamesWonButton_CheckedChanged(object sender, EventArgs e)
        {
            UsernameBox.Enabled = true;
            UsernameBox.Text = "Write here the username";




            
        }

        private void MoreGamesButton_CheckedChanged(object sender, EventArgs e)
        {
            UsernameBox.Enabled = false;

            if (MoreGamesButton.Checked)
            {
   
                string mensaje = consulta_server("2/ALGO");

                MessageBox.Show("El dia que se han jugado mas partidas es: " + mensaje);
            }

        }

        private void PlayersListButton_CheckedChanged(object sender, EventArgs e)
        {

            UsernameBox.Enabled = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;

            if (PlayersListButton.Checked)
            {
                // RESETEAMOS LA GRID PARA LOS DATOS

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                //CONSULTAMOS AL SERVIDOR SOBRE EL NUMERO DE JUGADORES
                //PREGUNTANDO EL ID MÁS GRANDE

                string mensaje = consulta_server("3/IDMAX");


                int i = 2;

                while (i <= Convert.ToInt32(mensaje))
                {
                    string istr = Convert.ToString(i);

                    //PREGUNTAMOS A LA CONSULTA 15 A QUIEN CORRESPONDE EL ID 
                    //IGUAL A LA ITERACION I. 

                    string message = consulta_server("15/" + istr);

                    //IMPRIMIMOS LOS RESULTADOS EN LA GRID DEL FORMS
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = message;
                    dataGridView1.Rows[n].Cells[1].Value = istr;
                    dataGridView1.Refresh();

                    i++;

                }
            }

        }

        private void OnlinePlayersBUTTON_CheckedChanged(object sender, EventArgs e)
        {
            UsernameBox.Enabled = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;

            if (OnlinePlayersBUTTON.Checked)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.Refresh();

                string index = consulta_server("9/index"); //LE PREGUNTAMOS CUANTO ES EL VALOR MAXIMO DEL IDENTIFICADOR
                int total = Convert.ToInt32(index);
                int i = 2;
                while (i <= total)
                {
                    string username = consulta_server("10/" + Convert.ToString(i));
                    if (username != "NO") //SI NOS DEVUELVE NO ESE USUARIO NO ESTA CONECTADO
                    {
                        int n = dataGridView2.Rows.Add();
                        dataGridView2.Rows[n].Cells[0].Value = username;
                        dataGridView1.Refresh();
                    }
                    i++;
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameBox_Click(object sender, EventArgs e)
        {
            UsernameBox.Text = ""; 
        }
    }
}

