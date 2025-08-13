# API de Reuniones (Meet) - Documentación

## Descripción
Esta API proporciona funcionalidades completas para la gestión de reuniones, incluyendo operaciones CRUD y consultas especializadas.

## Endpoints Principales

### 1. Reuniones (Meet)

#### Obtener todas las reuniones
```http
GET /api/meet
```

#### Obtener reunión por ID
```http
GET /api/meet/{id}
```

#### Crear nueva reunión
```http
POST /api/meet
Content-Type: application/json

{
  "titulo": "Reunión de Planificación",
  "descripcion": "Reunión para planificar el próximo trimestre",
  "fechaInicio": "2024-01-15T09:00:00",
  "horaInicio": "2024-01-15T09:00:00",
  "fechaFin": "2024-01-15T11:00:00",
  "horaFin": "2024-01-15T11:00:00",
  "idSala": 1,
  "idPrioridad": 1,
  "idEstado": 1,
  "idTipoMeet": 1
}
```

#### Actualizar reunión
```http
PUT /api/meet/{id}
Content-Type: application/json

{
  "idMeet": 1,
  "titulo": "Reunión de Planificación Actualizada",
  "descripcion": "Reunión actualizada para planificar el próximo trimestre",
  "fechaInicio": "2024-01-15T09:00:00",
  "horaInicio": "2024-01-15T09:00:00",
  "fechaFin": "2024-01-15T11:00:00",
  "horaFin": "2024-01-15T11:00:00",
  "idSala": 1,
  "idPrioridad": 1,
  "idEstado": 1,
  "idTipoMeet": 1
}
```

#### Eliminar reunión
```http
DELETE /api/meet/{id}
```

#### Obtener reuniones por sala
```http
GET /api/meet/sala/{idSala}
```

#### Obtener reuniones por fecha
```http
GET /api/meet/fecha/{fecha}
```
Formato de fecha: `yyyy-MM-dd`

#### Obtener reuniones por rango de fechas
```http
GET /api/meet/rango-fechas?fechaInicio=2024-01-01&fechaFin=2024-01-31
```

### 2. Catálogos (CatalogosMeet)

#### Obtener salas disponibles
```http
GET /api/catalogosmeet/salas
```

#### Obtener prioridades
```http
GET /api/catalogosmeet/prioridades
```

#### Obtener estados de formulario
```http
GET /api/catalogosmeet/estados
```

#### Obtener tipos de reunión
```http
GET /api/catalogosmeet/tipos-meet
```

## Modelos de Datos

### MeetModelo
```json
{
  "idMeet": 1,
  "titulo": "Reunión de Planificación",
  "descripcion": "Descripción de la reunión",
  "fechaInicio": "2024-01-15T09:00:00",
  "horaInicio": "2024-01-15T09:00:00",
  "fechaFin": "2024-01-15T11:00:00",
  "horaFin": "2024-01-15T11:00:00",
  "tokenQr": "ABC123DEF4",
  "idSala": 1,
  "idPrioridad": 1,
  "idEstado": 1,
  "idTipoMeet": 1,
  "nombreSala": "Sala A",
  "nombrePrioridad": "Alta",
  "nombreEstado": "Activo",
  "nombreTipoMeet": "Presencial"
}
```

### MeetCrearModelo
```json
{
  "titulo": "Reunión de Planificación",
  "descripcion": "Descripción de la reunión",
  "fechaInicio": "2024-01-15T09:00:00",
  "horaInicio": "2024-01-15T09:00:00",
  "fechaFin": "2024-01-15T11:00:00",
  "horaFin": "2024-01-15T11:00:00",
  "idSala": 1,
  "idPrioridad": 1,
  "idEstado": 1,
  "idTipoMeet": 1
}
```

### MeetActualizarModelo
```json
{
  "idMeet": 1,
  "titulo": "Reunión de Planificación",
  "descripcion": "Descripción de la reunión",
  "fechaInicio": "2024-01-15T09:00:00",
  "horaInicio": "2024-01-15T09:00:00",
  "fechaFin": "2024-01-15T11:00:00",
  "horaFin": "2024-01-15T11:00:00",
  "idSala": 1,
  "idPrioridad": 1,
  "idEstado": 1,
  "idTipoMeet": 1
}
```

## Respuestas

### Estructura de Respuesta Exitosa
```json
{
  "estado": "success",
  "icono": "success",
  "mensaje": "Reunión creada exitosamente",
  "titulo": "Creación de Reunión",
  "resultado": {
    "idMeet": 1,
    "tokenQr": "ABC123DEF4"
  }
}
```

### Estructura de Respuesta de Error
```json
{
  "estado": "error",
  "icono": "error",
  "mensaje": "La fecha de inicio no puede ser en el pasado",
  "titulo": "Validación de Negocio",
  "resultado": null
}
```

## Validaciones

### Validaciones de Modelo
- **Título**: Requerido, máximo 500 caracteres
- **Descripción**: Máximo 1000 caracteres
- **Fecha de Inicio**: Requerida
- **Sala**: Requerida
- **Prioridad**: Requerida
- **Estado**: Requerido
- **Tipo de Reunión**: Requerido

### Validaciones de Negocio
- La fecha de inicio no puede ser en el pasado
- La fecha de fin no puede ser anterior a la fecha de inicio
- La hora de fin debe ser posterior a la hora de inicio
- Solo se pueden eliminar reuniones que existan

## Características Especiales

### Generación Automática de Token QR
- Cada reunión creada genera automáticamente un token QR único de 10 caracteres
- El token se genera usando caracteres alfanuméricos (A-Z, 0-9)

### Consultas con Joins
- Las consultas incluyen información relacionada de las tablas de catálogo
- Se muestran nombres descriptivos en lugar de solo IDs

### Manejo de Errores
- Respuestas consistentes con códigos de estado HTTP apropiados
- Mensajes de error descriptivos
- Validación tanto a nivel de modelo como de negocio

## Códigos de Estado HTTP

- **200 OK**: Operación exitosa
- **201 Created**: Recurso creado exitosamente
- **400 Bad Request**: Datos de entrada inválidos
- **404 Not Found**: Recurso no encontrado
- **500 Internal Server Error**: Error interno del servidor

## Ejemplos de Uso

### Crear una Reunión
```bash
curl -X POST "https://api.ejemplo.com/api/meet" \
  -H "Content-Type: application/json" \
  -d '{
    "titulo": "Reunión de Equipo",
    "descripcion": "Reunión semanal del equipo de desarrollo",
    "fechaInicio": "2024-01-15T09:00:00",
    "horaInicio": "2024-01-15T09:00:00",
    "fechaFin": "2024-01-15T10:00:00",
    "horaFin": "2024-01-15T10:00:00",
    "idSala": 1,
    "idPrioridad": 2,
    "idEstado": 1,
    "idTipoMeet": 1
  }'
```

### Obtener Reuniones por Fecha
```bash
curl -X GET "https://api.ejemplo.com/api/meet/fecha/2024-01-15"
```

### Obtener Catálogos
```bash
curl -X GET "https://api.ejemplo.com/api/catalogosmeet/salas"
curl -X GET "https://api.ejemplo.com/api/catalogosmeet/prioridades"
curl -X GET "https://api.ejemplo.com/api/catalogosmeet/estados"
curl -X GET "https://api.ejemplo.com/api/catalogosmeet/tipos-meet"
```
