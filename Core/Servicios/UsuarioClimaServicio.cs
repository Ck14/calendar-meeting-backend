using Exceptionless;
using Core.Models;
using Core.Repositorios;
using Core.Correo.Servicios;
using System.Collections;
using System.Collections.Generic;

namespace Core.Servicios
{
    public class UsuarioClimaServicio : IUsuarioClimaServicio
    {
        private readonly IUsuarioClimaRepositorio usuarioClimaRepositorio;
        private readonly ICorreoServicio correoServicio;
        public UsuarioClimaServicio(IUsuarioClimaRepositorio usuarioClimaRepositorio, ICorreoServicio correoServicio)
        {
            this.usuarioClimaRepositorio = usuarioClimaRepositorio;
            this.correoServicio = correoServicio;
        }

        public async Task<UsuarioClimaModelo> ObtenerUsuario(string nit)
        {            
            var result = await usuarioClimaRepositorio.ObtenerUsuario(nit);            
            return result;
        }

        public async Task<int> InsertarUsuario(UsuarioClimaModelo usuario)
        {
            var result = await usuarioClimaRepositorio.InsertarUsuario(usuario);
            if (result==1)
            {                
                await correoServicio.CorreoCreacionUsuario(new List<string> { usuario.EmailInstitucional });

            }

            return result;
        }

        public async Task<IEnumerable<UsuarioClimaModelo>> ObtenerUsuarios()
        {
            var result = await usuarioClimaRepositorio.ObtenerUsuarios();
            return result;
        }

        public async Task<bool> AsignarRol(string nitUsuario, int idRol, string nitRegistro)
        {
            var result = await usuarioClimaRepositorio.AsignarRol(nitUsuario, idRol, nitRegistro);
            return result;
        }

        public async Task<int> DesasignarRol(string nitUsuario, int idRol, string nitRegistro)
        {
            var result = await usuarioClimaRepositorio.DesasignarRol(nitUsuario, idRol, nitRegistro);
            return result;

        }

        public async Task<bool> ActivarDesasctivar(string nitUsuario, bool activo)
        {
            var result = await usuarioClimaRepositorio.ActivarDesasctivar(nitUsuario, activo);
            return result;
        }


        public async Task<bool> ACtualizarCorreoInstitucional(string nitUsuario, string correoInstitucional)
        {
            var result = await usuarioClimaRepositorio.ACtualizarCorreoInstitucional(nitUsuario, correoInstitucional);
            return result;
        }

        public async Task<bool> ACtualizarCorreoPersonal(string nitUsuario, string correoPersonal)
        {
            var result = await usuarioClimaRepositorio.ACtualizarCorreoPersonal(nitUsuario, correoPersonal);
            return result;

        }

    }
}
