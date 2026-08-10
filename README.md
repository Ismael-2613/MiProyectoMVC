# CodeLab Academy - LMS

## Descripcion del proyecto

CodeLab Academy es una plataforma de gestion de aprendizaje (LMS) desarrollada para centralizar la educacion en linea de una escuela de programacion. El sistema permite a estudiantes matricularse en cursos, ver contenido multimedia organizado por modulos, y a profesores gestionar sus cursos y subir material educativo.

El proyecto nacio de la necesidad de reemplazar el uso de multiples plataformas como Google Classroom, WhatsApp y Drive, unificando todo en un solo entorno virtual de aprendizaje.

## Problema que resuelve

CodeLab Academy contaba con mas de 500 estudiantes activos y 20 instructores que utilizaban diferentes plataformas para gestionar el contenido educativo. Esto generaba confusion, falta de seguimiento y dificultades para escalar la operacion. Esta plataforma centraliza todo en un solo sistema.

## Arquitectura del sistema

El sistema esta construido con una arquitectura MVC (Modelo Vista Controlador) y separacion en capas:

- **Capa de presentacion:** Vistas Razor con HTML y CSS
- **Capa de logica:** Controllers de ASP.NET Core
- **Capa de datos:** Clases de acceso a datos con ADO.NET
- **Base de datos:** SQL Server con Stored Procedures

## Tecnologias utilizadas

- ASP.NET Core MVC con .NET 10
- SQL Server 2025
- Docker y Docker Compose
- HTML, CSS
- Git y GitHub

## Funcionalidades principales

### Para estudiantes
- Registro e inicio de sesion
- Ver catalogo de cursos disponibles
- Matricularse en cursos
- Ver modulos organizados por curso
- Descargar y visualizar contenido educativo

### Para profesores
- Panel de administracion de cursos
- Creacion de modulos dentro de cada curso
- Subida de videos y documentos por modulo
- Gestion de contenido educativo

## Instalacion y ejecucion

### Requisitos
- Docker Desktop instalado con WSL2 habilitado
- Git

### Pasos

1. Clonar el repositorio
```bash
git clone https://github.com/Ismael-2613/MiProyectoMVC.git
cd MiProyectoMVC
```

2. Levantar los contenedores
```bash
docker compose up --build
```

3. Esperar aproximadamente 30 segundos para que la base de datos se restaure automaticamente.

4. Acceder al sistema desde el navegador

## Credenciales de acceso

| Usuario | Contrasena | Rol |
|---|---|---|
| ismael.brenes | 12345 | Profesor |
| Crear cuenta nueva | - | Estudiante |

## Puertos del sistema

| Servicio | Puerto |
|---|---|
| Aplicacion web | 9090 |
| SQL Server | 1433 |

## Detener el sistema

```bash
docker compose down
```
