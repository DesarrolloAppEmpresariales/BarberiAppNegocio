using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Repository
{
    public class ServicioRepository: IServicio
    {
        readonly DatabaseContext _dbContext = new();

        public ServicioRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Servicio> ObtenerListaServicios()
        {
            try
            {
                return _dbContext.Servicio.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Servicio ObtenerServicioPorId(int id)
        {
            try
            {
                Servicio? servicio = _dbContext.Servicio.Find(id);
                if (servicio != null)
                {
                    return servicio;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void CrearServicio(Servicio servicio)
        {
            try
            {
                _dbContext.Servicio.Add(servicio);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void ActualizarServicio(Servicio servicio)
        {
            try
            {
                _dbContext.Entry(servicio).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Servicio EliminarServicio(int id)
        {
            try
            {
                Servicio? servicio = _dbContext.Servicio.Find(id);

                if (servicio != null)
                {
                    _dbContext.Servicio.Remove(servicio);
                    _dbContext.SaveChanges();
                    return servicio;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool ValidarServicio(int id)
        {
            return _dbContext.Servicio.Any(e => e.ServicioID == id);
        }

        public List<Servicio> ObtenerServicios()
        {
            throw new NotImplementedException();
        }
    }
}
