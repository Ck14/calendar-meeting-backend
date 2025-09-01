using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositorios
{
    public interface IMeetRepositorio
    {
        Task<IEnumerable<AttendeeModelo>> ObtenerParticipantes();
        Task<IEnumerable<MeetModelo>> GetSalasOcupadas(ValidarMeetModelo meet);
        Task<int> CrearAsync(MeetCrearModelo meet);
        Task<bool> ActualizarAsync(MeetCrearModelo meet);
        Task<bool> ActualizarHorariosAsync(MeetCrearModelo meet);
        Task<IEnumerable<MeetModelo>> ObtenerReunionesPorRango(DateTime startDate, DateTime endDate);
        Task<int> RegistrarAsistencia(AsistenciaModelo asistencia);
        Task<IEnumerable<AsistenciaModelo>> ObtenerParticipantes(string token);
    }
}
