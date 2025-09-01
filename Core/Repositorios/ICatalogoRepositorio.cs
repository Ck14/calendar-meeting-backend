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
        Task<IEnumerable<SalaModelo>> ObtenerSalas();
        Task<IEnumerable<PrioridadModelo>> ObtenerPrioridades();
        Task<IEnumerable<EstadoFormularioModelo>> ObtenerEstadosFormulario();
        Task<IEnumerable<TipoMeetModelo>> ObtenerTiposMeet();
        Task<IEnumerable<RangoEdad>> ObtenerRangoEdad();
        Task<IEnumerable<ComunidadLinguistica>> ObtenerLengua();
        Task<IEnumerable<Pueblo>> ObtenerPueblo();
        Task<IEnumerable<Discapacidad>> ObtenerDiscapacidad();
        Task<TokenValidationResponse> ValidarTokenAsync(string token);

    }
}
