using Core.Models;

namespace Core.Servicios
{
    public interface IContribuyenteServicio
    {
        Task<ContribuyenteModelo> ObtenerPorNitAsync(ReqNit req);
    }
}
