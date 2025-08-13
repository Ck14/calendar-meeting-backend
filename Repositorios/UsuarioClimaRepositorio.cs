 using Core.Models;
using Core.Repositorios;
using Dapper;
using Minfin.SSO.Api.Models.Usuario;
using System.ComponentModel;
using System.Data;

namespace Repositorios
{
    public class UsuarioClimaRepositorio : IUsuarioClimaRepositorio
    {
        private readonly IConnectionProvider connectionProvider;

        public UsuarioClimaRepositorio(IConnectionProvider connectionProvider)
        {
            this.connectionProvider = connectionProvider;
        }


        public async Task<UsuarioClimaModelo> ObtenerUsuario(string nit)
        {
            string ObtenerUsuario = @"SELECT nit_usuario nitUsuario,
                                            nombre_completo nombreCompleto,
                                            email_institucional emailInstitucional,
                                            email_personal emailPersonal,
                                            fecha_registro fechaRegistro,
                                            usuario_guardo nitRegistro
                                        FROM ad_usuarios
                                        WHERE nit_usuario = :nit";


            using (var connection = await connectionProvider.OpenAsync())
            {
                var usuarios = await connection.QueryAsync<UsuarioClimaModelo>(ObtenerUsuario, new { nit = nit });
                return usuarios.AsList().FirstOrDefault();
            }

        }


        public async Task<int> InsertarUsuario(UsuarioClimaModelo usuario)
        {
            string sqlInsertarUsuario = @"INSERT INTO AD_USUARIOS (NIT_USUARIO,
                                                            NOMBRE_COMPLETO,
                                                            EMAIL_INSTITUCIONAL,   
                                                            EMAIL_PERSONAL,
                                                            USUARIO_GUARDO,
                                                            ACTIVO)
                                                    VALUES ( :nitUsuario,
                                                            :nombreCompleto,
                                                            :correoInstitucional,                                                            
                                                            :correoPersonal,
                                                            :nitRegistro,
                                                            :activo)";

            using (var connection = await connectionProvider.OpenAsync())
            {
                var response = await connection.ExecuteAsync(sqlInsertarUsuario, new
                {
                    nitUsuario = usuario.NitUsuario,
                    nombreCompleto = usuario.NombreCompleto,
                    correoInstitucional = usuario.EmailInstitucional,
                    correoPersonal = usuario.EmailPersonal,
                    nitRegistro = usuario.NitRegistro,
                    activo = 1

                });
                return response;
            }
        }


        public async Task<IEnumerable<UsuarioClimaModelo>> ObtenerUsuarios()
        {
            string ObtenerUsuario = @"SELECT nit_usuario             nitUsuario,
                                        nombre_completo         nombreCompleto,
                                        email_institucional     emailInstitucional,
                                        email_personal          emailPersonal,
                                        fecha_registro          fechaRegistro,
                                        usuario_guardo          nitRegistro,
                                        activo
                                    FROM ad_usuarios order by nombre_completo asc";


            using (var connection = await connectionProvider.OpenAsync())
            {
                var usuarios = await connection.QueryAsync<UsuarioClimaModelo>(ObtenerUsuario);
                return usuarios;
            }

        }


        public async Task<bool> AsignarRol(string nitUsuario, int idRol, string nitRegistro)
        {

            string sqlInsertarRol = @"INSERT INTO AD_ROLES_USUARIOS ( NIT_USUARIO, ID_ROL, NIT_REGISTRO, FECHA_REGISTRO) 
                                                             VALUES (:nitUsuario, :idRol, :nitRegistro, sysdate )";

            try
            {

                using (var connection = await connectionProvider.OpenAsync())
                {
                    var responseInsert = await connection.ExecuteAsync(sqlInsertarRol, new
                    {
                        nitUsuario,
                        idRol,
                        nitRegistro

                    });
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<int> DesasignarRol(string nitUsuario, int idRol, string nitRegistro)
        {

            //string sqlEliminarRoles = @"DELETE FROM ad_roles_usuarios WHERE nit_usuario = :nitUsuario and id_rol = :idrol";
            //try
            //{
            //    using (var connection = await connectionProvider.OpenAsync())
            //    {
            //        var response = await connection.ExecuteAsync(sqlEliminarRoles, new { nitUsuario, idRol });
            //        return true;
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


            // Nombre del procedimiento almacenado
            string sqlEliminarRol = "delete_roles_item_with_user";

            // Parámetros del procedimiento almacenado
            var parameters = new DynamicParameters();
            parameters.Add("p_id_rol", idRol, DbType.Int32);
            parameters.Add("p_usuario", nitUsuario, DbType.String);
            parameters.Add("p_usuario_registro", nitRegistro, DbType.String);

            using (var connection = await connectionProvider.OpenAsync())
            {
                // Ejecutar el procedimiento almacenado
                var response = await connection.ExecuteAsync(sqlEliminarRol, parameters, commandType: CommandType.StoredProcedure);
                return response;
            }

        }


        public async Task<bool> ActivarDesasctivar(string nitUsuario, bool activo)
        {

            string sqlActualizarActivacion = @"UPDATE AD_USUARIOS
                                            SET ACTIVO = :ACTIVO
                                            WHERE NIT_USUARIO = :nitUsuario";
            try
            {
                using (var connection = await connectionProvider.OpenAsync())
                {
                    var response = await connection.ExecuteAsync(sqlActualizarActivacion, new { nitUsuario, activo = activo?1:0 });
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<bool> ACtualizarCorreoInstitucional(string nitUsuario, string correoInstitucional)
        {

            string sqlActualizarActivacion = @"UPDATE AD_USUARIOS
                                            SET EMAIL_INSTITUCIONAL = :correoInstitucional
                                            WHERE NIT_USUARIO = :nitUsuario";
            try
            {
                using (var connection = await connectionProvider.OpenAsync())
                {
                    var response = await connection.ExecuteAsync(sqlActualizarActivacion, new { nitUsuario, correoInstitucional });
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> ACtualizarCorreoPersonal(string nitUsuario, string correoPersonal)
        {

            string sqlActualizarActivacion = @"UPDATE AD_USUARIOS
                                            SET EMAIL_PERSONAL = :correoPersonal
                                            WHERE NIT_USUARIO = :nitUsuario";
            try
            {
                using (var connection = await connectionProvider.OpenAsync())
                {
                    var response = await connection.ExecuteAsync(sqlActualizarActivacion, new { nitUsuario, correoPersonal });
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }// end class
}// end namespace
