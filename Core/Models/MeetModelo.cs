using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class MeetModelo
    {
        public int IdMeet { get; set; }
        
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(500, ErrorMessage = "El título no puede exceder 500 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicio { get; set; }
        
        public DateTime? HoraInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        
        public DateTime? HoraFin { get; set; }
        
        [StringLength(10, ErrorMessage = "El token QR no puede exceder 10 caracteres")]
        public string? TokenQr { get; set; }
        
        [Required(ErrorMessage = "La sala es requerida")]
        public int IdSala { get; set; }
        
        [Required(ErrorMessage = "La prioridad es requerida")]
        public int IdPrioridad { get; set; }
        
        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }
        
        [Required(ErrorMessage = "El tipo de reunión es requerido")]
        public int IdTipoMeet { get; set; }
        
        // Propiedades de navegación para mostrar información relacionada
        public string? NombreSala { get; set; }
        public string? NombrePrioridad { get; set; }
        public string? NombreEstado { get; set; }
        public string? NombreTipoMeet { get; set; }
    }

    public class MeetCrearModelo
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(500, ErrorMessage = "El título no puede exceder 500 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicio { get; set; }
        
        public DateTime? HoraInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        
        public DateTime? HoraFin { get; set; }
        
        [Required(ErrorMessage = "La sala es requerida")]
        public int IdSala { get; set; }
        
        [Required(ErrorMessage = "La prioridad es requerida")]
        public int IdPrioridad { get; set; }
        
        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }
        
        [Required(ErrorMessage = "El tipo de reunión es requerido")]
        public int IdTipoMeet { get; set; }
    }

    public class MeetActualizarModelo
    {
        [Required(ErrorMessage = "El ID de la reunión es requerido")]
        public int IdMeet { get; set; }
        
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(500, ErrorMessage = "El título no puede exceder 500 caracteres")]
        public string Titulo { get; set; } = string.Empty;
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "La fecha de inicio es requerida")]
        public DateTime FechaInicio { get; set; }
        
        public DateTime? HoraInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        
        public DateTime? HoraFin { get; set; }
        
        [Required(ErrorMessage = "La sala es requerida")]
        public int IdSala { get; set; }
        
        [Required(ErrorMessage = "La prioridad es requerida")]
        public int IdPrioridad { get; set; }
        
        [Required(ErrorMessage = "El estado es requerido")]
        public int IdEstado { get; set; }
        
        [Required(ErrorMessage = "El tipo de reunión es requerido")]
        public int IdTipoMeet { get; set; }
    }
}
