
namespace Core.Models
{
    public class ContribuyenteModelo
    {
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public string Cui { get; set; }
        public string Email { get; set; }
        public string EmailSinMascara { get; set; }
        public string Estado { get; set; }
        public string Telefono { get; set; }
        public string DomicilioFiscal { get; set; }
        public int TipoOrganizacion { get; set; }
        public string ActividadEconomica { get; set; }
        public int Procedencia { get; set; }
        public string Sexo { get; set; }
        public DateTime? FechaInscripcionRTU { get; set; }
        public string EstadoInsolvencia { get; set; }
        public int Departamento { get; set; }
        public int Municipio { get; set; }
    }
}
