# Prerequisitos

[Volver](./index.md)

## Backend

### Visual Studio code (opción recomendada)
1. Instalar Visual Studio Code [Link](https://code.visualstudio.com/)
1. Presionar CTRL + SHIFT + P y escribir "Show Recommended Extensions"
	1. Instalar las que dicen "Workspace Recommended"
	1. Instalar las otras que prefieran
1. Instalar el último SDK de NetCore desde: [Link](https://dotnet.microsoft.com/download/dotnet-core). La versión que vamos a utilizar es la *2.1*.
1. Para poder Ejecutar y Debuggear el proyecto:
	1. CTRL + SHIFT + P y escribir "Generate Assets..."
	1. Presionamos F5 y debería abrirnos un Browser
	1. Vamos a http://localhost:5000/swagger
	1. Si todo funcionó correctamente, el backend está funcionando!


### Visual Studio
Otra opción, es utilizar Visual Studio, pero solo está disponible para Windows o Mac.

1. Instalar Visual Studio 2019 Community [Link](https://visualstudio.microsoft.com/es/vs/community/)

	1. Ingresar en el siguiente sitio [Link](https://visualstudio.microsoft.com/downloads/)
	y descargar la version Community seleccionando el boton Free Download. 
	2. Ejecutar el instalador descargado y seleccionar los siguientes WorkLoads:  
		1. ASP.NET and web development   
		1. NET Core cross-platform development   
	1. Hacer click en Install y avanzar con la instalacion. Tener en cuenta que podria pedir reiniciar la computadora, si despues de reiniciar no se continua la instalacion se debe ejecutar el instalador del paso 1 nuevamente.

1. Instalar el último SDK de NetCore desde: [Link](https://dotnet.microsoft.com/download/dotnet-core)
La versión que vamos a utilizar es la *2.1*.

1. Clonar el proyecto de GitHub en Visual Studio
	1. Desde la URL del proyecto en github, click en Clone or Download y Open in Visual Studio.
	1. Clonar el proyecto desde el Visual Studio.
