using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.Servicios
{
    public interface IMeetServicio
    {

        Task<IEnumerable<AttendeeModelo>> ObtenerParticipantes();
        Task<IEnumerable<MeetModelo>> ObtenerSalasOcupadas(ValidarMeetModelo meet);
        Task<ResultadoHttpModelo> CrearAsync(MeetCrearModelo meet);
        Task<ResultadoHttpModelo> ActualizarAsync(MeetCrearModelo meet);
        Task<ResultadoHttpModelo> ActualizarHorariosAsync(MeetCrearModelo meet);
        Task<IEnumerable<MeetModelo>> ObtenerReunionesPorRango(DateTime startDate, DateTime endDate);
        Task<ResultadoHttpModelo> RegistrarAsistencia(AsistenciaModelo asistencia);
        Task<IEnumerable<AsistenciaModelo>> ObtenerParticipantes(string token);

    }
}
