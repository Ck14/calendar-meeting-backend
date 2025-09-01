using Core.Models;
using Core.Repositorios;
using Core.Servicios;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogosMeetController : ControllerBase
    {
        private readonly ICatalogoServicio _catalogoServicio;

        public CatalogosMeetController(ICatalogoServicio catalogoServicio)
        {
            _catalogoServicio = catalogoServicio;
        }

        /// <summary>
        /// Obtiene las salas disponibles
        /// </summary>
        /// <returns>Lista de salas</returns>
        [HttpGet("salas")]
        public async Task<IActionResult> ObtenerSalas()
        {
            try
            {
                var resultado = await _catalogoServicio.ObtenerSalas();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                return BadRequest(ex.Message);
                


            }

        }

        /// <summary>
        /// Obtiene las prioridades disponibles
        /// </summary>
        /// <returns>Lista de prioridades</returns>
        [HttpGet("prioridades")]
        public async Task<IActionResult> ObtenerPrioridades()
        {

            var resultado = await _catalogoServicio.ObtenerPrioridades();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene los estados de formulario disponibles
        /// </summary>
        /// <returns>Lista de estados</returns>
        [HttpGet("estados")]
        public async Task<IActionResult> ObtenerEstados()
        {
            return Ok();
        }

        /// <summary>
        /// Obtiene los tipos de reunión disponibles
        /// </summary>
        /// <returns>Lista de tipos de reunión</returns>
        [HttpGet("tipos-meet")]
        public async Task<IActionResult> ObtenerTiposMeet()
        {
            
            return Ok();
        }


        [HttpGet("rangosEdad")]
        public async Task<IActionResult> ObtenerRangoEdad()
        {

            var resultado = await _catalogoServicio.ObtenerRangoEdad();
            return Ok(resultado);
        }

        [HttpGet("lenguajes")]
        public async Task<IActionResult> ObtenerLengua()
        {

            var resultado = await _catalogoServicio.ObtenerLengua();
            return Ok(resultado);
        }

        [HttpGet("pueblos")]
        public async Task<IActionResult> ObtenerPueblo()
        {

            var resultado = await _catalogoServicio.ObtenerPueblo();
            return Ok(resultado);
        }

        [HttpGet("discapacidades")]
        public async Task<IActionResult> ObtenerDiscapacidad()
        {

            var resultado = await _catalogoServicio.ObtenerDiscapacidad();
            return Ok(resultado);
        }

        [HttpGet("validateToken")]
        public async Task<IActionResult> ObtenerDiscapacidad(string token)
        {
            try
            {
                var resultado = await _catalogoServicio.ValidarTokenAsync(token);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                return BadRequest(ex.Message);
            }
            
        }


    }
}
