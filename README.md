# Calendar Meeting Backend

## 📋 Descripción
Backend completo para gestión de reuniones y calendario, desarrollado en .NET 8 con arquitectura en capas, Oracle Database y Dapper ORM.

## 🏗️ Arquitectura
- **Core**: Modelos, interfaces de servicios y repositorios, validadores
- **Repositorios**: Implementación de acceso a datos con Dapper
- **WebApi**: Controllers REST API con Swagger

## 🚀 Tecnologías
- **.NET 8**
- **Oracle Database**
- **Dapper ORM**
- **FluentValidation**
- **Swagger/OpenAPI**
- **Dependency Injection**

## 📁 Estructura del Proyecto
```
Backend/
├── Core/                    # Capa de dominio
│   ├── Models/             # Modelos de datos
│   ├── Repositorios/       # Interfaces de repositorios
│   ├── Servicios/          # Interfaces de servicios
│   ├── Validadores/        # Validación de datos
│   └── Constantes/         # Constantes del sistema
├── Repositorios/           # Implementación de acceso a datos
│   ├── ConnectionProvider.cs
│   ├── MeetRepositorio.cs
│   └── CatalogoRepositorio.cs
├── WebApi/                 # API REST
│   ├── Controllers/        # Controladores
│   ├── Extensions/         # Extensiones
│   └── Models/             # Modelos de respuesta
└── README_MEET_API.md      # Documentación detallada de la API
```

## 🎯 Funcionalidades Implementadas

### ✅ CRUD Completo de Reuniones
- Crear, leer, actualizar y eliminar reuniones
- Validaciones de modelo y negocio
- Generación automática de tokens QR únicos

### ✅ Consultas Especializadas
- Obtener reuniones por sala
- Obtener reuniones por fecha
- Obtener reuniones por rango de fechas

### ✅ Catálogos
- Salas disponibles
- Prioridades
- Estados de formulario
- Tipos de reunión

### ✅ Características Técnicas
- Arquitectura en capas (Clean Architecture)
- Inyección de dependencias
- Manejo de errores consistente
- Validaciones robustas
- Documentación completa con Swagger

## 🛠️ Instalación y Configuración

### Prerrequisitos
- .NET 8 SDK
- Oracle Database
- Visual Studio 2022 o VS Code

### Pasos de Instalación
1. Clonar el repositorio:
```bash
git clone https://github.com/Ck14/calendar-meeting-backend.git
cd calendar-meeting-backend
```

2. Restaurar dependencias:
```bash
dotnet restore
```

3. Configurar la cadena de conexión en `WebApi/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=tu_servidor;User Id=tu_usuario;Password=tu_password;"
  }
}
```

4. Compilar la solución:
```bash
dotnet build
```

5. Ejecutar la aplicación:
```bash
cd WebApi
dotnet run
```

## 📚 Documentación de la API

La documentación completa de la API está disponible en [README_MEET_API.md](README_MEET_API.md)

### Endpoints Principales
- `GET /api/meet` - Obtener todas las reuniones
- `POST /api/meet` - Crear nueva reunión
- `PUT /api/meet/{id}` - Actualizar reunión
- `DELETE /api/meet/{id}` - Eliminar reunión
- `GET /api/catalogosmeet/salas` - Obtener salas disponibles

## 🔧 Configuración de Base de Datos

### Scripts SQL Requeridos
```sql
-- Tabla principal de reuniones
CREATE TABLE MM_MEET (
    ID_MEET NUMBER PRIMARY KEY,
    TITULO VARCHAR2(500) NOT NULL,
    DESCRIPCION VARCHAR2(1000),
    FECHA_INICIO DATE NOT NULL,
    HORA_INICIO DATE,
    FECHA_FIN DATE,
    HORA_FIN DATE,
    TOKEN_QR VARCHAR2(10),
    ID_SALA NUMBER NOT NULL,
    ID_PRIORIDAD NUMBER NOT NULL,
    ID_ESTADO NUMBER NOT NULL,
    ID_TIPO_MEET NUMBER NOT NULL
);

-- Tablas de catálogo
CREATE TABLE MM_SALA (
    ID_SALA NUMBER PRIMARY KEY,
    NOMBRE_SALA VARCHAR2(100) NOT NULL,
    NIVEL NUMBER,
    HABILITADA CHAR(1) DEFAULT '1'
);

CREATE TABLE MM_PRIORIDAD (
    ID_PRIORIDAD NUMBER PRIMARY KEY,
    NOMBRE_PRIORIDAD VARCHAR2(50) NOT NULL
);

CREATE TABLE MM_ESTADO_FORMULARIO (
    ID_ESTADO NUMBER PRIMARY KEY,
    NOMBRE VARCHAR2(50) NOT NULL
);

CREATE TABLE MM_TIPO_MEET (
    ID_TIPO_MEET NUMBER PRIMARY KEY,
    NOMBRE VARCHAR2(50) NOT NULL
);
```

## 🧪 Testing

### Compilar y Verificar
```bash
# Compilar toda la solución
dotnet build

# Verificar que no hay errores
dotnet build --verbosity normal
```

### Probar Endpoints
Una vez ejecutada la aplicación, puedes acceder a:
- **Swagger UI**: `https://localhost:7001/swagger`
- **API Base**: `https://localhost:7001/api`

## 📝 Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👨‍💻 Autor

**Ck14**
- GitHub: [@Ck14](https://github.com/Ck14)

## 🙏 Agradecimientos

- .NET Community
- Dapper ORM
- FluentValidation
- Oracle Database

---

⭐ Si este proyecto te ayuda, ¡dale una estrella!
