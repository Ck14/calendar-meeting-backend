using Core.Constantes;
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

        public MeetServicio(IMeetRepositorio meetRepositorio, IValidador validador)
        {
            _meetRepositorio = meetRepositorio;
            _validador = validador;
        }

        public async Task<ResultadoHttpModelo> ObtenerTodasAsync()
        {
            return await _meetRepositorio.ObtenerTodasAsync();
        }

        public async Task<ResultadoHttpModelo> ObtenerPorIdAsync(int idMeet)
        {
            if (idMeet <= 0)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "El ID de la reunión debe ser mayor a 0",
                    Titulo = "Validación de Datos"
                };
            }

            return await _meetRepositorio.ObtenerPorIdAsync(idMeet);
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

            // Validaciones de negocio
            var businessValidation = ValidarReglasNegocioCrear(meet);
            if (!businessValidation.IsValid)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = businessValidation.ErrorMessage,
                    Titulo = "Validación de Negocio"
                };
            }

            return await _meetRepositorio.CrearAsync(meet);
        }

        public async Task<ResultadoHttpModelo> ActualizarAsync(MeetActualizarModelo meet)
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

            // Validaciones de negocio
            var businessValidation = ValidarReglasNegocioActualizar(meet);
            if (!businessValidation.IsValid)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = businessValidation.ErrorMessage,
                    Titulo = "Validación de Negocio"
                };
            }

            return await _meetRepositorio.ActualizarAsync(meet);
        }

        public async Task<ResultadoHttpModelo> EliminarAsync(int idMeet)
        {
            if (idMeet <= 0)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "El ID de la reunión debe ser mayor a 0",
                    Titulo = "Validación de Datos"
                };
            }

            // Verificar si la reunión existe antes de eliminar
            var meetExistente = await _meetRepositorio.ObtenerPorIdAsync(idMeet);
            if (meetExistente.Estado != EstadoSolicitudHttp.success.ToString())
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.warning)
                {
                    Mensaje = "No se puede eliminar una reunión que no existe",
                    Titulo = "Reunión No Encontrada"
                };
            }

            return await _meetRepositorio.EliminarAsync(idMeet);
        }

        public async Task<ResultadoHttpModelo> ObtenerPorSalaAsync(int idSala)
        {
            if (idSala <= 0)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "El ID de la sala debe ser mayor a 0",
                    Titulo = "Validación de Datos"
                };
            }

            return await _meetRepositorio.ObtenerPorSalaAsync(idSala);
        }

        public async Task<ResultadoHttpModelo> ObtenerPorFechaAsync(DateTime fecha)
        {
            if (fecha == default)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "La fecha es requerida",
                    Titulo = "Validación de Datos"
                };
            }

            return await _meetRepositorio.ObtenerPorFechaAsync(fecha);
        }

        public async Task<ResultadoHttpModelo> ObtenerPorRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "Las fechas de inicio y fin son requeridas",
                    Titulo = "Validación de Datos"
                };
            }

            if (fechaInicio > fechaFin)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "La fecha de inicio no puede ser mayor a la fecha de fin",
                    Titulo = "Validación de Fechas"
                };
            }

            return await _meetRepositorio.ObtenerPorRangoFechasAsync(fechaInicio, fechaFin);
        }

        #region Validaciones de Negocio

        private (bool IsValid, string? ErrorMessage) ValidarReglasNegocioCrear(MeetCrearModelo meet)
        {
            // Validar que la fecha de inicio no sea en el pasado
            if (meet.FechaInicio < DateTime.Today)
            {
                return (false, "La fecha de inicio no puede ser en el pasado");
            }

            // Validar que si hay fecha de fin, sea posterior a la fecha de inicio
            if (meet.FechaFin.HasValue && meet.FechaFin.Value < meet.FechaInicio)
            {
                return (false, "La fecha de fin no puede ser anterior a la fecha de inicio");
            }

            // Validar que si hay hora de fin, sea posterior a la hora de inicio
            if (meet.HoraFin.HasValue && meet.HoraInicio.HasValue && 
                meet.HoraFin.Value <= meet.HoraInicio.Value)
            {
                return (false, "La hora de fin debe ser posterior a la hora de inicio");
            }

            return (true, null);
        }

        private (bool IsValid, string? ErrorMessage) ValidarReglasNegocioActualizar(MeetActualizarModelo meet)
        {
            // Validar que la fecha de inicio no sea en el pasado
            if (meet.FechaInicio < DateTime.Today)
            {
                return (false, "La fecha de inicio no puede ser en el pasado");
            }

            // Validar que si hay fecha de fin, sea posterior a la fecha de inicio
            if (meet.FechaFin.HasValue && meet.FechaFin.Value < meet.FechaInicio)
            {
                return (false, "La fecha de fin no puede ser anterior a la fecha de inicio");
            }

            // Validar que si hay hora de fin, sea posterior a la hora de inicio
            if (meet.HoraFin.HasValue && meet.HoraInicio.HasValue && 
                meet.HoraFin.Value <= meet.HoraInicio.Value)
            {
                return (false, "La hora de fin debe ser posterior a la hora de inicio");
            }

            return (true, null);
        }

        #endregion
    }
}
