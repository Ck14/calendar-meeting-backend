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
}
