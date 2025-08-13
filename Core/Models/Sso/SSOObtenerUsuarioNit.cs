using System.ComponentModel.DataAnnotations;

namespace Core.Models.Sso
{
    public class SSOObtenerUsuarioNit
    {
        [Required(ErrorMessage = "El Nit es requerido")]
        public string Nit;

        [Required(ErrorMessage = "La private key es requerida")]
        public string PrivateKeyXml;

    }
}
