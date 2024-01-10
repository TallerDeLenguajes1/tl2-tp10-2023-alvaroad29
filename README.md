# PROYECTO TABLERO KANBAN 


Proyecto MVC de un Tablero Kanban hecho con C# y .NET. Apliqué el patron Repository ademas inyección de dependencias, manejo de excepciones y validaciones de datos. La base de datos es SQLite y está versionado con Git. 

---
- [Instalacion](#instalación)
- [Proyecto](#Proyecto)


## Instalación

### Configuración y ejecución de la aplicación
Para configurar, instalar y ejecutar la aplicación siga estos pasos. Asegurate de tener .NET 7 y SQLiteStudio instalados.

### Cloná el repositorio
Primero, cloná este repositorio en tu máquina local usando el siguiente comando en tu terminal:

git clone https://github.com/TallerDeLenguajes1/tl2-tp10-2023-alvaroad29.git

### Abrí el Proyecto en tu Entorno de Desarrollo (IDE)
Abrí tu entorno de desarrollo preferido y seleccioná "Open Project" (Abrir Proyecto) o su equivalente. Navegá hasta la carpeta del proyecto que acabas de clonar y ábrilo.

### Ejecutá la Aplicación
Una vez realizado esto abre la terminal y ejecuta el proyecto con F5 o en la terminal con el comando
dotnet run.

### Accede a la Aplicación
Una vez que la aplicación se haya iniciado correctamente, abrí un navegador web y ve a la dirección http://localhost:puerto (el puerto figurara en la terminal o se abrira el navegador automaticamente).

### Login
Usuarios y contreseñas para el acceso:

| Usuario | Contraseña | Rol|
|------- | ----- | ---- |
| admin | 1234 | admin |
| operador | 1234 | operador |


## Proyecto

### Funcionalidades
Se puede crear, eliminar y editar tableros, tareas y usuarios.