using Core.Constantes;
using Core.Models;
using Core.Servicios;
using Exceptionless;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MeetController : ControllerBase
    {
        private readonly IMeetServicio _meetServicio;

        public MeetController(IMeetServicio meetServicio)
        {
            _meetServicio = meetServicio;
        }

        /// <summary>
        /// Obtiene el listado de usuarios para agregar en los destinatarios
        /// </summary>
        /// <returns></returns>         
        [HttpGet("participantes")]                
        public async Task<IActionResult> ObtenerParticipantes()
        {
           
            try
            {
                var resultado = await _meetServicio.ObtenerParticipantes();
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Reuniones para mostrar para el cambio de vistas
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("reunionesPorRango")]
        public async Task<IActionResult> ObtenerReunionesPorRango([FromQuery] string startDate, [FromQuery] string endDate)
        {
            try
            {
                // Validar que las fechas no sean nulas o vacías
                if (string.IsNullOrEmpty(startDate))
                {
                    return BadRequest("La fecha de inicio es requerida");
                }

                if (string.IsNullOrEmpty(endDate))
                {
                    return BadRequest("La fecha de fin es requerida");
                }

                // Parsear las fechas como DateTime (solo fecha)
                if (!DateTime.TryParse(startDate, out DateTime startDateParsed))
                {
                    return BadRequest("Formato de fecha de inicio inválido. Use formato YYYY-MM-DD");
                }

                if (!DateTime.TryParse(endDate, out DateTime endDateParsed))
                {
                    return BadRequest("Formato de fecha de fin inválido. Use formato YYYY-MM-DD");
                }

                // Validar que la fecha de inicio no sea mayor que la fecha de fin
                if (startDateParsed > endDateParsed)
                {
                    return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin");
                }

                var resultado = await _meetServicio.ObtenerReunionesPorRango(startDateParsed, endDateParsed);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Registrar meet
        /// </summary>
        /// <param name="meet"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MeetCrearModelo meet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "Datos de entrada inválidos",
                    Titulo = "Validación de Modelo"
                });
            }

            try
            {
                var resultado = await _meetServicio.CrearAsync(meet);

                if (resultado.Estado == "warning")
                    return NotFound(resultado);

                if (resultado.Estado == "error")
                    return BadRequest(resultado);

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }


        }



        /// <summary>
        /// Actualizar meet
        /// </summary>
        /// <param name="meet"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] MeetCrearModelo meet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "Datos de entrada inválidos",
                    Titulo = "Validación de Modelo"
                });
            }

            try
            {
                var resultado = await _meetServicio.ActualizarAsync(meet);

                if (resultado.Estado == "warning")
                    return NotFound(resultado);

                if (resultado.Estado == "error")
                    return BadRequest(resultado);

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Actualizar horario al momento de drag & drop
        /// </summary>
        /// <param name="meet"></param>
        /// <returns></returns>
        [HttpPut("actualizarHorarios")]
        public async Task<IActionResult> ActualizarHorarios([FromBody] MeetCrearModelo meet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "Datos de entrada inválidos",
                    Titulo = "Validación de Modelo"
                });
            }

            try
            {
                var resultado = await _meetServicio.ActualizarHorariosAsync(meet);

                if (resultado.Estado == "warning")
                    return NotFound(resultado);

                if (resultado.Estado == "error")
                    return BadRequest(resultado);

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }


        }



        /// <summary>
        /// Comprobación de salas ocupadas
        /// </summary>
        /// <param name="meet"></param>
        /// <returns></returns>
        [HttpPost("salasOcupadas")]
        public async Task<IActionResult> ObtenerSalasOcupadas([FromBody] ValidarMeetModelo meet)
        {

            try
            {
                var resultado = await _meetServicio.ObtenerSalasOcupadas(meet);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }

        }



        /// <summary>
        /// Registrar Asistencia, método publico
        /// </summary>
        /// <param name="asistencia"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Asistencia")]        
        public async Task<IActionResult> RegistrarAsistencia([FromBody] AsistenciaModelo asistencia)
        {
            try
            {
                var resultado = await _meetServicio.RegistrarAsistencia(asistencia);

                if (resultado.Estado == "warning")
                    return NotFound(resultado);

                if (resultado.Estado == "error")
                    return BadRequest(resultado);

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }


        }

        [AllowAnonymous]
        [HttpGet("participantes/consulta")]
        public async Task<IActionResult> ConsultaParticipantes(string token)
        {

            try
            {
                var resultado = await _meetServicio.ObtenerParticipantes(token);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                return BadRequest(ex.Message);
            }

        }













    }
}
