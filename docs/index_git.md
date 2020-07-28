# Guía git
Para trabajar vamos a utilizar la herramienta Git, que es un VCS (version control system) que se descarga [aquí](https://git-scm.com/downloads).
Github será el servicio que almacenará nuestros proyectos en la nube, existen otros como Bitbucket, Gitlab, Azure Devops.

1. Instalar Visual Studio Code. [Link](https://code.visualstudio.com/)
1. Crear una carpeta __HxLabs__ y abrir Visual Studio Code en la misma
1. Ir a la web del proyecto correspondiente y crear un "fork" desde el botón
![forks](./images/forks_example.png)

1. Clonar el proyecto forkeado de GitHub en Visual Studio Code
	1. Copiar la URL del fork
	1. CTRL + SHIFT + P y escribir "git clone"
	1. Agregar la URL del proyecto y seleccionar una carpeta.
	1. Se puede clonar directamente en la carpeta haciendo *git clone "url"* desde una linea de comandos
    1. Repetir estos pasos para cada proyecto.
1. Luego, cuando comience la *etapa X* ir a la carpeta correspondiente y abrir una nueva instancia de VSCode, o directamente desde Code, *File/Open Folder*.

Debería quedar algo semejante a esto: 

![folders](./images/folders_git.png)

## Esquema de trabajo
Trabajar con git es sencillo por medio de VSCode y GitLens. 
Atlassian tiene un tutorial bastante completo de como trabajar [Link](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow). 

![](./images/git_flow.png)

Los comandos más comunes son: 
```
git clone
# Clonar el proyecto del servidor remoto, al local

git status
#  Muestra información de que archivos fueron modificados, commits pendientes, etc.

git add
# Después de editar o agregar archivos, permite agregarlos al trackeo de git.

git commit -m “Add a new feature”
# Permite agregar al repositorio local un nuevo commit con distintos cambios. 

git push origin master
# Permite pushear o enviar los cambios del repositorio local, al repositorio remoto (en este caso, Github y nuestro fork)


```


