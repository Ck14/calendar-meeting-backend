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
    public class TestController : Controller
    {
        

        public TestController()
        {
            
        }

        
        [Route("LivenessProbe")]
        [HttpGet]
        public async Task<IActionResult> LivenessProbe()
        {
            try
            {

                return Ok(new { liveness = true });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    liveness = false,
                    message = e.Message
                });
            }
        }


        [Route("ReadinessProbe")]
        [HttpGet]
        public async Task<IActionResult> ReadinessProbe()
        {
            try
            {
                
                
                return Ok(new { readiness = true });
                
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                return BadRequest(new { 
                    readiness = false, 
                    message= ex.Message 
                });
                
                
            }
        }


        [Route("Exceptionless")]
        [HttpGet]
        public async Task<IActionResult> Exceptionless()
        {
            try
            {

                throw new Exception("Testing exceptionless from clima-laboral");

            }
            catch (Exception ex)
            {
                try
                {
                    ex.ToExceptionless().Submit();
                }
                catch (Exception)
                {

                    throw;
                }
                
                return BadRequest(new
                {                    
                    message = ex.Message
                });
                

            }
        }


    }
}
