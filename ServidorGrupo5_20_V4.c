#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>


int contador;
int i=0;
int sockets[100]; //Vector de Sockets, que serán las conexiones de los usuarios
int puerto = 50022;  //50020

typedef struct {
	int socket;
	int conectado;
	char nombre[30];
}Usuario;

typedef struct{
	Usuario usuarios[100];
	int num;
	int current;
}ListaUsuarios;

typedef struct{
	Usuario usuarios[4];
	int num;
	int current;
	int status; //0 libre 1 ocupada
}Partida;
typedef struct{
	Partida partidas[100];
	int num;
	int totales;
}ListaPartidas;

ListaUsuarios l;
ListaPartidas lpartidas;

//Estructura necesaria para acesso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

void PonerJugadorPartida (Partida *part, int j, char nombre[30])
{
	strcpy(part->usuarios[j].nombre, nombre);
}
void PonerUsuariosConectados (ListaUsuarios *l, int socket, char nombre[30], int flag)
{
	int encontrado = 0;
	for(int i=0; i<l->num ;i++)
	{
		if (strcmp(l->usuarios[i].nombre, nombre)==0)
		{
			encontrado = 1;
			l->usuarios[i].socket = socket;
			l->usuarios[i].conectado = flag;
			if (flag == 1)
				l->current++;
			if (flag == 0)
				l->current--;
		}
	}
	if (encontrado == 0)
	{
		l->usuarios[l->num].socket = socket;
		strcpy(l->usuarios[l->num].nombre, nombre);
		l->usuarios[l->num].conectado = flag;
		l->num++;
		if (flag == 1)
			l->current++;
		if (flag == 0)
			l->current--;
	}
}

void PonerDatosPartida (ListaUsuarios l, Partida *part, int k, int j,char nombre[30])
{
	
	part->usuarios[j].socket = l.usuarios[k].socket;
	strcpy(part->usuarios[j].nombre, nombre);
	part->current++;
}

void PonerPartida (ListaPartidas *lpartidas, Partida part)
{
	lpartidas->partidas[lpartidas->num] = part;
	lpartidas->partidas[lpartidas->num].status = 1;
	lpartidas->num ++;
}
void CambioPosiciones(Partida *part, int i)
{
	if (i==1)
	{
		
		part->usuarios[1] = part->usuarios[2];
		part->usuarios[2] = part->usuarios[3];
		
	}
	if (i==2)
	{
		part->usuarios[2] = part->usuarios[3];
		
	}
	if (i==3)
	{
		
	}
	
}

