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
using System.Media;

namespace Version1
{
    public partial class Consultas : Form
    {
        bool cerrando = false;  //Variable para cerrar el programa
        Login FormsLogin = new Login();  //Creamos el foms Login
        Register FormsRegister = new Register(); //Creamos el formulario Register 
        Juego[] JuegosArray = new Juego[100];
        Socket server;
        bool disconecting = false;
        string usuario;
        int numjugadores;
        int id;
        bool conectado = false;
        int contador_servicios;
        //string socket;
        int k = 0;
        Thread atender;
        Thread juego;
        string[] partida = new string[4];
        string[] turnos = new string[4];
        int indicepartida;
        int[] listapartidas = new int[100];
        int partida_simultaneas = 0;
        int num = 1;
        int primerainvitacion = 0;
        int anfitrion;
        bool saliendo = false;
        int entroGrid3 = 0;
        int jugando = 0;
        string[] enviarpartidas = new string[4];
        string mchat;
        bool messageactivo = false;
        bool desconectado = false;
        bool formandopartida = false;
        bool cerrarsesion = false;

        //TODOS LOS DELEGADOS
        delegate void DelegadoParaEscribirRegister(string mensaje, int n, string id);
        delegate void DelegadoParaEscribirConectados(string mensaje, int n);
        delegate void DelegadoParaRefrescarTablas(int n, int filas);
        delegate void DelegadoParaEscribirPartida(string[] user, int n, int a);
        delegate void DelegadoParaListaAnfitrion(string nombre);
        delegate void DelegadoParaJuegoEnabled(bool Enabled);

        public Consultas()
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor
            //al que deseamos conectarnos
            InitializeComponent();

            //MessageBox.Show($"Current directory is '{Environment.CurrentDirectory}'");
            //"147.83.117.22" Shiva
            //50020
            //192.168.56.102 Maquina virtual
            //192.168.56.103 Maquina virtual David
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

                ///
                /// LLENAMOS EL FORMULARIO DE LOGIN
                ///
                FormsLogin.SetServer(server);
                FormsLogin.SetRegister(FormsRegister);
                FormsLogin.SetThread(atender);
                FormsRegister.SetServerRegister(server);
                FormsLogin.ShowDialog();

