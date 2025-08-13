using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.Servicios
{
    public interface IMeetServicio
    {
        Task<ResultadoHttpModelo> ObtenerTodasAsync();
        Task<ResultadoHttpModelo> ObtenerPorIdAsync(int idMeet);
        Task<ResultadoHttpModelo> CrearAsync(MeetCrearModelo meet);
        Task<ResultadoHttpModelo> ActualizarAsync(MeetActualizarModelo meet);
        Task<ResultadoHttpModelo> EliminarAsync(int idMeet);
        Task<ResultadoHttpModelo> ObtenerPorSalaAsync(int idSala);
        Task<ResultadoHttpModelo> ObtenerPorFechaAsync(DateTime fecha);
        Task<ResultadoHttpModelo> ObtenerPorRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
