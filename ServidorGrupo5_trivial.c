#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
#include <stdio.h>
#include <time.h>



int contador;
int i=0;
int sockets[100]; //Vector de Sockets, que serán las conexiones de los usuarios
int puerto = 50020;  //50020 //9004

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
	int current;
}ListaPartidas;

ListaUsuarios l;
Usuario users;
ListaPartidas lpartidas;
char tiempo[128];


//Estructura necesaria para acesso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

void EstablecerTiempo()
{
	time_t tiempos = time(0);
	struct tm *tlocal = localtime(&tiempos);
	char output[128];
	strftime(output,128,"%d/%m/%y&%H:%M:%S&",tlocal);
	printf("%s\n",output);
	sprintf(tiempo,"%s", output);
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
void PonerJugadorPartida (Partida *part, int j, char nombre[30])
{
	strcpy(part->usuarios[j].nombre, nombre);
}
void PonerDatosPartida (ListaUsuarios l, Partida *part, int k, int j,char nombre[30])
{
	
	part->usuarios[j].socket = l.usuarios[k].socket;
	strcpy(part->usuarios[j].nombre, nombre);
	
	part->current++;
	
}
void VaciarPartida (Partida *part)
{
	part->num=0;
	part->current=0;
	part->status=0;
	printf("\t vacio la lista de usuarios\n");
	part->usuarios[0] = users;
	part->usuarios[1] = users;
	part->usuarios[2] = users;
	part->usuarios[3] = users;
	printf("\t He vaciado la lista de usuarios\n");
}
void PonerDatosPartidaNum (Partida *part, int num)
{
	part->num = num;
}

void PonerPartida (ListaPartidas *lpartidas, Partida part)
{
	lpartidas->partidas[lpartidas->num] = part;
	lpartidas->partidas[lpartidas->num].status = 1;
	lpartidas->num ++;
}

void CambioPosiciones(Partida *part, int i)
{
	if (i ==0)
	{
		part->usuarios[0] = part->usuarios[1];
		part->usuarios[1] = part->usuarios[2];
		part->usuarios[2] = part->usuarios[3];
		
	}
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
	//inicializar la conexion shiva2.upc.es //localhost
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
				write(sock_conn, respuesta, strlen(respuesta));
				
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
				write(sock_conn, respuesta, strlen(respuesta));
				
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
				write(sock_conn, respuesta, strlen(respuesta));
				
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
			printf("Envio LOG IN con: %s\n", respuesta);
			printf("Mi socket es: %d\n", sock_conn);
			write (sock_conn, respuesta, strlen(respuesta));
			
			
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
					sprintf(respuesta, "7/%s/", "NO");
					exit(1);
					
				}
				else
				{
				   printf("\t  Usuario registrado con exito\n\t}\n");
				   sprintf(respuesta, "7/%s/", "REGISTRADO");
				}
				
				
				
			}
			
			else
			{
				printf ("\tYa existe un usuario con ese username, escoja otro por favor\n");
				sprintf(respuesta, "7/%s/", "NO");
			}
			write(sock_conn, respuesta, strlen(respuesta));
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
				strcat(envio, "true/");
			}
			else 
			{
				printf("\tENVIAMOS FALSE\n"); 
				strcat(envio, "false/");
			}
			sprintf(respuesta, "%s", envio);
			write(sock_conn, respuesta, strlen(respuesta));
		}
		if (codigo == 7)
		{
			//nos llega /nombre/0 (desconectar) /nombre/1 (conectar)
			
			// 1 conectado, 0 desconectado (FLAGS)
			printf("\tEntro en el codigo 7\n"); 
			char usuario[30];
			strcpy (usuario, nombre);
			printf("\tSTRTOK\n"); 
			char *p;
			p = strtok(NULL, "/");
			int conectadoflag = atoi(p);
			printf("\tPONEMOS EN CONECTADOS\n"); 
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
			
			// 9/primera/3/Ems/Cris o 9/indicepartida/3/Ems/Cris
			int primera = 1;
			int numpartida;
			if (strcmp(nombre, "primera")==0)
			{
				
				numpartida = lpartidas.current;
				printf("\t lpartidas.current = %d\n",  lpartidas.current);
				lpartidas.current ++;
				printf("\t lpartidas.current 2-step = %d\n",  lpartidas.current);
/*				if (lpartidas.num == 99)*/
/*				{*/
/*					lpartidas.num = 0; */
/*				}*/
			}
			else
			{
				
				primera = 0;
				numpartida = atoi(nombre); //lpartidas.partidas[numpartida]
				printf("\t indicepartida = %d\n",  numpartida);
			}
			printf("\t lpartidas.current FUERA IF = %d\n",  lpartidas.current);
			printf("\t indicepartida FUERA IF = %d\n",  numpartida);
			char *p;
			p = strtok(NULL, "/"); 
			int indice = atoi(p);
			
			
			char IndicePartida[30];
			sprintf(IndicePartida, "%d", numpartida);
			
			
			char invitador[30];
			char participantes[50];
			int Socket[indice];
			int entro=0;
			printf("\t Indice: %d\n", numpartida);
			for (int j=0; j<indice; j++)
			{
				char *p;
				p = strtok(NULL,"/");
				printf("\t NombreSocket: %s\n",p);
				if (j==0)
				{
					strcpy(invitador, p);
					strcat(invitador, "/");
					pthread_mutex_lock( &mutex);
					PonerDatosPartidaNum(&lpartidas.partidas[numpartida], indice);
					pthread_mutex_unlock( &mutex);
				}
				
				for (int k=0; k<=l.num; k++)
				{
					if (strcmp(l.usuarios[k].nombre, p) == 0)
					{
						if (primera == 1)
						{
							printf("\t Estoy en primera = 1\n");
							pthread_mutex_lock( &mutex);
							PonerDatosPartida(l, &lpartidas.partidas[numpartida],k, j, p);
							pthread_mutex_unlock( &mutex);
							
						}
						
						Socket[j] = l.usuarios[k].socket;
						printf("\t Estoy poniendo el socket: %d\n",Socket[j]);
					}
				}
				
				
				
			}
			//if lpartidas.partidas[lpartidas.num].status == 0 	
			printf("CURRENT %d\n", lpartidas.partidas[numpartida].current);
			char notificacion[512];
			strcat(invitador, IndicePartida);
			strcat(invitador, "/");
			sprintf (notificacion, "8/%s", invitador);
			printf ("Notificacion %s\n", notificacion);
			for(int j=0; j< indice; j++)
			{
				if (j==0)
				{
					char envio[40];
					strcpy(envio, "9/indicepartida/");
					strcat(envio, IndicePartida);
					strcat(envio, "/");
					write (Socket[j], envio, strlen(envio));
				}
				if (j!=0)
					write (Socket[j], notificacion, strlen(notificacion));
			}
			
			
			
		}
		if (codigo == 10) 
		{
			
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
				
				pthread_mutex_lock( &mutex);
				lpartidas.partidas[indice].num--;
				pthread_mutex_unlock( &mutex);
				
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
						pthread_mutex_lock( &mutex);
						PonerDatosPartida(l, &lpartidas.partidas[indice], k, numparticipantes, invitado); 	
						pthread_mutex_unlock( &mutex);
						printf("\t current dentro del match: %d \n",lpartidas.partidas[indice].current);
						entro = 1;
					}
				}	
				char envio[60];
				char envioanfi[60];
				strcpy(envio, "9/llena/");
				strcpy(envioanfi, "9/anfi/");
				char numusuarios[30];	
				
				//strcat(envio, "0/");
				strcat(envioanfi, "1/");
				
				sprintf(numusuarios, "%d", lpartidas.partidas[indice].current);
				printf("\n");
				strcat(envio, numusuarios);
				strcat(envio, "/");
				strcat(envioanfi, numusuarios);
				strcat(envioanfi, "/");
				for (int j=0; j<lpartidas.partidas[indice].current; j++) 
				{	
					strcat(envio, lpartidas.partidas[indice].usuarios[j].nombre);
					strcat(envio, "/");
					strcat(envioanfi, lpartidas.partidas[indice].usuarios[j].nombre);
					strcat(envioanfi, "/");
					
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
							write(lpartidas.partidas[indice].usuarios[j].socket, envioanfi, strlen(envioanfi));
						}
						else
							write(lpartidas.partidas[indice].usuarios[j].socket, envio, strlen(envio));
						
					}
				}
			}
			if (strcmp("Quitame", nombre) == 0)
			{
				//10/Quitame/numpart/nombre
				printf("ESTOY EN QUITAME");
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
						soy = i; //lo buscamos para no enviarle el socket
						sock = lpartidas.partidas[indice].usuarios[i].socket;
						pthread_mutex_lock( &mutex);
						lpartidas.partidas[indice].current--;
						pthread_mutex_unlock( &mutex);
						pthread_mutex_lock( &mutex);
						CambioPosiciones(&lpartidas.partidas[indice], i);
						pthread_mutex_unlock( &mutex);
						
						char mensaje[40];
						strcpy(mensaje, "9/Mevoy/");
						printf("Estoy en MEVOY, mensaje: %s\n", mensaje);
						write(sock, mensaje, strlen(mensaje));
						entro = 1; //Si esta en la partida, no lo volvemos a poner
						noenviar = 1; //para no mandarle la lista al desconectado
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
/*				if (noenviar == 1)*/
/*				{*/printf("CURRENT: %d \n", lpartidas.partidas[indice].current);
					for (int j=0; j<lpartidas.partidas[indice].current; j++) //j=1 porque al que invita no se le envia					
					{
						if (j!=soy)
						{
							if (j==0)
							{
								char send[40];
								strcpy(send, "9/Eliminado/");
								strcat(send, eliminado);
								strcat(send, "/");
								printf("J=0, Mensaje: %s\n", send);
								printf("SOCKET: %d\n", lpartidas.partidas[indice].usuarios[j].socket);
								write(lpartidas.partidas[indice].usuarios[j].socket, send, strlen(send));
							}
							else
								write(lpartidas.partidas[indice].usuarios[j].socket, envio, strlen(envio));
						}
					}
				//}
			}
			
			if (strcmp("desconectando", nombre)==0)
			{
				char *p;
				p = strtok(NULL,"/");
				int partida;
				partida = atoi(p);
				for (int j=1; j<lpartidas.partidas[partida].current;j++)
				{	
					char message[30];
					strcpy(message, "9/anfitrionfuera/");		
					write(lpartidas.partidas[partida].usuarios[j].socket, message, strlen(message));
				}
				pthread_mutex_lock( &mutex);
				VaciarPartida(&lpartidas.partidas[partida]);
				pthread_mutex_unlock( &mutex);
				
			}
			
			
		
		}
		if (codigo == 11)
		{ 	//MENSAJE 11/indicepartida
			int partidaindex = atoi(nombre);
			int indexfor = lpartidas.partidas[partidaindex].current;
			//int indexfor = 4;
			printf("Entramos en 11\n"); 
			printf("Indexfor: %d\n", indexfor);
			char jugadores[100];
			sprintf(jugadores, "%d/", indexfor);
			for(int j=0;j<indexfor;j++)
			{
				strcat(jugadores, lpartidas.partidas[partidaindex].usuarios[j].nombre);
				strcat(jugadores, "/");	
			}
			for(int j=0;j<indexfor;j++)
			{
				char mensaje[40];
				strcpy (mensaje, "10/");
				strcat(mensaje, jugadores);
				printf("Socket: %d\n", lpartidas.partidas[partidaindex].usuarios[j].socket);
				printf("j: %d\n", j);
				write(lpartidas.partidas[partidaindex].usuarios[j].socket, mensaje, strlen(mensaje));
			}
			
			
		}
		if (codigo == 12)
		{
			
			// 12/indice/mensaje/nombre 
			// nombre = indice
			char *p;
			p = strtok(NULL, "/"); 
			char envio[30];
			strcpy(envio, "12/");
			strcat(envio, nombre); //INDICE
			strcat(envio,"/");
			strcat(envio, p); // MENSAJE
			strcat(envio, "/");
			p = strtok(NULL, "/");
			int encontrado;
			for (int j= 0;j<lpartidas.partidas[atoi(nombre)].current;j++)
			{
				if (strcmp(lpartidas.partidas[atoi(nombre)].usuarios[j].nombre, p) == 0)
					encontrado = j;
			}
			
			printf("Envio: %s", envio);
			for (int j= 0;j<lpartidas.partidas[atoi(nombre)].current;j++)
			{
				if (j != encontrado)
					write(lpartidas.partidas[atoi(nombre)].usuarios[j].socket, envio, strlen(envio));
			}
		}
		if (codigo == 13)
		{ 
			char envio[40];
			 //NOS LLEGA: 13/HISTORIA/id_pregunta/indice
			
			strcpy(consulta, "SELECT pregunta, respuesta, opcion1, opcion2, opcion3, opcion4 FROM "); 
			strcat(consulta, nombre);
			strcat(consulta,	" WHERE id = ");
			p = strtok(NULL, "/");
			strcat(consulta, p);
			printf ("\tLa consulta que mandamos a MySQL es: %s\n", consulta); 
			p = strtok(NULL, "/");
			char index[30];
			strcpy(index, p);
			
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
				
				sprintf(envio, "13/%s/%s/%s/%s/%s/%s/%s/%s/", index, row[0],row[1],row[2],row[3],row[4],row[5],nombre);
				write(sock_conn, envio, strlen(envio));
			}	
				
				
				
			
				
		}
		if (codigo == 14) // 14/indice/nombre
		{
			//FUNCION PARA CAMBIAR DE TURNO
			char envio[30];
			sprintf(envio, "14/%s/", nombre); //nombre=indice
			p = strtok(NULL, "/");
			for (int j= 0;j<lpartidas.partidas[atoi(nombre)].current;j++)
			{
				if (strcmp(lpartidas.partidas[atoi(nombre)].usuarios[j].nombre, p) == 0)
				{
					if ( j == lpartidas.partidas[atoi(nombre)].current - 1)
						write(lpartidas.partidas[atoi(nombre)].usuarios[0].socket, envio, strlen(envio)); 
					else
						write(lpartidas.partidas[atoi(nombre)].usuarios[j+1].socket, envio, strlen(envio)); 
				}
			}
			
		}
		if (codigo == 16) //16/indice/nombre/puntuacion
		{
			//nombre=indice
			
			p = strtok(NULL, "/");
			char jugador[30];
			strcpy (jugador, p);
			p = strtok(NULL, "/");
			char puntuacion[20];
			strcpy (puntuacion, p);
			char envio[40];
			if (strcmp(puntuacion, "GANADOR") != 0)
			{
				sprintf(envio, "15/%s/%s/%s/", nombre, jugador, puntuacion);
				for (int j= 0;j<lpartidas.partidas[atoi(nombre)].current;j++)
				{
					write(lpartidas.partidas[atoi(nombre)].usuarios[j].socket, envio, strlen(envio));
				}
			}
			else
			{
				
				pthread_mutex_lock(&mutex);
				EstablecerTiempo();
				printf("ENTRO EN 16\n tiempo: %s\n", tiempo);
				pthread_mutex_unlock(&mutex);
				char fecha[40];
				char horas[40];
				char dia[20];
				char mes[20];
				char ano[20];
				
				p = strtok (tiempo, "&");
				strcpy(fecha, p);
				p = strtok (NULL, "&");
				strcpy(horas, p);
				printf ("FECHA: %s y HORA: %s", fecha, horas);
				p = strtok(fecha, "/");
				strcpy(dia, p);
				p = strtok(NULL, "/");
				strcpy(mes, p);
				p = strtok(NULL, "/");
				strcpy (ano, p);
				
				sprintf (fecha, "%s.%s.20%s",dia,mes,ano);
				printf ("FECHA: %s y HORA: %s", fecha, horas);
				
				char mensaje[80];
				sprintf (mensaje, "('%s','%s',20,'%s')", fecha, horas, jugador);
				
				strcpy(consulta, "INSERT INTO GAME (day, hour, matchtime, winner) VALUES ");
				strcat(consulta, mensaje);
				
				
				printf("\tHacemos consulta a mySQL\n");
				
				//hacemos la consulta
				err=mysql_query(conn, consulta);
				if(err!=0)
				{
					printf("Error al consultar datos de la base %u%s\n", mysql_errno(conn), mysql_error(conn));
					exit(1);
					
				}
				
				printf("\tConsulta realizada con exito\n");
				
				sprintf(envio, "16/%s/%s/GANADOR/", nombre, jugador);
				printf("ENVIO: %s \n",envio);
				for (int j= 0;j<lpartidas.partidas[atoi(nombre)].current;j++)
				{
					write(lpartidas.partidas[atoi(nombre)].usuarios[j].socket, envio, strlen(envio));
				}
				
				
			}
		
		}
		if (codigo == 17)
		{
			//10/Quitame/numpart/nombre
			printf("ESTOY EN QUITAME");
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
					soy = i; //lo buscamos para no enviarle el socket
					sock = lpartidas.partidas[indice].usuarios[i].socket;
					pthread_mutex_lock( &mutex);
					lpartidas.partidas[indice].current--;
					pthread_mutex_unlock( &mutex);
					pthread_mutex_lock( &mutex);
					CambioPosiciones(&lpartidas.partidas[indice], i);
					pthread_mutex_unlock( &mutex);
					
					entro = 1; //Si esta en la partida, no lo volvemos a poner
					noenviar = 1; //para no mandarle la lista al desconectado
				}
			}
			
			if (lpartidas.partidas[indice].current == 1) //Solo queda un jugador por lo tanto gana la partida
			{
				char envio[40];
				//17/Eliminado/indicepartida/jugador
				sprintf(envio, "17/Ganas/%d/", indice);
				strcat(envio, lpartidas.partidas[indice].usuarios[0].nombre);
				strcat(envio, "/");
				printf("J=0, Mensaje: %s\n", envio);
				
				
				write(lpartidas.partidas[indice].usuarios[0].socket, envio, strlen(envio));
			}
			else
			{
				
				printf("CURRENT: %d \n", lpartidas.partidas[indice].current);
				for (int j=0; j<lpartidas.partidas[indice].current; j++) //j=1 porque al que invita no se le envia					
				{
					
					
					char send[40];
					//17/Eliminado/indicepartida/jugador
					sprintf(send, "17/Eliminado/%d/", indice);
					strcat(send, eliminado);
					strcat(send, "/");
					printf("J=0, Mensaje: %s\n", send);
					printf("SOCKET: %d\n", lpartidas.partidas[indice].usuarios[j].socket);
					write(lpartidas.partidas[indice].usuarios[j].socket, send, strlen(send));
						
					
			}
			}
			//}
		}
		if (codigo == 18)
		{
			//FUNCION PARA DAR MUÑECO
			char envio[30];
			
			p = strtok(NULL, "/");
			int indice = atoi(p);
			p = strtok(NULL, "/");
			char jugador[30];
			strcpy(jugador, p);
			sprintf(envio, "18/%d/%s/%s/", indice, nombre, jugador); //nombre=CATEGORIA
			for (int j= 0;j<lpartidas.partidas[indice].current;j++)
			{
				write(lpartidas.partidas[indice].usuarios[j].socket, envio, strlen(envio)); 
			}
		}
		if (codigo == 19)
		{
			//funcion vaciar partida
			pthread_mutex_lock(&mutex);
			VaciarPartida(&lpartidas.partidas[atoi(nombre)]);
			pthread_mutex_unlock(&mutex);
		}
		if (codigo == 20) //Queremos Borrar a un jugador
		{
			// 20/user
			printf("\tEntrado en borrar usuario (codigo==20)\n");
			
			strcpy(consulta, "DELETE FROM PLAYER WHERE username ='");
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
			
			strcpy (respuesta, "20/eliminado/");
			
			write(sock_conn, respuesta, strlen(respuesta));
				
			
			printf("\tSe acaba la consulta 20\n"); 
			
			
		}
		
		if (codigo == 99)
		{
			char envio[40];
			strcpy(envio, "99/control/juegocerrado");
			write(sock_conn, envio, strlen(envio));
		}