                int desconectandologin = FormsLogin.GetDesconectando();
                if (desconectandologin == 1) //Condicion para poder cerrar en caso de no querer hacer Log In y salir de la aplicación 
                {
                    string index = FormsLogin.GetId(); //recogemos el id del usuario loggeado 

                    usuario = FormsLogin.GetUser();
                    dataGridView3.Rows[0].Cells[0].Value = this.usuario; //metemos el usuario propio en la partida
                    this.id = Convert.ToInt32(index);

                    saliendo = false; //boolean para que no salga que el anfitrion te expulsa.

                    UsuarioLabel.Text = "User: " + this.usuario;
                    IdLabel.Text = "ID: " + Convert.ToString(this.id);

                    this.CenterToScreen();
                    this.MaximizeBox = false;

                    //NOS VAMOS A PONER COMO ONLINE


                    StatusLabel.Text = "Eres el anfitrion";
                    anfitrion = 1;
                    enviar_server("7/" + this.usuario + "/1");
                    // this.BackColor = Color.Green; 
                    conectado = true;
                }
                else
                {

                    //Abortamos la conexion, el usuario ha decidido cerrar la aplicacion sin hacer LogIn

                    atender.Abort();
                    cerrando = true;
                    enviar_server("0/Deconectamos");
                    Thread.Sleep(100);
                    server.Shutdown(SocketShutdown.Both);
                    server.Close();
                    Application.Exit();
                }
            }
            else
                Application.Exit();

        }
        private void enviar_server(string mensaje)  //UNA FUNCION PARA SIMPLIFICAR EL ENVIAR DATOS AL SERVER
        { 
            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

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
        public void SetChat(string mchat)
        {
            this.mchat = mchat;
        }

        //
        // FUNCIONES DEL DELEGATE DE TABLAS
        //
        
        public void PonData1(string user, int n, string id)  //PONDREMOS DATOS A LA TABLA DE REGISTRADOS
        {
            
            dataGridView1.Rows[n].Cells[0].Value = user;
            dataGridView1.Rows[n].Cells[1].Value = id;
            dataGridView1.Refresh();
        }
        public void PonData2(string user, int n) //PONEMOS DATOS EN CONECTADOS
        {
            
            dataGridView2.Rows[n].Cells[0].Value = user;
            dataGridView2.Refresh();
        }
        public void PonData3(string[] user, int n, int a) //PONEMOS DATOS EN LA PARTIDA
        {
            
            if (a == 0)
            {
                dataGridView3.Rows[n].Cells[0].Value = user[n + 3];
                dataGridView3.Enabled = false;
                JugarButton.Enabled = false;
                StatusLabel.Text = "Eres el invitado";
                anfitrion = 0;
            }
            if (a == 1)
            {
                dataGridView3.Rows[n].Cells[0].Value = user[n + 4];
            }
            //InvitarButton.Enabled = false;
            dataGridView3.Refresh();
        }
        
        public void ClearRefresh (int n, int filas)  //LIMPIAMOS LAS TABLAS
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
               // InvitarButton.Enabled = true;
                dataGridView3.Refresh();
                
            }

        }
        private void PonJugarEnabled(bool Enabled)
        {
            JugarButton.Enabled = Enabled;
            dataGridView2.Enabled = Enabled;
        }


        public void AtenderServidor()
        {
            if (desconectado == false)
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
                            case 4: //LLENAMOS LA TABLA DE CONECTADOS
                                string index = trozos[1].Split('/')[0]; //4/IDMAX/Player1/Player2


                                int total = Convert.ToInt32(index) - 1;
                                if (total > 0)
                                {
                                    DelegadoParaRefrescarTablas del2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                    dataGridView2.Invoke(del2, new object[] { 2, total });
                                }

                                if(total == 0)
                                {
                                    DelegadoParaRefrescarTablas del2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                    dataGridView2.Invoke(del2, new object[] { 2, 1 });
                                }

                                i = 2;
                                n = 0;
                                while (i < total + 3)
                                {
                                    string username = trozos[i].Split('/')[0];
                                    if ((username != "NO") & (username != this.usuario)) //SI NOS DEVUELVE NO ESE USUARIO NO ESTA CONECTADO
                                    {

                                        DelegadoParaEscribirConectados delegado = new DelegadoParaEscribirConectados(PonData2);
                                        dataGridView2.Invoke(delegado, new object[] { username, n });
                                    }
                                    if (username != this.usuario)
                                        n++;
                                    i++;
                                }


                                break;
                            case 6: //CONSULTA DEL LOG IN
                                FormsLogin.SetMensaje(trozos);
                                break;
                            case 7: //Consulta del Register
                                FormsRegister.SetRegister(trozos);
                                break;
                            case 8: //NOS LLEGA UNA INVITACION

                                //PONEMOS UN SONIDO DE RECIBIDO
                                SoundPlayer Player2 = new SoundPlayer();
                                Player2.SoundLocation = "sounds/invitacion.wav";
                                Player2.Play();

                                while (messageactivo == true)
                                {//sI HAY ALGUN MESSAGE BOX ABIERTO HACEMOS UN BUCLE HASTA QUE ESTA VARIABLE GENERAL SE ACABE 
                                }
                                
                                messageactivo = true;


                                if (MessageBox.Show("Te ha invitado " + trozos[1] + " a jugar", " ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    //ACEPTAMOS LA SOLICITUD

                                    indicepartida = Convert.ToInt32(trozos[2]);//trozos[2] nos manda                                            
                                    messageactivo = false;
                                    enviar_server("10/Si/" + trozos[2] + "/" + this.usuario);
                                }
                                else
                                {
                                    //RECHAZAMOS LA SOLICITUD
                                    enviar_server("10/No/" + trozos[2] + "/" + this.usuario);
                                    messageactivo = false;
                                }

                                break;
                            case 9: //DIFERENTES OPCIONES DE CONSTITUCIÓN DE PARTIDA
                                if (trozos[1] == "No")//emisor invitacion (el usuario)
                                {
                                    //SE HA RECHAZADO LA INVITACIÓN DE PARTIDA 
                                    string message = trozos[2];
                                    string nombre = message;
                                    message = message + " no ha aceptado la invitacion";
                                    while (messageactivo == true)
                                    {

                                    }
                                    messageactivo = true;

                                    MessageBox.Show(message);
                                    messageactivo = false;
                                    DelegadoParaJuegoEnabled delegacionJuego = new DelegadoParaJuegoEnabled(PonJugarEnabled);
                                    JugarButton.Invoke(delegacionJuego, new object[] { true });
                                    //JugarButton.Enabled = true;

                                    //VOLVEMOS A QUITAR AL USUARIO DE LA LISTA DE LA PARTIDA
                                    DelegadoParaListaAnfitrion delegacion = new DelegadoParaListaAnfitrion(CambiarListaAnfitrion);
                                    dataGridView3.Invoke(delegacion, new object[] { nombre });

                                }
                                if (trozos[1] == "Si")//emisor invitacion (el usuario)
                                {
                                    //SE ACEPTA LA INVITACION A LA PARTIDA
                                    while (messageactivo == true)
                                    {

                                    }
                                    messageactivo = true;

                                    MessageBox.Show(trozos[2] + " ha aceptado la invitacion");

                                    //YA PODEMOS DARLE A JUGAR PORQUE TENEMOS UN INVITADO 

                                    DelegadoParaJuegoEnabled delegacionJuego = new DelegadoParaJuegoEnabled(PonJugarEnabled);
                                    JugarButton.Invoke(delegacionJuego, new object[] { true });
                                    
                                    messageactivo = false;                                   

                                }
                                if (trozos[1] == "llena") //es el receptor (el usuario)
                                {
                                    //CUANDO UN USUARIO SE HA UNIDO A UNA PARTIDA SE LLENA LA LISTA DE LA PARTIDA Y SE PUEDE JUGAR

                                    int filas = Convert.ToInt32(trozos[2]);
                                    DelegadoParaRefrescarTablas delegado2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                    dataGridView3.Invoke(delegado2, new object[] { 3, 4 });
                                    for (int j = 0; j < filas; j++)
                                    {
                                        DelegadoParaEscribirPartida delegado = new DelegadoParaEscribirPartida(PonData3);
                                        dataGridView3.Invoke(delegado, new object[] { trozos, j, 0 });

                                    }


                                }
                                if (trozos[1] == "anfi")
                                {
                                    //LLENAR LA TABLA EN EL CASO DE SER EL ANFITRION

                                    int filas = Convert.ToInt32(trozos[3]);
                                    DelegadoParaRefrescarTablas delegado2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                    dataGridView3.Invoke(delegado2, new object[] { 3, 4 });
                                    for (int j = 0; j < filas; j++)
                                    {
                                        DelegadoParaEscribirPartida delegado = new DelegadoParaEscribirPartida(PonData3);
                                        dataGridView3.Invoke(delegado, new object[] { trozos, j, Convert.ToInt32(trozos[2]) });

                                    }
                                }
                                if (trozos[1] == "Mevoy")
                                {
                                    //CUANDO UN USUARIO SALE DE LA PARTIDA PORQUE EL ANFITRION LO HA EXPULSADO
                                    if (saliendo == false)
                                    {
                                        while (messageactivo == true)
                                        {

                                        }
                                        messageactivo = true;
                                        MessageBox.Show("El anfitrion te ha expulsado");
                                        messageactivo = false;
                                        DelegadoParaRefrescarTablas delegado2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                        dataGridView3.Invoke(delegado2, new object[] { 3, 4 });
                                        anfitrion = 1;
                                    }

                                }
                                if (trozos[1] == "indicepartida") //9/indicepartida/3
                                {                            
                                    indicepartida = Convert.ToInt32(trozos[2]);
                                }
                                if (trozos[1] == "Eliminado")
                                {
                                    if (entroGrid3 == 0)
                                    {
                                        CambiarListaAnfitrion(trozos[2]);
                                    }
                                    entroGrid3 = 0;
                                }
                                if (trozos[1] == "anfitrionfuera")
                                {
                                    while (messageactivo == true)
                                    {

                                    }
                                    messageactivo = true;
                                    MessageBox.Show("El anfitrion se ha desconectado");
                                    messageactivo = false;
                                    DelegadoParaRefrescarTablas delegado2 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                    dataGridView3.Invoke(delegado2, new object[] { 3, 4 });
                                    anfitrion = 1;
                                }
                                break;
                            case 10:

                                //SE INICIA EL JUEGO 

                                listapartidas[partida_simultaneas] = indicepartida;
                                partida_simultaneas++;
                                num = 1;
                                jugando = 1;
                                primerainvitacion = 0;
                                DelegadoParaRefrescarTablas delegado3 = new DelegadoParaRefrescarTablas(ClearRefresh);
                                dataGridView3.Invoke(delegado3, new object[] { 3, 4 });
                                numjugadores = Convert.ToInt32(trozos[1]);
                                for (int z = 0; z < numjugadores; z++)
                                {
                                    enviarpartidas[z] = trozos[z + 2];
                                }
                                
                                primerainvitacion = 0;
                                formandopartida = false;

                                IniciarJuego();

                                break;
                            case 11:
                                FormsLogin.SetExiste(trozos[1]);
                                break;

                            case 12: //Escribimos los mensajes en el chat
                                int indice = 0;
                                bool encontrado = false;

                                for (int j = 0; j < partida_simultaneas; j++)
                                {
                                    if ((JuegosArray[j].GetIndice() == Convert.ToInt32(trozos[1])) && (encontrado == false))
                                    {
                                        //Buscamos en que indice hemos guardado la partida, para así encontrar el forms, ya que tienen el mismo indice
                                        indice = j;
                                        encontrado = true;
                                    }
                                }
                                JuegosArray[indice].SetMensajes(trozos[2]);


                                break;
                            case 13:
                                indice = encontrarform(trozos[1]);
                                //ENVIAMOS LA PREGUNTA QUE RECIBIMOS AL JUEGO
                                JuegosArray[indice].SetPregunta(trozos[2], trozos[3], trozos[4], trozos[5], trozos[6], trozos[7], trozos[8]);
                                break;
                            case 14:

                                //CAMBIO DE TURNO
                                indice = encontrarform(trozos[1]);
                                SoundPlayer Player = new SoundPlayer();
                                Player.SoundLocation = "sounds/SD_ALERT_3.wav";

                                Player.Play();
                                JuegosArray[indice].SetEnabledCategoria();

                                break;
                            case 15:
                                //PONEMOS LA PUNTUACION

                                indice = encontrarform(trozos[1]);
                                JuegosArray[indice].SetPuntuacion(trozos[2], Convert.ToInt32(trozos[3]));
                                break;
                            case 16:
                                // ENVIAMOS EL GANADOR DE LA PARTIDA 

                                indice = encontrarform(trozos[1]);
                                JuegosArray[indice].SetGanador(trozos[2]);
                                break;
                            case 17:

                                indice = encontrarform(trozos[2]);
                                if (trozos[1] == "Ganas")
                                {
                                    enviar_server("16/" + trozos[2] + "/" + trozos[3] + "/GANADOR");
                                    //Thread.Sleep(300);
                                    //JuegosArray[indice].SetGanador(trozos[3]);
                                }
                                if (trozos[1] == "Eliminado")
                                {
                                    JuegosArray[indice].QuitarJugador(trozos[3]);
                                }
                                break;
                            case 18:
                                // 18/indice/CATEGORIA/jugador
                                //PONEMOS LAS CATEGORIAS GANADAS EN FORMA DE MUÑECOS
                                indice = encontrarform(trozos[1]);
                                JuegosArray[indice].SetGanadorMuñeco(trozos[2], trozos[3]);
                                break;

                        }
                    }
                }
            }
        }

        private int encontrarform (string id)
        {
            //FUNCION PARA PODER ENCONTRAR EL INDICE DEL JUEGO 
            int indice = 0;
            bool encontrado = false;

            for (int j = 0; j < partida_simultaneas; j++)
            {
                if ((JuegosArray[j].GetIndice() == Convert.ToInt32(id)) && (encontrado == false))
                {
                    //Buscamos en que indice hemos guardado la partida, para así encontrar el forms, ya que tienen el mismo indice
                    indice = j;
                    encontrado = true;
                }
            }
            return indice;
        }

        //
        //FUNCIONES DEL APARTADO ESTADISTICAS
        //
        private void SendButton_Click(object sender, EventArgs e)
        {

            if(GamesWonButton.Checked)
            {
                //CONSULTAR PARTIDAS GANADAS POR JUGADOR
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
            //DIA QUE MAS PARTIDAS SE HAN JUGADO 
            if (MoreGamesButton.Checked)
            {
                SendButton.Enabled = false;
                enviar_server("2/ALGO");   
            }

        }

        private void PlayersListButton_CheckedChanged(object sender, EventArgs e)
        {

            UsernameBox.Enabled = false;
            //FUNCION PARA VER TODOS LOS USUARIOS REGISTRADOS

            if (PlayersListButton.Checked)
            {
                SendButton.Enabled = false;
                enviar_server("3/IDMAX");
            }

        }

        private void UsernameBox_Click(object sender, EventArgs e)
        {
            //CONSEGUIMOS QUITAR EL TEXTO DEL TEXTBOX CUANDO VAYAMOS A ESCRIBIR 
            UsernameBox.Text = ""; 
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            //FUNCION PARA BORRAR LA CUENTA QUE ESTA INCIADA 

            if (MessageBox.Show("¿Estás seguro que quieres eliminar tu cuenta?", " ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                enviar_server("20/" + this.usuario);

                disconecting = true;
                saliendo = true;

                UsuarioLabel.Text = "";
                UsuarioLabel.Text = "";

                // NOS QUITAMOS DEL MODO ONLINE


                if (anfitrion == 1)
                {
                    for (int j = 0; j <= partida_simultaneas; j++)
                        if (formandopartida)
                            enviar_server("10/desconectando/" + Convert.ToString(listapartidas[j]));
                }
                if (anfitrion == 0)
                {
                    for (int j = 0; j <= partida_simultaneas; j++)
                        if (formandopartida)
                            enviar_server("10/Quitame/" + Convert.ToString(listapartidas[j]) + "/" + this.usuario);
                }
                Thread.Sleep(500);
                conectado = false;
                enviar_server("7/" + this.usuario + "/0");//NOS DESCONECTAMOS FLAG = 0

                FormsLogin.SetLogin(false);
                FormsLogin.ShowDialog();
                bool log = FormsLogin.GetLogin();
                if (log)
                {

                    string index = FormsLogin.GetId();
                    string sock = FormsLogin.GetSocket();
                    usuario = FormsLogin.GetUser();
                    this.id = Convert.ToInt32(index);
                    dataGridView3.Rows.Clear();
                    dataGridView3.RowCount = 4;
                    dataGridView3.Rows[0].Cells[0].Value = this.usuario;
                    JugarButton.Enabled = true;
                    dataGridView3.Enabled = true;
                    StatusLabel.Text = "Eres el anfitrion";



                    UsuarioLabel.Text = "User: " + this.usuario;
                    IdLabel.Text = "ID: " + Convert.ToString(this.id);

                    enviar_server("7/" + this.usuario + "/1");//NOS CONECTAMOS FLAG = 1
                    conectado = true;
                    cerrarsesion = false;
                    saliendo = false;

                }
                else
                {
                    conectado = false;
                    cerrarsesion = true;
                    this.Close();


                }
            }
            else
            {
            }
        }



        //
        // FUNCIONES JUGAR
        //
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // DATAGRID DE JUGADORES ONLINE
            // CON UN CLICK SELECCIONAREMOS EL JUGADOR CON QUIEN QUERRAMOS JUGAR 
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
                        //SI LA PARTIDA YA SON 4 NO PUEDES UNIR A NADIE MAS
                        MessageBox.Show("La partida esta llena");
                    }
                    else
                    {
                        //SI HAY HUECO EN LA PARTIDA INCLUIREMOS EL NOMBRE EN UN VECTOR E INFORMACIONES QUE MANDAREMOS AL FORMULARIO JUEGO 
                        formandopartida = true;
                        partida[num] = nombre;
                        num++;
                        string mensaje = "9/";
                       
                        if (primerainvitacion == 0)
                        {
                            //SI ES LA PRIMERA INVITACION NO TENEMOS ASIGNADO TODAVIA UN INDICE DE PARTIDA
                            //UNA VEZ MANDADO EL MENSAJE, EL SERVIDOR NOS INFORMARA DE LA PARTIDA QUE PODEMOS OCUPAR 
                            primerainvitacion = 1;
                            mensaje = mensaje + "primera/" + "2" + "/" + this.usuario + "/"+nombre; //Ponemos un dos porque siempre mandaremos 2 usuarios
                        }
                        else
                        {
                            //UNA VEZ TENEMOS EL INDICE DE PARTIDA YA SOLO NOS QUEDA MANDAR SIEMPRE A ESE ID 
                            mensaje = mensaje + Convert.ToString(indicepartida) +"/"+ "2" + "/" + this.usuario + "/"+ nombre;
                        }
                        

                        //MessageBox.Show(mensaje);
                        StatusLabel.Text = "Eres el anfitrion";
                        anfitrion = 1;
                        JugarButton.Enabled = false;
                        dataGridView2.Enabled = false;
                        enviar_server(mensaje);

                        //dataGridView3.Rows[num-1].Cells[0].Value = nombre;
                        //dataGridView3.Refresh();
                        

                    }

                }
                

            }
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //FUNCION PARA EXPULSAR A UN USUARIO DE LA PARTIDA MIENTRAS SE ESTÁ CONTITUYENDO 
            if (dataGridView3.CurrentCell.Value != null)
            {
                string nombre = dataGridView3.CurrentCell.Value.ToString();
                if (MessageBox.Show("¿Seguro que quieres borrar a  " + nombre + " de la partida?", " ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int indice = dataGridView3.CurrentCell.RowIndex;
                    entroGrid3 = 1;
                    enviar_server("10/Quitame/"+Convert.ToString(indicepartida)+"/"+nombre);
                    CambiarListaAnfitrion(nombre);
                    
                }
            }    
        }

        
        private void IniciarJuego()
        {

            //FUNCION QUE LLAMAMOS EN ATENDER SERVIDOR, LA UTILIZAMOS PARA INCIALIZAR EL JUEGO 

            ThreadStart ts = delegate { AtenderServidor(); };
            juego = new Thread(ts);
            juego.Start();
            Juego FormsJuego = new Juego();
            FormsJuego.SetJugadores(numjugadores, enviarpartidas);
            FormsJuego.SetId(this.id);
            FormsJuego.SetPartida(indicepartida);
            FormsJuego.SetThread(juego);
            FormsJuego.SetServer(server);
            FormsJuego.SetAnfitrion(this.anfitrion);
            FormsJuego.SetUsuario(this.usuario);
            FormsJuego.SetUsersPartida(partida);
            string siguienteturno;

            for (int z = 0; z < num; z++)
            {
                //ESTABLECEMOS LOS TURNOS 
                if (partida[z] == this.usuario)
                {
                    if (z == num)
                    {
                        siguienteturno = partida[0];
                        FormsJuego.SetSetSiguienteTurno(siguienteturno);
                    }
                    else
                    {
                        siguienteturno = partida[z + 1];
                        FormsJuego.SetSetSiguienteTurno(siguienteturno);
                    }
                }
            }

            partida = new string[4];
            anfitrion = 1;

            //INTRODUCIMOS EL FORMS QUE HEMOS CREADO EN UN VECTOR PARA PODER MODIFICARLO DE FORMA GENERAL
            JuegosArray[partida_simultaneas - 1] = FormsJuego;
            JuegosArray[partida_simultaneas - 1].ShowDialog();
            juego.Abort();
            int indicevaciar = JuegosArray[partida_simultaneas - 1].GetIndicePartida();

            //ENVIAMOS ESTE MENSAJE DE CONTROL YA QUE TENIAMOS UN BUG DE QUE HASTA QUE NO SE MANDABA UN MENSAJE AL SERVIDOR
            //NO SE RESTABLECIA LA CONEXION, LO SOLUCIONAMOS MANDANDON ESTE MENSAJE DE CONTROL
            enviar_server("99/controlsalidajuego");

            bool jugando = JuegosArray[partida_simultaneas - 1].GetJugando();
           
            //SI LA PARTIDA SE HA ACABADO LA VACIAMOS EN EL SERVIDOR 
            if (jugando == false)
                enviar_server("19/" + Convert.ToString(indicevaciar));

        }


        private void CambiarListaAnfitrion (string nombre)
        {
            //CUANDO HAY CAMBIOS EN LA PARTIDA TENEMOS QUE CAMBIAR EL ORDEN DE LOS USUARIOS 
            //POR LO TANTO HEMOS CREADO ESTA FUNCION PARA QUE LOS USUARIOS SIEMPRE ESTEN
            //EN LAS PRIMERAS POSICIONES DEL VECTOR Y NO HAYAN HUECOS VACIOS. 
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
        private void button1_Click(object sender, EventArgs e) //boton jugar
        {
            //EL ANFITRION DECIDE DARLE A JUGAR Y SE ENVIA AL SERVIDOR LA PARTIDA QUE SE INCIA
            listapartidas[partida_simultaneas] = indicepartida;
            Thread.Sleep(200);
           
            enviar_server("11/" + Convert.ToString(listapartidas[partida_simultaneas]));
                 
        }
       
        private void Instrucciones_Click(object sender, EventArgs e)
        {
            //BOTON DE COMO JUGAR
            Como_Jugar forms = new Como_Jugar();
            forms.ShowDialog();
        }

       
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            //CERRAREMOS LA SESION DEL USUARIO
            //ABRIREMOS UN FORMULARIO DE INICIO DE SESION POR SI QUIERE VOLVER A INGRESAR

            disconecting = true;
            saliendo = true;

            UsuarioLabel.Text = "";
            UsuarioLabel.Text = "";

            // NOS QUITAMOS DEL MODO ONLINE

            saliendo = true;
            if (anfitrion == 1)
            {
                for (int j = 0; j <= partida_simultaneas; j++)
                    if (formandopartida)
                        enviar_server("10/desconectando/" + Convert.ToString(listapartidas[j]));
            }
            if (anfitrion == 0)
            {
                for (int j = 0; j <= partida_simultaneas; j++)
                    if (formandopartida)
                        enviar_server("10/Quitame/" + Convert.ToString(listapartidas[j]) + "/" + this.usuario);
            }
            Thread.Sleep(500);
            conectado = false;
            enviar_server("7/" + this.usuario + "/0");//NOS DESCONECTAMOS FLAG = 0

            FormsLogin.SetLogin(false);
            FormsLogin.ShowDialog();
            bool log = FormsLogin.GetLogin();
            if (log)
            {

                string index = FormsLogin.GetId();
                string sock = FormsLogin.GetSocket();
                usuario = FormsLogin.GetUser();
                this.id = Convert.ToInt32(index);
                dataGridView3.Rows.Clear();
                dataGridView3.RowCount = 4;
                dataGridView3.Rows[0].Cells[0].Value = this.usuario;
                JugarButton.Enabled = true;
                dataGridView3.Enabled = true;
                StatusLabel.Text = "Eres el anfitrion";



                UsuarioLabel.Text = "User: " + this.usuario;
                IdLabel.Text = "ID: " + Convert.ToString(this.id);

                enviar_server("7/" + this.usuario + "/1");//NOS CONECTAMOS FLAG = 1
                conectado = true;
                cerrarsesion = false;
                saliendo = false;
            }
            else
            {
                conectado = false;
                cerrarsesion = true;
                this.Close();


            }

        }

        private void Consultas_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //SE CIERRA EL PROGRAMA 
            //PROTOCOLOS PARA UN BUEN CIERRE DEL SERVIDOR
            //DEPENDERA DE DONDE VENGAMOS, DEL TIPO DE CIERRE
            //SI SOMOS ANFITRIONES O SI SOMOS INVITADOS EN EL MOMENTO
            //TAMBIEN SI ESTAMOS JUGANDO UNA PARTIDA O NO. 

            if ((cerrando == false) && (k == 1))
            {
                saliendo = true;
                if (anfitrion == 1)
                {
                    if (jugando == 1)
                    {
                        for (int j = 0; j < partida_simultaneas; j++)
                        {
                            if (formandopartida)
                                enviar_server("10/desconectando/" + Convert.ToString(listapartidas[j]));
                        }
                        Thread.Sleep(300);
                    }
                    else
                    {
                        for (int j = 0; j <= partida_simultaneas; j++)
                            if (formandopartida)
                                enviar_server("10/desconectando/" + Convert.ToString(listapartidas[j]));
                        Thread.Sleep(300);
                    }
                }
                if (anfitrion == 0)
                {
                    if (jugando == 1)
                    {
                        for (int j = 0; j < partida_simultaneas; j++)
                            enviar_server("10/Quitame/" + Convert.ToString(listapartidas[j]) + "/" + this.usuario);
                        Thread.Sleep(300);
                    }
                    else
                    {
                        for (int j = 0; j <= partida_simultaneas; j++)
                            enviar_server("10/Quitame/" + Convert.ToString(listapartidas[j]) + "/" + this.usuario);
                        Thread.Sleep(300);
                    }
                }

                if (cerrarsesion == false)
                {
                    Thread.Sleep(1000);
                    enviar_server("7/" + this.usuario + "/0"); //NOS DESCONECTAMOS flag = 0
                    MessageBox.Show("DESCONECTADO");
                }
                //Nos desconectamos
                Thread.Sleep(200);
                atender.Abort();
                enviar_server("0/Desconectado");
                desconectado = true;


                this.BackColor = Color.Gray;
                //Nos desconectamos
                //server.Shutdown(SocketShutdown.Both);
                //server.Close();


            }
            cerrando = true;
            disconecting = true;
            Application.Exit();

        }
    }
}

