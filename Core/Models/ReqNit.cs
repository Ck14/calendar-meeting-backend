using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class ReqNit
    {
        [Required(ErrorMessage = "Nit es requerido")]
        public string Nit { get; set; }
    }
}
