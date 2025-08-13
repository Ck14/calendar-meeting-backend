using Core.Constantes;
using Core.Models;
using Core.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetController : ControllerBase
    {
        private readonly IMeetServicio _meetServicio;

        public MeetController(IMeetServicio meetServicio)
        {
            _meetServicio = meetServicio;
        }

        /// <summary>
        /// Obtiene todas las reuniones
        /// </summary>
        /// <returns>Lista de todas las reuniones</returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var resultado = await _meetServicio.ObtenerTodasAsync();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene una reunión por su ID
        /// </summary>
        /// <param name="id">ID de la reunión</param>
        /// <returns>Información de la reunión</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _meetServicio.ObtenerPorIdAsync(id);
            
            if (resultado.Estado == "warning")
                return NotFound(resultado);
            
            return Ok(resultado);
        }

        /// <summary>
        /// Crea una nueva reunión
        /// </summary>
        /// <param name="meet">Datos de la reunión a crear</param>
        /// <returns>Resultado de la creación</returns>
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

            var resultado = await _meetServicio.CrearAsync(meet);
            
            if (resultado.Estado == "error")
                return BadRequest(resultado);
            
            return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.Resultado }, resultado);
        }

        /// <summary>
        /// Actualiza una reunión existente
        /// </summary>
        /// <param name="id">ID de la reunión</param>
        /// <param name="meet">Datos actualizados de la reunión</param>
        /// <returns>Resultado de la actualización</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] MeetActualizarModelo meet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "Datos de entrada inválidos",
                    Titulo = "Validación de Modelo"
                });
            }

            if (id != meet.IdMeet)
            {
                return BadRequest(new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = "El ID de la URL no coincide con el ID del modelo",
                    Titulo = "Validación de ID"
                });
            }

            var resultado = await _meetServicio.ActualizarAsync(meet);
            
            if (resultado.Estado == "warning")
                return NotFound(resultado);
            
            if (resultado.Estado == "error")
                return BadRequest(resultado);
            
            return Ok(resultado);
        }

        /// <summary>
        /// Elimina una reunión
        /// </summary>
        /// <param name="id">ID de la reunión a eliminar</param>
        /// <returns>Resultado de la eliminación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _meetServicio.EliminarAsync(id);
            
            if (resultado.Estado == "warning")
                return NotFound(resultado);
            
            if (resultado.Estado == "error")
                return BadRequest(resultado);
            
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene reuniones por sala
        /// </summary>
        /// <param name="idSala">ID de la sala</param>
        /// <returns>Lista de reuniones de la sala</returns>
        [HttpGet("sala/{idSala}")]
        public async Task<IActionResult> ObtenerPorSala(int idSala)
        {
            var resultado = await _meetServicio.ObtenerPorSalaAsync(idSala);
            
            if (resultado.Estado == "error")
                return BadRequest(resultado);
            
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene reuniones por fecha
        /// </summary>
        /// <param name="fecha">Fecha de las reuniones (formato: yyyy-MM-dd)</param>
        /// <returns>Lista de reuniones de la fecha</returns>
        [HttpGet("fecha/{fecha:datetime}")]
        public async Task<IActionResult> ObtenerPorFecha(DateTime fecha)
        {
            var resultado = await _meetServicio.ObtenerPorFechaAsync(fecha);
            
            if (resultado.Estado == "error")
                return BadRequest(resultado);
            
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene reuniones por rango de fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio (formato: yyyy-MM-dd)</param>
        /// <param name="fechaFin">Fecha de fin (formato: yyyy-MM-dd)</param>
        /// <returns>Lista de reuniones en el rango de fechas</returns>
        [HttpGet("rango-fechas")]
        public async Task<IActionResult> ObtenerPorRangoFechas(
            [FromQuery] DateTime fechaInicio, 
            [FromQuery] DateTime fechaFin)
        {
            var resultado = await _meetServicio.ObtenerPorRangoFechasAsync(fechaInicio, fechaFin);
            
            if (resultado.Estado == "error")
                return BadRequest(resultado);
            
            return Ok(resultado);
        }
    }
}
