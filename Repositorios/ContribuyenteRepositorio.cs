using Core.Modelos;
using Core.Models;
using Core.Repositorios;
using Repositorios.ServiciosSoap;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;

namespace Repositorios
{
    public class ContribuyenteRepositorio : IContribuyenteRepositorio
    {
        private readonly IConsultaSatServicio service;
        private readonly ConsultaCLConfig config;        

        public ContribuyenteRepositorio(ConsultaCLConfig config)
        {
            this.config = config;
            
            var binding = new BasicHttpsBinding();
            var endpointAddress = new EndpointAddress(new Uri(config.Endpoint));            
            service = new ConsultaSatSoapClient(binding, endpointAddress);
            
        }

        public string ObtenerEscrituracionPorNit(ReqNit req)
        {
            throw new NotImplementedException();
        }

        public ContribuyenteModelo ObtenerPorNit(ReqNit req)
        {
            XmlDocument doc = null;
            var request = new findContribuyenteByNitGCRequest { pUsuario = config.Usuario, pContrasenia = config.Clave, pNit = req.Nit };
            var response = service.findContribuyenteByNitGC(request);
            if (response == null)
            {
                return null;
            }

            var xml = response?.findContribuyenteByNitGCReturn;
            doc = new XmlDocument();
            doc.LoadXml(xml);

            if (doc == null)
            {
                return null;
            }

            var firstChild = doc.DocumentElement.FirstChild;
            if (firstChild.Name == "CODIGO_ERROR")
            {
                if (firstChild.InnerText == "56")
                {
                    return null;
                }
            }

            var serializer = new XmlSerializer(typeof(GC_REQCONTRIBUYENTE));
            var contribuyente = serializer.Deserialize(new StringReader(xml)) as GC_REQCONTRIBUYENTE;
            var datosGenerales = contribuyente?.DG;
            var domicilioFiscal = contribuyente?.DOM_FIS;
            var insolvencia = contribuyente?.INSOLVENCIA;

            if (datosGenerales == null) return null;
            var direccion = $@"{domicilioFiscal.COA} {domicilioFiscal.NC} {domicilioFiscal.ZON} {domicilioFiscal.COL}";

            var fechaInscripcionRTU = new DateTime?();

            try
            {
                fechaInscripcionRTU = new DateTime(Int32.Parse(datosGenerales.FIRTU.Substring(4)),
                    Int32.Parse(datosGenerales.FIRTU.Substring(2, 2)),
                    Int32.Parse(datosGenerales.FIRTU.Substring(0, 2)));
            }
            catch
            {
                fechaInscripcionRTU = null;
            }

            return new ContribuyenteModelo
            {
                Nit = datosGenerales.NIT,
                Estado = datosGenerales.ESTNIT,
                Cui = datosGenerales.CUI,
                Nombre = datosGenerales.NOM,
                Email = domicilioFiscal.EMA,
                Telefono = domicilioFiscal.TEL,
                DomicilioFiscal = direccion,
                TipoOrganizacion = datosGenerales.TO,
                ActividadEconomica = datosGenerales.AECONOMICA,
                Sexo = datosGenerales.SEXO,
                FechaInscripcionRTU = fechaInscripcionRTU,
                EstadoInsolvencia = insolvencia.EIIN,
                Departamento = domicilioFiscal.CD,
                Municipio = domicilioFiscal.CM
            };
        }

        

        public List<string> ObtenerRlgPorNit(ReqNit req)
        {
            throw new NotImplementedException();
        }
    }
}
