using Core.Constantes;
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

        public MeetRepositorio(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<ResultadoHttpModelo> ObtenerTodasAsync()
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = @"
                    SELECT 
                        m.ID_MEET IdMeet,
                        m.TITULO Titulo,
                        m.DESCRIPCION Descripcion,
                        m.FECHA_INICIO FechaInicio,
                        m.HORA_INICIO HoraInicio,
                        m.FECHA_FIN FechaFin,
                        m.HORA_FIN HoraFin,
                        m.TOKEN_QR TokenQr,
                        m.ID_SALA IdSala,
                        m.ID_PRIORIDAD IdPrioridad,
                        m.ID_ESTADO IdEstado,
                        m.ID_TIPO_MEET IdTipoMeet,
                        s.NOMBRE_SALA NombreSala,
                        p.NOMBRE_PRIORIDAD NombrePrioridad,
                        e.NOMBRE NombreEstado,
                        t.NOMBRE NombreTipoMeet
                    FROM MM_MEET m
                    LEFT JOIN MM_SALA s ON m.ID_SALA = s.ID_SALA
                    LEFT JOIN MM_PRIORIDAD p ON m.ID_PRIORIDAD = p.ID_PRIORIDAD
                    LEFT JOIN MM_ESTADO_FORMULARIO e ON m.ID_ESTADO = e.ID_ESTADO
                    LEFT JOIN MM_TIPO_MEET t ON m.ID_TIPO_MEET = t.ID_TIPO_MEET
                    ORDER BY m.FECHA_INICIO DESC";
                
                var meets = await connection.QueryAsync<MeetModelo>(sql);
                connection.Close();

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reuniones obtenidas exitosamente",
                    Titulo = "Consulta de Reuniones",
                    Resultado = meets
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener reuniones: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
            }
        }

        public async Task<ResultadoHttpModelo> ObtenerPorIdAsync(int idMeet)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = @"
                    SELECT 
                        m.ID_MEET IdMeet,
                        m.TITULO Titulo,
                        m.DESCRIPCION Descripcion,
                        m.FECHA_INICIO FechaInicio,
                        m.HORA_INICIO HoraInicio,
                        m.FECHA_FIN FechaFin,
                        m.HORA_FIN HoraFin,
                        m.TOKEN_QR TokenQr,
                        m.ID_SALA IdSala,
                        m.ID_PRIORIDAD IdPrioridad,
                        m.ID_ESTADO IdEstado,
                        m.ID_TIPO_MEET IdTipoMeet,
                        s.NOMBRE_SALA NombreSala,
                        p.NOMBRE_PRIORIDAD NombrePrioridad,
                        e.NOMBRE NombreEstado,
                        t.NOMBRE NombreTipoMeet
                    FROM MM_MEET m
                    LEFT JOIN MM_SALA s ON m.ID_SALA = s.ID_SALA
                    LEFT JOIN MM_PRIORIDAD p ON m.ID_PRIORIDAD = p.ID_PRIORIDAD
                    LEFT JOIN MM_ESTADO_FORMULARIO e ON m.ID_ESTADO = e.ID_ESTADO
                    LEFT JOIN MM_TIPO_MEET t ON m.ID_TIPO_MEET = t.ID_TIPO_MEET
                    WHERE m.ID_MEET = :IdMeet";
                
                var meet = await connection.QueryFirstOrDefaultAsync<MeetModelo>(sql, new { IdMeet = idMeet });
                connection.Close();

                if (meet == null)
                {
                    return new ResultadoHttpModelo(EstadoSolicitudHttp.warning)
                    {
                        Mensaje = "No se encontró la reunión especificada",
                        Titulo = "Reunión No Encontrada"
                    };
                }

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reunión obtenida exitosamente",
                    Titulo = "Consulta de Reunión",
                    Resultado = meet
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener reunión: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
            }
        }

        public async Task<ResultadoHttpModelo> CrearAsync(MeetCrearModelo meet)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                // Generar token QR único
                var tokenQr = GenerarTokenQr();
                
                var sql = @"
                    INSERT INTO MM_MEET (
                        TITULO, DESCRIPCION, FECHA_INICIO, HORA_INICIO, 
                        FECHA_FIN, HORA_FIN, TOKEN_QR, ID_SALA, 
                        ID_PRIORIDAD, ID_ESTADO, ID_TIPO_MEET
                    ) VALUES (
                        :Titulo, :Descripcion, :FechaInicio, :HoraInicio,
                        :FechaFin, :HoraFin, :TokenQr, :IdSala,
                        :IdPrioridad, :IdEstado, :IdTipoMeet
                    ) RETURNING ID_MEET INTO :IdMeet";
                
                var parameters = new
                {
                    meet.Titulo,
                    meet.Descripcion,
                    meet.FechaInicio,
                    meet.HoraInicio,
                    meet.FechaFin,
                    meet.HoraFin,
                    TokenQr = tokenQr,
                    meet.IdSala,
                    meet.IdPrioridad,
                    meet.IdEstado,
                    meet.IdTipoMeet
                };

                var idMeet = await connection.QuerySingleAsync<int>(sql, parameters);
                connection.Close();

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reunión creada exitosamente",
                    Titulo = "Creación de Reunión",
                    Resultado = new { IdMeet = idMeet, TokenQr = tokenQr }
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al crear reunión: {ex.Message}",
                    Titulo = "Error en Creación"
                };
            }
        }

        public async Task<ResultadoHttpModelo> ActualizarAsync(MeetActualizarModelo meet)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = @"
                    UPDATE MM_MEET SET 
                        TITULO = :Titulo,
                        DESCRIPCION = :Descripcion,
                        FECHA_INICIO = :FechaInicio,
                        HORA_INICIO = :HoraInicio,
                        FECHA_FIN = :FechaFin,
                        HORA_FIN = :HoraFin,
                        ID_SALA = :IdSala,
                        ID_PRIORIDAD = :IdPrioridad,
                        ID_ESTADO = :IdEstado,
                        ID_TIPO_MEET = :IdTipoMeet
                    WHERE ID_MEET = :IdMeet";
                
                var parameters = new
                {
                    meet.IdMeet,
                    meet.Titulo,
                    meet.Descripcion,
                    meet.FechaInicio,
                    meet.HoraInicio,
                    meet.FechaFin,
                    meet.HoraFin,
                    meet.IdSala,
                    meet.IdPrioridad,
                    meet.IdEstado,
                    meet.IdTipoMeet
                };

                var rowsAffected = await connection.ExecuteAsync(sql, parameters);
                connection.Close();

                if (rowsAffected == 0)
                {
                    return new ResultadoHttpModelo(EstadoSolicitudHttp.warning)
                    {
                        Mensaje = "No se encontró la reunión para actualizar",
                        Titulo = "Reunión No Encontrada"
                    };
                }

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reunión actualizada exitosamente",
                    Titulo = "Actualización de Reunión"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al actualizar reunión: {ex.Message}",
                    Titulo = "Error en Actualización"
                };
            }
        }

        public async Task<ResultadoHttpModelo> EliminarAsync(int idMeet)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = "DELETE FROM MM_MEET WHERE ID_MEET = :IdMeet";
                
                var rowsAffected = await connection.ExecuteAsync(sql, new { IdMeet = idMeet });
                connection.Close();

                if (rowsAffected == 0)
                {
                    return new ResultadoHttpModelo(EstadoSolicitudHttp.warning)
                    {
                        Mensaje = "No se encontró la reunión para eliminar",
                        Titulo = "Reunión No Encontrada"
                    };
                }

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reunión eliminada exitosamente",
                    Titulo = "Eliminación de Reunión"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al eliminar reunión: {ex.Message}",
                    Titulo = "Error en Eliminación"
                };
            }
        }

        public async Task<ResultadoHttpModelo> ObtenerPorSalaAsync(int idSala)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = @"
                    SELECT 
                        m.ID_MEET IdMeet,
                        m.TITULO Titulo,
                        m.DESCRIPCION Descripcion,
                        m.FECHA_INICIO FechaInicio,
                        m.HORA_INICIO HoraInicio,
                        m.FECHA_FIN FechaFin,
                        m.HORA_FIN HoraFin,
                        m.TOKEN_QR TokenQr,
                        m.ID_SALA IdSala,
                        m.ID_PRIORIDAD IdPrioridad,
                        m.ID_ESTADO IdEstado,
                        m.ID_TIPO_MEET IdTipoMeet,
                        s.NOMBRE_SALA NombreSala,
                        p.NOMBRE_PRIORIDAD NombrePrioridad,
                        e.NOMBRE NombreEstado,
                        t.NOMBRE NombreTipoMeet
                    FROM MM_MEET m
                    LEFT JOIN MM_SALA s ON m.ID_SALA = s.ID_SALA
                    LEFT JOIN MM_PRIORIDAD p ON m.ID_PRIORIDAD = p.ID_PRIORIDAD
                    LEFT JOIN MM_ESTADO_FORMULARIO e ON m.ID_ESTADO = e.ID_ESTADO
                    LEFT JOIN MM_TIPO_MEET t ON m.ID_TIPO_MEET = t.ID_TIPO_MEET
                    WHERE m.ID_SALA = :IdSala
                    ORDER BY m.FECHA_INICIO DESC";
                
                var meets = await connection.QueryAsync<MeetModelo>(sql, new { IdSala = idSala });
                connection.Close();

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reuniones por sala obtenidas exitosamente",
                    Titulo = "Consulta de Reuniones por Sala",
                    Resultado = meets
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener reuniones por sala: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
            }
        }

        public async Task<ResultadoHttpModelo> ObtenerPorFechaAsync(DateTime fecha)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = @"
                    SELECT 
                        m.ID_MEET IdMeet,
                        m.TITULO Titulo,
                        m.DESCRIPCION Descripcion,
                        m.FECHA_INICIO FechaInicio,
                        m.HORA_INICIO HoraInicio,
                        m.FECHA_FIN FechaFin,
                        m.HORA_FIN HoraFin,
                        m.TOKEN_QR TokenQr,
                        m.ID_SALA IdSala,
                        m.ID_PRIORIDAD IdPrioridad,
                        m.ID_ESTADO IdEstado,
                        m.ID_TIPO_MEET IdTipoMeet,
                        s.NOMBRE_SALA NombreSala,
                        p.NOMBRE_PRIORIDAD NombrePrioridad,
                        e.NOMBRE NombreEstado,
                        t.NOMBRE NombreTipoMeet
                    FROM MM_MEET m
                    LEFT JOIN MM_SALA s ON m.ID_SALA = s.ID_SALA
                    LEFT JOIN MM_PRIORIDAD p ON m.ID_PRIORIDAD = p.ID_PRIORIDAD
                    LEFT JOIN MM_ESTADO_FORMULARIO e ON m.ID_ESTADO = e.ID_ESTADO
                    LEFT JOIN MM_TIPO_MEET t ON m.ID_TIPO_MEET = t.ID_TIPO_MEET
                    WHERE TRUNC(m.FECHA_INICIO) = :Fecha
                    ORDER BY m.FECHA_INICIO ASC";
                
                var meets = await connection.QueryAsync<MeetModelo>(sql, new { Fecha = fecha.Date });
                connection.Close();

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reuniones por fecha obtenidas exitosamente",
                    Titulo = "Consulta de Reuniones por Fecha",
                    Resultado = meets
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener reuniones por fecha: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
            }
        }

        public async Task<ResultadoHttpModelo> ObtenerPorRangoFechasAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using var connection = await _connectionProvider.OpenAsync();
                
                var sql = @"
                    SELECT 
                        m.ID_MEET IdMeet,
                        m.TITULO Titulo,
                        m.DESCRIPCION Descripcion,
                        m.FECHA_INICIO FechaInicio,
                        m.HORA_INICIO HoraInicio,
                        m.FECHA_FIN FechaFin,
                        m.HORA_FIN HoraFin,
                        m.TOKEN_QR TokenQr,
                        m.ID_SALA IdSala,
                        m.ID_PRIORIDAD IdPrioridad,
                        m.ID_ESTADO IdEstado,
                        m.ID_TIPO_MEET IdTipoMeet,
                        s.NOMBRE_SALA NombreSala,
                        p.NOMBRE_PRIORIDAD NombrePrioridad,
                        e.NOMBRE NombreEstado,
                        t.NOMBRE NombreTipoMeet
                    FROM MM_MEET m
                    LEFT JOIN MM_SALA s ON m.ID_SALA = s.ID_SALA
                    LEFT JOIN MM_PRIORIDAD p ON m.ID_PRIORIDAD = p.ID_PRIORIDAD
                    LEFT JOIN MM_ESTADO_FORMULARIO e ON m.ID_ESTADO = e.ID_ESTADO
                    LEFT JOIN MM_TIPO_MEET t ON m.ID_TIPO_MEET = t.ID_TIPO_MEET
                    WHERE TRUNC(m.FECHA_INICIO) BETWEEN :FechaInicio AND :FechaFin
                    ORDER BY m.FECHA_INICIO ASC";
                
                var meets = await connection.QueryAsync<MeetModelo>(sql, 
                    new { FechaInicio = fechaInicio.Date, FechaFin = fechaFin.Date });
                connection.Close();

                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Reuniones por rango de fechas obtenidas exitosamente",
                    Titulo = "Consulta de Reuniones por Rango",
                    Resultado = meets
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener reuniones por rango de fechas: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
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


    }
}
