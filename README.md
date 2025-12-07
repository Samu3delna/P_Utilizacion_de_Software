# P_Utilizacion_de_Software

# 📘 Gestor de Proyectos Académicos  
Sistema web desarrollado como proyecto del curso **Ingeniería y Utilización de Software**.  
La aplicación permite gestionar usuarios, proyectos y tareas, con roles diferenciados de **profesor** y **estudiante**, siguiendo buenas prácticas, MVC, arquitectura por capas y conexión a base de datos.

---

## ✨ Funcionalidades principales

### 👤 Gestión de usuarios
- Registro de usuarios (nombre, correo, contraseña, rol).
- Inicio de sesión.
- Roles: **Profesor** y **Estudiante**.
- Autorización para restringir funciones según rol.

### 📁 Gestión de proyectos
- Creación, edición y eliminación de proyectos (solo profesor).
- Campos: nombre, descripción, fecha de inicio y finalización.
- Asignación de estudiantes a proyectos.
- Visualización según rol:
  - Profesor → todos los proyectos que creó.
  - Estudiante → solo los proyectos donde participa.

### 📝 Gestión de tareas
- Todas las tareas pertenecen a un proyecto.
- Creación de tareas:
  - Profesor → asignar tareas a cualquier estudiante del proyecto.
  - Estudiante → crear subtareas propias.
- Campos: título, descripción, estudiante asignado, fecha límite, estado.
- Estados: **Pendiente → En progreso → Completada**.
- Edición:
  - Profesor → puede modificar cualquier tarea del proyecto.
  - Estudiante → puede modificar únicamente el estado de sus tareas.

### 📊 Reportes
- Avance de proyectos (profesor).
- Reporte por estudiante (profesor).
- Avance personal (estudiante).

### 🔔 Programación por eventos
- Confirmación al crear tareas.
- Notificación al completar una tarea.
- Advertencia al intentar eliminar proyectos con tareas asignadas.
- Alerta visual cuando una tarea está cerca de su fecha límite.


## 🛠️ Tecnologías utilizadas

- **C#**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **HTML, CSS y JavaScript**
- **Bootstrap**
- **GitHub** 

---

## 📂 Estructura del proyecto

