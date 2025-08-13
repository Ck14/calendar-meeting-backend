using Core.Models;
using Core.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    public interface ICatalogoRepositorio
    {
        Task<ResultadoHttpModelo> ObtenerOpcionesMenu();
        Task<ResultadoHttpModelo> ObtenerRoles();
        
        // Métodos para catálogos de reuniones
        Task<ResultadoHttpModelo> ObtenerSalas();
        Task<ResultadoHttpModelo> ObtenerPrioridades();
        Task<ResultadoHttpModelo> ObtenerEstadosFormulario();
        Task<ResultadoHttpModelo> ObtenerTiposMeet();
    }
}
