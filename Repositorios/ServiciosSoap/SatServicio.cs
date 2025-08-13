using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Repositorios.ServiciosSoap
{

    [ServiceContract(Namespace = "http://www.sat.gob.gt")]
    interface IConsultaSatServicio
    {
        [OperationContract(Action = "", ReplyAction = "*")]
        findContribuyenteByNitGCResponse findContribuyenteByNitGC(findContribuyenteByNitGCRequest request);
    }

    [MessageContract(WrapperName = "findContribuyenteByNitGC", WrapperNamespace = "http://ws.guatecompras.rtu.sat.gob.gt", IsWrapped = true)]
    class findContribuyenteByNitGCRequest
    {
        [MessageBodyMember(Namespace = "", Order = 0)]
        public string pUsuario { get; set; }

        [MessageBodyMember(Namespace = "", Order = 1)]
        public string pContrasenia { get; set; }

        [MessageBodyMember(Namespace = "", Order = 2)]
        public string pNit { get; set; }
    }

    [MessageContract(WrapperName = "findContribuyenteByNitGCResponse", WrapperNamespace = "http://ws.guatecompras.rtu.sat.gob.gt", IsWrapped = true)]
    class findContribuyenteByNitGCResponse
    {
        [MessageBodyMember(Namespace = "", Order = 0)]
        public string findContribuyenteByNitGCReturn { get; set; }
    }

    class ConsultaSatSoapClient : ClientBase<IConsultaSatServicio>, IConsultaSatServicio
    {

        public ConsultaSatSoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {

        }

        public findContribuyenteByNitGCResponse findContribuyenteByNitGC(findContribuyenteByNitGCRequest request)
        {
            return Channel.findContribuyenteByNitGC(request);
        }
    }

    public class GC_REQCONTRIBUYENTE
    {
        public DG DG { get; set; }
        public DOM_FIS DOM_FIS { get; set; }
        public INSOLVENCIA INSOLVENCIA { get; set; }

    }


    public class DG
    {
        public string NIT { get; set; }
        public string NOM { get; set; }
        public string CUI { get; set; }
        public string ESTNIT { get; set; }
        public int TO { get; set; }
        public string AECONOMICA { get; set; }
        public string SEXO { get; set; }
        public string FIRTU { get; set; }
    }

    public class DOM_FIS
    {
        public string COA { get; set; }
        public string NC { get; set; }
        public string APA { get; set; }
        public string ZON { get; set; }
        public string TEL { get; set; }
        public string EMA { get; set; }
        public string COL { get; set; }
        public int CD { get; set; }
        public int CM { get; set; }
    }

    public class INSOLVENCIA
    {
        public string TIIN { get; set; }
        public string EIIN { get; set; }
        public string MINS { get; set; }
    }

    public class CONTRIBUYENTES
    {
        public string CODIGO_ERROR { get; set; }
    }

}
