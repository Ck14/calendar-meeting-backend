using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    /// <summary>
    /// Modelo base para catálogos del sistema
    /// </summary>
    public class CatalogoModelo
    {
        /// <summary>
        /// Identificador único del catálogo
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Nombre del elemento del catálogo
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
        
        /// <summary>
        /// Descripción detallada del elemento del catálogo
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
    }

    public class CatalogoOpcionMenuModelo
    {
        public int IdOpcionMenu { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Path { get; set; }
    }

}
