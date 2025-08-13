using Core.Models;
using Core.Servicios;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SatController : Controller
    {
        private readonly IContribuyenteServicio servicio;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public SatController(IContribuyenteServicio repo)
        {
            this.servicio = repo;
        }

        /// <summary>
        /// Obtiene los datos del contribuyente en base al nit.
        /// El correo electrónico se retorna enmascarado.
        /// </summary>
        /// <param name="nit">Nit del contribuyente</param>
        /// <returns>Objeto que contiende los datos del contribuyente</returns>
        [Route("consulta/{nit}")]
        [HttpGet]
        public async Task<IActionResult> Get(String nit)
        {
            ReqNit req = new ReqNit();
            req.Nit = nit.ToUpper();

            try
            {
                var contribuyente = await servicio.ObtenerPorNitAsync(req);

                if (contribuyente == null)
                {
                    return NotFound();
                }

                contribuyente.Email = contribuyente.Email != null ? contribuyente.Email.Replace(" ", "") : String.Empty;
                contribuyente.Email = contribuyente.Email == String.Empty ? null : contribuyente.Email;
                return Ok(contribuyente);
            }
            catch (Exception e)
            {
                //TO DO: Agregar Exceptionless
                // NO se agrega cuando el NIT enviado NO tiene el formato correcto
                if (!e.Message.ToLower().Contains("nit no tiene el formato correcto"))
                {
                    e.ToExceptionless().Submit();
                }
                return StatusCode(500);
            }
        }
    }
}
