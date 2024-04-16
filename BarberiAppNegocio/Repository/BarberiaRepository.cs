using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Repository
{
    public class BarberiaRepository: IBarberia
    {
        readonly DatabaseContext _dbContext = new();

        public BarberiaRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Barberia> ObtenerListaBarberias()
        {
            try
            {
                return _dbContext.Barberia.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Barberia ObtenerBarberiaPorId(int id)
        {
            try
            {
                Barberia? barberia = _dbContext.Barberia.Find(id);
                if (barberia != null)
                {
                    return barberia;
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

        public void CrearBarberia(Barberia barberia)
        {
            try
            {
                _dbContext.Barberia.Add(barberia);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void ActualizarBarberia(Barberia barberia)
        {
            try
            {
                _dbContext.Entry(barberia).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Barberia EliminarBarberia(int id)
        {
            try
            {
                Barberia? barberia = _dbContext.Barberia.Find(id);

                if (barberia != null)
                {
                    _dbContext.Barberia.Remove(barberia);
                    _dbContext.SaveChanges();
                    return barberia;
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

        public bool ValidarBarberia(int id)
        {
            return _dbContext.Barberia.Any(e => e.BarberiaID == id);
        }
    }
}
