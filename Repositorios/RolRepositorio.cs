using Core.Models;
using Core.Repositorios;
using Dapper;

namespace Repositorios
{
    public class RolClimaRepositorio : IRolClimaRepositorio
    {
        private readonly IConnectionProvider connectionProvider;

        public RolClimaRepositorio(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }            

       
        


        public async Task<IEnumerable<RolClimaModelo>> ObtenerRoles()
        {
            string ObtenerRoles = @"SELECT id_rol idrol, nombre, descripcion, activo FROM ad_roles where activo = 1";

            using (var connection = await connectionProvider.OpenAsync())
            {
                var roles = await connection.QueryAsync<RolClimaModelo>(ObtenerRoles);
                return roles;
            }
        }


        public async Task<IEnumerable<RolClimaModelo>> RolesPorUsuario(string nitUsuario)
        {
            string ObtenerRoles = @"SELECT ar.id_rol     idrol,
                                            ar.nombre,
                                            ar.descripcion,
                                            ar.activo
                                        FROM ad_roles ar INNER JOIN ad_roles_usuarios aru ON ar.id_rol = aru.id_rol
                                        WHERE aru.nit_usuario = :nitUsuario";

            using (var connection = await connectionProvider.OpenAsync())
            {
                var roles = await connection.QueryAsync<RolClimaModelo>(ObtenerRoles, new { nitUsuario});
                return roles;
            }
        }



        public async Task<IEnumerable<RolClimaModelo>> RolesSinAsignar(string nitUsuario)
        {
            string ObtenerRoles = @"SELECT ar.id_rol     idrol,
                                        ar.nombre,
                                        ar.descripcion,
                                        ar.activo
                                    FROM ad_roles ar
                                    WHERE ar.id_rol NOT IN (SELECT id_rol
                                                            FROM ad_roles_usuarios aru
                                                            WHERE aru.nit_usuario = :nitUsuario)";

            using (var connection = await connectionProvider.OpenAsync())
            {
                var roles = await connection.QueryAsync<RolClimaModelo>(ObtenerRoles, new { nitUsuario });
                return roles;
            }
        }





    }
}
