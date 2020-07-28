# HexactaLabs-.NETCore_React

Hexacta 2020

**Bienvenidos a los Hexacta Labs**

Este curso está orientado a profesionales, no profesionales y recién iniciados en el desarrollo web. 
Se trata de la implementación guiada de un sitio web sobre el manejo de stock de productos generales.

El curso tiene diferentes etapas y nivelaciones con este formato:
* __Initial__: Presentación de la aplicación básica, pasos para correrla localmente y planteo de la primer actividad: Backend con .NetCore.
* __Level 1__: Se nivelará presentando una aplicación con las actividades de la etapa inicial completas. Planteo de la segunda actividad: Frontend con ReactJS.
* __Level 2__: Se presenta la aplicación con las actividades anteriores completas. Planteo de la tercera actividad: FullStack development.
* __Final__: Se presenta la aplicación completa. Planteo de la actividad final. 

## Requisitos
* Conocimientos básicos de HTML
* Manejo básico de bases de datos
* Conocimientos básicos sobre ORMs

## [Documentación](./Docs/index.md)
Seguir la documentación para instalar las herramientas necesarias y comprobar que todo esté funcionando.

# Actividad Inicial
Para el trabajo inicial, se necesita crear un servicio backend que se conecte a la base de datos local para obtener información y brindar operaciones CRUD de la entidad __Provider__.

*IMPORTANTE*: Al momento de crear los servicios del lado Backend es necesario descomentar en la clase __Startup.cs__:
```
//services.AddTransient<ProviderService>();
```
Este código permite utilizar un objeto sin instanciarlo directamente. 
Luego podremos usarlo en otras clases, como por ejemplo en __ProductTypeController.cs__ se utiliza la inyección de __ProductTypeService__. En la [introducción a NetCore](./Docs/netcore.md) hay una explicación sobre el concepto de inyección de dependencias.

Luego descomentar la configuración del mapeo para Provider en __ModelProfile.cs__:
```
//CreateMap<Provider, ProviderDTO>().ReverseMap();  
```
Esta línea nos permite mapear entre Provider y ProviderDTO según las convenciones de AutoMapper. 
Será parte de la actividad crear un ProviderDTO para enviar la información necesaria a la vista.
Para más información sobre [ReverseMap() aquí.](https://docs.automapper.org/en/stable/Reverse-Mapping-and-Unflattening.html)
En la [introducción a NetCore](./Docs/netcore.md) hay información sobre automapper.

El sistema debe ser capaz de:
* Crear, editar y eliminar un nuevo provider a través de la sección Proveedores dentro del sitio.
* Realizar búsquedas de proveedores.
* La interfaz swagger debe mostrar todos los servicios expuestos.
* La web React debe conectarse con estos servicio configurando un store.

# Tips
## Entidad Store (Tienda)
En la web se encuentra una sección llamada Tiendas en donde se presenta una pantalla con la implementación completa sobre:
* Búsqueda de tiendas: A través de un formulario en pantalla se envía una serie de parámetros para realizar la consulta en la base de datos.
* Listado de tiendas: una grilla presenta el detalle de cada tienda además de los botones de acción para:
  * Ir al detalle de una tienda
  * Navegar a la pantalla de edición
  * Eliminar tienda

Se recomienda utilizar esta entidad tanto del lado frontend como backend para realizar las actividades teniendo esta sección como referencia.

## Swagger
La idea de swagger es crear una interfaz para probar nuestro backend, a partir de poca información que le dejemos anotada a nuestros endpoints. 
Si buscamos en la carpeta __Stock.Api/Controllers__ en la clase __ProductTypeController.cs__

```csharp
...
/// <summary>
/// Permite recuperar una instancia mediante un identificador
/// </summary>
/// <param name="id">Identificador de la instancia a recuperar</param>
/// <returns>Una instancia</returns>
[HttpGet("{id}")]
public ActionResult<ProductTypeDTO> Get(string id)
...
```
Destacamos como primordial: 
- summary: da información sobre la funcionalidad de nuestro endpoint
- param: da información sobre nuestros atributos *query*, *param*, *form*
- returns: informacion sobre el tipo retornado.

Luego si nos abrimos el browser en *localhost:5000/swagger* nos abrirá una página con todos nuestros endpoints: 
![swagger-ui](./Docs/images/swagger-ui.png)

Para ver información más extensa de esta herramienta [aquí](https://github.com/domaindrivendev/Swashbuckle.AspNetCore).
En la [introducción a NetCore](./Docs/netcore.md) hay más información sobre Swagger.

## Front end

Para correr la app, solo hace falta estar situado en la carpeta `Stock.Web/client-app` y ejecutar `npm start` en la consola.

Los request a la API se hacen a través del server de desarrollo que usa create-react-app, el cual se configura en el archivo
`package.json` bajo la key `proxy`. Por defecto, se espera que la API corra en `localhost:5000`.

## Backend
Inicialmente, debería funcionar ejecutar la siguiente instrucción en consola:

```
dotnet run --project Stock.Api/Stock.Api.csproj
```
En [Docs/Prerequisitos Netcore](./Docs/prerequisitosnetcore.md) hay una guía de como ejecutar/debuggear el backend

### ¿Tuviste algun problema? 
- [Troubleshooting](./Docs/troubleshooting.md)
- Creá un issue para que lo resolvamos.

