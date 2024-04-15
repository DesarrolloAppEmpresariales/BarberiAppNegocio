using BarberiAppNegocio.Models;

namespace BarberiAppNegocio.Interface
{
    public interface IServicio
    {
        public List<Servicio> ObtenerListaServicios();
        public Servicio ObtenerServicioPorId(int id);
        public void CrearServicio(Servicio Servicio);
        public void ActualizarServicio(Servicio Servicio);
        public Servicio EliminarServicio(int id);
        public bool ValidarServicio(int id);
    }
}
