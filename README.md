Proyecto de WEB App para consumir datos de Tareas y Colaboradores desde un servicio WebAPI

- Se crea un proyecto ASP.NET Core MVC para gestionar la presentcion de los datos de dos tablas (tarea y colaborador) de una base de datos en SQL SERVER, utilizando un WebAPI para consumir los datos.
	
- La estructura del proyecto queda de la siguiente manera:
	- NCQ.Tareas.WebApp: nombre del proyecto y la solución.
		- Controllers: carpeta con las clases de los controladores con las acciones para cada una de las rutas de la aplicacion: 
		GET-POST-PUT-DELETE.
			- TareaController: 
				- Index: HTTPGET: Listado de tareas, permite realizar filtros sobre la informacion listada
				- Crear: HTTPGET / HTTPPOST: creación de tareas.
				- Modificar: HTTPGET / HTTPOST: modificacion de información de tareas
				- Eliminar / EliminarConfirm: HTTPGET / HTTPPOST Eliminación de información, pantalla de confirmacion.
				
			- ColaboradorController: acciones para la gestion de datos de colaboradores
				- Index: HTTPGET: Listado de colaboadores, Posee busquda por texto del Nombre
				- Crear: HTTPGET / HTTPPOST: creación de colaboradores.
				- Modificar: HTTPGET / HTTPOST: modificacion de información de colaboradores
				- Eliminar / EliminarConfirm: HTTPGET / HTTPPOST Eliminación de información de colaboradores, pantalla de confirmacion.
		- Services
			- Interfaces:
				- IConsumirApiService: contiene la definicion de métodos para interactuar con el API.
			- ConsumirApiService: clase que contiene la implementación de los métodos para interactuar con el API
		- Modelos:
			- ViewModels
				- TareaVM: View Model para la tabla Tarea y poder manejar las descripciones de campos relalcionados y lista de vlores
			- Colaborador: clase que define la estructura de la tabla Colaborador de la Base de Datos.
			- Tarea: clase que define la estructura de la tabla Tarea de la Base de Datos.
		- AppSettings.cs: clase para obtener la direccion base de la API, configurda en el appsettings.json.
		- appsettings.json:
			- Contiene entrada "ApiBaseUrl" que contiene la direccion base de la API.
			 
		- Program.cs
			- Se obtiene la entrada de configuracion a la clase appsettings para poder inyectarla en el ConsumirApiService y obtener la url base para llamar al api.
			- Se agrega el servicio del repositorio para consumir el API (ConsumirApiService) y usar en los controladores y poder inyectarlo.