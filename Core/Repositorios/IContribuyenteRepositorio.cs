
using Core.Models;

namespace Core.Repositorios
{
    public interface IContribuyenteRepositorio
    {
        List<string> ObtenerRlgPorNit(ReqNit req);        
        ContribuyenteModelo ObtenerPorNit(ReqNit req);
        string ObtenerEscrituracionPorNit(ReqNit req);
    }
}
