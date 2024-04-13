using BarberiApp.WebApi.Interface;
using BarberiApp.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiApp.WebApi.Repository
{
    public class CitaRepository : ICita
    {
        readonly DatabaseContext _dbContext = new();

        public CitaRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Cita> ObtenerListaCitas()
        {
            try
            {
                return _dbContext.Cita.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Cita ObtenerCitaPorId(int id)
        {
            try
            {
                Cita? cita = _dbContext.Cita.Find(id);
                if (cita != null)
                {
                    return cita;
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

        public void CrearCita(Cita cita)
        {
            try
            {
                _dbContext.Cita.Add(cita);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void ActualizarCita(Cita cita)
        {
            try
            {
                _dbContext.Entry(cita).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Cita EliminarCita(int id)
        {
            try
            {
                Cita? cita = _dbContext.Cita.Find(id);

                if (cita != null)
                {
                    _dbContext.Cita.Remove(cita);
                    _dbContext.SaveChanges();
                    return cita;
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

        public bool ValidarCita(int id)
        {
            return _dbContext.Cita.Any(e => e.CitaID == id);
        }
    }
}