using Core.Models;
using Core.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogosMeetController : ControllerBase
    {
        private readonly ICatalogoRepositorio _catalogoRepositorio;

        public CatalogosMeetController(ICatalogoRepositorio catalogoRepositorio)
        {
            _catalogoRepositorio = catalogoRepositorio;
        }

        /// <summary>
        /// Obtiene las salas disponibles
        /// </summary>
        /// <returns>Lista de salas</returns>
        [HttpGet("salas")]
        public async Task<IActionResult> ObtenerSalas()
        {
            var resultado = await _catalogoRepositorio.ObtenerSalas();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene las prioridades disponibles
        /// </summary>
        /// <returns>Lista de prioridades</returns>
        [HttpGet("prioridades")]
        public async Task<IActionResult> ObtenerPrioridades()
        {
            var resultado = await _catalogoRepositorio.ObtenerPrioridades();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene los estados de formulario disponibles
        /// </summary>
        /// <returns>Lista de estados</returns>
        [HttpGet("estados")]
        public async Task<IActionResult> ObtenerEstados()
        {
            var resultado = await _catalogoRepositorio.ObtenerEstadosFormulario();
            return Ok(resultado);
        }

        /// <summary>
        /// Obtiene los tipos de reunión disponibles
        /// </summary>
        /// <returns>Lista de tipos de reunión</returns>
        [HttpGet("tipos-meet")]
        public async Task<IActionResult> ObtenerTiposMeet()
        {
            var resultado = await _catalogoRepositorio.ObtenerTiposMeet();
            return Ok(resultado);
        }
    }
}
