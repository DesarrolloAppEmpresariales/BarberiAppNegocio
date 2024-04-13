using BarberiApp.WebApi.Models;
using BarberiApp.WebApi.Models;

namespace BarberiApp.WebApi.Interface
{
    public interface ICita
    {
        public List<Cita> ObtenerListaCitas();
        public Cita ObtenerCitaPorId(int id);
        public void CrearCita(Cita cita);
        public void ActualizarCita(Cita cita);
        public Cita EliminarCita(int id);
        public bool ValidarCita(int id);
    }
}
