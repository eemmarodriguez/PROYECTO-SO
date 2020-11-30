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
    public partial class Consultas : Form
    {
        bool cerrando = false;
        Login FormsLogin = new Login();
        Register FormsRegister = new Register();
        Socket server;
        bool disconecting = false; 
        string usuario;
        int id;
        bool conectado = false;
        int contador_servicios;
        string socket;
        int k = 0;
        Thread atender;

        delegate void DelegadoParaEscribirRegister(string mensaje, int n, string id);
        delegate void DelegadoParaEscribirConectados(string mensaje, int n);
        delegate void DelegadoParaRefrescarTablas(int n, int filas);
        
        public Consultas()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor
            //al que deseamos conectarnos
            InitializeComponent();


            //"147.83.117.22" Shiva
            //192.168.56.102 Maquina virtual
            IPAddress direc = IPAddress.Parse("147.83.117.22");//DireccionIP de la Maquina Virtual
            IPEndPoint ipep = new IPEndPoint(direc, 50020); //Le pasamos el acceso y el puerto que asignamos en el codigo del servidor

            
            while (k == 0)
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//Parámetros estándard
                try
                {
                    server.Connect(ipep); //Intentamos conectar el socket
                    //this.BackColor = Color.Green;
                    //SendButton.Enabled = true;
                    //MessageBox.Show("conectado");
                    k = 1;
                }
                catch (SocketException ex)
                {
                    //Si hay excepción imprimimos error y salimos del programa con return
                    MessageBox.Show("No se ha podido conectar con el servidor");
                    if (MessageBox.Show("Quiere reintentar la conexion?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        k = 0;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            //ponemos en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

            

            
            //CheckForIllegalCrossThreadCalls = false; 
            
        }
        
        private void Consultas_Load(object sender, EventArgs e)
        {
            if (k == 1)
            {
                FormsLogin.SetServer(server);
                FormsLogin.SetRegister(FormsRegister);
                FormsLogin.SetThread(atender);
                FormsRegister.SetServerRegister(server);
                FormsLogin.ShowDialog();

                string index = FormsLogin.GetId();
                string sock = FormsLogin.GetSocket();
                usuario = FormsLogin.GetUser();
                this.id = Convert.ToInt32(index);
                this.socket = sock;


                UsuarioLabel.Text = "User: " + this.usuario;
                IdLabel.Text = "ID: " + Convert.ToString(this.id);

                this.CenterToScreen();


                if (this.BackColor != Color.Green)
                    SendButton.Enabled = false;




                //NOS VAMOS A PONER COMO ONLINE


                int calculo = Convert.ToInt32(socket);
                calculo++;
                socket = Convert.ToString(calculo);
                enviar_server("7/" + sock + "/" + index);
                conectado = true;


            }
            else
                Application.Exit();

        }
        
        public void SetSocket(string t)
        {
            this.socket = t;
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
        public void PonData1(string user, int n, string id)
        {
            
            dataGridView1.Rows[n].Cells[0].Value = user;
            dataGridView1.Rows[n].Cells[1].Value = id;
            dataGridView1.Refresh();
        }
        public void PonData2(string user, int n)
        {
            
            dataGridView2.Rows[n].Cells[0].Value = user;
            dataGridView2.Refresh();
        }
        public void ClearRefresh (int n, int filas)
        {
            if (n == 1)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.RowCount = filas;
                dataGridView1.Refresh();

            }
            if (n == 2)
            {
                dataGridView2.Rows.Clear();
                dataGridView2.RowCount = filas;
                dataGridView2.Refresh();

            }

        }
        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos un vector de bytes y lo convertimos a string
                byte[] msg2 = new byte[512];
                server.Receive(msg2);
                if (msg2[0] != 0)
                {
                    string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                    int codigo = Convert.ToInt32(trozos[0]);
                    string mensaje;

                    switch (codigo)
                    {
                        case 1: //Vicotrias jugador concreto
                            mensaje = trozos[1].Split('\0')[0];
                            MessageBox.Show("El numero de partidas ganadas por el usuario son: " + mensaje);
                            break;
                        case 2: //Dia con mas partidas jugadas

                            mensaje = trozos[1].Split('\0')[0];
                            MessageBox.Show("El dia que se han jugado mas partidas es: " + mensaje);
                            break;
                        case 3:
                            // RESETEAMOS LA GRID PARA LOS DATOS
                            string index2 = trozos[1].Split('/')[0];
                            

                            //CONSULTAMOS AL SERVIDOR SOBRE EL NUMERO DE JUGADORES
                            //PREGUNTANDO EL ID MÁS GRANDE

                            int i = 2;
                            int total2 = Convert.ToInt32(index2);

                            DelegadoParaRefrescarTablas del = new DelegadoParaRefrescarTablas(ClearRefresh);
                            dataGridView1.Invoke(del, new object[] { 1, total2 });
                            int n = -1;
                            while (i <= total2 + 1)
                            {
                                string istr = Convert.ToString(i - 1);
                                string username = trozos[i].Split('/')[0];

                                if (i != 2)
                                {
                                    //IMPRIMIMOS LOS RESULTADOS EN LA GRID DEL FORMS
                                    
                                    DelegadoParaEscribirRegister delegado = new DelegadoParaEscribirRegister(PonData1);
                                    
                                    dataGridView1.Invoke(delegado, new object[] { username, n, istr });

                                }
                                n++;
                                i++;
                            }
                            
                            break;
                        case 4: //CONECTADOS
                            string index = trozos[1].Split('/')[0]; //4/IDMAX/Player1/Player2
                            

                            int total = Convert.ToInt32(index);

                            DelegadoParaRefrescarTablas del2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                            dataGridView2.Invoke(del2, new object[] {2, total});

                            i = 2;
                            n = -1;
                            while (i < total + 2)
                            {
                                string username = trozos[i].Split('/')[0];
                                if ((username != "NO") && (i != 2)) //SI NOS DEVUELVE NO ESE USUARIO NO ESTA CONECTADO
                                {
                                    
                                    DelegadoParaEscribirConectados delegado = new DelegadoParaEscribirConectados(PonData2);
                                    dataGridView2.Invoke(delegado, new object[] { username, n });
                                }
                                n++;
                                i++;
                            }
                           

                            break;
                        case 6: // LOGIN
                            FormsLogin.SetMensaje(trozos); 
                            break;
                        case 7: // REGISTER
                            FormsRegister.SetRegister(trozos);
                            break;
                    }
                }
            }
        }
        private void enviar_server(string mensaje)  //UNA FUNCION PARA SIMPLIFICAR EL ENVIAR DATOS AL SERVER
        {

            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

           
        }


        private void ConnectButton_Click(object sender, EventArgs e)  //NOS CONECTAMOS AL SERVIDOR 
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {

            if(GamesWonButton.Checked)
            {
                
                enviar_server("1/" + UsernameBox.Text);          
            }

            if (MoreGamesButton.Checked)
            {
                SendButton.Enabled = false;
                enviar_server("2/ALGO");

            }

            if (PlayersListButton.Checked)
            {
                SendButton.Enabled = false;
                // RESETEAMOS LA GRID PARA LOS DATOS

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                //CONSULTAMOS AL SERVIDOR SOBRE EL NUMERO DE JUGADORES
                //PREGUNTANDO EL ID MÁS GRANDE

                enviar_server("3/IDMAX");
            }
            

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            
            disconecting = true; 

            UsuarioLabel.Text = "";
            UsuarioLabel.Text = "";

            // NOS QUITAMOS DEL MODO ONLINE
            
            conectado = false;
            enviar_server("8/" + Convert.ToString(id));
      
            
            FormsLogin.ShowDialog();

            string index = FormsLogin.GetId();
            string sock = FormsLogin.GetSocket();
            usuario = FormsLogin.GetUser();
            this.id = Convert.ToInt32(index);
            this.socket = sock;

            UsuarioLabel.Text = "User: " + this.usuario;
            IdLabel.Text = "ID: " + Convert.ToString(this.id);

            enviar_server("7/" + socket + "/" + Convert.ToString(id));
            conectado = true;
           
        }

        private void Consultas_FormClosing_1(object sender, FormClosingEventArgs e)
        {

            if ((cerrando == false) && (k == 1))
            {
                enviar_server("8/" + Convert.ToString(id));
                //Nos desconectamos
                enviar_server("0/Desconectado");
               
                
                
                this.BackColor = Color.Gray;
                //Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                atender.Abort();
                MessageBox.Show("DESCONECTADO");
            }
            cerrando = true;         
            disconecting = true;     
            Application.Exit();
               
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //FUNCIONES PARA QUITAR Y PONER LA BARRA DE TEXTO Y LAS TABLAS DE DATOS
        private void GamesWonButton_CheckedChanged(object sender, EventArgs e)
        {
            UsernameBox.Enabled = true;
            UsernameBox.Text = "Write here the username";
            SendButton.Enabled = true;




        }

        private void MoreGamesButton_CheckedChanged(object sender, EventArgs e)
        {
            UsernameBox.Enabled = false;

            if (MoreGamesButton.Checked)
            {
                SendButton.Enabled = false;
                enviar_server("2/ALGO");

                
            }

        }

        private void PlayersListButton_CheckedChanged(object sender, EventArgs e)
        {

            UsernameBox.Enabled = false;
            

            if (PlayersListButton.Checked)
            {
                SendButton.Enabled = false;
                enviar_server("3/IDMAX");
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

        private void OnlineButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}