/*		if (codigo != 0)*/
/*		{*/
			
/*			printf ("Respuesta: %s\n", respuesta);*/
/*			strcat(respuesta, "/");*/
			// Y lo enviamos
/*			write (sock_conn, respuesta, strlen(respuesta));*/
			
			
/*		}*/
/*		if ((codigo == 1) || (codigo = 2) || (codigo = 3) || (codigo = 4) || (codigo = 5) || (codigo = 6) || (codigo = 7) || (codigo = 8) || (codigo = 9) || (codigo = 10) || (codigo = 15) || (codigo = 96) )*/
/*		{*/
/*			pthread_mutex_unlock( &mutex);*/
/*			printf("Funcion general\n");*/
			 //NO me interrumpas ahora
/*			contador ++; */
/*			pthread_mutex_unlock( &mutex);*///Ya puedes interrumpirme
			//notificar a todos los usuarios
/*			char notificacion[20];*/
/*			sprintf (notificacion, "5/%d", contador);*/
/*			for(int j=0; j< i; j++)*/
/*			{*/
/*				write (sockets[j], notificacion, strlen(notificacion));*/
/*				printf("Notificacion: %s\n", notificacion);*/
/*			}*/
/*		}*/
			

		
		
		
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
		//lpartidas.partidas[j].current=0;
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
