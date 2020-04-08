# Prerequisitos

[Volver](./index.md)

## Frontend

1. Instalar la version LTS de NodeJs. [Link](https://nodejs.org/en/)

Nota: NodeJs ya viene con `npm`. Este es el gestor de paquetes que se usa principalmente para bajar librerias y correr scripts definidos en el archivo `package.json`

2. Instalar un IDE. Cualquiera basta y en general va en gusto de cada uno. Si no se conoce/usó ninguno, `VSCode` se suele usar mucho en el terreno de frontend.
En el siguiente apartado, tenemos como utilizar __VSCode__ para debuggear en React

3. (opcional) Instalar en el navegador las extenciones `React Developer Tools` y `Redux DevTools`. Si bien no son necesarios para desarollar ayudan a la hora de debuggear código de React/Redux respectívamente.

### Visual Studio code
1. Instalar Visual Studio Code [Link](https://code.visualstudio.com/)
1. Abrir la carpeta Stock.Web/client-app, allí es donde se aloja la aplicación web.
   1. Ir a File/Open New Window y seleccionar esa carpeta. 
1. Presionar CTRL + SHIFT + P y escribir "Show Recommended Extensions"
	1. Instalar las que dicen "Workspace Recommended"
	1. Instalar las otras que prefieran
1. Para poder Ejecutar y Debuggear el proyecto:
   1. Ejecutar *npm install* para instalar todas las dependencias.
	1. Tipear en la consola *npm run start*, esto levantará un cliente en http://localhost:3000
   1. Si accedemos desde cualquier pestaña, podríamos utilizar la aplicación.
      - Recordar que el backend y el frontend necesitan estar ambos ejecutándose.
	1. Si presionamos F5 y debería abrirnos un Browser vacío, este mismo tiene un puente para debuggear la applicación web.
      - Para comprobar si funcionó, agregar un breakpoint en *src/app/App.js* y recargar la pestaña. El código debería frenar allí. 
	