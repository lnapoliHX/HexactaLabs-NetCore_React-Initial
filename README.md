# HexactaLabs-.NETCore_React

Hexacta 2019

## [Documentación](./Docs/index.md)

## Front end

Para correr la app, solo hace falta estar situado en la carpeta `Stock.Web/client-app` y ejecutar `npm start` en la consola.

Los request a la API se hacen a través del server de desarrollo que usa create-react-app, el cual se configura en el archivo
`package.json` bajo la key `proxy`. Por defecto, se espera que la API corra en `localhost:5000`.

## Backend

```dotnet run --project Stock.Api/Stock.Api.csproj


Posibles problemas: 
para crear los assets para buildear: 
ctrl+alt+p => Net generate assets

Si vscode no les carga c# y les muestra un error de: 

se resuelve con este issue: 
https://stackoverflow.com/questions/55535177/omnisharp-msbuild-projectmanager-attempted-to-update-project-that-is-not-loaded

The SDK 'Microsoft.NET.Sdk.Web' specified could not be found.
https://github.com/OmniSharp/omnisharp-roslyn/issues/1313#issuecomment-429039879
```

## Actividad Final
Para el trabajo final disponemos de una versión de la Stock Web completa con el manejo CRUD de todas las entidades tanto de backend como de frontend.
Este ejercicio final propone el modelado e implementación de un carrito de compra, para el que se necesita:

* Agregar en la tabla de productos la opción de _agregar producto al carrito_ con un botón y un campo de cantidad.
* Los productos cuyo stock sea 0 no deben habilitar la opción para agregar al carrito.
* Una nueva sección en el sitio debe mostrar el detalle del carrito con una tabla donde se muestren los productos seleccionados, la cantidad y el precio unitario. En la sección inferior de la pantalla se debe mostrar el precio total a pagar y un botón de checkout.
* Al presionar el botón para realizar la compra, el sistema debe chequear el stock disponible en ese momento para cada producto seleccionado. Si hay stock para un producto en particular, se debe actualizar la cantidad de existencias restando la cantidad que el usuario seleccionó.
* Aquellos productos que no tienen suficiente stock al momento de realizar la compra no deben ser actualizados.
* Al finalizar la compra el sistema debe mostrar al usuario una nueva página donde se muestre el detalle de qué productos pudieron reservarse y el precio total de la compra. Tener en cuenta que este precio puede ser distinto al precio que se mostró en la página anterior por falta de stock.
