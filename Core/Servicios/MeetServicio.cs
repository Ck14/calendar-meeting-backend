using Core.Constantes;
using Core.Correo.Servicios;
using Core.Models;
using Core.Repositorios;
using Core.Validadores;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Servicios
{
    public class MeetServicio : IMeetServicio
    {
        private readonly IMeetRepositorio _meetRepositorio;
        private readonly IValidador _validador;
        private readonly ICorreoServicio _correoServicio;

        public MeetServicio(IMeetRepositorio meetRepositorio, IValidador validador, ICorreoServicio correoServicio)
        {
            _meetRepositorio = meetRepositorio;
            _validador = validador;
            _correoServicio = correoServicio;
        }

        public async Task<IEnumerable<AttendeeModelo>> ObtenerParticipantes()
        {
            try
            {
                var participantes = await _meetRepositorio.ObtenerParticipantes();
                return participantes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener participantes: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<MeetModelo>> ObtenerSalasOcupadas(ValidarMeetModelo meet)
        {

            try
            {
                // Obtener las salas ocupadas
                var salasOcupadas = await _meetRepositorio.GetSalasOcupadas(meet);
                return salasOcupadas;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las salas: {ex.Message}", ex);
            }
        }

        public async Task<ResultadoHttpModelo> CrearAsync(MeetCrearModelo meet)
        {
            try
            {
                // Validar el modelo
                _validador.Validar(meet);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)),
                    Titulo = "Validación de Datos"
                };
            }


            var idMeet = await _meetRepositorio.CrearAsync(meet);
            if (idMeet!=0){
                await _correoServicio.EnviarCorreoInvitacion(meet);

            }


            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Reunión creada exitosamente",
                Titulo = "Creación de Reunión",
                Resultado = new { IdMeet = idMeet }
            };
        }


        public async Task<ResultadoHttpModelo> ActualizarAsync(MeetCrearModelo meet)
        {
            try
            {
                // Validar el modelo
                _validador.Validar(meet);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)),
                    Titulo = "Validación de Datos"
                };
            }


            var idMeet = await _meetRepositorio.ActualizarAsync(meet);
            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Reunión actualizada exitosamente",
                Titulo = "Actualización de Reunión",
                Resultado = new { IdMeet = idMeet }
            };
        }



        public async Task<ResultadoHttpModelo> ActualizarHorariosAsync(MeetCrearModelo meet)
        {
            try
            {
                // Validar el modelo
                _validador.Validar(meet);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)),
                    Titulo = "Validación de Datos"
                };
            }


            var idMeet = await _meetRepositorio.ActualizarHorariosAsync(meet);
            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Reunión actualizada exitosamente",
                Titulo = "Actualización de Reunión",
                Resultado = new { IdMeet = idMeet }
            };
        }

        public async Task<IEnumerable<MeetModelo>> ObtenerReunionesPorRango(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reuniones = await _meetRepositorio.ObtenerReunionesPorRango(startDate, endDate);
                return reuniones;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener reuniones por rango de fechas: {ex.Message}", ex);
            }
        }

        public async Task<ResultadoHttpModelo> RegistrarAsistencia(AsistenciaModelo asistencia)
        {

            try
            {
                var idParticipante = await _meetRepositorio.RegistrarAsistencia(asistencia);

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Asistencia registrada exitosamente",
                    Titulo = "Asistencia registrada",
                    Resultado = new { IdParticipante = idParticipante }
                };

            }
            catch (Exception ex)
            {

                if(ex.Message.Contains("ORA-00001"))
                {
                    return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                    {
                        Mensaje = $"El usuario con no. DPI {asistencia.Dpi} ya registró su asistencia en la meet",
                        Titulo = "Asistencia registrada",
                        Resultado = new { IdParticipante = 0 }
                    };

                }
                else
                {
                    throw;
                }


            }
            

        }

        public async Task<IEnumerable<AsistenciaModelo>> ObtenerParticipantes(string token)
        {
            try
            {
                var participantes = await _meetRepositorio.ObtenerParticipantes(token);
                return participantes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener participantes", ex);
            }

        }

    }
}
