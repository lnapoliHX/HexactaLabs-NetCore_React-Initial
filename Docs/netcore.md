[Volver](./index.md)

# NET Core

## Introducción

ASP.NET Core es un Framework de código abierto creado por Microsoft, que nos proporciona herramientas para el desarrollo de aplicaciones multiplataforma, lo que significa que podemos desarrollar aplicaciones Web y hospedarlas en servidores Windows, Linux o macOS.
[NetCore Docs](https://docs.microsoft.com/es-es/dotnet/core/)

## ¿Qué es una API?

> Una API (Application Programming Interface) es la interfaz que un software utiliza para interactuar con otro software.
Un Web API es un API que se invoca a través del protocolo HTTP.  Se detalla solamente la forma en que cada rutina debe ser llevada a cabo y la funcionalidad que brinda, sin otorgar información acerca de cómo se lleva a cabo la tarea. La ventaja de usar HTTP es que es posible hacer peticiones desde cualquier lenguaje de programación, lo que hace a la Web un medio ideal para conectar aplicaciones. Para gestionar las solicitudes, una API web usa Controllers.

## Tipos de Clases:

### Controller

- Un controller maneja requests HTTP a una ruta determinada y devuelve responses al llamador. 
Microsoft.AspNetCore.Mvc proporciona atributos que se pueden usar para configurar el comportamiento de los controladores API web y los métodos de acción, como por ejemplo especificar el verbo de acción HTTP admitido y cualquier código de estado HTTP conocido que se pueda devolver. Contiene logica de la aplicación y pasa la información del usuario al servicio.

### Service

- Es un middleware entre el control y el repositorio. Toma data del controller, hace validaciones y logica de negocio y llama al repositorio para que manipule la información.

### Repository
- Capa de interaccion con el modelo que performa operaciones con la base de datos. Generalmente incluyen las funcionalidades de ABM.
Puede implementarse como un repositorio generalizado.

### Model

- Es la representación de la información de negocio con la cual el sistema opera.

### DTO

- "Data transfer object" por sus siglas en inglés, es un objeto simple que va a ser transferido del cliente al servidor o viceversa, representa a un objeto de negocio (Model), pero debe ser de solo lectura.

### Mapper

- Es utilizado para mapear un modelo de negocio a un DTO o viceversa.

La [Estructura de Carpetas](./estructura-carpetas-netcore.md) sigue la lógica de separar los elementos antes mencionados.

## Referencias

[Documentación Oficial Net Core](https://docs.microsoft.com/es-es/dotnet/core/)
[Documentación Oficial Web API](https://docs.microsoft.com/en-us/aspnet/web-api/)
[Documentación Oficial MVC](https://docs.microsoft.com/en-us/aspnet/mvc/)