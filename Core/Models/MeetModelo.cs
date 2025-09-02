using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{



    public class MeetCrearModelo
    {   public int? IdMeet { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }        
        public DateTime? FechaFin { get; set; }        
        public int IdSala { get; set; }
        public string? Sala { get; set; }
        public int IdPrioridad { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoMeet { get; set; }
        public string[]? invitados { get; set; }
        public string[]? organizadores { get; set; }
        public string? TokenQr { get; set; }
    }


    public class ValidarMeetModelo
    {
       
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdSala { get; set; }
    }


    public class MeetModelo
    {
        public int IdMeet { get; set; }
        
        public string Titulo { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        
        public DateTime FechaInicio { get; set; }
        
        public DateTime? HoraInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        
        public DateTime? HoraFin { get; set; }
        
        public string? TokenQr { get; set; }
        
        public int IdSala { get; set; }
        
        public int IdPrioridad { get; set; }
        
        public int IdEstado { get; set; }
        
        public int IdTipoMeet { get; set; }
        
        // Propiedades de navegación para mostrar información relacionada
        public string? NombreSala { get; set; }
        public string? NombrePrioridad { get; set; }
        public string? NombreEstado { get; set; }
        public string? NombreTipoMeet { get; set; }
        public string? Invitados { get; set; }
        public string? Organizadores { get; set; }
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



    //---------------------- validacion token
    public class TokenValidationResponse
    {
        public bool IsValid { get; set; }
        public int ValidationCode { get; set; }  // Nuevo campo

        public MeetingInfo? Meeting { get; set; }

        public string? Message { get; set; }
    }

    public class MeetingInfo
    {
        public int IdMeet { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }        
    }


    public class AsistenciaModelo
    {
        public string Token { get; set; } = string.Empty;
        public long Dpi { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Puesto { get; set; } = string.Empty;
        public string Institucion { get; set; } = string.Empty;
        public string TelefonoExtension { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public int RangoEdad { get; set; }
        public string RangoEdadTexto { get; set; } = string.Empty;
        public int Discapacidad { get; set; }
        public string DiscapacidadTexto { get; set; } = string.Empty;
        public int Pueblo { get; set; }
        public string PuebloTexto { get; set; } = string.Empty;
        public int ComunidadLinguistica { get; set; }
        public string ComunidadLinguisticaTexto { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }




}

