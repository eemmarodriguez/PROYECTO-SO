DROP DATABASE IF EXISTS datagame;
CREATE DATABASE datagame;

USE datagame;

CREATE TABLE PLAYER (
	username VARCHAR(30) NOT NULL,
	pwd VARCHAR(20) NOT NULL,
	id INT AUTO_INCREMENT,
	PRIMARY KEY (id)
	

)ENGINE=InnoDB;

CREATE TABLE GAME (
	id INT AUTO_INCREMENT,
	day VARCHAR(12),
	hour VARCHAR(10),
	matchtime INT,
	winner VARCHAR(30),
	PRIMARY KEY (id)
)ENGINE=InnoDB;

CREATE TABLE PARTICIPATION (
	ID_G INT NOT NULL,
	FOREIGN KEY (ID_G) REFERENCES GAME (id)
)ENGINE=InnoDB;

CREATE TABLE CONNECT (
	socket INT NOT NULL,
	id_P INT NOT NULL,
	id_C INT AUTO_INCREMENT,
	PRIMARY KEY (id_C),
	FOREIGN KEY (id_P) REFERENCES PLAYER (id)
	
)ENGINE=InnoDB; 

CREATE TABLE DEPORTE (
	pregunta VARCHAR(200),
	respuesta INT NOT NULL,
	opcion1 VARCHAR(50),
	opcion2 VARCHAR(50),
	opcion3 VARCHAR(50),
	opcion4 VARCHAR(50),
	id INT AUTO_INCREMENT,
	PRIMARY KEY(id)

)ENGINE=InnoDB;
CREATE TABLE HISTORIA (
	pregunta VARCHAR(200),
	respuesta INT NOT NULL,
	opcion1 VARCHAR(50),
	opcion2 VARCHAR(50),
	opcion3 VARCHAR(50),
	opcion4 VARCHAR(50),
	id INT AUTO_INCREMENT,
	PRIMARY KEY(id)

)ENGINE=InnoDB;
CREATE TABLE CIENCIA (
	pregunta VARCHAR(200),
	respuesta INT NOT NULL,
	opcion1 VARCHAR(150),
	opcion2 VARCHAR(150),
	opcion3 VARCHAR(150),
	opcion4 VARCHAR(150),
	id INT AUTO_INCREMENT,
	PRIMARY KEY(id)

)ENGINE=InnoDB;
CREATE TABLE ARTE (
	pregunta VARCHAR(200),
	respuesta INT NOT NULL,
	opcion1 VARCHAR(50),
	opcion2 VARCHAR(50),
	opcion3 VARCHAR(50),
	opcion4 VARCHAR(50),
	id INT AUTO_INCREMENT,
	PRIMARY KEY(id)

)ENGINE=InnoDB;
CREATE TABLE ENTRETENIMIENTO (
	pregunta VARCHAR(200),
	respuesta INT NOT NULL,
	opcion1 VARCHAR(50),
	opcion2 VARCHAR(50),
	opcion3 VARCHAR(50),
	opcion4 VARCHAR(50),
	id INT AUTO_INCREMENT,
	PRIMARY KEY(id)

)ENGINE=InnoDB;
CREATE TABLE GEOGRAFIA (
	pregunta VARCHAR(200),
	respuesta INT NOT NULL,
	opcion1 VARCHAR(50),
	opcion2 VARCHAR(50),
	opcion3 VARCHAR(50),
	opcion4 VARCHAR(50),
	id INT AUTO_INCREMENT,
	PRIMARY KEY(id)

)ENGINE=InnoDB;


INSERT INTO PLAYER (username, pwd, id) VALUES ('admin', 'admin', 1);
INSERT INTO PLAYER (username, pwd) VALUES ('Cris', '1234');
INSERT INTO PLAYER (username, pwd) VALUES ('Davis', '4321');
INSERT INTO PLAYER (username, pwd) VALUES ('Ems', '5678');

