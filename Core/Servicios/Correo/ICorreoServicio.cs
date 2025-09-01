
using Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Correo.Servicios
{
    public interface ICorreoServicio
    {
        Task<bool> CorreoCreacionUsuario(List<string> destinatarios);
        Task<bool> EnAutorizacion(int noSolicitud, string fechaVisita, string destino, string horaInicio, string horaFin, List<string> destinatarios);
        Task<bool> Rechazo(int noSolicitud, string motivoRechazo, List<string> destinatarios);
        Task<bool> Autorizada(int noSolicitud, List<string> destinatarios);

        Task<string> EnviarCorreoEncuesta(int idEncuesta, string url);

        Task<string> EnviarCorreoInvitacion(MeetCrearModelo meet);
    }
}
