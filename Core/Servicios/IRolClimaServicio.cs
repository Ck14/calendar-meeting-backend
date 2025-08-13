using Core.Models;

namespace Core.Servicios
{
    public interface IRolClimaServicio
    {

        Task<IEnumerable<RolClimaModelo>> ObtenerRoles();
        Task<IEnumerable<RolClimaModelo>> RolesPorUsuario(string nitUsuario);
        Task<IEnumerable<RolClimaModelo>> RolesSinAsignar(string nitUsuario);



    }
}