INSERT INTO GAME (day, hour, matchtime, winner) VALUES ('15.10.2020','12:43', 20, 'Cris');
INSERT INTO GAME (day, hour, matchtime, winner) VALUES ('15.10.2020','13:10', 10, 'Davis');
INSERT INTO GAME (day, hour, matchtime, winner) VALUES ('16.10.2020','13:30', 15, 'Ems');
INSERT INTO GAME (day, hour, matchtime, winner) VALUES ('17.10.2020','13:30', 5, 'Davis');
INSERT INTO GAME (day, hour, matchtime, winner) VALUES ('17.10.2020','18:30', 20, 'Davis');
INSERT INTO GAME (day, hour, matchtime, winner) VALUES ('17.10.2020','20:30', 12, 'Cris');


INSERT INTO PARTICIPATION VALUES (1);
INSERT INTO PARTICIPATION VALUES (1);
INSERT INTO PARTICIPATION VALUES (2);
INSERT INTO PARTICIPATION VALUES (2);
INSERT INTO PARTICIPATION VALUES (3);
INSERT INTO PARTICIPATION VALUES (3);
INSERT INTO PARTICIPATION VALUES (3);
INSERT INTO PARTICIPATION VALUES (4);
INSERT INTO PARTICIPATION VALUES (4);
INSERT INTO PARTICIPATION VALUES (5);
INSERT INTO PARTICIPATION VALUES (5);
INSERT INTO PARTICIPATION VALUES (6);
INSERT INTO PARTICIPATION VALUES (6);

INSERT INTO CONNECT (socket, id_P) VALUES (1, 1); 


INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual fue el primer pais en aprobar el sufragio femenino?', 3, 'Alemania', 'Japon', 'Nueva Zelanda', 'Luxemburgo');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien fue el primer presidente de la democracia espanola tras el franquismo?', 2, 'Felipe Gonzalez', 'Adolfo Suarez', 'Jose Maria Aznar', 'Manuel Fraga');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('La invasion de que fortaleza por parte de los revolucionarios es considerada como el punto de inicio de la Revolucion Francesa?', 4, 'Louvre', 'Versalles', 'Chateau', 'Bastille');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano el hombre piso la Luna por primera vez?', 1, '1969', '1875', '2000', '1975');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien fue el primer presidente de Estados Unidos?', 2, 'Frankling D. Roosevelt', 'George Washington', 'Abraham Licoln', 'Donald Trump');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuanto duro la Guerra de los Cien Anos?', 3, '100', '132', '116', '118');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano se creo la Organizacion de las Naciones Unidas?', 4, '1936', '1830', '1940', '1945');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que carabela no regreso del viaje en el que Colon llego a America?', 1, 'Santa Maria', 'Pinta', 'La Nina', 'Volvieron todas');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se apellidaban los dos exploradores que dieron la primera vuelta al mundo?', 2, 'Magallanes-Vespucci', 'Magallanes-Elcano', 'Vespucci-Elcano', 'Elcano-Colon');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que filosofo de la Antigua Grecia creia que el elemento del que estan compuestas todas las cosas es el agua?', 2, 'Epicuro', 'Tales de Mileto', 'Aristoteles', 'Socrates');



INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que isla sirvio de prision para Napoleon tras su derrota en la batalla de Waterloo?', 3, 'Mallorca', 'Sicilia', 'Santa Helena', 'Capri');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quienes fueron, segun la leyenda, los dos hermanos fundadores de la ciudad de Roma?', 2, 'Remo y Pancho', 'Romulo y Remo', 'Rucula y Ricardo', 'Rostulo y Remulo');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano colon descubrio america?', 3, '1462', '1478', '1492', '1542');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que guerra participo Juana de Arco?', 2, 'Primera Cruzada', 'La guerra de los 100 anos', 'La guerra de los 30 anos', 'Guerras napoleonicas');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la rama mayoritaria del Islam?', 4, 'Chiismo', 'Jariyismo', 'Sufismo', 'Sunismo');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Capital del imperio Inca', 4, 'Quito', 'Machu Picchu', 'Lima', 'Cuzco');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuando se produjo principalmente el siglo de Oro?', 1, 'siglo XVI', 'siglo XVII', 'siglo XV', 'siglo XIV');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llamaba el padre de Alejandro Magno?', 2, 'Ptolomeo I', 'Filipo II de Macedonia', 'Leonidas', 'Flipo I de Grecia');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De que ano es la pepa, la primera constitucion espanola?', 4, '1806', '1822', '1802', '1812');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien fue el primer emperador romano?', 3, 'Julio Cesar', 'Neron', 'Cesar Augusto', 'Caligula');


INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que Rey encargo Las Meninas?', 3, 'Felipe III', 'Carlos III', 'Felipe IV', 'Felipe II');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quienes fueron, segun la leyenda, los dos hermanos fundadores de la ciudad de Roma?', 2, 'Remo y Pancho', 'Romulo y Remo', 'Rucula y Ricardo', 'Rostulo y Remulo');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llamaba el canon aleman mas famoso de la I Guerra Mundial?', 1, 'Gran Berta', 'Luftwaffe', 'Mackensen M- 14', 'Enola Gay');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que pais nacio Adolf Hitler?', 4, 'Alemania', 'Suiza', 'Polonia', 'Austria');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Por que condeno la iglesia a Galileo Galilei?', 3, 'Por decir que la tierra era redonda', 'Por negar la existencia de Dios', 'Por decir que la tierra giraba alrededor del sol', 'Por decir que la tierra era el centro del universo');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que faraon era el marido de Nefertiti?', 4, 'Ramses II', 'Ramses I', 'Tuntankamon', 'Akenaton');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llamo durante mas de 50 anos San Petersburgo?', 1, 'Lenningrado', 'Petroburgo', 'Trotskigrado', 'Stalingrado');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que isla murio Napoleon?', 2, 'Cerdena', 'Corcega', 'Santa Elena', 'Elba');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quienes lucharon en la batalla de Maraton? ', 4, 'Los griegos y los espartanos', 'Los romanos y los cartagineses', 'Los griegos y los egipcios', 'Los griegos y los persas');
INSERT INTO HISTORIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano cayo el imperio romano de occidente?', 3, '456', '501', '476', '496');





INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la velocidad de la luz?', 3, '3.000.000', '250.000.000', '300.000.000', '2.500.000.000');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que gas nos protege de la radiacion solar, concretamente de la radiacion ultravioleta, formando una capa en la atmosfera?', 1, 'Ozono', 'Nitrogeno', 'Hidrogeno', 'Oxigeno');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el nombre tecnico del miedo o fobia a las alturas?', 2, 'agorafobia', 'acrofobia', 'aviatofobia', 'claustrofobia');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('La formula E=mc2, en que teoria cientifica aparece?', 4, 'Ley de keppler', 'Principio de incertidumbre', 'Leyes de Newton', 'La relatividad');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el principal tipo de celula que forma parte del sistema nervioso de humanos y otros animales?', 2, 'axioma', 'neurona', 'golgi', 'mitocondria');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Por que fue famosa Marie Curie?', 4, 'invento el transformador', 'descubrio la bombilla', 'resolvio el problema de Fermat', 'descubrio la radioactividad');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el ave rapaz que se alimenta fundamentalmente de huesos?', 1, 'buitre', 'halcon', 'aguila', 'paloma');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que especie de anfibio es conocida por su increible capacidad para regenerar partes de su cuerpo que han sido rotas o amputadas?', 3, 'asqueoplo', 'rana', 'ajolote', 'ispetlopo');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuales son las bases nitrogenadas del ADN?', 1, 'guanina, la adenina, la timina y la citosina', 'guanina, la adenina, la timina y uracilo', 'guanina, uracilo, la timina y la citosina', 'uracilo, la adenina, la timina y la citosina');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Alrededor de que planeta orbitan los satelites Ganimedes, Calisto, Io y Europa?', 1, 'Jupiter', 'Saturno', 'Marte', 'Urano');


INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Con que denominacion se conoce la linea dibujada por las estrellas Alnitak, Alnilam y Mintaka vistas desde nuestro planeta?', 2, 'Osa mayor', 'Orion', 'Tauro', 'Leo');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De que color es la sangre de los peces?', 3, 'Verde oscuro', 'Marron oscuro', 'Rojo', 'Azul');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que nombre recibe el sistema de transcripcion fonetica usado en el chino mandarin?', 2, 'payun', 'pinyin', 'panyun', 'pionjan');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que periodo de la era paleozoica tuvo lugar entre el Devonico y el Permico?', 4, 'Empezo hace 9 millones de anos y acabo hace 2 millones', 'Empezo hace 1500 millones de anos y acabo hace 600 millones', 'Empezo hace 539 millones de anos y acabo hace 199 millones', 'Empezo hace 359 millones de anos y acabo hace 299 millones');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de los siguientes organos NO es parte del sistema inmunologico?', 2, 'Medula Osea', 'Esofago', 'Bazo', 'Timo');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantas caras tiene un icosaedro?', 3, '16', '8', '20', '24');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que es el calostro?', 1, 'La primera leche materna', 'Una hormona', 'Una parte del intestino grueso', 'Un hueso de la espina dorsal');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el hueso mas diminuto del cuerpo?', 3, 'El yunque', 'La falange', 'El estribo', 'Ninguna es correcta');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Donde vive el delfin rosado?', 4, 'Solo en aguas calidas', 'Solo en aguas muy frias', 'En Oceania', 'En Brasil');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que estudia la icitologia?', 1, 'Los peces', 'Cachalote', 'Elefante', 'Rinoceronte');




INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que es un equino?', 2, 'Una vaca', 'Un caballo', 'Un antilope', 'Una oveja');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama la planta a partir de la cual suele ser elaborado el tequila?', 1, 'Agave', 'Amapola', 'Girasol', 'Lavanda');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantas cavidades estomacales tiene una vaca?', 3, 'Tres', 'Una', 'Cuatro', 'Dos');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Donde estan los meniscos?', 4, 'En el perone', 'En los codos', 'En la punta del pie', 'En las rodillas');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Por que los cocodrilos mantienen la boca abierta durante largo rato?', 2, 'Para hacer la digestion', 'Para calentarse', 'Para beber agua', 'Por si se cuela algo que puedan comerse');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que funcion cumplen los bigotes del gato?', 4, 'Son un organo auditivo', 'Son un organo olfativo', 'Son de adorno', 'Son un organo tactil');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que sustancias se liberan en una combustion completa?', 1, 'Dioxido de carbono y agua ', 'Carbono, oxigeno y agua', 'Monoxido de carbono y agua', 'Solamente agua');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la raiz cuadrada del 169?', 3, '15', '14', '13', '17');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De cual de estas plantas se extrae la marihuana?', 2, 'Violeta africana', 'Cannabis', 'Girasol', 'Amapola');
INSERT INTO CIENCIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que hueso se encuentra en el pene de la mayoria de mamiferos?', 1, 'Braculo', 'Penioforme', 'Pitoideo', 'Ninguno');

INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano se estreno la pelicula de Disney Pinocho?', 1,  '1940', '1942', '1950', '1955');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el personaje que interpreta Al Pacino en Scarface?', 2, 'Sonny Montana', 'Tony Montana', 'Michael Corleone', 'Frank Slade');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Por cual de estas peliculas gano Clint Eastwood el premio al mejor director?', 4, 'Los puentes de Madison', 'Cartas de Iwo Jima', 'Mystic River', 'Million Dollar Baby');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que animal es la mascota de Jazmin en Aladdin?', 2, 'Elefante','Tigre','Mono','Serpiente');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('A quien se considera el Rey del Pop?', 1, 'Michael Jackson','Elvis Presley','Justin Bieber','Zac Efron');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que actor ha hecho mas peliculas como James Bond?', 4, 'Pierce Brosnan','Sean Connery','Daniel Craig','Roger Moore');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que Beatle fue asesinado por un fan?', 3, 'George Harrison','Ninguno','John Lennon','Ringo Star');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De que distrito es Katniss de Los juegos del hambre?', 2, 'Distrito 3','Distrito 12','Distrito 8','Distrito 11');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el payaso de Los Simpson?|', 1, 'Krusty','Kristy','Kris','Kroger');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('A que se dedica el personaje Ted Mosby de  Como conoci a vuestra madre?', 2, 'Abogado','Arquitecto','Publicista','Periodista');

INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('A que familia pertenece Dobby,el elfo domestico en la historia de Harry Potter?', 3, 'Weasley','Potter','Malfoy','Black');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano se fundo The Walt Disney Company?', 4, '1972','186','1932','1923');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la nacionalidad de la cantante Shakira?', 1, 'Colombiana','Venezolana','Ecuatoriana','Peruana');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que es Assassins Creed?', 2, 'Un disco','Un videojuego','Una banda','Una marca de ropa');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el planeta de origen del maestro Yoda?', 3, 'Hoth','Tatooine','Dagobah','Naboo');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien resulta ser el principe mestizo en la sexta pelicula de Harry Potter?', 3, 'Lord Voldemort','Draco Malfoy','Severus Snape','Albus Dumbledore');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llamaba el primer gato de Los Simpsons?', 2, 'Bolita','Bolita de nieve','Nieve','Gato');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantos anos tenia Mozart al morir?', 1, '35','45','96','54');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ciudad tiene lugar la serie The Office?', 4, 'Stamford','Buffalo','Utica','Scranton');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es el actor vampiro protagonista de la saga Crepusculo?', 2, 'Taylor Lautner','Robert Pattinson','Danny De Vito','Kristen Stewart');

INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano se fundo Twitter?', 1, '2006','2004','2005','2007');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el protagonista de la serie The Office?', 2, 'Michael Scarn','Michael Scott','Michael Klump','Michael Scofield');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De cuantas partes se compone la saga de El Padrino?', 3, '4','2','3','5');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que saga de videojuegos aparece la entrega Ocarina Of Time?', 2, 'Pokemon','Legend of Zelda','Metal Gear','Grand Theft Auto');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que programa sale Sheldon Cooper?', 1, 'The Big Bang Theory','Friends','How I Met Your Mother','Two and a Half Men');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el vocalista de los Guns N Roses?', 1, 'Axl Rose','Duff McKagan','Slash','Billie Joe Amstrong');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es la mascota de SEGA?', 1, 'Sonic','Mario','Pac Man','Ryu');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien dirigio la pelicula los lunes al sol?', 4, 'Pedro Almodovar','Jose Luis Garcia','Alejandro Amenabar','Fernando Leon de Aranoa');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama el protagonista de la saga Indiana Jones?', 3, 'Jack Nicholson','Michael Fox','Harrison Ford','Robin Williams');
INSERT INTO ENTRETENIMIENTO (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual era el apellido original de Luke en Star Wars Episodio IV antes de ser bautizado como Skywalker?', 2, 'Organa','Starkiller','Dameron','Skyfort');


INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el codigo internacional para Cuba?', 2, 'CA','CU','CH','CB');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la capital del estado de Arkansas?', 2, 'Denver','Little Rock','Boise','Topeka');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de estas caracteristicas no pertenece al clima Mediterraneo?', 3, 'Veranos secos y calurosos','Clima templado','Lluvias muy abundantes','Variables temperaturas en primavera');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que es el Cabo de Creus?', 1, 'El punto mas oriental de la Peninsula','El punto mas occidental de Cataluna','El punto mas oriental de Espana','Ninguna es correcta');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Con cuantos paises limita Argentina?', 3, 'Tres','Cuatro','Cinco','Seis');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la capital de Suiza?', 4, 'Zurich','Ginebra','Basilea','Berna');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que pais esta Ushuaia, la ciudad mas al sur del mundo?', 2, 'Chile','Argentina','Sudafrica','Nueva Zelanda');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de estos paises africanos no tiene costa?', 4, 'Mauritania','Senegal','Gambia','Todas tienen costa');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que accidente geografico se define como un conjunto de islas, islotes y diminutas masas de tierra cercanas entre si?', 3, 'Islotes','Peninsula','Archipielago','Meandro');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Donde esta el desierto del Gobi?', 1, 'Al sur de Mongolia','Al sur de China','Al sur de Nepal','Ninguna es correcta');

INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuando es verano en el hemisferio sur?', 1, 'De diciembre a marzo','De marzo a junio','De junio a septiembre','De septiembre a diciembre');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el oceano mas profundo?', 2, 'Atlantico','Pacifico','Indico','Artico');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuales son los colores de la bandera de Bolivia?', 4, 'Verde, azul y amarillo','Rojo, azul y amarillo','Amarillo, verde y blanco','Rojo, amarillo y verde');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Donde se encuentra el Cabo de Gata?', 2, 'Cadiz','Almeria','Huelva','Sevilla');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el rio mas largo de Europa?', 4, 'Danubio','Ural','Ebro','Volga');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De cuantas islas consta el archipielago canario?', 3, '5','8','7','9');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la montana mas alta de la Peninsula Iberica?', 3, 'Aneto','Teide','Mulhacen','Vinamala');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Donde se encuentra el monte Vesubio?', 2, 'Espana','Italia','Francia','Mexico');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantas comunidades autonomas hay en Espana?', 2, '16','17','14','12');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el rio mas caudaloso del mundo?', 4, 'Nilo','Orinoco','Volga','Amazonas');

INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la capital de Hungria?', 3, 'Sofia','Kiev','Budapest','Bucarest');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que pais se encuentra la mayor cantidad del mundo de oro y diamantes?', 1, 'Sudafrica','Brasil','China','Rusia');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el pais de origen de la galletita pretzel?', 2, 'Austria','Alemania','Dinamarca','Italia');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que cataratas separan Brasil y Argentina?', 2, 'Cataratas del Niagara','Cataratas del Iguazu','Cataratas del Paraiso','Cataratas del Krka');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es la moneda oficial de Costa Rica?', 1, 'Colon costarricense','Dolar costarricense','Lempira','Quetzal');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De que pais el panda es el animal nacional?', 4, 'Canada','Tanzania','Australia','China');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de estas ciudades no es de Argentina?', 1,'Cali','Cordoba','Rosario','Mendoza');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de los siguientes paises no es una isla?', 4, 'Sri Lanka','Taiwan','Madagascar','Todas son islas');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llamaba la gran masa de tierra que formaba un solo gran continente?', 2, 'Atlas','Pangea','Tierra','Continental');
INSERT INTO GEOGRAFIA (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de estas provincias NO tiene costa?', 1, 'Toledo','Lugo','Girona','Huelva');


INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien pinto el cuadro El jardin de las delicias?', 1, 'El Bosco','Cavaggio','Velazquez','Arcimboldo');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien vivia en el 221B de Backer Street?', 3, 'Truman Capote','Philip Marlowe','Sherlock Holmes','Arthur Conan Doyle');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que profesion tenia Hercules Poiriot en las novelas de Agatha Christie?', 2, 'Policia','Medico','Detective',' Bombero');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de los siguientes artistas es una figura clave del dadaismo y el surrealismo?', 1, 'Max Ernst','Claude Monet','Vincent Van Gogh','Rafael');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es el autor de la obra teatral Casa de Muñecas?', 4, 'Sigrid Undset','Emile Zola','Isaac Asimov','Henrik Ibsen');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que era Le Chat Noir, el establecimiento que anunciaban los carteles el famoso gato negro de Paris?', 2, 'Un restaurante','Un Cabaret','Un cine','Una biblioteca');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Los amantes de Teruel...', 3, 'Existieron en papel','Viajaban en carrusel','Tonta ella y tonto el','Vivian en un burdel');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es el autor de El retrato de Dorian Gray?', 2, 'Charles Dickens','Oscar Wilde','Arthur Conan Doyle','George Orwell');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es el autor de Moby Dick?', 1, 'Herman Melville','Henry David Thoreau','Ralph Waldo Emerson','Henry James');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien introdujo la lira en la lirica espanola?', 4, 'Luis de Gongora','Lope de Vega','Miguel de Cervantes','Garcilaso de la Vega');

INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual fue el genero mas cultivado por los autores de la generacion del 27?', 4,'Teatro','Novela','Ensayo','Poesia');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que escritor espanol firmaba como Figaro?', 1, 'Mariano Jose de Larra','Federico Garcia Lorca','Francisco de Quevedo','Antonio Machado');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama a la gente que no posee magia en la saga de Harry Potter?', 3, 'Impuro','Simplon','Muggles','Humano');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Gato con guantes...', 2, 'Y con botas','No caza ratones','No rasca bigotes','No corre al trote');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Para que sirve la paleta?', 4, 'Para hacer los deberes sobre ella','Para romperla','Para pintar sobre ella','Para mezclar pinturas');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('¿Que forma es caracteristica de las plantas de las iglesias romanicas?', 3, 'Cruz','Ovalo','Rectangulo','Cuadrado');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('La autoria de la pintura El Coloso se ha puesto muy en duda en los ultimos anos. A quien se le atribuye tradicionalmente?', 1, 'Goya','Velazquez','Sorolla','Quevedo');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que esta esculpida la Venus de Milo?', 1, 'Marmol','Bronce','Piedra','Madera');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien fue Antonio Lucio Vivaldi?', 4, 'Guitarrista Clasico','Tenor de Opera','Clavesista Italiano','Violinista y Compositor del Barroco');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien hizo la obra La vida es sueno?', 3, 'Tirso de Molina','Feliz Lope de Vega','Calderon de la Barca','Miguel de Cervantes');

INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien pinto La Capilla Sixtina?', 2, 'Giorgio Vasari','Miguel Angel','Leonardo Da Vinci','Tiziano');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que filosofo dijo solo se que no se nada?', 2, 'Heraclito','Socrates','Platon','Tales de Mileto');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es la autora de Los Juegos del Hambre?', 3, 'Blue Jeans','Jordi Sierra i Fabra','Suzanne Collins','Bono Bidari');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es la autor del libro El gato negro y otros cuentos de terror?', 4, 'Manuel Broncano','Julio-Cesar Santoyo','Jesus Gaban','Edgar Allan Poe');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De que siglo es representativo el arte mozarabe?', 1, 'X','VII','V','XIX');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('A quien esta dedicada la torre mas alta de la Sagrada Familia de Gaudi, aun pendiente de construccion?', 2, 'La Sagrada Familia','Jesucristo','La Virgen Maria','El Espiritu Santo');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien es el autor de La Celestina?', 1, 'Fernando de Rojas','Jorge Manrique ','Miguel de Cervantes ','Anonima');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que artista aparece retratado en Las Meninas?', 4, 'Miguel Angel','Sorolla','Goya','Velazquez');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien pinto Galatea de las esferas?', 3, 'William Turner','Monet','Salvador Dali','Goya');
INSERT INTO ARTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien escribio Coplas por la muerte de su padre?', 1, 'Jorge Manrique','Boccaccio','Antonio de Nebrija','Juan de Mena');


INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantos mangos por lado tiene el futbolin?', 1, 'Cuatro','Dos','Tres','Cinco');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el estilo de natacion mas rapido?', 3, 'Espalda','Mariposa','Crol','Pecho');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantos jugadores componen un equipo de rugby?', 2, '11','15','12','21');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que pais se invento el voleibol?', 2, 'Gran Bretana','Estados Unidos','Francia','Rusia');
INSERT INTO DEPORTE(pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de las siguientes modalidades no forma parte del deporte rural vasco?', 4, 'Arrastre de piedra','Lanzamiento de fardo','Corte de troncos','Desintegramiento de piedra');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantos puntos vale un tiro libre encestado en baloncesto?', 3, 'Depende','3','1','2');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuanto dura un partido de futbol?', 1, '90 minutos','45 minutos','75 minutos','80 minutos');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el derbi mas esperado en Andalucia?', 4, 'Real Madrid vs At. Madrid','Real Madrid vs Sevilla','Granada vs Malaga','Betis vs Sevilla');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama la zona de hierba rasa donde esta ubicado el hoyo en golf?', 3, 'Esplanada','Campo','Green','Terreno');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de estos pilotos no es de F1?', 1, 'Richard Petty','Fernando Alonso','Sebastian Vettel','Christian Kiel');

INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el pais de origen del futbolista Lionel Messi?', 2, 'Espana','Argentina','Brasil','Chile');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama la cantera del FC Barcelona?', 3, 'El Chalet','La Fabrica','La Masia','La Depuradora');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual de los siguientes es un truco de skate?', 4, 'Mollie','Clop','Superclop','Ollie');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien gano el mundial de futbol del ano 2002?', 2, 'Italia','Brasil','Espana','Francia');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En cual de estas situaciones se sacara una bandera amarilla, o incluso roja, en las carreras de coches?', 1, 'Un peligro en la pista','Se ha declarado un ganador','Queda una vuelta','La carrera va a empezar');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Donde se invento el tenis de mesa?', 4, 'China','Japon','Korea del Sur','Inglaterra');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que jugador frances ha ganado un balon de oro mientras jugaba en Italia?', 2, 'Platini','Zidane
','Ribery','Maradona');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De donde viene el jugador Alexis Sanchez?', 4, 'India','Espana','Moscu','Chile');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien quedo numero 1 del mundo de tenis en 2008?', 3, 'Roger Federer','Novak Djokovic','Rafael Nadal','Carlos Moya');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cual es el club de futbol mas antiguo de Espana?', 2, 'Athletic Club de Bilbao','Recreativo de Huelva','F.C. Barcelona','Real Madrid');

INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuanto dura un partido de balonmano?', 3, '90 minutos','30 minutos','60 minutos','45 minutos');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantos cuadros tiene un tablero de ajedrez?', 1, '64','54','36','81');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Como se llama la liga espanola de balonmano?', 4, 'Liga Balonmano','Balonbal','Abobal','Asobal');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('De que tipo eran las 3 medallas conseguidas por Usain Bolt en los Juegos Olimpicos de Londres 2012?', 2, 'Plata ','Oro ','Bronce','No consiguio ninguna');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que clase de peso en el boxeo esta entre el peso gallo y el peso ligero?', 1, 'Peso pluma','Sobrepeso','Peso mosca','Peso ideal');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ciudad sudafricana gano Espana su unico mundial de futbol?', 3, 'Bloemfontein','Ciudad del Cabo ','Johanesburgo ','Pretoria ');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('En que ano se celebraron los primeros juegos olimpicos de verano en Atenas?', 4, '1888','1904','1932','1896');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Cuantos titulos mundiales posee el motociclista Valentino Rossi?', 1, '9
','2','5','7');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Que arte marcial utiliza mayor porcentaje de extremidades inferiores?', 2, 'Judo','Taekwondo ','Karate ','Boxeo');
INSERT INTO DEPORTE (pregunta, respuesta, opcion1, opcion2, opcion3, opcion4) VALUES ('Quien gano dos veces tres campeonatos consecutivamente en la NBA?', 3, 'Lebron James','Brian Scalabrine','Michael Jordan','Bill Russel');
