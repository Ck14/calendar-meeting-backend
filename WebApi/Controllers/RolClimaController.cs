using Core.Models;
using Core.Servicios;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RolController : Controller
    {
        private readonly IRolClimaServicio rolClimaServicio;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public RolController(IRolClimaServicio rolClimaServicio)
        {
            this.rolClimaServicio = rolClimaServicio;
        }

      

        /// <summary>
        /// Obtiene los datos del contribuyente en base al nit.
        /// El correo electrónico se retorna enmascarado.
        /// </summary>
        /// <param name="nit">Nit del contribuyente</param>
        /// <returns>Objeto que contiende los datos del contribuyente</returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> ObtnerRoles(string nitUsuario)
        {
            try
            {
                var usuario = await rolClimaServicio.ObtenerRoles();
                return Ok(usuario);
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
                return BadRequest();
            }
        }



        [Route("porUsuario")]
        [HttpGet]
        public async Task<IActionResult> RolesPorUsuario(string nitUsuario)
        {
            try
            {
                var usuario = await rolClimaServicio.RolesPorUsuario(nitUsuario);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
                return BadRequest();
            }
        }

        [Route("sinAsignar")]
        [HttpGet]
        public async Task<IActionResult> RolesSinAsignar(string nitUsuario)
        {
            try
            {
                var usuario = await rolClimaServicio.RolesSinAsignar(nitUsuario);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
                return BadRequest();
            }
        }


    }
}
