using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Core.Models.Correo
{
    public class EnviarCorreoElectronico
    {
        public string Remitente { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public string Sistema { get; set; }
        public string NombrePlantilla { get; set; }
        public string RutaPlantilla { get; set; }
        public object Modelo { get; set; }
        public ICollection<string> Destinatarios { get; set; }
        public ICollection<string> Adjuntos { get; set; }        


        public EnviarCorreoElectronico()
        {
            Destinatarios = new List<string>();
            Adjuntos = new List<string>();
        }

    }

    public class EnviarCorreoEvento : EnviarCorreoElectronico
    {
        public ICollection<string> DestinatariosBCC { get; set; }
        public string TituloEvento { get; set; }
        public string DescripcionEvento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ubicacion { get; set; }
        public string OrganizadorNombre { get; set; }
        public string OrganizadorEmail { get; set; }
        public string MensajePersonalizado { get; set; }
        public string LinkReunion { get; set; }
        public string InstruccionesAdicionales { get; set; }
        public string MaterialPreparatorio { get; set; }
        public bool RequiereRespuesta { get; set; }
        public DateTime? FechaLimiteRespuesta { get; set; }
        public string TokenQr{ get; set; }

        public EnviarCorreoEvento() : base()
        {
            DestinatariosBCC = new List<string>();
        }
    }
}
