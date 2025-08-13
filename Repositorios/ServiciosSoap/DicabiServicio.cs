namespace Repositorios.ServiciosSoap
{
    [System.ServiceModel.ServiceContract(Namespace = "http://ws.contribuyente.rtu.sat.gob.gt", ConfigurationName = "DicabiService.ContribuyenteWsEp")]
    public interface IContribuyenteWsEp
    {

        [System.ServiceModel.OperationContract(Action = "", ReplyAction = "*")]
        System.Threading.Tasks.Task<listContribuyenteByNombreDicabiResponse> listContribuyenteByNombreDicabiAsync(listContribuyenteByNombreDicabiRequest request);

        [System.ServiceModel.OperationContract(Action = "", ReplyAction = "*")]
        System.Threading.Tasks.Task<listContribuyentesByFechaAdicionDicabiResponse> listContribuyentesByFechaAdicionDicabiAsync(listContribuyentesByFechaAdicionDicabiRequest request);

        [System.ServiceModel.OperationContract(Action = "", ReplyAction = "*")]
        System.Threading.Tasks.Task<listContribuyentesByFechaModificacionDicabiResponse> listContribuyentesByFechaModificacionDicabiAsync(listContribuyentesByFechaModificacionDicabiRequest request);

        [System.ServiceModel.OperationContract(Action = "", ReplyAction = "*")]
        System.Threading.Tasks.Task<listContribuyentesByNitDicabiResponse> listContribuyentesByNitDicabiAsync(listContribuyentesByNitDicabiRequest request);
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyenteByNombreDicabi", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyenteByNombreDicabiRequest
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string pUsuario;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 1)]
        public string pClave;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 2)]
        public string pPrimerApellido;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 3)]
        public string pSegundoApellido;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 4)]
        public string pApellidoCasada;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 5)]
        public string pPrimerNombre;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 6)]
        public string pSegundoNombre;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 7)]
        public string pFechaNacimiento;

        public listContribuyenteByNombreDicabiRequest()
        {
        }

        public listContribuyenteByNombreDicabiRequest(string pUsuario, string pClave, string pPrimerApellido, string pSegundoApellido, string pApellidoCasada, string pPrimerNombre, string pSegundoNombre, string pFechaNacimiento)
        {
            this.pUsuario = pUsuario;
            this.pClave = pClave;
            this.pPrimerApellido = pPrimerApellido;
            this.pSegundoApellido = pSegundoApellido;
            this.pApellidoCasada = pApellidoCasada;
            this.pPrimerNombre = pPrimerNombre;
            this.pSegundoNombre = pSegundoNombre;
            this.pFechaNacimiento = pFechaNacimiento;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyenteByNombreDicabiResponse", WrapperNamespace = "http://www.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyenteByNombreDicabiResponse
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string listContribuyenteByNombreDicabiReturn;

        public listContribuyenteByNombreDicabiResponse()
        {
        }

        public listContribuyenteByNombreDicabiResponse(string listContribuyenteByNombreDicabiReturn)
        {
            this.listContribuyenteByNombreDicabiReturn = listContribuyenteByNombreDicabiReturn;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyentesByFechaAdicionDicabi", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyentesByFechaAdicionDicabiRequest
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string pUsuario;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 1)]
        public string pClave;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 2)]
        public string pFecha;

        public listContribuyentesByFechaAdicionDicabiRequest()
        {
        }

        public listContribuyentesByFechaAdicionDicabiRequest(string pUsuario, string pClave, string pFecha)
        {
            this.pUsuario = pUsuario;
            this.pClave = pClave;
            this.pFecha = pFecha;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyentesByFechaAdicionDicabiResponse", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyentesByFechaAdicionDicabiResponse
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string listContribuyentesByFechaAdicionDicabiReturn;

        public listContribuyentesByFechaAdicionDicabiResponse()
        {
        }

        public listContribuyentesByFechaAdicionDicabiResponse(string listContribuyentesByFechaAdicionDicabiReturn)
        {
            this.listContribuyentesByFechaAdicionDicabiReturn = listContribuyentesByFechaAdicionDicabiReturn;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyentesByFechaModificacionDicabi", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyentesByFechaModificacionDicabiRequest
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string pUsuario;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 1)]
        public string pClave;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 2)]
        public string pFecha;

        public listContribuyentesByFechaModificacionDicabiRequest()
        {
        }

        public listContribuyentesByFechaModificacionDicabiRequest(string pUsuario, string pClave, string pFecha)
        {
            this.pUsuario = pUsuario;
            this.pClave = pClave;
            this.pFecha = pFecha;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyentesByFechaModificacionDicabiResponse", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyentesByFechaModificacionDicabiResponse
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string listContribuyentesByFechaModificacionDicabiReturn;

        public listContribuyentesByFechaModificacionDicabiResponse()
        {
        }

        public listContribuyentesByFechaModificacionDicabiResponse(string listContribuyentesByFechaModificacionDicabiReturn)
        {
            this.listContribuyentesByFechaModificacionDicabiReturn = listContribuyentesByFechaModificacionDicabiReturn;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyentesByNitDicabi", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyentesByNitDicabiRequest
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string pUsuario;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 1)]
        public string pClave;

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 2)]
        public string Pnit;

        public listContribuyentesByNitDicabiRequest()
        {
        }

        public listContribuyentesByNitDicabiRequest(string pUsuario, string pClave, string Pnit)
        {
            this.pUsuario = pUsuario;
            this.pClave = pClave;
            this.Pnit = Pnit;
        }
    }

    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContract(WrapperName = "listContribuyentesByNitDicabiResponse", WrapperNamespace = "http://ws.contribuyente.rtu.sat.gob.gt", IsWrapped = true)]
    public partial class listContribuyentesByNitDicabiResponse
    {

        [System.ServiceModel.MessageBodyMember(Namespace = "", Order = 0)]
        public string listContribuyentesByNitDicabiReturn;

        public listContribuyentesByNitDicabiResponse()
        {
        }

        public listContribuyentesByNitDicabiResponse(string listContribuyentesByNitDicabiReturn)
        {
            this.listContribuyentesByNitDicabiReturn = listContribuyentesByNitDicabiReturn;
        }
    }

    public interface ContribuyenteWsEpChannel : IContribuyenteWsEp, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThrough()]
    public partial class ContribuyenteWsEpClient : System.ServiceModel.ClientBase<IContribuyenteWsEp>, IContribuyenteWsEp
    {

        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public ContribuyenteWsEpClient() :
                base(GetDefaultBinding(), GetDefaultEndpointAddress())
        {
            Endpoint.Name = EndpointConfiguration.ConsultasPublicasPort.ToString();
            ConfigureEndpoint(Endpoint, ClientCredentials);
        }

        public ContribuyenteWsEpClient(EndpointConfiguration endpointConfiguration) :
                base(GetBindingForEndpoint(endpointConfiguration), GetEndpointAddress(endpointConfiguration))
        {
            Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(Endpoint, ClientCredentials);
        }

        public ContribuyenteWsEpClient(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(Endpoint, ClientCredentials);
        }

        public ContribuyenteWsEpClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(Endpoint, ClientCredentials);
        }

        public ContribuyenteWsEpClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<listContribuyenteByNombreDicabiResponse> IContribuyenteWsEp.listContribuyenteByNombreDicabiAsync(listContribuyenteByNombreDicabiRequest request)
        {
            return Channel.listContribuyenteByNombreDicabiAsync(request);
        }

        public System.Threading.Tasks.Task<listContribuyenteByNombreDicabiResponse> listContribuyenteByNombreDicabiAsync(string pUsuario, string pClave, string pPrimerApellido, string pSegundoApellido, string pApellidoCasada, string pPrimerNombre, string pSegundoNombre, string pFechaNacimiento)
        {
            listContribuyenteByNombreDicabiRequest inValue = new listContribuyenteByNombreDicabiRequest();
            inValue.pUsuario = pUsuario;
            inValue.pClave = pClave;
            inValue.pPrimerApellido = pPrimerApellido;
            inValue.pSegundoApellido = pSegundoApellido;
            inValue.pApellidoCasada = pApellidoCasada;
            inValue.pPrimerNombre = pPrimerNombre;
            inValue.pSegundoNombre = pSegundoNombre;
            inValue.pFechaNacimiento = pFechaNacimiento;
            return ((IContribuyenteWsEp)this).listContribuyenteByNombreDicabiAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<listContribuyentesByFechaAdicionDicabiResponse> IContribuyenteWsEp.listContribuyentesByFechaAdicionDicabiAsync(listContribuyentesByFechaAdicionDicabiRequest request)
        {
            return Channel.listContribuyentesByFechaAdicionDicabiAsync(request);
        }

        public System.Threading.Tasks.Task<listContribuyentesByFechaAdicionDicabiResponse> listContribuyentesByFechaAdicionDicabiAsync(string pUsuario, string pClave, string pFecha)
        {
            listContribuyentesByFechaAdicionDicabiRequest inValue = new listContribuyentesByFechaAdicionDicabiRequest();
            inValue.pUsuario = pUsuario;
            inValue.pClave = pClave;
            inValue.pFecha = pFecha;
            return ((IContribuyenteWsEp)this).listContribuyentesByFechaAdicionDicabiAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<listContribuyentesByFechaModificacionDicabiResponse> IContribuyenteWsEp.listContribuyentesByFechaModificacionDicabiAsync(listContribuyentesByFechaModificacionDicabiRequest request)
        {
            return Channel.listContribuyentesByFechaModificacionDicabiAsync(request);
        }

        public System.Threading.Tasks.Task<listContribuyentesByFechaModificacionDicabiResponse> listContribuyentesByFechaModificacionDicabiAsync(string pUsuario, string pClave, string pFecha)
        {
            listContribuyentesByFechaModificacionDicabiRequest inValue = new listContribuyentesByFechaModificacionDicabiRequest();
            inValue.pUsuario = pUsuario;
            inValue.pClave = pClave;
            inValue.pFecha = pFecha;
            return ((IContribuyenteWsEp)this).listContribuyentesByFechaModificacionDicabiAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<listContribuyentesByNitDicabiResponse> IContribuyenteWsEp.listContribuyentesByNitDicabiAsync(listContribuyentesByNitDicabiRequest request)
        {
            return Channel.listContribuyentesByNitDicabiAsync(request);
        }

        public System.Threading.Tasks.Task<listContribuyentesByNitDicabiResponse> listContribuyentesByNitDicabiAsync(string pUsuario, string pClave, string Pnit)
        {
            listContribuyentesByNitDicabiRequest inValue = new listContribuyentesByNitDicabiRequest();
            inValue.pUsuario = pUsuario;
            inValue.pClave = pClave;
            inValue.Pnit = Pnit;
            return ((IContribuyenteWsEp)this).listContribuyentesByNitDicabiAsync(inValue);
        }

        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)this).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)this).EndOpen));
        }

        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)this).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)this).EndClose));
        }

        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if (endpointConfiguration == EndpointConfiguration.ConsultasPublicasPort)
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if (endpointConfiguration == EndpointConfiguration.ConsultasPublicasPort)
            {
                return new System.ServiceModel.EndpointAddress("https://farm2.sat.gob.gt/DICABIWS/ConsultaDicabi");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return GetBindingForEndpoint(EndpointConfiguration.ConsultasPublicasPort);
        }

        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return GetEndpointAddress(EndpointConfiguration.ConsultasPublicasPort);
        }

        public enum EndpointConfiguration
        {

            ConsultasPublicasPort,
        }

        public class CONTRIBUYENTES
        {
            public CONT CONT { get; set; }
        }

        public class CONT
        {
            public string NIT { get; set; }
            public string NOM { get; set; }
            public string CA { get; set; }
            public string NC { get; set; }
            public string LE { get; set; }
            public string APA { get; set; }
            public string ZON { get; set; }
            public string COL { get; set; }
            public int CD { get; set; }
            public int CM { get; set; }
            public string TEL { get; set; }
            public float COD_AC { get; set; }
            public string EMA { get; set; }
            public string SEXO { get; set; }
            public string FN { get; set; }
            public int CODNAC { get; set; }
            public string CED_REG { get; set; }
            public string CED_NUM { get; set; }
            public string CUI { get; set; }
            public string PAS { get; set; }
            public string NUM_ESC { get; set; }
            public string FESC { get; set; }
            public string FINS { get; set; }
            public string FINS_DEF { get; set; }
            public string FAX { get; set; }
            public string COD_POS { get; set; }
            public int TC { get; set; }
            public string NIT_REP { get; set; }
            public string NOM_REP { get; set; }
        }
    }
}