void *AtenderCliente (void *socket)
{
	//Generamos las variables
	int sock_conn, ret;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	char pwd[20];
	int playerID;
	char consulta[80];
	
	char peticion[512];
	char respuesta[512];
	
	//TODO LO NECESARIO PARA SQL
	
	// Estructura especial para almacenar resultados de consultas -Wall -pedantic-errors -O0 -lm
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	MYSQL_ROW row_user;
	MYSQL_ROW row_connected;
	MYSQL *conn;
	int err;
	
	
		
	//Creamos la conexión al servidor MYSQL
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//inicializar la conexion shiva2.upc.es
	conn = mysql_real_connect (conn,"shiva2.upc.es","root","mysql","datagame",0,NULL,0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Bucle de atención al cliente (Aqui encontramos todas las posibles peticiones)
	int terminar = 0;
	while (terminar == 0)
	{
		// Ahora recibimos su nombre, que dejamos en buff. Recoge la peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que añadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		//Peticion de determinacion de lo que piden
		//Escribimos el nombre en la consola
		
		printf ("\tLa peticion del socket es: %s\n", peticion);
		
		
		char *p = strtok( peticion, "/"); //Split, cogemos el primer numero
		int codigo =  atoi (p); // Convertimos "1" en integer  5/1/Ems
		char nombre[30]; 
		strcpy(respuesta, "0/0/0/0/0/");

		if (codigo != 0)
		{
			
			
			printf("\tEl codigo es diferente de 0\n");
			p = strtok( NULL, "/");
			strcpy (nombre, p);
			printf ("\tCodigo: %d, Nombre: %s\n", codigo, nombre);
			
			
		}
		printf("codigo: %d\n",codigo);
		if (codigo == 0)
		{
			
			
			printf("\tEl codigo es igual a 0, se finaliza el programa\n } \n");
			terminar = 1;
			
			
		}
		
		if (codigo == 1) //Queremos saber las victorias de un jugador concreto
		{
			
			printf("\tEntrado en contar victorias del jugador (codigo==1)\n");
			
			strcpy(consulta, "SELECT COUNT(winner) FROM GAME WHERE winner ='");
			strcat(consulta, nombre);
			strcat(consulta,"'");
			
			printf("\tHacemos consulta a mySQL\n");
			
			//hacemos la consulta
			err=mysql_query(conn, consulta);
			if(err!=0)
			{
				printf("Error al consultar datos de la base %u%s\n", mysql_errno(conn), mysql_error(conn));
				exit(1);
				
			}
			
			printf("\tConsulta realizada con exito\n");
			
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL){
				
				printf ("\t\tNo se han obtenido datos en la consulta\n");}
			
			else{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el numero de victorias
				printf ("\tEl numero de victorias es: 1/%s\n", row[0] );
				sprintf(respuesta, "1/%s", row[0]);
				
			}
			printf("\tSe acaba la consulta 1\n"); 
			
			
		}
		if (codigo == 2) //Consulta para saber que día hubo más partidas
		{	
			
			printf("\tEntramos en la consulta del día con mas partidas(codigo==2)\n");
			
			strcpy(consulta, "SELECT day, COUNT(day) AS total FROM GAME GROUP BY day ORDER BY total DESC");
			
			printf("\tHacemos consulta a mySQL\n");
			
			err=mysql_query(conn, consulta);
			if(err!=0)
			{
				printf("Error al consultar datos de la base %u%s\n", mysql_errno(conn), mysql_error(conn));
				exit(1);
			}
			
			printf("\tConsulta realizada con exito\n");
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL){
				
				printf ("No se han obtenido datos en la consulta\n");}
			
			else{
				printf ("\t El día obtenido es: 2/%s \n", row[0] );
				sprintf(respuesta, "2/%s", row[0]);
				
			}
			printf("\tAcaba la consulta con codigo 2\n");
			
			
		}
		
		
		
		if(codigo == 3) //Seleccionamos el id más grande que haya en la tabla player
		{
			
			printf("\tEntramos en la consulta de id más grande (codigo==3)\n");
			
			char envio[512];
			int indice;
			strcpy(consulta,"SELECT COUNT(username) FROM PLAYER");
			err=mysql_query (conn, consulta);
			
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			printf("\tConsulta a MySQL realiada con exito\n");
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado); 
			
			if (row == NULL)
				printf ("No se han obtenido datos en la consulta\n");
			else
			{	
				
				printf("\tEl maximo id es: %s \n", row[0]); 
				strcpy(envio, row[0]); // IDMAX
				strcat(envio, "/"); // IDMAX/
				indice = atoi(row[0]); 
				
				printf ("\tEntramos en la consulta del username\n");
				
				strcpy(consulta, "SELECT username FROM PLAYER");
				printf ("%s\n", consulta);
				
				
				printf ("\tLa consulta que mandamos a MySQL es: %s\n", consulta); 
			
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				resultado = mysql_store_result(conn);
				row_user = mysql_fetch_row(resultado);
							
				printf("\tConsulta realizada con exito\n");
				
				if (row_user == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else
				{
					int j = 0;
					while (j<indice)
					{
						
						
						strcat(envio, row_user[0]); // IDMAX/Player1/Player2
						strcat(envio, "/"); // IDMAX/Player1/Player2/
						printf ("\tEl usuario encontrado es: %s\n", row_user[0]);
						printf ("\t envio %s\n", envio);
						
						row_user = mysql_fetch_row(resultado);
						
						j++;
					}
					
				}
				
				sprintf(respuesta, "3/%s", envio); 
				
			}
			printf("\tSe acaba la consulta Consultar Registered\n"); 
			
		}
		
		
		if (codigo == 4) //LOG IN
		{
			char envio[50];
			
			printf("\tEntramos en la consulta de Log In (consulta==4)\n");
			
			strcpy(consulta, "SELECT pwd FROM PLAYER WHERE username ='");
			strcat(consulta, nombre);
			strcat(consulta,"'");
			
			
			
			
			printf ("\tLa consulta que mandamos a MySQL es: %s\n", consulta); 
			
			//hacemos la consulta
			err=mysql_query(conn, consulta);
			if(err!=0)
			{
				printf("Error al consultar datos de la base %u%s\n", mysql_errno(conn), mysql_error(conn));
				exit(1);
				
			}
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			printf("\tConsulta realizada con exito\n");
			
			if (row == NULL){
				
				printf ("No se han obtenido datos en la consulta\n");
				strcpy(respuesta, "NO");
				
			}
			
			
			else{
				// El resultado debe ser una matriz con una sola fila
				// y una columna que contiene el numero de victorias
				printf ("\tHemos obtenido la contraseña con exito\n");
				
				sprintf(envio, "%s", row[0]);
				strcat(envio, "/");
			}	
			printf("\tSe acaba la consulta de Log In\n"); 
			
			
			
			printf("\tEntramos en Busca de ID, (consulta==96)\n");
			
			strcpy(consulta, "SELECT id FROM PLAYER WHERE username = '");
			strcat(consulta, nombre);
			strcat(consulta, "'");
			printf("%s\n", consulta); 
			
			printf ("\tLa consulta que enviamos es: %s\n", consulta);
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("\t	Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado); 
			printf("\tConsulta realizada con exito\n");
			
			if (row == NULL)
				printf ("\t No se han obtenido datos en la consulta\n");
			else
			{
				printf("\t El id es: %s\n", row[0]);
				strcat(envio, row[0]);
				strcat(envio,"/");
				
			}
			
			sprintf(respuesta,"6/%s", envio);
		
			
			
			printf("\tSe acaba la consulta de Buscar ID\n");
					
			
		}
		if (codigo == 5) //REGISTER
		{
			//pthread_mutex_lock( &mutex ); //NO me interrumpas ahora
			// entramos el strtok como: nombre/contraseña
			
			printf("\tEntramos en Register, (consulta==5)\n");
			
			strcpy(consulta, "SELECT id FROM PLAYER WHERE username ='");
			strcat(consulta, nombre);
			strcat(consulta,"'");
			
			printf ("\tLa consulta que enviamos es: %s\n", consulta);
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado); 
			
			printf("\tConsulta realizada con exito\n\t{\n");
			
			if (row == NULL)
			{
				printf("\t  Como el usuario no esta en el registro lo incluimos\n");
				printf ("\t  Usuario todavia no registrado\n");
				
				
				p = strtok (NULL, "/");
				char pwd [30];
				strcpy(pwd, p);
				strcpy(consulta, "INSERT INTO PLAYER (username, pwd) VALUES ('");
				strcat(consulta, nombre);
				strcat(consulta, "', '");
				strcat(consulta, pwd);
				strcat(consulta, "')");
				
				printf ("\t  La consulta que enviamos es: %s\n", consulta);
				
				err=mysql_query(conn, consulta);
				if(err!=0)
				{
					printf("\t  Error al consultar datos de la base %u%s\n", mysql_errno(conn), mysql_error(conn));
					printf ("\t  No se ha podido registrar al usuario\n");
					sprintf(respuesta, "7/%s", "NO");
					exit(1);
					
				}
				else
				   printf("\t  Usuario registrado con exito\n\t}\n");
				   sprintf(respuesta, "7/%s", "REGISTRADO");
				
				
				
			}
			
			else
			{
				printf ("\tYa existe un usuario con ese username, escoja otro por favor\n");
				sprintf(respuesta, "%s", "NO");
			}
			printf("\tSe acaba la consulta de Register\n"); 
			//pthread_mutex_unlock( &mutex);//Ya puedes interrumpirme
			
		}
		if (codigo == 6)
		{
			int encontrado = 0;
			char envio[40];
			strcpy(envio, "11/");
			printf("\tConsulta 6\n"); 
			printf("\tAntes Del For\n"); 
			for(int j=0; j<l.num;j++)
			{
				printf("\tEn el for: %d\n", j); 
				if ((strcmp(l.usuarios[j].nombre, nombre)==0)&&(l.usuarios[j].conectado==1))
				{
					printf("\ENCONTRADO\n"); 
					encontrado = 1;
				}
			}
			if (encontrado == 1)
			{
				printf("\tENVIAMOS TRUE\n"); 
				strcat(envio, "true");
			}
			else 
			{
				printf("\tENVIAMOS FALSE\n"); 
				strcat(envio, "false");
			}
			sprintf(respuesta, "%s", envio);
		}
		if (codigo == 7)
		{
			//nos llega /nombre/0 (desconectar) /nombre/1 (conectar)
			
			// 1 conectado, 0 desconectado (FLAGS)
			char usuario[30];
			strcpy (usuario, nombre);
			char *p;
			p = strtok(NULL, "/");
			int conectadoflag = atoi(p);
			
			pthread_mutex_lock( &mutex);
			PonerUsuariosConectados (&l, sock_conn, nombre, conectadoflag);
			pthread_mutex_unlock( &mutex);
			
			char envio[50]; //enviaremos 4/IDMAX/Player1/Player2
			sprintf(envio, "%d", l.current);
			strcat(envio, "/");
			
			int socks[100];
			int SockIndex = 0;
			printf("SOCK_CONN: %d\n", sock_conn);
			for(int j=0;j<l.num;j++)
			{
				if (l.usuarios[j].conectado == 1)
				{
					strcat(envio,l.usuarios[j].nombre);
					strcat(envio, "/");
					socks[SockIndex] = l.usuarios[j].socket;
					printf("SOCKETS: %d\n", l.usuarios[j].socket);
					SockIndex++;						
				}
			}
			printf("\tsocks[SOCKINDEX]: %d\n", socks[0]);
			
			
			char notificacion[512];
			sprintf (notificacion, "4/%s", envio);
			printf ("Notificacion %s\n", notificacion);
			for(int j=0; j< SockIndex; j++)
				write (socks[j], notificacion, strlen(notificacion));
			
		}

		if (codigo == 9) //partida
		{
			pthread_mutex_lock( &mutex);
			
			
			int indice = atoi(nombre);
			Partida party;
			party.num = indice;
			
			char IndicePartida[30];
			sprintf(IndicePartida, "%d", lpartidas.num);
			
			party.current = 0;
			char invitador[30];
			char participantes[50];
			int Socket[indice];
			int entro=0;
			for (int j=0; j<indice; j++)
			{
				char *p;
				p = strtok(NULL,"/");
				if (j==0)
				{
					strcpy(invitador, p);
					strcat(invitador, "/");
					PonerJugadorPartida(&party, j, p);
				}
				
				
				for (int k=0; k<=l.num; k++)
				{
					if (strcmp(l.usuarios[k].nombre, p) == 0)
					{
						if ((j==0)&&(entro==0))
						{
							printf("CURRENT %d\n", party.current);
							PonerDatosPartida(l, &party, k, j, p);
							entro = 1;
							printf("CURRENT %d\n", party.current);
						}
						Socket[j] = l.usuarios[k].socket;
					}
				}
				
			}
			//if lpartidas.partidas[lpartidas.num].status == 0)
			PonerPartida(&lpartidas, party);
			printf("CURRENT %d\n", lpartidas.partidas[0].current);
			char notificacion[512];
			strcat(invitador, IndicePartida);
			strcat(invitador, "/");
			sprintf (notificacion, "8/%s", invitador);
			printf ("Notificacion %s\n", notificacion);
			for(int j=1; j< indice; j++)
				write (Socket[j], notificacion, strlen(notificacion));
			
			pthread_mutex_unlock( &mutex);
			
		}
		if (codigo == 10) 
		{
			pthread_mutex_lock( &mutex);
			printf("\t Estamos en codigo 10\n");
			if (strcmp("No", nombre) == 0)
			{
				printf("\t Es No\n");
				char *p;
				p = strtok(NULL, "/"); 
				int indice = atoi(p);
				p = strtok(NULL,"/");
				char nojuega[30];
				strcpy (nojuega, p);
				
				
				int socket = lpartidas.partidas[indice].usuarios[0].socket;
				lpartidas.partidas[indice].num--;
				char notificacion[512];
				sprintf(notificacion, "9/%s", "No/");
				strcat(notificacion, nojuega);
				strcat(notificacion, "/");
				write(socket, notificacion, strlen(notificacion));
			}
			
			if (strcmp("Si", nombre) == 0)
			{
				printf("\t Es Si\n");
				printf("\t %s\n", nombre);
				char *p;
				p = strtok(NULL,"/");
				int indice = atoi(p); //en que partida estamos
				printf("\t %s\n", p);
				p = strtok(NULL,"/");
				char invitado[30];
				int entro = 0;
				int noenviar = 0;
				strcpy(invitado, p);
				printf("\t %s\n", invitado);
				
				int numparticipantes = lpartidas.partidas[indice].current;
				printf("\t numeroparticipantes: %d \n", numparticipantes);
				//strcpy(lpartidas->partidas[indice].usuarios[numparticipantes].nombre, invitado);
				
				printf("\t current: %d \n",lpartidas.partidas[indice].current);
				for (int i=0; i<lpartidas.partidas[indice].num; i++)//comprobamos si el usuario ya esta en la partida
				{
					if(strcmp(lpartidas.partidas[indice].usuarios[i].nombre, invitado)==0)
					{
						entro = 1; //Si esta en la partida, no lo volvemos a poner
						
					}
				}
				for (int k=0; k<=l.num; k++)
				{
					printf("COMPARACION\n");
					printf("\tlista %s\n", l.usuarios[k].nombre);
					printf("\tinvitado %s\n", invitado);
					if ((strcmp(l.usuarios[k].nombre, invitado) == 0)&&(entro==0))
					{
						printf("\t entramos en match\n");
						PonerDatosPartida(l, &lpartidas.partidas[indice], k, numparticipantes, invitado); 	
						printf("\t current dentro del match: %d \n",lpartidas.partidas[indice].current);
						entro = 1;
					}
				}	
				char envio[60];
				strcpy(envio, "9/llena/");
				char numusuarios[30];
				sprintf(numusuarios, "%d", lpartidas.partidas[indice].current);
				printf("\n");
				strcat(envio, numusuarios);
				strcat(envio, "/");
				for (int j=0; j<lpartidas.partidas[indice].current; j++) 
				{	
					strcat(envio, lpartidas.partidas[indice].usuarios[j].nombre);
					strcat(envio, "/");
					
				}
				
				printf("ENVIO: %s\n", envio);
				if (noenviar == 0)
				{
					for (int j=0; j<lpartidas.partidas[indice].current; j++) //j=1 porque al que invita no se le envia					
					{
						if (j==0)
						{
							char mensaje[40];
							char quepartidasoy[30];
							strcpy(mensaje, "9/Si/");
							strcat(mensaje, invitado);
							strcat(mensaje, "/");
							sprintf(quepartidasoy, "%d", lpartidas.num-1);
							strcat(mensaje, quepartidasoy);
							strcat(mensaje, "/");
							write(lpartidas.partidas[indice].usuarios[j].socket, mensaje, strlen(mensaje));
						}
						else
							write(lpartidas.partidas[indice].usuarios[j].socket, envio, strlen(envio));
						
					}
				}
			}
			if (strcmp("Quitame", nombre) == 0)
			{
				//10/Quitame/numpart/nombre
				int entro = 0;
				int noenviar = 0;
				char *p;
				p = strtok(NULL,"/");
				int indice = atoi(p); //en que partida estamos
				printf("\t %s\n", p);
				p = strtok(NULL,"/");
				char eliminado[30];
				strcpy(eliminado, p);
				int soy = i;
				int sock;
				
				for (int i=0; i<lpartidas.partidas[indice].current; i++)//comprobamos si el usuario ya esta en la partida
				{
					if(strcmp(lpartidas.partidas[indice].usuarios[i].nombre, eliminado)==0)
					{
						soy = i;
						sock = lpartidas.partidas[indice].usuarios[i].socket;
						lpartidas.partidas[indice].current--;
						CambioPosiciones(&lpartidas.partidas[indice], i);
						
						char mensaje[40];
						strcpy(mensaje, "9/Mevoy/");
						write(sock, mensaje, strlen(mensaje));
						entro = 1; //Si esta en la partida, no lo volvemos a poner
						noenviar = 1;
					}
				}
				
				char envio[60];
				strcpy(envio, "9/llena/");
				char numusuarios[30];
				sprintf(numusuarios, "%d", lpartidas.partidas[indice].current);
				printf("\n");
				strcat(envio, numusuarios);
				strcat(envio, "/");
				for (int j=0; j<lpartidas.partidas[indice].current; j++) 
				{	
					strcat(envio, lpartidas.partidas[indice].usuarios[j].nombre);
					strcat(envio, "/");
					
				}
				
				printf("ENVIO: %s\n", envio);
				if (noenviar == 1)
				{
					for (int j=0; j<lpartidas.partidas[indice].current; j++) //j=1 porque al que invita no se le envia					
					{
						if (j==0)
						{
							char mensaje[40];
							strcpy(mensaje, "9/Eliminado/");
							strcat(mensaje, eliminado);
							strcat(mensaje, "/");
							write(lpartidas.partidas[indice].usuarios[j].socket, mensaje, strlen(mensaje));
						}
						else
							write(lpartidas.partidas[indice].usuarios[j].socket, envio, strlen(envio));
					}
				}
			}
			pthread_mutex_unlock( &mutex);
		}
		if (codigo == 11)
		{ 	//MENSAJE 11/indicepartida
			int partidaindex = atoi(nombre);
			int indexfor = lpartidas.partidas[partidaindex].current;
			for(int j=0;j<indexfor;j++)
			{
				char mensaje[40];
				strcpy (mensaje, "10/comienza/");
				write(lpartidas.partidas[partidaindex].usuarios[j].socket, mensaje, strlen(mensaje));
			}
			
			
		}
		if (codigo != 0)
		{
			
			printf ("Respuesta: %s\n", respuesta);
			strcat(respuesta, "/");
			// Y lo enviamos
			write (sock_conn, respuesta, strlen(respuesta));
			
		}
		if ((codigo == 1) || (codigo = 2) || (codigo = 3) || (codigo = 4) || (codigo = 5) || (codigo = 6) || (codigo = 7) || (codigo = 8) || (codigo = 9) || (codigo = 10) || (codigo = 15) || (codigo = 96) )
		{
			pthread_mutex_unlock( &mutex);
			printf("Funcion general\n");
			 //NO me interrumpas ahora
			contador ++; 
			pthread_mutex_unlock( &mutex);//Ya puedes interrumpirme
			//notificar a todos los usuarios
			char notificacion[20];
			sprintf (notificacion, "5/%d", contador);
			for(int j=0; j< i; j++)
			{
				write (sockets[j], notificacion, strlen(notificacion));
				printf("Notificacion: %s\n", notificacion);
			}
		}
			

		
		
		
    }
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
	
	
	
}




int main(int argc, char *argv[])
{
	//MAIN DEL SERVIDOR
	
	
	//GENERAMOS LAS VARIABLES DE LOS SOCKETS 
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;

	// INICIALIZACIONES
	
	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	
	// Hacemos el bind al puerto
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la maquina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9080
	serv_adr.sin_port = htons(puerto);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 3
	if (listen(sock_listen, 3) < 0) 
		printf("Error en el Listen");
	
	//Generamos las variables para el for
	contador = 0;
	
	pthread_t thread[100]; //Vector de threads
	int num = 0;
	l.num = 0;
	lpartidas.num=0;
	lpartidas.totales = 100;
	for(int j=0; j<lpartidas.totales; j++)
	{
		lpartidas.partidas[j].status=0;
	}
	

	
	for(i;i<100;i++){
	
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL); //El servidor espera hasta que alguien se conecta
		printf ("He recibido conexion\n");
		printf("\n");
		sockets[i] = sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		//Crear thread y decirle lo que tiene que hacer
		printf("Creamos thread\n{\n");
		pthread_create (&thread[i], NULL, AtenderCliente,&sockets[i]);	
		
		
	}
	
	
	// Atenderemos peticiones, en este caso 100, pero queremos infinitas (preguntar)
}
