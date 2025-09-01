using Core.Models;
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



        async Task<string> ICorreoServicio.EnviarCorreoInvitacion(MeetCrearModelo meet)
        {
            var correo = new EnviarCorreoEvento();

            // Construcción de campos básicos
            correo.Asunto = $"Invitación: {meet.Titulo}";
            correo.Remitente = configuration.GetSection("ServidorCorreo:remitente").Value;
            correo.Sistema = configuration.GetSection("ServidorCorreo:ApiKey").Value;
            //correo.NombrePlantilla = "plantillaCorreo2";

            // Destinatarios (los invitados de la reunión)
            if (meet.invitados != null)
                correo.Destinatarios = meet.invitados.ToList();

            // Se podría agregar copia oculta si aplica
            //correo.DestinatariosBCC = new List<string>();

            // Título y descripción del evento
            correo.TituloEvento = meet.Titulo;
            correo.DescripcionEvento = meet.Descripcion ?? "Reunión institucional";

            // Fechas
            correo.FechaInicio = meet.FechaInicio;
            correo.FechaFin = meet.FechaFin ?? meet.FechaInicio.AddHours(1); // default: 1 hora si no mandan fin

            // Ubicación (se podría mapear desde IdSala con otra tabla/configuración)
            correo.Ubicacion = $"Sala: {meet.Sala} - Ministerio de Finanzas Públicas";

            // Organizador (siempre el primero de organizadores o default)
            correo.OrganizadorNombre = "Minfin Meetings";
            correo.OrganizadorEmail = string.Join(",", meet.organizadores ?? Array.Empty<string>());

            // Mensaje personalizado
            correo.MensajePersonalizado = "Esta reunión es importante para la institución. Su participación es fundamental.";

            // Link de la reunión (puede salir de config o del modelo si lo agregas después)
            //correo.LinkReunion = configuration.GetSection("ServidorCorreo:linkReunionDefault").Value;

            // Instrucciones y material (estáticos o parametrizables)
            //correo.InstruccionesAdicionales = "INSTRUCCIONES:\n• Llegar puntual\n• Revisar documentos previos\n• Traer laptop con VPN";
            //correo.MaterialPreparatorio = "Material disponible en SharePoint: https://sharepoint.minfin.gob.gt";

            // Confirmación de asistencia
            correo.RequiereRespuesta = true;
            correo.FechaLimiteRespuesta = meet.FechaInicio.AddDays(-1); // un día antes por default

            // Modelo usado dentro de la plantilla
            correo.Modelo = new
            {
                mensaje = "Te invitamos a participar en nuestra reunión.",
                url = correo.LinkReunion
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var mailServer = configuration.GetSection("ServidorCorreo:servidor").Value;

                    string jsonValue = JsonConvert.SerializeObject(
                        correo,
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore, // no incluir nulls
                            Formatting = Formatting.Indented
                        });

                    var response = await client.PostAsync(mailServer, new StringContent(jsonValue, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            catch (Exception ex)
            {
                ex.ToExceptionless();
                throw;
            }
        }


    }// end class
}// end namespace
