using Core.Models;

namespace Core.Repositorios
{
    public interface IRolClimaRepositorio
    {
        Task<IEnumerable<RolClimaModelo>> ObtenerRoles();
        Task<IEnumerable<RolClimaModelo>> RolesPorUsuario(string nitUsuario);
        Task<IEnumerable<RolClimaModelo>> RolesSinAsignar(string nitUsuario);


    }
}
