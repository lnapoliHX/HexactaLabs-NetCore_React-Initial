[Volver](./index.md)

# Estructura de carpetas

## Root:

    Stock.Api
    Stock.AppService
    Stock.Model
    Stock.Repository.LiteDb

- Stock.Api: Nuestra aplicación propiamente dicha, ésta consume de las otras 3 librerías.
- Stock.AppService: Librería que contiene los servicios
- Stock.Model: Librería que contiene los modelos
- Stock.Repository.LiteDb: Librería que contiene el repositorio genérico, y la interfaz que utilizaremos como base de datos.

## Stock.Api:

      |- Controllers
      |- DTOs
      |- Exceptions
      |- Extensions
      |- MapperProfiles
      Program.cs
      Startup.cs

- Cada una de éstas carpetas contiene clases de su tipo. 
- Program.cs es donde se crea el Host de la aplicación.
- Startup.cs es donde se registran y configuran todos los servicios y repositorios que utilizaremos entre otras cosas.

## Stock.AppService

      |- Base
      |- Service
      |- Settings

 - Base: Donde se define el servicio base genérico
 - Service: Donde están los distintos servicios que heredan del servicio genérico
 - Settings: Para otras configuraciones

## Stock.Model

      |- Base
      |- Entities
      |- Exceptions

 - Base: Donde se define la interfaz base de modelo
 - Entities: Donde están los distintos modelos que implementan el modelo base
 - Exceptions: Para manejo de excepciones

## Stock.Repository.LiteDb

      |- Configuration
      |- Exceptions
      |- Interface
      |- Repository
      DataContext.cs

 - Configuration Donde se configura la base de datos LiteDb
 - Exceptions: Para manejo de excepciones
 - Interface: Donde se definen las interfaces del repositorio
 - Repository: Donde se define el repositorio base con los metodos que utilizan los servicios
 - DataContext.cs: Contexto de la base de datos
