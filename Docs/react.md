[Volver](./index.md)

# React

## Introducción

`React` es una librería de Javascript la cuál es usada para el desarrollo de interfaces de usuario.
Más información se puede encontrar en el siguiente link
[React Docs](https://reactjs.org/docs/getting-started.html).

[Vídeo introductorio](https://www.youtube.com/watch?v=Ke90Tje7VS0&t) 
Aclaración: El vídeo se encuentra en inglés

## Filosofía

> Todo se puede encapsular en un Componente.

El "Componente" es el principal principio de abstracción en React. Estos se pueden anidar mediante composición y cada uno puede contener toda lógica que se crea necesaria.

Visto desde arriba, una aplicación de React es una jerarquía con forma de árbol, con la raíz siendo el punto de entrada, dónde los nodos intermedios son los distintos Componentes implementados, y las hojas son elementos válidos del DOM. La tarea de "plasmar" las hojas en el browser para que este muestre el resultado es una tarea de React, dejando en manos del desarrollador la preocupación de cómo o qué va en cada Componente.

## Component API

Existen 2 tipos de componentes: Funcionales y de Clase

### Componentes funcionales

Son los más simples de entender, ya que es una función de Javascript que retorna la estructura que se debe renderizar usando JSX.

### JSX

```javascript
import React from 'react';
...
const SomeComponent = (props) => (
  <ComponentOne>
    <SubComponent>
    <div>
    ...
    </div>
  </ComponentOne>
);
```

Es una sintaxís tipo XML especial que permite escribir código parecido a HTML. React siempre tiene que estar en el scope del módulo importandolo para que todo código JSX pueda ser intepretado correctamente.

La función de JSX es describir cómo es la estructura que el componente que se está implementando quiere renderizar.

Hay que prestar especial atención a las mayúsculas, ya que en JSX un componente que empiece con mínuscula le da la pauta a React que se tiene que renderizar un elemento del DOM real.

Ejemplo:

```
  <div>...</div>
```

mientras que para los componentes creados por el desarrollador se deben escribir con mayúscula:

```
  <Componente />
```

Para más detalles. [JSX Docs](https://reactjs.org/docs/introducing-jsx.html)

### Propiedades (Props)

Cada Componente puede recibir de su padre una lista de propiedades, una suerte de parámetros, que se agrupan en un objeto denominado `props`. Estó permite desligarlo de la responsabilidad de conseguirlos dejandosela al Componente padre.

Visto desde el padre:

```javascript
const Padre = props => <Hijo dato="dato" />;
```

En el hijo:

```javascript
const Hijo = props => <div>`Mostrando ${props.dato}`</div>;
```

En el hijo este dato aparece como `props.dato` (o `this.props.dato` si se usan componentes de tipo clase). El objeto `props` lo genera React de forma trasparente al desarrollador tomando los atributos JSX definidos.

El objeto `props` **no debe** ser modificado, el Componente solo debe usarlos para cumplir su próposito, incluso podemos pasar funciones
```javascript
<Form onSubmit={()=> someFunction()} />
```

### Componentes tipo Clase

Todo elemento de clase debe extender a `Component`.

```javascript
import React, { Component } from 'react';

class SomeComponent extends Component {

  constructor(props) {
    super(props);
  }

  render() {
    return (
      <h1>Hello World! </h1>
    )
  }
}
```

El único método requerido es `render()` y es donde se describe mediante JSX la estructura que se debería renderizar usando otros Componentes y/o elementos del DOM, por ejemplo `div`, `h1`, etc.

[Aquí](https://reactjs.org/docs/components-and-props.html) se profundiza más los temas de Componentes y props.

### Estado (State)

Los componentes de tipo clase puede mantener estado interno, con cualquier información que sea pertinente para llevar a cabo su función y que esta no sea necesaria fuera del mismo. Por ejemplo, conservar los datos de un fetch para luego mostrarlos en el `render()`.

Para usar esta funcionalidad se debe declarar el estado inicial en el constructor del componente para luego poder ser actualizado usando el método de instancia `setState()`.

```javascript
import React from 'react';
class SomeComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      // some initial state
    }
  }
  ...
  componentDidMount() {
    // acá va el código de fetch
    // luego
    this.setState({ data });
  }
}
```

El método `setState()` le dice a React que algo cambió en el estado del componente y este debe ser renderizado nuevamente.

### Métodos de ciclo de vida (Lifecycle Methods)

Todo componente en React pasa por un ciclo de vida en el cuál mediante métodos de la API le dan al desarrollador la habilidad de poder escribir lógica que se ejecute en una determinada situación. Un ejemplo común es realizar la búsqueda de datos cuando un componente se monta, ya que este los requiere para poder mostrarlos. El _fetch_ se escribiría en el `componentDidMount()`.

Para utilizar estos métodos en los componentes funcionales, React incorporó hace relativamente poco [Hooks](https://reactjs.org/docs/hooks-intro.html) que permiten "engancharse" a estos componentes en el ciclo de vida.

[Acá](http://projects.wojtekmaj.pl/react-lifecycle-methods-diagram/) se encuentra documentado en forma de gráfico el ciclo de vida completo.

Para información más detallada. [State / Lifecycle Docs](https://reactjs.org/docs/react-component.html)

## ¿Cómo pensar y dividir los Componentes en React?

Es importante partir del principio que la idea de React es crear componentes que puedan ser reutilizables, mantenibles y faciles de testear. Es muy fácil caer en la tentación de englobar toda la lógica y renderización en un solo componente. Dicho esto, hay algunos consejos que podemos seguir para evitar estas malas prácticas y sacarle el mayor rendimiento posible a esta libreria.

- Dividir nuestra UI en una jerarquía de componentes, partiendo desde lo más global hacia lo mas específico.

- Por lo general, hay componentes que siempre tendremos que crear en toda aplicación. Podemos mencionar un Header, un Toolbar, Sidebar, Input. Estos son componentes que siempre se tienen que hacer en toda aplicación.

- Crear componentes pequeños. Podemos establecernos un máximo de líneas en cada componente. Si este se supera, podemos dividir ese componente en dos.

- Cada componente debe tener UNA sola responsabilidad. Si vemos que un componente cumple mas de una función, separar esta funcionalidad en otro componente.

- Siempre pensar en la reutilización. Si veo en muchas partes de mi aplicacion código repetido, probablemente sea una señal de que debamos crear un componente que represente eso que estamos repitiendo.

[Pensando en React](https://reactjs.org/docs/thinking-in-react.html)


## Plugin de React para Google Chrome

React nos ofrece un plugin para hacer debug de nuestro código. Es una extensión que se instala desde el Web Store de Chrome. Una vez instalado, tendremos el icono react en el margen superior derecho como una nueva tab. Si este se encuentra coloreado de azul, significa que el sitio web en el que estamos está utilizando la libreria, por lo que podremos acceder a sus componentes y subcomponentes que estan siendo renderizados. Cuando inspeccionamos un elemento, veremos que se agregan dos nuevas tabs, "Profiler" y "Components". Cada una ofrece funciones específicas. Por ejemplo dentro de "Components", estará declarado la estructura de árbol que se esta usando para la página. En cada componente, podremos también ver sus props.

[React Plugin](https://chrome.google.com/webstore/detail/react-developer-tools/fmkadmapgofadopljbjfkapdkoienihi?hl=es)
[Documentación React Plugin](https://reactjs.org/blog/2014/01/02/react-chrome-developer-tools.html)

## Referencias
[Documentación Oficial](https://es.reactjs.org/)
Componentes Bootstrap4 para React [Reactstrap](https://reactstrap.github.io/)
Para ver ejercicios de React. [Freecodecamp - React](https://learn.freecodecamp.org/front-end-libraries/react/)
