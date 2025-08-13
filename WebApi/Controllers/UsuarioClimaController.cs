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
    public class UsuarioClimaController : Controller
    {
        private readonly IUsuarioClimaServicio usuarioClimaServicio;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public UsuarioClimaController(IUsuarioClimaServicio usuarioClimaServicio)
        {
            this.usuarioClimaServicio = usuarioClimaServicio;
        }

        /// <summary>
        /// Obtiene los datos del contribuyente en base al nit.
        /// El correo electrónico se retorna enmascarado.
        /// </summary>
        /// <param name="nit">Nit del contribuyente</param>
        /// <returns>Objeto que contiende los datos del contribuyente</returns>
        [Route("consulta/{nit}")]
        [HttpGet]
        public async Task<IActionResult> ObtnerUsuario(String nit)
        {
            try
            {
                var usuario = await usuarioClimaServicio.ObtenerUsuario(nit);
                return Ok(usuario);                
            }
            catch (Exception e)
            {
                e.ToExceptionless().Submit();
                return  BadRequest();   
            }
        }


        /// <summary>
        /// Registra un usuario en el sistema de clima laboral
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioClimaModelo usuario)
        {
            try
            {
                var claimsDictionary = (User.Identity as ClaimsIdentity)?.Claims.ToDictionary(x => x.Type, x => x.Value);
                var nit = claimsDictionary.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                   ? claimsDictionary.Where(e => e.Key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                   .Select(e => e.Value).First()
                   : "";

                usuario.NitRegistro = nit;
                var insertado = await usuarioClimaServicio.InsertarUsuario(usuario);
                return Ok(insertado);
            }
            catch (Exception ex)//Para enviar la excepción a exceptionless
            {
                ex.ToExceptionless().Submit();
                return StatusCode(500);
            }

        }



        /// <summary>
        /// Registra un usuario en el sistema de clima laboral
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("asignarRol")]
        public async Task<IActionResult> AsignarRol(string nitUsuario, int idRol)
        {
            try
            {
                var claimsDictionary = (User.Identity as ClaimsIdentity)?.Claims.ToDictionary(x => x.Type, x => x.Value);
                var nit = claimsDictionary.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                   ? claimsDictionary.Where(e => e.Key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                   .Select(e => e.Value).First()
                   : "";
                
                var insertado = await usuarioClimaServicio.AsignarRol(nitUsuario, idRol, nit);
                return Ok(insertado);
            }
            catch (Exception ex)//Para enviar la excepción a exceptionless
            {
                ex.ToExceptionless().Submit();
                return StatusCode(500);
            }

        }


        /// <summary>
        /// Registra un usuario en el sistema de clima laboral
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpDelete("desasignarRol")]
        public async Task<IActionResult> DesasignarRol(string nitUsuario, int idRol)
        {
            try
            {
                var claimsDictionary = (User.Identity as ClaimsIdentity)?.Claims.ToDictionary(x => x.Type, x => x.Value);
                var nit = claimsDictionary.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                   ? claimsDictionary.Where(e => e.Key == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                   .Select(e => e.Value).First()
                   : "";

                var eliminado = await usuarioClimaServicio.DesasignarRol(nitUsuario, idRol, nit);
                return Ok(eliminado);
            }
            catch (Exception ex)//Para enviar la excepción a exceptionless
            {
                ex.ToExceptionless().Submit();
                return StatusCode(500);
            }

        }


        [HttpPut("activar")]
        public async Task<IActionResult> ActivarDesactivar(string nitUsuario, bool activar)
        {
            try
            {
                var actualizado = await usuarioClimaServicio.ActivarDesasctivar(nitUsuario, activar);
                return Ok(actualizado);
            }
            catch (Exception ex)//Para enviar la excepción a exceptionless
            {
                ex.ToExceptionless().Submit();
                return StatusCode(500);
            }

        }

        [HttpPut("actualizarCorreoInstitucional")]
        public async Task<IActionResult> actualizarCorreoInstitucional(string nitUsuario, string correoInstitucional)
        {
            try
            {
                var actualizado = await usuarioClimaServicio.ACtualizarCorreoInstitucional(nitUsuario, correoInstitucional);
                return Ok(actualizado);
            }
            catch (Exception ex)//Para enviar la excepción a exceptionless
            {
                ex.ToExceptionless().Submit();
                return StatusCode(500);
            }

        }


        [HttpPut("actualizarCorreoPersonal")]
        public async Task<IActionResult> actualizarCorreoPersonal(string nitUsuario, string correoPersonal)
        {
            try
            {
                var actualizado = await usuarioClimaServicio.ACtualizarCorreoPersonal(nitUsuario, correoPersonal);
                return Ok(actualizado);
            }
            catch (Exception ex)//Para enviar la excepción a exceptionless
            {
                ex.ToExceptionless().Submit();
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Obtiene los datos del contribuyente en base al nit.
        /// El correo electrónico se retorna enmascarado.
        /// </summary>
        /// <param name="nit">Nit del contribuyente</param>
        /// <returns>Objeto que contiende los datos del contribuyente</returns>
        [Route("consulta")]
        [HttpGet]
        public async Task<IActionResult> ObtnerUsuarios()
        {
            try
            {
                var usuario = await usuarioClimaServicio.ObtenerUsuarios();
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
