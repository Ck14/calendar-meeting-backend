using Core.Constantes;
using Core.Correo.Servicios;
using Core.Models;
using Core.Repositorios;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorios
{
    public class MeetRepositorio : IMeetRepositorio
    {
        private readonly IConnectionProvider _connectionProvider;
        private readonly ICorreoServicio _correoServicio;

        public MeetRepositorio(IConnectionProvider connectionProvider, ICorreoServicio correoServicio)
        {
            _connectionProvider = connectionProvider;
            _correoServicio = correoServicio;
        }


        public async Task<IEnumerable<AttendeeModelo>> ObtenerParticipantes()
        {
            using (var connection = await _connectionProvider.OpenAsync())
            {
                try
                {
                    var sql = @"SELECT nombre, correo_electronico correo FROM vw_empleados_clima_laboral";
                    var attendees = await connection.QueryAsync<AttendeeModelo>(sql);
                    return attendees;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<IEnumerable<MeetModelo>> GetSalasOcupadas(ValidarMeetModelo meet)
        {

            using (var connection = await _connectionProvider.OpenAsync())
            {
                try
                {
                    var sql = @"  SELECT m.ID_MEET,
                                    m.TITULO,
                                    m.HORA_INICIO FechaInicio,
                                    m.HORA_FIN FechaFin
                            FROM MM_MEET m
                            WHERE     m.ID_SALA = :IdSala
                                    AND (m.HORA_INICIO < :FechaFin AND m.HORA_FIN > :FechaInicio)
                            ORDER BY m.HORA_INICIO";

                    var parameters = new
                    {
                        meet.IdSala,
                        meet.FechaInicio,
                        meet.FechaFin
                    };

                    var salasOcupadas = await connection.QueryAsync<MeetModelo>(sql, parameters);
                    return salasOcupadas;

                }
                catch (Exception)
                {

                    throw;
                }
            }

        }


        public async Task<int> CrearAsync(MeetCrearModelo meet)
        {
            var tokenQr = GenerarTokenQr();

            var sql = @"INSERT INTO MM_MEET (
                            ID_MEET, TITULO, DESCRIPCION, HORA_INICIO, 
                             HORA_FIN, TOKEN_QR, ID_SALA, 
                            ID_PRIORIDAD, ID_ESTADO, ID_TIPO_MEET, INVITADOS, ORGANIZADORES
                        ) VALUES (
                            :IdMeet, :Titulo, :Descripcion, :FechaInicio, 
                            :FechaFin,  :TokenQr, :IdSala,
                            :IdPrioridad, :IdEstado, :IdTipoMeet, :Invitados, :Organizadores
                        )";

            using (var connection = await _connectionProvider.OpenAsync())
            {
                using (var trx = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlGetNextId = "SELECT SEQ_MM_MEET.NEXTVAL FROM DUAL";
                        var idMeet = await connection.QuerySingleAsync<int>(sqlGetNextId);

                        var invitadosStr = string.Join(",", meet.invitados ?? Array.Empty<string>());
                        var organizadoresStr = string.Join(",", meet.organizadores ?? Array.Empty<string>());

                        var parameters = new
                        {
                            IdMeet = idMeet,
                            meet.Titulo,
                            meet.Descripcion,
                            meet.FechaInicio,
                            meet.FechaFin,
                            TokenQr = tokenQr,
                            meet.IdSala,
                            meet.IdPrioridad,
                            meet.IdEstado,
                            meet.IdTipoMeet,
                            Invitados = invitadosStr.ToUpper(),
                            Organizadores = organizadoresStr.ToUpper()
                        };

                        await connection.ExecuteAsync(sql, parameters);
                        meet.TokenQr  = tokenQr;
                        await _correoServicio.EnviarCorreoInvitacion(meet);
                        trx.Commit();
                        return idMeet;
                    }
                    catch (Exception)
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }




        public async Task<bool> ActualizarAsync(MeetCrearModelo meet)
        {
            

            var sql = @"UPDATE MM_MEET
                        SET TITULO = :titulo,
                            DESCRIPCION = :descripcion,
                            HORA_INICIO = :FechaInicio,
                            HORA_FIN = :FechaFin,                            
                            ID_SALA = :IdSala,
                            ID_PRIORIDAD = :IdPrioridad,
                            ID_ESTADO = :IdEstado,
                            ID_TIPO_MEET = :IdTipoMeet,
                            INVITADOS = :Invitados,
                            ORGANIZADORES = :Organizadores
                        WHERE ID_MEET = :IdMeet";

            using (var connection = await _connectionProvider.OpenAsync())
            {
                using (var trx = connection.BeginTransaction())
                {
                    try
                    {
                        

                        var invitadosStr = string.Join(",", meet.invitados ?? Array.Empty<string>());
                        var organizadoresStr = string.Join(",", meet.organizadores ?? Array.Empty<string>());

                        var parameters = new
                        {
                            meet.IdMeet,
                            meet.Titulo,
                            meet.Descripcion,
                            meet.FechaInicio,
                            meet.FechaFin,                            
                            meet.IdSala,
                            meet.IdPrioridad,
                            meet.IdEstado,
                            meet.IdTipoMeet,
                            Invitados = invitadosStr.ToUpper(),
                            Organizadores = organizadoresStr.ToUpper()
                        };

                        await connection.ExecuteAsync(sql, parameters);                        
                        trx.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }



        public async Task<bool> ActualizarHorariosAsync(MeetCrearModelo meet)
        {


            var sql = @"UPDATE MM_MEET
                        SET HORA_INICIO = :FechaInicio,
                            HORA_FIN = :FechaFin                            
                        WHERE ID_MEET = :IdMeet";

            using (var connection = await _connectionProvider.OpenAsync())
            {
                using (var trx = connection.BeginTransaction())
                {
                    try
                    {
                        var invitadosStr = string.Join(",", meet.invitados ?? Array.Empty<string>());
                        var organizadoresStr = string.Join(",", meet.organizadores ?? Array.Empty<string>());

                        var parameters = new
                        {
                            meet.IdMeet,                            
                            meet.FechaInicio,
                            meet.FechaFin                            
                        };

                        await connection.ExecuteAsync(sql, parameters);
                        trx.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }







        public async Task<IEnumerable<MeetModelo>> ObtenerReunionesPorRango(DateTime startDate, DateTime endDate)
        {
            using (var connection = await _connectionProvider.OpenAsync())
            {
                try
                {
                    // TODO: Agregar aquí la query SQL para obtener reuniones por rango de fechas
                    var sql = @"SELECT m.titulo,
                                       m.descripcion,
                                       m.hora_inicio          fechaInicio,
                                       m.hora_inicio          horaInicio,
                                       m.hora_fin             fechaFin,
                                       m.hora_fin             horaFin,
                                       m.id_sala              idSala,
                                       m.id_prioridad         idPrioridad,
                                       m.id_estado            idEstado,
                                       m.id_tipo_meet         idTipoMeet,
                                       m.invitados,
                                       m.ORGANIZADORES,
                                       m.id_meet              IdMeet,
                                       s.nombre_sala          NombreSala,
                                       p.NOMBRE_PRIORIDAD     NombrePrioridad,
                                       e.NOMBRE               NombreEstado,
                                       tm.NOMBRE              NombreTipoMeet
                                  FROM mm_meet  m
                                       INNER JOIN mm_sala s ON m.ID_SALA = s.ID_SALA
                                       INNER JOIN mm_prioridad p ON m.ID_PRIORIDAD = p.ID_PRIORIDAD
                                       INNER JOIN mm_estado_formulario e ON m.ID_ESTADO = e.ID_ESTADO
                                       INNER JOIN mm_tipo_meet tm ON m.ID_TIPO_MEET = tm.ID_TIPO_MEET
                                  WHERE m.hora_inicio >= :startDate 
                                    AND m.hora_inicio <= :endDate
                                  ORDER BY m.hora_inicio";

                    var parameters = new
                    {
                        startDate,
                        endDate
                    };

                    var reuniones = await connection.QueryAsync<MeetModelo>(sql, parameters);
                    return reuniones;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private string GenerarTokenQr()
        {
            // Generar un token QR único de 10 caracteres
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public async Task<int> RegistrarAsistencia(AsistenciaModelo asistencia)
        {
            var tokenQr = GenerarTokenQr();

            var sql = @"INSERT INTO MM_PARTICIPANTE (ID_PARTICIPANTE,
                                                 DPI,
                                                 NOMBRE_COMPLETO,
                                                 PUESTO,
                                                 INSTITUCION,
                                                 TELEFONO_EXTENSION,
                                                 CORREO,
                                                 SEXO,
                                                 ID_MEET,
                                                 ID_RANGO,
                                                 ID_DISCAPACIDAD,
                                                 ID_PUEBLO,
                                                 ID_COM_LINGUISTICA,
                                                 FECHA_REGISTRO)
                         VALUES ( :idParticipante,
                                 :dpi,
                                 :nombreCompleto,
                                 :puesto,
                                 :institucion,
                                 :telefonoExtension,
                                 :correo,
                                 :sexo,
                                 (SELECT id_meet
                                    FROM mm_meet
                                   WHERE token_qr = :token),
                                 :RangoEdad,
                                 :Discapacidad,
                                 :Pueblo,
                                 :ComunidadLinguistica,
                                  SYSDATE)";

            using (var connection = await _connectionProvider.OpenAsync())
            {
                using (var trx = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlGetNextId = "SELECT SEQ_MM_PARTICIPANTE.NEXTVAL FROM DUAL";
                        var idParticipante = await connection.QuerySingleAsync<int>(sqlGetNextId);

                        

                        var parameters = new
                        {
                            idParticipante,
                            asistencia.Dpi,
                            asistencia.NombreCompleto,
                            asistencia.Puesto,
                            asistencia.Institucion,
                            asistencia.TelefonoExtension,
                            asistencia.Correo,
                            asistencia.Sexo,
                            asistencia.RangoEdad,
                            asistencia.Discapacidad,
                            asistencia.Pueblo,
                            asistencia.ComunidadLinguistica,
                            asistencia.Token


                        };

                        await connection.ExecuteAsync(sql, parameters);
                        trx.Commit();
                        return idParticipante;
                    }
                    catch (Exception)
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }



        public async Task<IEnumerable<AsistenciaModelo>> ObtenerParticipantes(string token)
        {
            using (var connection = await _connectionProvider.OpenAsync())
            {
                try
                {
                    // TODO: Agregar aquí la query SQL para obtener reuniones por rango de fechas
                    var sql = @"SELECT M.DPI,
                                    M.NOMBRE_COMPLETO         nombreCompleto,
                                    M.PUESTO,
                                    M.INSTITUCION,
                                    M.TELEFONO_EXTENSION      telefonoExtension,
                                    M.CORREO,
                                    M.SEXO,
                                    mm.TOKEN_QR               token,
                                    M.ID_RANGO                rangoEdad,
                                    rangoEdad.DESCRIPCION     rangoEdadTexto,
                                    M.ID_DISCAPACIDAD         discapacidad,
                                    discap.NOMBRE             discapacidadTexto,
                                    M.ID_PUEBLO               pueblo,
                                    pueblo.NOMBRE             puebloTexto,
                                    M.ID_COM_LINGUISTICA      comunidadLinguistica,
                                    comunidad.NOMBRE          ComunidadLinguisticaTexto,
                                    M.FECHA_REGISTRO          fechaRegistro             
                                FROM MM_PARTICIPANTE  M
                                    INNER JOIN mm_meet mm ON M.ID_MEET = mm.ID_MEET
                                    INNER JOIN mm_rango_edad rangoEdad ON M.ID_RANGO = rangoEdad.ID_RANGO
                                    INNER JOIN mm_discapacidad discap
                                        ON M.ID_DISCAPACIDAD = discap.ID_DISCAPACIDAD
                                    INNER JOIN mm_pueblo pueblo ON M.ID_PUEBLO = pueblo.ID_PUEBLO
                                    INNER JOIN mm_comunidad_linguistica comunidad
                                        ON M.ID_COM_LINGUISTICA = comunidad.ID_COM_LINGUISTICA
                                WHERE mm.TOKEN_QR = :token";

                    

                    var participantes = await connection.QueryAsync<AsistenciaModelo>(sql, new { token });
                    return participantes;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


    }
}
