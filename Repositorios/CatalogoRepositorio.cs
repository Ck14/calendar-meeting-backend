using Core.Constantes;
using Core.Models;
using Core.Models.Seguridad;
using Core.Repositorios;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class CatalogoRepositorio : ICatalogoRepositorio
    {
        private readonly IConnectionProvider _connectionProvider;
        public CatalogoRepositorio(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;            
        }

        public async Task<ResultadoHttpModelo> ObtenerOpcionesMenu()
        {
            using var connection = await _connectionProvider.OpenAsync();
            var catalogo = await connection.QueryAsync<CatalogoOpcionMenuModelo>(sqlOpcionesMenu);
            connection.Close();

            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "La información se ha guardado exitosamente",
                Titulo = "Registro de Roles",
                Resultado = catalogo
            };
        }

        public async Task<ResultadoHttpModelo> ObtenerRoles()
        {
            using var connection = await _connectionProvider.OpenAsync();
            var catalogo = await connection.QueryAsync<CatalogoModelo>(sqlRoles);
            connection.Close();

            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "La información se ha guardado exitosamente",
                Titulo = "Registro de Roles",
                Resultado = catalogo
            };
        }

        private const string sqlOpcionesMenu =
           "SELECT \n" +
           "   ID_OPCION_MENU IdOpcionMenu, \n" +
           "   NOMBRE Nombre, \n" +
           "   DESCRIPCION Descripcion, \n" +
           "   URL Path \n" +
           "FROM AD_OPCIONES_MENU \n" +
           "WHERE ACTIVO = 1 \n" +
           "ORDER BY NOMBRE ASC";

        private const string sqlRoles =
           "SELECT \n" +
           "   ID_ROL Id, \n" +
           "   NOMBRE Nombre, \n" +
           "   DESCRIPCION Descripcion \n" +
           "FROM AD_ROLES \n" +
           "WHERE ACTIVO = 1 \n" +
           "ORDER BY NOMBRE ASC";

        public async Task<IEnumerable<SalaModelo>> ObtenerSalas()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_SALA IdSala, 
                    NOMBRE_SALA NombreSala, 
                    NIVEL                    
                FROM MM_SALA 
                WHERE HABILITADA = '1' 
                ORDER BY NIVEL ASC, NOMBRE_SALA ASC";
            
            var salas = await connection.QueryAsync<SalaModelo>(sql);
            connection.Close();
            
            return salas;
        }

        public async Task<IEnumerable<PrioridadModelo>> ObtenerPrioridades()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_PRIORIDAD IdPrioridad, 
                    NOMBRE_PRIORIDAD NombrePrioridad                    
                FROM MM_PRIORIDAD 
                ORDER BY ID_PRIORIDAD ASC";
            
            var prioridades = await connection.QueryAsync<PrioridadModelo>(sql);
            connection.Close();
            
            return prioridades;
        }

        public async Task<IEnumerable<EstadoFormularioModelo>> ObtenerEstadosFormulario()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_ESTADO IdEstado, 
                    NOMBRE Nombre, 
                    FECHA_CREACION FechaCreacion
                FROM MM_ESTADO_FORMULARIO 
                ORDER BY ID_ESTADO ASC";
            
            var estados = await connection.QueryAsync<EstadoFormularioModelo>(sql);
            connection.Close();
            
            return estados;
        }

        public async Task<IEnumerable<TipoMeetModelo>> ObtenerTiposMeet()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_TIPO_MEET IdTipoMeet, 
                    NOMBRE Nombre, 
                    FECHA_CREACION FechaCreacion
                FROM MM_TIPO_MEET 
                ORDER BY ID_TIPO_MEET ASC";
            
            var tiposMeet = await connection.QueryAsync<TipoMeetModelo>(sql);
            connection.Close();
            
            return tiposMeet;
        }


        public async Task<IEnumerable<RangoEdad>> ObtenerRangoEdad()
        {
            using var connection = await _connectionProvider.OpenAsync();

            var sql = @"SELECT M.ID_RANGO idRango, M.DESCRIPCION
                        FROM CLIMA_LABORAL.MM_RANGO_EDAD M
                        order by id_rango asc";

            var rangos = await connection.QueryAsync<RangoEdad>(sql);
            connection.Close();

            return rangos;
        }


        public async Task<IEnumerable<ComunidadLinguistica>> ObtenerLengua()
        {
            using var connection = await _connectionProvider.OpenAsync();

            var sql = @"SELECT M.ID_COM_LINGUISTICA idLenguaje, M.NOMBRE nombreLenguaje
                        FROM CLIMA_LABORAL.MM_COMUNIDAD_LINGUISTICA M
                        ORDER BY m.nombre ASC";

            var lenguaje = await connection.QueryAsync<ComunidadLinguistica>(sql);
            connection.Close();

            return lenguaje;
        }

        public async Task<IEnumerable<Pueblo>> ObtenerPueblo()
        {
            using var connection = await _connectionProvider.OpenAsync();

            var sql = @"SELECT M.ID_PUEBLO idPueblo, M.NOMBRE nombrePueblo
                        FROM CLIMA_LABORAL.MM_PUEBLO M
                        ORDER BY m.id_pueblo ASC";

            var lenguaje = await connection.QueryAsync<Pueblo>(sql);
            connection.Close();

            return lenguaje;
        }


        public async Task<IEnumerable<Discapacidad>> ObtenerDiscapacidad()
        {
            using var connection = await _connectionProvider.OpenAsync();

            var sql = @"Select M.ID_DISCAPACIDAD idDiscapacidad, M.NOMBRE nombreDiscapacidad
                        From   CLIMA_LABORAL.MM_DISCAPACIDAD M
                        order by id_discapacidad asc";

            var discapacidad = await connection.QueryAsync<Discapacidad>(sql);
            connection.Close();

            return discapacidad;
        }

        //public async Task<TokenValidationResponse> ValidarTokenAsync(string token)
        //{
        //    using var connection = await _connectionProvider.OpenAsync();

        //    var sql = @"SELECT CASE
        //                        WHEN M.ID_MEET IS NOT NULL AND M.HORA_FIN >= SYSDATE THEN 1
        //                        ELSE 0
        //                    END              AS isValid,
        //                    M.ID_MEET        AS idMeet,
        //                    M.TITULO         AS titulo,
        //                    M.HORA_INICIO    AS horaInicio,
        //                    M.HORA_FIN       AS horaFin,
        //                    CASE
        //                        WHEN M.ID_MEET IS NULL THEN 'Token no válido'
        //                        WHEN M.HORA_FIN < SYSDATE THEN 'La reunión ha expirado'
        //                        ELSE 'Token válido'
        //                    END              AS MESSAGE
        //                FROM MM_MEET M
        //                WHERE M.TOKEN_QR = :token";

        //    var parameters = new { token };

        //    var result = await connection.QueryFirstOrDefaultAsync<TokenValidationResponse>(sql, parameters);
        //    connection.Close();

        //    if (result == null)
        //    {
        //        return new TokenValidationResponse
        //        {
        //            IsValid = false,
        //            Message = "Token no encontrado"
        //        };
        //    }

        //    return result;
        //}



        public async Task<TokenValidationResponse> ValidarTokenAsync2(string token)
        {
            using var connection = await _connectionProvider.OpenAsync();

            // Primero validamos el token
            var validationSql = @"SELECT 
                            CASE 
                                WHEN M.ID_MEET IS NOT NULL AND M.HORA_FIN >= SYSDATE THEN 1 
                                ELSE 0 
                            END AS isValid,
                            M.ID_MEET AS IdMeet,
                            CASE 
                                WHEN M.ID_MEET IS NULL THEN 'Token no válido'
                                WHEN M.HORA_FIN < SYSDATE THEN 'La reunión ha expirado'
                                ELSE 'Token válido'
                            END AS Message
                          FROM MM_MEET M
                          WHERE M.TOKEN_QR = :token";

            var validation = await connection.QueryFirstOrDefaultAsync(validationSql, new { token });

            if (validation == null)
            {
                return new TokenValidationResponse
                {
                    IsValid = false,
                    Message = "Token no encontrado"
                };
            }

            var response = new TokenValidationResponse
            {
                IsValid = validation.ISVALID == 1,
                Message = validation.MESSAGE
            };

            // Si es válido, obtenemos los detalles de la reunión
            if (response.IsValid && validation.IdMeet != null)
            {
                var meetingSql = @"SELECT 
                            ID_MEET AS IdMeet,
                            TITULO AS Titulo,                            
                            HORA_INICIO AS HoraInicio,
                            HORA_FIN AS HoraFin
                          FROM MM_MEET
                          WHERE ID_MEET = :idMeet";

                var meeting = await connection.QueryFirstOrDefaultAsync<MeetingInfo>(meetingSql, new { idMeet = validation.IdMeet });
                response.Meeting = meeting;
            }

            connection.Close();
            return response;
        }



        /*
         * Códigos de validación:
         * 0 = Token no válido (no existe)
         * 1 = Válido (dentro del rango de hora)
         * 2 = Muy temprano (antes de HORA_INICIO)
         * 3 = Muy tarde (después de HORA_FIN)  
         */
        public async Task<TokenValidationResponse> ValidarTokenAsync(string token)
        {
            using var connection = await _connectionProvider.OpenAsync();

            var validationSql = @"SELECT 
                            CASE 
                                WHEN M.ID_MEET IS NULL THEN 0
                                WHEN SYSDATE < M.HORA_INICIO THEN 2
                                WHEN SYSDATE > M.HORA_FIN THEN 3
                                ELSE 1
                            END AS validationCode,
                            M.ID_MEET AS IdMeet,
                            M.HORA_INICIO AS HoraInicio,
                            M.HORA_FIN AS HoraFin,
                            CASE 
                                WHEN M.ID_MEET IS NULL THEN 'Token no válido'
                                WHEN SYSDATE < M.HORA_INICIO THEN 'La reunión comenzará el ' || TO_CHAR(M.HORA_INICIO, 'DD/MM/YYYY') || ' a las ' || TO_CHAR(M.HORA_INICIO, 'HH24:MI')
                                WHEN SYSDATE > M.HORA_FIN THEN 'La reunión finalizó el ' || TO_CHAR(M.HORA_FIN, 'DD/MM/YYYY') || ' a las ' || TO_CHAR(M.HORA_FIN, 'HH24:MI')
                                ELSE 'Token válido'
                            END AS Message
                          FROM MM_MEET M
                          WHERE M.TOKEN_QR = :token";

            var validation = await connection.QueryFirstOrDefaultAsync(validationSql, new { token });

            if (validation == null)
            {
                return new TokenValidationResponse
                {
                    IsValid = false,
                    ValidationCode = 0,
                    Message = "Token no encontrado"
                };
            }

            var response = new TokenValidationResponse
            {
                IsValid = validation.VALIDATIONCODE == 1,
                ValidationCode = Convert.ToInt32(validation.VALIDATIONCODE), 
                Message = validation.MESSAGE
            };

            // Solo obtenemos los detalles si es válido
            if (response.IsValid && validation.IDMEET != null)
            {
                var meetingSql = @"SELECT 
                            ID_MEET AS IdMeet,
                            TITULO AS Titulo,                            
                            HORA_INICIO AS HoraInicio,
                            HORA_FIN AS HoraFin
                          FROM MM_MEET
                          WHERE ID_MEET = :idMeet";

                var meeting = await connection.QueryFirstOrDefaultAsync<MeetingInfo>(meetingSql, new { idMeet = validation.IDMEET });
                response.Meeting = meeting;
            }

            connection.Close();
            return response;
        }




    }
}
