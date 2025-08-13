using Exceptionless;
using Core.Models;
using Core.Repositorios;

namespace Core.Servicios
{
    public class ContribuyenteServicio : IContribuyenteServicio
    {
        private readonly IContribuyenteRepositorio repo;
        public ContribuyenteServicio(IContribuyenteRepositorio repo)
        {
            this.repo = repo;
        }

        public async Task<ContribuyenteModelo> ObtenerPorNitAsync(ReqNit req)
        {
            int intentosSat = 1;
            int intentosDicabi = 1;
            ContribuyenteModelo contribuyente = null;
            //ContribuyenteModelo contribuyenteDicabi = null;

            while (intentosSat <= 2 && contribuyente == null)
            {
                try
                {
                    contribuyente = repo.ObtenerPorNit(req);
                    intentosSat++;
                }
                catch (Exception ex)
                {
                    ex.ToExceptionless().Submit();
                    contribuyente = null;
                    intentosSat++;
                }
            }           

            if (contribuyente == null) { contribuyente = new ContribuyenteModelo(); }
            

            var result = new ContribuyenteModelo()
            {
                Nit = contribuyente.Nit,
                Nombre = contribuyente.Nombre,
                ActividadEconomica = contribuyente.ActividadEconomica,
                Cui = contribuyente.Cui != null ? contribuyente.Cui.Replace(" ", "") : contribuyente.Cui,
                DomicilioFiscal = contribuyente.DomicilioFiscal,
                Email = contribuyente.Email,
                Estado = contribuyente.Estado,
                Telefono = contribuyente.Telefono,
                TipoOrganizacion = contribuyente.TipoOrganizacion,
                Procedencia = contribuyente.Procedencia,
                Sexo = contribuyente.Sexo,
                FechaInscripcionRTU = contribuyente.FechaInscripcionRTU,
                EstadoInsolvencia = contribuyente.EstadoInsolvencia,
                Departamento = contribuyente.Departamento,
                Municipio = contribuyente.Municipio
            };
            return result;
        }
    }
}
