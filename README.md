# HexactaLabs-.NETCore_React

Hexacta 2020

Bienvenidos a los Hexacta Labs

Agenda:
* _Initial_: Presentación de la aplicación básica, pasos para correrla localmente y planteo de la primer actividad: Backend con .NetCore.
* __Level 1: Se nivelará presentando una aplicación con las actividades de la etapa inicial completas. Planteo de la segunda actividad: Frontend con ReactJS.__
* _Level 2_: Se presenta la aplicación con las actividades anteriores completas. Planteo de la tercera actividad: FullStack development.
* _Final_: Se presenta la aplicación completa. Planteo de la actividad final. 

## [Readme General](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/README.md)

## [Documentación](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/Docs/index.md)


# Nivelación 1
En esta presentación disponemos de una aplicación con las actividades de la etapa inicial resueltas. La sección Proveedores ya se encuentra implementada por lo que se puede comparar la solución propuesta con lo realizado anteriormente.
No es necesario utilizar esta versión de la Stock Web pero sí es recomendable para poder tener una base en común.

En este nivel el ejercicio es hacer lo opuesto a la etapa anterior. Disponemos de un backend completo para el manejo de tipos de producto (Entidad __ProductType__), que podemos inspeccionar en la interfaz __Swagger__ (ejemplo: http://localhost:5000/swagger)
El objetivo en esta etapa es crear el frontend y una sección en la Home para acceder a los detalles de las categorías de Productos.

El sistema debe ser capaz de:
* Crear, editar y eliminar una categoría de producto a través de la sección __Categorías__ dentro del sitio.
* La web React debe conectarse con estos servicio configurando un store.

Recordemos que la documentación de React se encuentra [aquí](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/Docs/react.md) y sobre Redux [aquí](https://github.com/lnapoliHX/HexactaLabs-NetCore_React-Initial/blob/master/Docs/redux.md)

## Tips
- Revisar *Stock.Web/client-app/src/modules/providers* para comparar el frontend.
- Utilizar Swagger para comprender el funcionamiento del backend de ProductType
