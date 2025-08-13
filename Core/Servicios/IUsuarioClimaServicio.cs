using Core.Models;

namespace Core.Servicios
{
    public interface IUsuarioClimaServicio
    {
        Task<UsuarioClimaModelo> ObtenerUsuario(string nit);
        Task<int> InsertarUsuario(UsuarioClimaModelo usuario);
        Task<IEnumerable<UsuarioClimaModelo>> ObtenerUsuarios();
        Task<bool> AsignarRol(string nitUsuario, int idRol, string nitRegistro);
        Task<int> DesasignarRol(string nitUsuario, int idRol, string nitRegistro);
        Task<bool> ActivarDesasctivar(string nitUsuario, bool activo);
        Task<bool> ACtualizarCorreoInstitucional(string nitUsuario, string correoInstitucional);

        Task<bool> ACtualizarCorreoPersonal(string nitUsuario, string correoPersonal);



    }
}
