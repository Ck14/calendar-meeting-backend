using Core.Models.Correo;
using Exceptionless;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections;
using System.Text;

namespace Core.Correo.Servicios
{
    public class CorreoServicio : ICorreoServicio
    {

        public IConfiguration configuration;



        public CorreoServicio(IConfiguration config)
        {
            this.configuration = config;
        }
        /// <summary>
        /// REGISTRADOR -> APROBADOR
        /// </summary>
        /// <param name="idPropuesta"></param>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        async Task<bool> ICorreoServicio.CorreoCreacionUsuario(List<string> destinatarios)
        {
            var correo = new EnviarCorreoElectronico();


            var msj = "";

            var Fecha = DateTime.Now;
            correo.Asunto = string.Format("Creación de Usuario Clima Laboral");

            correo.Modelo = new
            {
                fechaActual = DateTime.Now,
                direccionSistema = configuration.GetSection("ServidorCorreo:direccionSistema").Value
            };

            correo.Destinatarios = destinatarios;
            correo.Remitente = configuration.GetSection("ServidorCorreo:remitente").Value;
            correo.Sistema = configuration.GetSection("ServidorCorreo:ApiKey").Value;
            correo.NombrePlantilla = "plantillaCorreo";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    var mailServer = configuration.GetSection("ServidorCorreo:servidor").Value;

                    string jsonValue = JsonConvert.SerializeObject(correo);
                    response = await client.PostAsync(mailServer, new StringContent(jsonValue, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();

                }
                return true;

            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                throw;
            }

        }// fin


        async Task<bool> ICorreoServicio.EnAutorizacion(int noSolicitud, string fechaVisita, string destino, string horaIngreso, string horaEgreso, List<string> destinatarios)
        {

            var correo = new EnviarCorreoElectronico();


            var msj = "";

            var Fecha = DateTime.Now;
            correo.Asunto = string.Format("Notificación de Creación de Solicitud");

            correo.Modelo = new
            {
                noSolicitud,
                fechaVisita,
                destino,
                horaIngreso,
                horaEgreso
            };

            correo.Destinatarios = destinatarios.ToArray();
            correo.Remitente = configuration.GetSection("ServidorCorreo:remitente").Value;
            correo.Sistema = configuration.GetSection("ServidorCorreo:ApiKey").Value;
            correo.NombrePlantilla = "plantillaCambioEstado";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    var mailServer = configuration.GetSection("ServidorCorreo:servidor").Value;

                    string jsonValue = JsonConvert.SerializeObject(correo);
                    response = await client.PostAsync(mailServer, new StringContent(jsonValue, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();

                }
                return true;

            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                throw;
            }

        }// end envío de correo


        async Task<bool> ICorreoServicio.Rechazo(int noSolicitud, string motivoRechazo, List<string> destinatarios)
        {

            var correo = new EnviarCorreoElectronico();


            var msj = "";

            var Fecha = DateTime.Now;
            correo.Asunto = string.Format("Notificación de Rechazo de Solicitud");

            correo.Modelo = new
            {
                noSolicitud,
                motivoRechazo
            };


            correo.Destinatarios = destinatarios.ToArray();
            correo.Remitente = configuration.GetSection("ServidorCorreo:remitente").Value;
            correo.Sistema = configuration.GetSection("ServidorCorreo:ApiKey").Value;
            correo.NombrePlantilla = "plantillaRechazo";


            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    var mailServer = configuration.GetSection("ServidorCorreo:servidor").Value;

                    string jsonValue = JsonConvert.SerializeObject(correo);
                    response = await client.PostAsync(mailServer, new StringContent(jsonValue, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();

                }
                return true;

            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                throw;
            }

        }// end envío de correo



        async Task<bool> ICorreoServicio.Autorizada(int noSolicitud, List<string> destinatarios)
        {

            var correo = new EnviarCorreoElectronico();


            var msj = "";

            var Fecha = DateTime.Now;
            correo.Asunto = string.Format("Notificación de Autorización de Solicitud");

            correo.Modelo = new
            {
                noSolicitud
            };


            correo.Destinatarios = destinatarios.ToArray();
            correo.Remitente = configuration.GetSection("ServidorCorreo:remitente").Value;
            correo.Sistema = configuration.GetSection("ServidorCorreo:ApiKey").Value;
            correo.NombrePlantilla = "plantillaAutorizado";


            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    var mailServer = configuration.GetSection("ServidorCorreo:servidor").Value;

                    string jsonValue = JsonConvert.SerializeObject(correo);
                    response = await client.PostAsync(mailServer, new StringContent(jsonValue, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();

                }
                return true;

            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
                throw;
            }

        }// end envío de correo



        async Task<string> ICorreoServicio.EnviarCorreoEncuesta(int idEncuesta, string url)
        {
            var correo = new EnviarCorreoElectronico();


            var Fecha = DateTime.Now;
            correo.Asunto = string.Format("CLIMA LABORAL - ENCUESTA", Fecha);
            string msj = string.Format("Te invitamos a participar en nuestra encuesta en línea para ayudarnos a mejorar nuestra institución.");


            correo.Modelo = new
            {
                mensaje = msj,
                url = string.Format("'{0}'", url)
            };


            correo.Destinatarios = configuration.GetSection("ServidorCorreo:correoSoporte").GetChildren().Select(x => x.Value).ToArray();
            correo.Remitente = configuration.GetSection("ServidorCorreo:remitente").Value;
            correo.Sistema = configuration.GetSection("ServidorCorreo:ApiKey").Value;
            correo.NombrePlantilla = "plantillaCorreo2";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    var mailServer = configuration.GetSection("ServidorCorreo:servidor").Value;

                    string jsonValue = JsonConvert.SerializeObject(correo);
                    response = await client.PostAsync(mailServer, new StringContent(jsonValue, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    return result;

                    //return "";

                }
                
            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                throw;
            }

        }// fin

    }// end class
}// end namespace
