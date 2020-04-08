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

*IMPORTANTE*: Al momento de crear los servicios del lado Backend es necesario descomentar lo siguiente de la clase __Startup.cs__:
```
//services.AddTransient<ProviderService>();
```

Además es necesario descomentar la configuración del mapeo para Provider en __ModelProfile.cs__:
```
//CreateMap<Provider, ProviderDTO>().ReverseMap();  
```

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

