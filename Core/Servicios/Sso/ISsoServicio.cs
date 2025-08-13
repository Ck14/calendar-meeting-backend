
using Core.Models.Sso;
using Core.Repositorios.Sso;

namespace Core.Servicios.Sso
{
    public interface ISsoServicio
    {
        UsuarioSsoModelo Autenticar(SsoAutModelo SSOReq);
        UsuarioSsoModelo ObtenerUsuarioNit(SSOObtenerUsuarioNit SSOReq);
        ResultadoRegistro RegistrarUsuario(SSORegistrar SSOReq);

        bool CambiarAcceso(SSOAcceso SSOReq);

    }
}
