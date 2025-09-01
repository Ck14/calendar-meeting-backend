using Core.Models;
using Core.Repositorios;
using Core.Constantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicios
{
    public class CatalogoServicio : ICatalogoServicio
    {
        private readonly ICatalogoRepositorio _catalogoRepositorio;
        public CatalogoServicio(ICatalogoRepositorio catalogoRepositorio)
        {
            _catalogoRepositorio = catalogoRepositorio;            
        }
        public async Task<ResultadoHttpModelo> ObtenerOpcionesMenu()
        {
            return await _catalogoRepositorio.ObtenerOpcionesMenu();
        }

        public async Task<ResultadoHttpModelo> ObtenerRoles()
        {
            return await _catalogoRepositorio.ObtenerRoles();
        }

        public async Task<IEnumerable<SalaModelo>> ObtenerSalas()
        {
            var salas = await _catalogoRepositorio.ObtenerSalas();
            return salas;
            
        }

        public async Task<IEnumerable<PrioridadModelo>> ObtenerPrioridades()
        {
            var prioridades = await _catalogoRepositorio.ObtenerPrioridades();
            return prioridades;
        }

        public async Task<IEnumerable<RangoEdad>> ObtenerRangoEdad()
        {
            var rangos = await _catalogoRepositorio.ObtenerRangoEdad();
            return rangos;
        }

        public async Task<IEnumerable<ComunidadLinguistica>> ObtenerLengua()
        {
            var lenguas = await _catalogoRepositorio.ObtenerLengua();
            return lenguas;
        }

        public async Task<IEnumerable<Pueblo>> ObtenerPueblo()
        {
            var pueblos = await _catalogoRepositorio.ObtenerPueblo();
            return pueblos;
        }

        public async Task<IEnumerable<Discapacidad>> ObtenerDiscapacidad()
        {
            var discapacidades = await _catalogoRepositorio.ObtenerDiscapacidad();
            return discapacidades;
        }

        public async Task<ResultadoHttpModelo> ObtenerEstadosFormulario()
        {
            try
            {
                var estados = await _catalogoRepositorio.ObtenerEstadosFormulario();
                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Estados obtenidos exitosamente",
                    Titulo = "Consulta de Estados",
                    Resultado = estados
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener estados: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
            }
        }

        public async Task<ResultadoHttpModelo> ObtenerTiposMeet()
        {
            try
            {
                var tiposMeet = await _catalogoRepositorio.ObtenerTiposMeet();
                return new ResultadoHttpModelo(EstadoSolicitudHttp.success)
                {
                    Mensaje = "Tipos de reunión obtenidos exitosamente",
                    Titulo = "Consulta de Tipos de Reunión",
                    Resultado = tiposMeet
                };
            }
            catch (Exception ex)
            {
                return new ResultadoHttpModelo(EstadoSolicitudHttp.error)
                {
                    Mensaje = $"Error al obtener tipos de reunión: {ex.Message}",
                    Titulo = "Error en Consulta"
                };
            }
        }

        public async Task<TokenValidationResponse> ValidarTokenAsync(string token)
        {
            var tokenValidationResponse = await _catalogoRepositorio.ValidarTokenAsync(token);
            return tokenValidationResponse;

        }
    }
}
