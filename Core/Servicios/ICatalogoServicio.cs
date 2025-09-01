using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios
{
    public interface ICatalogoServicio
    {

        Task<IEnumerable<SalaModelo>> ObtenerSalas();
        Task<IEnumerable<PrioridadModelo>> ObtenerPrioridades();
        Task<ResultadoHttpModelo> ObtenerOpcionesMenu();
        Task<ResultadoHttpModelo> ObtenerRoles();
        Task<IEnumerable<RangoEdad>> ObtenerRangoEdad();
        Task<IEnumerable<ComunidadLinguistica>> ObtenerLengua();
        Task<IEnumerable<Pueblo>> ObtenerPueblo();
        Task<IEnumerable<Discapacidad>> ObtenerDiscapacidad();
        Task<TokenValidationResponse> ValidarTokenAsync(string token);
    }
}
