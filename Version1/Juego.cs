<<<<<<< HEAD
﻿using System;
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
    public partial class Juego : Form
    {
        Thread juegoThread;
        Socket server;
        int indicepartida;
        string mensajeserver;
        string mchat;
        string usuario;

        delegate void DelegadoParaChat(string mensaje);

        public Juego()
        {
            InitializeComponent();
        }

        private void Juego_Load(object sender, EventArgs e)
        {

        }
        public void SetThread(Thread thread)
        {
            this.juegoThread = thread;
        }
        public void SetServer(Socket servidor)
        {
            this.server = servidor;
        }
        public void SetPartida(int indice)
        {
            this.indicepartida = indice;
            PartidaLabel.Text = "Estas en la partida: "+Convert.ToString(indice);
        }

        public void SetUsuario(string usuario)
        {
            this.usuario = usuario;
        }

        public string GetMensajes()
        {
            return this.mchat;
        }
        public int GetIndice()
        {
            return this.indicepartida;
        }
        public void SetMensajes(string mchat)
        {
            this.mchat = mchat;
            DelegadoParaChat del = new DelegadoParaChat(PonChat);
            ChatBOX.Invoke(del, new object[] { mchat });
        }

        private void PonChat(string mensaje)
        {
            ChatBOX.Items.Add(mchat);
        }
        private void enviar_server(string mensaje)
        {

            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void SendBUTTON_Click(object sender, EventArgs e)
        {
            
            mensajeserver = MensajeBOX.Text;
            MensajeBOX.Text = "";
            enviar_server("12/" + Convert.ToString(indicepartida) +"/"+ this.usuario +": "+ mensajeserver);

            
          
        }
        private void Juego_FormClosing(object sender, FormClosingEventArgs e)
        {
            juegoThread.Abort();
        }

        private void ChatBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChatBOX.Items.Add("Hello");
        }
    }
}
=======
﻿using System;
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
    public partial class Juego : Form
    {
        Thread juegoThread;
        Socket server;
        Bitmap bitmap1;
        Bitmap pua;
        Graphics ruleta;
        string[] usuarios = new string[4];
        string siguienteturno;
        int indicepartida;
        int id;
        string mensajeserver;
        string mchat;
        string usuario;
        int anfitrion;
        int[] preguntasdeportes = new int[100];
        int[] numdeportes = new int[1];
        bool correctasdeportes = false;
        int[] preguntashistoria = new int[100];
        int[] numhistoria = new int[1];
        bool correctashistoria = false;
        int[] preguntasciencia = new int[100];
        int[] numciencia = new int[1];
        bool correctasciencia = false;
        int[] preguntasarte = new int[100];
        int[] numarte = new int[1];
        bool correctasarte = false;
        int[] preguntasentretenimiento = new int[100];
        int[] numentretenimiento = new int[1];
        bool correctasentretenimiento = false;
        int[] preguntasgeografia = new int[100];
        int[] numgeografia = new int[1];
        bool correctasgeografia = false;
        string[] partida = new string[4];
        int numjugadores;
        int tupuntuacion;
        int cerrandovoluntario = 0;
        bool messageactivo = false;
        int categoria;
        int angulo;
        int anteriorcategoria = 0;
        int stop = 0;
        bool timerfinish = true;
        int randomruleta = 0;
        bool cerrando = false;
        int catcorona = 0;
        bool incorona = false;
        bool enjuego = false;

        //DELEGADOS DEL JUEGO

        delegate void DelegadoParaChat(string mensaje);
        delegate void DelegadoParaEnabled(int enabled);
        delegate void DelegadoParaDataGrid(string jugador, int puntuacion);
        delegate void DelegadoParaCerrar();
        delegate void DelegadoParaGanador(string ganador);
        delegate void DelegadoParaQuitarJugador(string jugador);

        public Juego()
        {
            InitializeComponent();
        }

        private void Juego_Load(object sender, EventArgs e)
        {
            enjuego = true;
            this.MaximizeBox = false;
            this.AdjustFormScrollbars(true);
            bitmap1 = (Bitmap)Bitmap.FromFile("Imagenes/Ruleta.png");
            ruleta = pictureBox1.CreateGraphics();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            ruleta.TranslateTransform((float)bitmap1.Width / 2, (float)bitmap1.Height / 2);
            ruleta.RotateTransform(30);
            ruleta.TranslateTransform(-(float)bitmap1.Width / 2, -(float)bitmap1.Height / 2);
            this.TransparencyKey = Color.Gray;
            if (anfitrion == 1)
            {
                CategoriaButton.Enabled = true;
                TurnoLabel.Text = "Es tu turno";
            }
            else
            {
                CategoriaButton.Enabled = false;
                TurnoLabel.Text = "No es tu turno";
            }
            enviar_server("15/" + Convert.ToString(this.indicepartida));

            // CARGAMOS LA TABLA DE MUÑECOS 
            ScoreBoardGridView.ColumnCount = 8;
            ScoreBoardGridView.ColumnHeadersVisible = true;
            ScoreBoardGridView.RowHeadersVisible = false;
            ScoreBoardGridView.Columns[0].HeaderText = "Jugadores";
            ScoreBoardGridView.Columns[1].HeaderText = "Puntuaciones";
            ScoreBoardGridView.Columns[2].HeaderText = "Deportes";     
            ScoreBoardGridView.Columns[3].HeaderText = "Historia";
            ScoreBoardGridView.Columns[4].HeaderText = "Ciencia";
            ScoreBoardGridView.Columns[5].HeaderText = "Arte";
            ScoreBoardGridView.Columns[6].HeaderText = "Entretenimiento";
            ScoreBoardGridView.Columns[7].HeaderText = "Geografia";
            ScoreBoardGridView.RowCount = numjugadores;
            for (int z= 0; z<numjugadores; z++)
            {
                ScoreBoardGridView.Rows[z].Cells[0].Value = partida[z];
                ScoreBoardGridView.Rows[z].Cells[1].Value = 0;
                ScoreBoardGridView.Rows[z].Cells[2].Value = Image.FromFile("Iconos/DeportesIconoNegro.png");
                ScoreBoardGridView.Rows[z].Cells[3].Value = Image.FromFile("Iconos/HistoriaIconoNegro.png");
                ScoreBoardGridView.Rows[z].Cells[4].Value = Image.FromFile("Iconos/CienciaIconoNegro.png");
                ScoreBoardGridView.Rows[z].Cells[5].Value = Image.FromFile("Iconos/ArteIconoNegro.png");
                ScoreBoardGridView.Rows[z].Cells[6].Value = Image.FromFile("Iconos/EntretenimientoIconoNegro.png");
                ScoreBoardGridView.Rows[z].Cells[7].Value = Image.FromFile("Iconos/GeografiaIconoNegro.png");
            }
            ScoreBoardGridView.Enabled = false;

        }
        //FUCNIONES PARA COMUNICARNOS CON CONSULTAS.CS
        public void SetAnfitrion(int anfi)
        {
            this.anfitrion = anfi;
        }
        public void SetThread(Thread thread)
        {
            this.juegoThread = thread;
        }
        public void SetSetSiguienteTurno(string nombre)
        {
            siguienteturno = nombre;
        }
        public int GetIndicePartida()
        {
            return this.indicepartida;
        }
        public void SetServer(Socket servidor)
        {
            this.server = servidor;
        }
        public void SetPartida(int indice)
        {
            this.indicepartida = indice;
            PartidaLabel.Text = "Estas en la partida: " + Convert.ToString(indice);
        }
        public void SetJugadores(int num, string[] jugadores)
        {
            numjugadores = num;
            partida = jugadores;
            
        }
        public void SetId(int id_user)
        {
            this.id = id_user;
        }
        public bool GetJugando()
        {
            return enjuego;
        }
        public void SetUsuario(string usuario)
        {
            this.usuario = usuario;
            UsuarioLabel.Text = "User: " + usuario;
        }
        public void SetUsersPartida (string[] partida)
        {
            usuarios = partida; 
        }
        public string GetMensajes()
        {
            return this.mchat;
        }
        public int GetIndice()
        {
            return this.indicepartida;
        }
        public void SetMensajes(string mchat)
        {
            this.mchat = mchat;
            //PARA PODER PONER LOS MENSAJES TENDREMOS QUE DELEGAR
            DelegadoParaChat del = new DelegadoParaChat(PonChat);
            ChatBOX.Invoke(del, new object[] { mchat });
        }

        private void PonChat(string mensaje)
        {
            //AÑADIREMOS EL MENSAJE
            ChatBOX.Items.Add(mensaje);
        }
        private void PonEnabled (int enabled)
        {
            //CAMBIO DE TURNO

            if (enabled == 1)
            {
                CategoriaButton.Enabled = true;
                TurnoLabel.Text = "Es tu turno";
            }
            if (enabled == 0)
            {
                CategoriaButton.Enabled = false;
                TurnoLabel.Text = "No es tu turno";
            }
        }
        private void enviar_server(string mensaje)
        {
            //Envimos al servidor el nombre tecleado
            //Cogemos el string creado y lo convertimos en un vector de Bytes
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void SendBUTTON_Click(object sender, EventArgs e)
        {
            //ENVIAMOS EL MENSAJE AL SERVIDOR 
            mensajeserver = MensajeBOX.Text;
            MensajeBOX.Text = "";
            enviar_server("12/" + Convert.ToString(indicepartida) + "/" + this.usuario + ": " + mensajeserver + "/" + this.usuario);
            PonChat("You: " + mensajeserver);
        }
        
        private int NumeroIDpregunta()
        {
            //Cogemos un numero random para la pregunta que toque del 1 al 30
            Random dado = new Random();
            int numero = dado.Next(1, 31);
            return numero;
        }
        
        private int NumeroRuleta()
        {
            //PONEMOS UN DADO RANDOM PARA EL NUMERO DE VUELTAS
            Random dado = new Random();
            int numero = dado.Next(1, 5);
            return numero;
        }
        private int NumeroCategoria()
        {
            //PARA LA CATEGORIA ESCOGEMOS UN NUMERO RANDOM ENTRE 1 Y 6
            Random dado = new Random();
            int numero = dado.Next(1, 7);//entre 1 y 6
            
            return numero;
        }
        private int NumeroPregunta(int[] preguntas, int[] numeroparti)
        {
            //FUNCION PARA EVITAR LA REPETICION DE UN NUMERO RANDOM EN UNA CATEGORIA
            //ESTO EVITA QUE SE REPITAN PREGUNTAS 
            int numerorandom = NumeroIDpregunta();
            if (numeroparti[0] == 0)
            {
                preguntas[0] = numerorandom;
                numeroparti[0] = 1;
            }
            else
            {
                bool acabado = false;

                while (acabado == false)
                {
                    bool encontrado = false;

                    for (int j = 0; j < numeroparti[0]; j++)
                    {

                        if (preguntas[j] == numerorandom)
                        {
                            encontrado = true;
                        }
                    }
                    if (encontrado == false)
                    {
                        preguntas[numeroparti[0]] = numerorandom;
                        acabado = true;
                        
                    }
                    else
                        numerorandom = NumeroIDpregunta();
                }
                numeroparti[0]++;
            }
            return numerorandom;
            //DEVOLVEMOS UN NUMERO RANDOM QUE SABEMOS QUE NO HA SIDO UTILIZADO 

        }
        private void HistoriaButton_Click(object sender, EventArgs e)
        {
            //BOTON DE PRUEBAS PARA COMPROBAR QUE FUNCIONABAN LOS NUMEROS RANDOM
            int numenviar = NumeroPregunta(preguntashistoria, numhistoria);
            enviar_server("13/Historia/1/" + this.indicepartida); //enviar_server("13/Historia/"+Convert.ToString(numenviar)+"/"+this.indicepartida"); 
        }
        public void SetPregunta(string pregunta, string respuesta, string op1, string op2, string op3, string op4, string tipo)
        {
            //RECIBIREMOS LA PREGUNTA Y LA PONDREMOS EN UN FORMULARIO PREGUNTA 
            anteriorcategoria = categoria;
            Preguntas preguntasForm = new Preguntas();

            preguntasForm.SetFormPregunta(pregunta, op1, op2, op3, op4, respuesta);
            preguntasForm.SetTipo(tipo); //PARA SELECCIONAR EL FONDO
            preguntasForm.ShowDialog();
            int respuestaForm = preguntasForm.GetRespuesta();
            if (respuestaForm == Convert.ToInt32(respuesta))
            {
                DelegadoParaEnabled del = new DelegadoParaEnabled(PonEnabled);
                CategoriaButton.Invoke(del, new object[] { 1 }); //enabled
                //"HAS ACERTADO"
                //SIGO TIRANDO
                int numenviar;
                if (incorona == false)
                {
                    tupuntuacion++;
                }
                if (incorona == true)
                {
                    incorona = false;
                    switch (catcorona)
                    {
                        case 1:
                            correctasdeportes = true;
                            enviar_server("18/DEPORTE/" + this.indicepartida + "/" + this.usuario);
                            break;
                        case 2:
                            correctashistoria = true;
                            enviar_server("18/HISTORIA/" + this.indicepartida + "/" + this.usuario);
                            break;
                        case 3:
                            correctasciencia = true;
                            enviar_server("18/CIENCIA/" + this.indicepartida + "/" + this.usuario);
                            break;
                        case 4:      
                            correctasarte =true;
                            enviar_server("18/ARTE/" + this.indicepartida + "/" + this.usuario);
                            break;
                        case 5:
                            correctasentretenimiento = true;
                            enviar_server("18/ENTRETENIMIENTO/" + this.indicepartida + "/" + this.usuario);
                            break;
                        case 6:
                            correctasgeografia = true;
                            enviar_server("18/GEOGRAFIA/" + this.indicepartida + "/" + this.usuario); 
                            break;
                    }
                }
                
                if (tupuntuacion == 3)
                {
                    IniciarCorona();
                    //EL SIGUIENTE TURNO SERA CORONA
                    incorona = true;
                    tupuntuacion = 0;
                    switch (catcorona)
                    {
                        case 1:
                            numenviar = NumeroPregunta(preguntasdeportes, numdeportes);
                            enviar_server("13/DEPORTE/" + Convert.ToString(numenviar) + "/" + this.indicepartida);

                            break;
                        case 2:
                            numenviar = NumeroPregunta(preguntashistoria, numhistoria);
                            enviar_server("13/HISTORIA/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
 
                            break;
                        case 3:
                            numenviar = NumeroPregunta(preguntasciencia, numciencia);
                            enviar_server("13/CIENCIA/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
  
                            break;
                        case 4:
                            numenviar = NumeroPregunta(preguntasarte, numarte);
                            enviar_server("13/ARTE/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
  
                            break;
                        case 5:
                            numenviar = NumeroPregunta(preguntasentretenimiento, numentretenimiento);
                            enviar_server("13/ENTRETENIMIENTO/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                           
                            break;
                        case 6:
                            numenviar = NumeroPregunta(preguntasgeografia, numgeografia);
                            enviar_server("13/GEOGRAFIA/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
       
                            break;
                    }
                }
                if (correctasdeportes & correctashistoria & correctasciencia & correctasarte & correctasentretenimiento & correctasgeografia)
                {
                    //SI HEMOS ACERTADO TODAS LAS CATEGORIAS GANAMOS
                    enviar_server("16/" + this.indicepartida + "/" + this.usuario + "/GANADOR");
                    enjuego = false;
                }
                else
                    enviar_server("16/" + this.indicepartida + "/" + this.usuario + "/" + tupuntuacion);
               
                    
            }
            else
            {
                //"HAS FALLADO"
                //CAMBIO DE TURNO
                // DISBALED RULETA
                //ENVIO SERVER CAMBIO DE TURNO 
                DelegadoParaEnabled del = new DelegadoParaEnabled(PonEnabled);
                CategoriaButton.Invoke(del, new object[] { 0 }); //disabled

                incorona = false; 
                enviar_server("14/"+this.indicepartida+"/"+this.usuario); //siguienteturno es un nombre
            }
        }
        private void IniciarCorona()
        {
            //FUCNION QUE INICIA EL FORMS DE LA CORONA 
            Corona Corona = new Corona();
            Corona.SetCorrectas(correctasdeportes, correctashistoria, correctasciencia, correctasarte, correctasentretenimiento, correctasgeografia);
            Corona.ShowDialog();
            
            catcorona = Corona.GetCat();      
        }
        public void SetEnabledCategoria()
        {
            DelegadoParaEnabled del = new DelegadoParaEnabled(PonEnabled);
            CategoriaButton.Invoke(del, new object[] { 1 }); //enabled
        }
        public void SetPuntuacion(string jugador, int puntuacion)
        {
            DelegadoParaDataGrid datadel = new DelegadoParaDataGrid(PuntuacionDataGrid);
            ScoreBoardGridView.Invoke(datadel, new object[] { jugador, puntuacion }); //Cambiamos la puntuacion
        }
        public void PuntuacionDataGrid(string jugador, int puntuacion)
        {
            int indice = ScoreBoardGridView.Rows.Count; 
            for (int z=0;z<indice;z++)
            {
                if (Convert.ToString(ScoreBoardGridView.Rows[z].Cells[0].Value)==jugador)
                {
                    ScoreBoardGridView.Rows[z].Cells[1].Value = puntuacion;
                }
            }
        }
        public void QuitarJugadorDataGrid (string jugador)
        {
            //PONDREMOS LA INFO DE QUIEN SE HA IDO CON UN "se ha ido" 
            int indice = ScoreBoardGridView.Rows.Count;
            int encontrado = 0;
            int quitar = 0;
            for (int z=0;z<indice;z++)
            {
                if (Convert.ToString(ScoreBoardGridView.Rows[z].Cells[0].Value) == jugador)
                {
                    encontrado = 1;
                    quitar = z;
                }
            }
            if (encontrado == 1)
            {
                string mensaje = "Se ha ido";
                ScoreBoardGridView.Rows[quitar].Cells[1].Value = mensaje;
            }
        }
        private void CategoriaButton_Click(object sender, EventArgs e)
        {      
            //BOTON DE TIRAR
            //ESTE BOTON SELECCIONA LA CATEGORIA HASTA DONDE SE VA A MOVER LA RULETA
            stop = 0;
            ruleta.DrawImage(bitmap1, new Point(0, 0));
            
            int numero = NumeroCategoria();
            categoria = numero;
            angulo = 900;
            timer1.Start(); //Se mueve la ruleta
            randomruleta = NumeroRuleta();

            timerfinish = false;          
        }
        
        public void SetGanador(string ganador)
        {
            //GANADOR 
            while (messageactivo == true)
            {

            }
            messageactivo = true;
            cerrandovoluntario = 1;
            MessageBox.Show("El ganador/a es: " + ganador);
            messageactivo = false;
            enjuego = false;
            DelegadoParaCerrar del = new DelegadoParaCerrar(CerrarForms);
            this.Invoke(del, new object[] {}); //Cerramos Forms
        }
        
        public void QuitarJugador(string nombre)
        {
            //CUANDO UN JUGADOR SE VA DE LA PARTIDA, LO NOTIFICAMOS A LOS JUGADORES 
            DelegadoParaQuitarJugador del = new DelegadoParaQuitarJugador(QuitarJugadorDataGrid);
            ScoreBoardGridView.Invoke(del, new object[] { nombre });
            MessageBox.Show("El jugador " + nombre + " ha abandonado la partida");
        }
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            //BOTON DE PRUEBAS PARA FINALIZAR PARTIDA 
            enviar_server("16/" + this.indicepartida + "/" + this.usuario + "/GANADOR");
        }
        
        private void EnviarParaPregunta(int numero)
        {
            //FUNCION PARA ENVIAR AL SERVIDOR LA PREGUNTA QUE HA TOCADO
            Thread.Sleep(300);
            int numenviar;
            CategoriaButton.Enabled = false;
            switch (numero)
            {
                case 1:
                    CategoriaLabel.Text = "Deportes";
                    numenviar = NumeroPregunta(preguntasdeportes, numdeportes);
                    PreguntaLabel.Text = Convert.ToString(numenviar);
                    enviar_server("13/DEPORTE/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                    break;
                case 2:
                    CategoriaLabel.Text = "Historia";
                    numenviar = NumeroPregunta(preguntashistoria, numhistoria);
                    //enviar_server("13/Historia/1/" + this.indicepartida); 
                    enviar_server("13/HISTORIA/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                    break;
                case 3:
                    CategoriaLabel.Text = "Ciencia";
                    numenviar = NumeroPregunta(preguntasciencia, numciencia);
                    PreguntaLabel.Text = Convert.ToString(numenviar);
                    enviar_server("13/CIENCIA/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                    break;
                case 4:
                    CategoriaLabel.Text = "Arte";
                    numenviar = NumeroPregunta(preguntasarte, numarte);
                    PreguntaLabel.Text = Convert.ToString(numenviar);
                    enviar_server("13/ARTE/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                    break;
                case 5:
                    CategoriaLabel.Text = "Entretenimiento";
                    numenviar = NumeroPregunta(preguntasentretenimiento, numentretenimiento);
                    PreguntaLabel.Text = Convert.ToString(numenviar);
                    enviar_server("13/ENTRETENIMIENTO/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                    break;
                case 6:
                    CategoriaLabel.Text = "Geografia";
                    numenviar = NumeroPregunta(preguntasgeografia, numgeografia);
                    PreguntaLabel.Text = Convert.ToString(numenviar);
                    enviar_server("13/GEOGRAFIA/" + Convert.ToString(numenviar) + "/" + this.indicepartida);
                    break;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //DEPENDIENDO DE LA CATEGORIA QUE NOS HAYA TOCADO MOVEREMOS LA RULETA
            //HASTA UNA POSICION CONCRETA, UNA VEZ CONSEGUIDA ENVIAREMOS LA CONSULTA AL SERVIDOR 
            if (stop == 360 * randomruleta + angulo) // 360 * randomruleta 
            {
                timer1.Stop();
                timerfinish = true;
                EnviarParaPregunta(categoria);
            }
            else
            {
                timerfinish = false;
                switch (categoria)
                {
                    //BUSCAMOS CUANTO TIENE QUE GIRAR LA RULETA PARA LLEGAR A LA POSICION
                    case 1: //deportes
                        label1.Text = "DEPORTES";
                        if (anteriorcategoria == 0)
                        {
                            angulo = 120;
                        }
                        if (anteriorcategoria == 1) { angulo =360; }
                        if (anteriorcategoria == 2) { angulo = 3*60; } //DESDE LA CATEGORIA NECESITAREMOS IR TRES VECES HACIA LA IZQUIERDA PARA LLEGAR A DEPORTES 
                        if (anteriorcategoria == 3) { angulo = 2*60; }
                        if (anteriorcategoria == 4) { angulo = 5*60; }
                        if (anteriorcategoria == 5) { angulo = 1*60; }
                        if (anteriorcategoria == 6) { angulo = 4*60; }
                        break;
                    case 2: //historia
                        label1.Text = "HISTORIA";
                        if (anteriorcategoria == 0)
                        {
                            angulo = 300;
                        }
                        if (anteriorcategoria == 1) { angulo = 3*60; }
                        if (anteriorcategoria == 2) { angulo = 6*60; }
                        if (anteriorcategoria == 3) { angulo = 5*60; }
                        if (anteriorcategoria == 4) { angulo = 2*60; }
                        if (anteriorcategoria == 5) { angulo = 4*60; }
                        if (anteriorcategoria == 6) { angulo = 1*60; }
                        break;
                    case 3: //ciencia
                        label1.Text = "CIENCIA";
                        if (anteriorcategoria == 0)
                        {
                            angulo = 360;
                        }
                        if (anteriorcategoria == 1) { angulo = 4 * 60; }
                        if (anteriorcategoria == 2) { angulo = 1 * 60; }
                        if (anteriorcategoria == 3) { angulo = 6 * 60; }
                        if (anteriorcategoria == 4) { angulo = 3 * 60; }
                        if (anteriorcategoria == 5) { angulo = 5 * 60; }
                        if (anteriorcategoria == 6) { angulo = 2 * 60; }
                        break;
                    case 4: //arte
                        label1.Text = "ARTE";
                        if (anteriorcategoria == 0)
                        {
                            angulo = 180;
                        }
                        if (anteriorcategoria == 1) { angulo = 1 * 60; }
                        if (anteriorcategoria == 2) { angulo = 4 * 60; }
                        if (anteriorcategoria == 3) { angulo = 3 * 60; }
                        if (anteriorcategoria == 4) { angulo = 6 * 60; }
                        if (anteriorcategoria == 5) { angulo = 2 * 60; }
                        if (anteriorcategoria == 6) { angulo = 5 * 60; }
                        break;
                    case 5: //entretenimiento
                        label1.Text = "ENTRETENIMIENTO";
                        if (anteriorcategoria == 0)
                        {
                            angulo = 60;
                        }
                        if (anteriorcategoria == 1) { angulo = 5 * 60; }
                        if (anteriorcategoria == 2) { angulo = 2 * 60; }
                        if (anteriorcategoria == 3) { angulo = 1 * 60; }
                        if (anteriorcategoria == 4) { angulo = 4 * 60; }
                        if (anteriorcategoria == 5) { angulo = 6 * 60; }
                        if (anteriorcategoria == 6) { angulo = 3 * 60; }
                        break;
                    case 6: //Geografia
                        label1.Text = "GEOGRAFIA";
                        if (anteriorcategoria == 0)
                        {
                            angulo = 240;
                        }
                        if (anteriorcategoria == 1) { angulo = 2 * 60; }
                        if (anteriorcategoria == 2) { angulo = 5 * 60; }
                        if (anteriorcategoria == 3) { angulo = 4 * 60; }
                        if (anteriorcategoria == 4) { angulo = 1 * 60; }
                        if (anteriorcategoria == 5) { angulo = 3 * 60; }
                        if (anteriorcategoria == 6) { angulo = 6 * 60; }
                        break;
                }
                ruleta.TranslateTransform((float)bitmap1.Width / 2, (float)bitmap1.Height / 2);
                ruleta.RotateTransform(5);
                stop = stop + 5;
                ruleta.TranslateTransform(-(float)bitmap1.Width / 2, -(float)bitmap1.Height / 2);
                ruleta.DrawImage(bitmap1, new Point(0, 0));
            }
        }
        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox2.Image, new Point(0, 600));
        }

        private void MensajeBOX_KeyPress(object sender, KeyPressEventArgs e)
        {
            //FUNCION PARA PODER ENVIAR MENSAJES CON EL ENTER
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                mensajeserver = MensajeBOX.Text;
                MensajeBOX.Text = "";
                enviar_server("12/" + Convert.ToString(indicepartida) + "/" + this.usuario + ": " + mensajeserver + "/" + this.usuario);
                PonChat("You: " + mensajeserver);
            }
        }
        public void SetGanadorMuñeco (string cat, string jugador)
        {
            //PONEMOS EL MUÑECO QUE HA GANADO EL JUGADOR CORRESPONDIENTE
            int z;
            int a = ScoreBoardGridView.Rows.Count; //numjugadores
            for(int o=0;o<a;o++)
            {
                if (Convert.ToString(ScoreBoardGridView.Rows[o].Cells[0].Value) == jugador)
                {
                    z = o;
                    if (cat == "DEPORTE") { ScoreBoardGridView.Rows[z].Cells[2].Value = Image.FromFile("Iconos/DeportesIcono.png"); }
                    if (cat == "HISTORIA") { ScoreBoardGridView.Rows[z].Cells[3].Value = Image.FromFile("Iconos/HistoriaIcono.png"); }
                    if (cat == "CIENCIA") { ScoreBoardGridView.Rows[z].Cells[4].Value = Image.FromFile("Iconos/CienciaIcono.png"); }
                    if (cat == "ARTE") { ScoreBoardGridView.Rows[z].Cells[5].Value = Image.FromFile("Iconos/ArteIcono.png"); }
                    if (cat == "ENTRETENIMIENTO") { ScoreBoardGridView.Rows[z].Cells[6].Value = Image.FromFile("Iconos/EntretenimientoIcono.png"); }
                    if (cat == "GEOGRAFIA") { ScoreBoardGridView.Rows[z].Cells[7].Value = Image.FromFile("Iconos/GeografiaIcono.png"); }  
                    
                }
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //BOTON DE PRUEBAS
            tupuntuacion = 2;
        }
        private void CerrarForms()
        {
            this.Close();
        }

        private void Juego_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cerrandovoluntario == 0)
            {
                if (CategoriaButton.Enabled == true)
                {
                    enviar_server("14/" + this.indicepartida + "/" + this.usuario);
                    Thread.Sleep(200);
                }
                enviar_server("17/Quitame/" + Convert.ToString(indicepartida) + "/" + this.usuario);
                Thread.Sleep(400);
            }
        }
    }
}
>>>>>>> 376b2f8 (Version Preguntados)
