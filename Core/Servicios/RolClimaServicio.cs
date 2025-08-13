using Exceptionless;
using Core.Models;
using Core.Repositorios;
using Core.Correo.Servicios;
using System.Collections;
using System.Collections.Generic;

namespace Core.Servicios
{
    public class RolClimaServicio : IRolClimaServicio
    {
        private readonly IRolClimaRepositorio rolRepositorio;
        
        public RolClimaServicio(IRolClimaRepositorio rolRepositorio)
        {
            this.rolRepositorio = rolRepositorio;
            
        }

        public async Task<IEnumerable<RolClimaModelo>> ObtenerRoles()
        {
            var result = await rolRepositorio.ObtenerRoles();
            return result;
        }

        public async Task<IEnumerable<RolClimaModelo>> RolesPorUsuario(string nitUsuario)
        {
            var result = await rolRepositorio.RolesPorUsuario(nitUsuario);
            return result;
        }

        public async Task<IEnumerable<RolClimaModelo>> RolesSinAsignar(string nitUsuario)
        {
            var result = await rolRepositorio.RolesSinAsignar(nitUsuario);
            return result;
        }


        



    }
}
