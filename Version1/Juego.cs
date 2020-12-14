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
