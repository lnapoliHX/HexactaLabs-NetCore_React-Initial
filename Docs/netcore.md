[Volver](./index.md)

# NET Core

## Introducción

[NET Core](https://docs.microsoft.com/es-es/dotnet/core/) es un Framework de código abierto creado por Microsoft, que nos proporciona herramientas para el desarrollo de aplicaciones multiplataforma .lo que significa que podemos desarrollar y ejecutar una aplicación en varios sistemas operativos, como Linux, Windows, iOS. Se pueden desarrollar en .Net Core: aplicaciones de escritorio, aplicaciones web, aplicaciones móviles, juegos, inteligencia artificial, etc.

[ASP.NET Core](https://docs.microsoft.com/es-es/aspnet/core/?view=aspnetcore-3.1) es un marco de trabajo para desarrollar aplicaciones web multiplataforma compilado con .Net Core.


### ¿Qué es una API?

Una API (Application Programming Interface) es la interfaz que un software utiliza para interactuar con otro software.

Un Web API es un API que se invoca a través del protocolo HTTP.  Se detalla solamente la forma en que cada rutina debe ser llevada a cabo y la funcionalidad que brinda, sin otorgar información acerca de cómo se lleva a cabo la tarea. La ventaja de usar HTTP es que es posible hacer peticiones desde cualquier lenguaje de programación, lo que hace a la Web un medio ideal para conectar aplicaciones. Para gestionar las solicitudes, una API web usa Controllers.

Netcore posee un framework de trabajo para desarrollar las APIs. [Link](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio)

### Inyección de Dependencias
Es una técnica en la que un objeto suministra las dependencias de otro. Está tecnica nos permite evitar la creación de objetos de servicio, para lograr legibilidad y reuso de código.
La inyección de dependencias es una de las maneras de implementar [Inversión de Control o IoC](https://en.wikipedia.org/wiki/Inversion_of_control).

Netcore posee soporte nativo para Inyección de Dependencias, el cual es utilizado en este proyecto. [Link](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1)


## Tipos de Clases
Se detallan las clases generales que se encuentran en el backend.

### Controller

- Un controller maneja requests HTTP a una ruta determinada y devuelve responses al llamador. 
Microsoft.AspNetCore.Mvc proporciona atributos que se pueden usar para configurar el comportamiento de los controladores API web y los métodos de acción, como por ejemplo especificar el verbo de acción HTTP admitido y cualquier código de estado HTTP conocido que se pueda devolver. Contiene lógica de la aplicación y pasa la información del usuario al servicio.

### Service

- Es un middleware entre el controller y el repositorio. Toma data del controller, hace validaciones y lógica de negocio y llama al repositorio para que manipule la información.

### Repository
- Capa de interaccion con el modelo que performa operaciones con la base de datos. Es el componente que encapsula la lógica para acceder a la base de datos. 
[Link](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)


### Model
- Es la representación de la información de negocio con la cual el sistema opera.
- Es un objeto del dominio que encapsula comportamiento y datos. Cuando encapsula poco comportamiento recibe el nombre de "modelo anémico". 
[Domain Model](https://martinfowler.com/eaaCatalog/domainModel.html)  
[Anemic Domain Model](https://martinfowler.com/bliki/AnemicDomainModel.html)

### DTO

- "Data transfer object" por sus siglas en inglés, es un objeto simple que transporta datos. Por ejemplo, puede ser utilizado cuando hay una transferencia del cliente al servidor o viceversa. Se utiliza para reducir el número de llamadas a la hora de retornar objetos del modelo, es decir son objetos que no poseen comportamientos y los mismos pueden agrupar varios modelos.
[DTO](https://martinfowler.com/eaaCatalog/dataTransferObject.html)

### Mapper
- Es utilizado para mapear un modelo de negocio a un DTO o viceversa.

### Notas
La [Estructura de Carpetas](./estructura-carpetas-netcore.md) sigue la lógica de separar los elementos antes mencionados.
Normalmente este esquema recibe el nombre de [N-Layer](https://es.wikipedia.org/wiki/Programaci%C3%B3n_por_capas)

## Referencias
[Documentación Oficial Net Core](https://docs.microsoft.com/es-es/dotnet/core/)   
[Documentación Oficial Web API](https://docs.microsoft.com/en-us/aspnet/web-api/)   
[Documentación Oficial MVC](https://docs.microsoft.com/en-us/aspnet/mvc/)   
