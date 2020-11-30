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
        //string socket;
        int k = 0;
        Thread atender;
        string[] partida = new string[4];
        string[] listapartidas = new string[100];
        int partida_simultaneas = 0; 
        int num = 1;
        

        delegate void DelegadoParaEscribirRegister(string mensaje, int n, string id);
        delegate void DelegadoParaEscribirConectados(string mensaje, int n);
        delegate void DelegadoParaRefrescarTablas(int n, int filas);
        delegate void DelegadoParaEscribirPartida(string[] user, int n);
        delegate void DelegadoParaListaAnfitrion(string nombre);
        
        public Consultas()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor
            //al que deseamos conectarnos
            InitializeComponent();


            //"147.83.117.22" Shiva
            //50020
            //192.168.56.102 Maquina virtual
            IPAddress direc = IPAddress.Parse("147.83.117.22");//DireccionIP de la Maquina Virtual
            IPEndPoint ipep = new IPEndPoint(direc, 50022); //Le pasamos el acceso y el puerto que asignamos en el codigo del servidor

            
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
            dataGridView3.RowCount = 4;
            
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

                int desconectandologin = FormsLogin.GetDesconectando();
                if (desconectandologin == 1)
                {
                    string index = FormsLogin.GetId();
                    //string sock = FormsLogin.GetSocket();
                    usuario = FormsLogin.GetUser();
                    dataGridView3.Rows[0].Cells[0].Value = this.usuario; //metemos el usuario propio en la partida
                    this.id = Convert.ToInt32(index);
                    //this.socket = sock;


                    UsuarioLabel.Text = "User: " + this.usuario;
                    IdLabel.Text = "ID: " + Convert.ToString(this.id);

                    this.CenterToScreen();


                    if (this.BackColor != Color.Green)
                        SendButton.Enabled = false;




                    //NOS VAMOS A PONER COMO ONLINE


                    StatusLabel.Text = "Eres el anfitrion";
                    enviar_server("7/" + this.usuario + "/1");
                    conectado = true;

                }
                else
                {
                    atender.Abort();
                    cerrando = true;
                    Application.Exit();
                }
            }
            else
                Application.Exit();

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
        public void PonData3(string[] user, int n)
        {
            dataGridView3.Rows[n].Cells[0].Value = user[n + 3];
            dataGridView3.Enabled = false;
            JugarButton.Enabled = false;
            StatusLabel.Text = "Eres el invitado";
            InvitarButton.Enabled = false;
            dataGridView3.Refresh();
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
            if (n == 3)
            {
                dataGridView3.Rows.Clear();
                dataGridView3.RowCount = filas;
                dataGridView3.Rows[0].Cells[0].Value = this.usuario;
                dataGridView3.Enabled = true;
                JugarButton.Enabled = true;
                StatusLabel.Text = "Eres el anfitrion";
                InvitarButton.Enabled = true;
                dataGridView3.Refresh();
                
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
                        case 4:
                            string index = trozos[1].Split('/')[0]; //4/IDMAX/Player1/Player2


                            int total = Convert.ToInt32(index);

                            DelegadoParaRefrescarTablas del2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                            dataGridView2.Invoke(del2, new object[] { 2, total });

                            i = 2;
                            n = 0;
                            while (i < total + 2)
                            {
                                string username = trozos[i].Split('/')[0];
                                if ((username != "NO")) //SI NOS DEVUELVE NO ESE USUARIO NO ESTA CONECTADO
                                {

                                    DelegadoParaEscribirConectados delegado = new DelegadoParaEscribirConectados(PonData2);
                                    dataGridView2.Invoke(delegado, new object[] { username, n });
                                }
                                n++;
                                i++;
                            }


                            break;
                        case 6:
                            FormsLogin.SetMensaje(trozos);
                            break;
                        case 7:
                            FormsRegister.SetRegister(trozos);
                            break;
                        case 8:
                            if (MessageBox.Show("Te ha invitado " + trozos[1] + " a jugar", " ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                listapartidas[partida_simultaneas] = trozos[2];//trozos[2] nos manda 
                                //partida_simultaneas++;
                                
                                enviar_server("10/Si/"+ trozos[2] + "/"+ this.usuario);
                            }
                            else
                                enviar_server("10/No/"+ trozos[2] + "/"+ this.usuario);
                            break;
                        case 9:
                            if (trozos[1] == "No")//emisor invitacion (el usuario)
                            {
                                string message = trozos[2];
                                string nombre = message;
                                message = message + " no ha aceptado la invitacion";
                                MessageBox.Show(message );

                                DelegadoParaListaAnfitrion delegacion = new DelegadoParaListaAnfitrion(CambiarListaAnfitrion);
                                dataGridView3.Invoke(delegacion, new object[] { nombre });
                                
                            }
                            if (trozos[1] == "Si")//emisor invitacion (el usuario)
                            {
                                
                                MessageBox.Show(trozos[2] + " ha aceptado la invitacion");
                                listapartidas[partida_simultaneas] = trozos[3];//trozos[2] nos manda

                            }
                            if (trozos[1] == "llena") //es el receptor (el usuario)
                            {
                                int filas = Convert.ToInt32(trozos[2]);
                                DelegadoParaRefrescarTablas delegado2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                dataGridView3.Invoke(delegado2, new object[] { 3, 4});
                                for (int j=0; j<filas; j++)
                                {
                                    DelegadoParaEscribirPartida delegado = new DelegadoParaEscribirPartida(PonData3);
                                    dataGridView3.Invoke(delegado, new object[] { trozos, j});
                                   
                                }


                            }
                            if (trozos[1] == "Mevoy")
                            {
                                MessageBox.Show("El anfitrion te ha expulsado");
                                DelegadoParaRefrescarTablas delegado2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                dataGridView3.Invoke(delegado2, new object[] { 3, 4 });
                                
                            }
                            break;
                        case 10:

                            MessageBox.Show("Empieza la partida");
                            break;
                        case 11:
                            FormsLogin.SetExiste(trozos[1]);
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
            enviar_server("7/" + this.usuario+"/0");//NOS DESCONECTAMOS FLAG = 0
      
            
            FormsLogin.ShowDialog();

            string index = FormsLogin.GetId();
            string sock = FormsLogin.GetSocket();
            usuario = FormsLogin.GetUser();
            this.id = Convert.ToInt32(index);
            
            dataGridView3.Rows[0].Cells[0].Value = this.usuario;
            dataGridView3.RowCount = 4;

            UsuarioLabel.Text = "User: " + this.usuario;
            IdLabel.Text = "ID: " + Convert.ToString(this.id);

            enviar_server("7/" + this.usuario +"/1");//NOS CONECTAMOS FLAG = 1
            conectado = true;
           
        }

        private void Consultas_FormClosing_1(object sender, FormClosingEventArgs e)
        {

            if ((cerrando == false) && (k == 1))
            {
                enviar_server("7/" + this.usuario+"/0"); //NOS DESCONECTAMOS flag = 0
                //Nos desconectamos
                Thread.Sleep(200);
                atender.Abort();
                enviar_server("0/Desconectado");


                MessageBox.Show("DESCONECTADO");
                this.BackColor = Color.Gray;
                //Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                
                
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

       

        private void InvitarButton_Click(object sender, EventArgs e)
        {
            string mensaje = "9/";
            mensaje = mensaje + Convert.ToString(num) + "/" + this.usuario + "/";
            int j;
            for(j=0; j<4;j++)
            {
                if (partida[j] != null)
                {
                    mensaje = mensaje + partida[j] + "/";
                }
            }
            if (partida_simultaneas == 0)
            {
                listapartidas[partida_simultaneas] = Convert.ToString(partida_simultaneas);
               
            }
            if (partida_simultaneas != 0)
            {

            }
            //MessageBox.Show(mensaje);
            StatusLabel.Text = "Eres el anfitrion";
            enviar_server(mensaje);
            

        }

    

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string nombre = dataGridView2.CurrentCell.Value.ToString();

            if (MessageBox.Show("Quieres jugar con: " + nombre, " ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (nombre == this.usuario)
                {

                    MessageBox.Show("No puedes invitarte a ti mismo, ya estas en la partida");
                }
                else
                {
                    if ((num == 4) && (nombre != this.usuario))
                    {
                        MessageBox.Show("La partida esta llena");
                    }
                    else
                    {
                        
                        dataGridView3.Rows[num].Cells[0].Value = nombre;
                        dataGridView3.Refresh();
                        partida[num] = nombre;
                        num++;
                    }

                }
                

            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView3.CurrentCell.Value != null)
            {


                string nombre = dataGridView3.CurrentCell.Value.ToString();
                if (MessageBox.Show("¿Seguro que quieres borrar a  " + nombre + " de la partida?", " ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int indice = dataGridView3.CurrentCell.RowIndex;
                    if (partida_simultaneas==0)
                    {
                        enviar_server("10/Quitame/"+Convert.ToString(listapartidas[0])+"/"+nombre);
                    }
                    if (partida_simultaneas > 0)
                    {

                    }

                    //dataGridView3.Rows[indice].Cells[0].Value="";
                    CambiarListaAnfitrion(nombre);
                    
                }
            }
            
                
            
        }
        private void CambiarListaAnfitrion (string nombre)
        {
            int j = 0;
            while (j < num)
            {
                if (partida[j] == nombre)
                {
                    if (j == 0)
                    {
                        dataGridView3.Rows[0].Cells[0].Value = partida[1];
                        partida[0] = partida[1];
                        dataGridView3.Rows[1].Cells[0].Value = partida[2];
                        partida[1] = partida[2];
                        dataGridView3.Rows[2].Cells[0].Value = partida[3];
                        partida[2] = partida[3];
                        dataGridView3.Rows[3].Cells[0].Value = "";
                        partida[3] = null;
                    }
                    if (j == 1)
                    {
                        dataGridView3.Rows[1].Cells[0].Value = partida[2];
                        partida[1] = partida[2];
                        dataGridView3.Rows[2].Cells[0].Value = partida[3];
                        partida[2] = partida[3];
                        dataGridView3.Rows[3].Cells[0].Value = "";
                        partida[3] = null;
                    }
                    if (j == 2)
                    {
                        dataGridView3.Rows[2].Cells[0].Value = partida[3];
                        partida[2] = partida[3];
                        dataGridView3.Rows[3].Cells[0].Value = "";
                        partida[3] = null;
                    }
                    if (j == 3)
                    {
                        dataGridView3.Rows[3].Cells[0].Value = "";
                        partida[3] = null;
                    }

                }
                j++;
            }
            num = num - 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            enviar_server("11/" + Convert.ToString(listapartidas[0]));
            //if (partida_simultaneas == 0)
            //{

            //}
            //else
            //{
            //    listapartidas[partida_simultaneas] = Convert.ToString(partida_simultaneas);
            //    partida_simultaneas++;
            //}
        }
    }
}

