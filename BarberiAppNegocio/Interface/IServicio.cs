using BarberiAppNegocio.Models;

namespace BarberiAppNegocio.Interface
{
    public interface IServicio
    {
        public List<Servicio> ObtenerListaServicios();
        public Servicio ObtenerServicioPorId(int id);
        public void CrearServicio(Servicio servicio);
        public void ActualizarServicio(Servicio servicio);
        public Servicio EliminarServicio(int id);
        public bool ValidarServicio(int id);
    }
}
