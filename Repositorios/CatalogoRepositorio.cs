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

        public async Task<ResultadoHttpModelo> ObtenerSalas()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_SALA Id, 
                    NOMBRE_SALA Nombre, 
                    'Sala ' || NOMBRE_SALA || ' - Nivel ' || NIVEL Descripcion 
                FROM MM_SALA 
                WHERE HABILITADA = '1' 
                ORDER BY NIVEL ASC, NOMBRE_SALA ASC";
            
            var salas = await connection.QueryAsync<CatalogoModelo>(sql);
            connection.Close();

            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Salas obtenidas exitosamente",
                Titulo = "Consulta de Salas",
                Resultado = salas
            };
        }

        public async Task<ResultadoHttpModelo> ObtenerPrioridades()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_PRIORIDAD Id, 
                    NOMBRE_PRIORIDAD Nombre, 
                    NOMBRE_PRIORIDAD Descripcion 
                FROM MM_PRIORIDAD 
                ORDER BY ID_PRIORIDAD ASC";
            
            var prioridades = await connection.QueryAsync<CatalogoModelo>(sql);
            connection.Close();

            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Prioridades obtenidas exitosamente",
                Titulo = "Consulta de Prioridades",
                Resultado = prioridades
            };
        }

        public async Task<ResultadoHttpModelo> ObtenerEstadosFormulario()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_ESTADO Id, 
                    NOMBRE Nombre, 
                    NOMBRE Descripcion 
                FROM MM_ESTADO_FORMULARIO 
                ORDER BY ID_ESTADO ASC";
            
            var estados = await connection.QueryAsync<CatalogoModelo>(sql);
            connection.Close();

            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Estados obtenidos exitosamente",
                Titulo = "Consulta de Estados",
                Resultado = estados
            };
        }

        public async Task<ResultadoHttpModelo> ObtenerTiposMeet()
        {
            using var connection = await _connectionProvider.OpenAsync();
            
            var sql = @"
                SELECT 
                    ID_TIPO_MEET Id, 
                    NOMBRE Nombre, 
                    NOMBRE Descripcion 
                FROM MM_TIPO_MEET 
                ORDER BY ID_TIPO_MEET ASC";
            
            var tiposMeet = await connection.QueryAsync<CatalogoModelo>(sql);
            connection.Close();

            return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
            {
                Mensaje = "Tipos de reunión obtenidos exitosamente",
                Titulo = "Consulta de Tipos de Reunión",
                Resultado = tiposMeet
            };
        }


    }
}
