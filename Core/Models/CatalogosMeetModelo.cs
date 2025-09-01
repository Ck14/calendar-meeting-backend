using System;

namespace Core.Models
{
    /// <summary>
    /// Modelo para salas de reuniones
    /// </summary>
    public class SalaModelo
    {
        /// <summary>
        /// Identificador único de la sala
        /// </summary>
        public int IdSala { get; set; }
        
        /// <summary>
        /// Nombre de la sala
        /// </summary>
        public string NombreSala { get; set; } = string.Empty;
        
        /// <summary>
        /// Nivel de la sala (1: Básico, 2: Intermedio, 3: Avanzado)
        /// </summary>
        public int Nivel { get; set; }
        
        /// <summary>
        /// Indica si la sala está habilitada (1: Habilitada, 0: Deshabilitada)
        /// </summary>
        public string Habilitada { get; set; } = "1";
        
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Descripción completa de la sala
        /// </summary>
        public string Descripcion => $"Sala {NombreSala} - Nivel {Nivel}";
    }

    /// <summary>
    /// Modelo para prioridades de reuniones
    /// </summary>
    public class PrioridadModelo
    {
        /// <summary>
        /// Identificador único de la prioridad
        /// </summary>
        public int IdPrioridad { get; set; }
        
        /// <summary>
        /// Nombre de la prioridad
        /// </summary>
        public string NombrePrioridad { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Descripción de la prioridad
        /// </summary>
        public string Descripcion => NombrePrioridad;
    }

    /// <summary>
    /// Modelo para estados de formulario
    /// </summary>
    public class EstadoFormularioModelo
    {
        /// <summary>
        /// Identificador único del estado
        /// </summary>
        public int IdEstado { get; set; }
        
        /// <summary>
        /// Nombre del estado
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Descripción del estado
        /// </summary>
        public string Descripcion => Nombre;
    }

    /// <summary>
    /// Modelo para tipos de reunión
    /// </summary>
    public class TipoMeetModelo
    {
        /// <summary>
        /// Identificador único del tipo de reunión
        /// </summary>
        public int IdTipoMeet { get; set; }
        
        /// <summary>
        /// Nombre del tipo de reunión
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
        
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Descripción del tipo de reunión
        /// </summary>
        public string Descripcion => Nombre;
    }

    public class RangoEdad
    {
        public int IdRango { get; set; }
        public string? Descripcion { get; set; }
    }

    public class ComunidadLinguistica
    {
        public int IdLenguaje{ get; set; }
        public string? NombreLenguaje { get; set; }
    }

    public class Pueblo
    {
        public int IdPueblo { get; set; }
        public string? NombrePueblo { get; set; }
    }

    public class Discapacidad
    {
        public int IdDiscapacidad { get; set; }
        public string? NombreDiscapacidad { get; set; }
    }
}

