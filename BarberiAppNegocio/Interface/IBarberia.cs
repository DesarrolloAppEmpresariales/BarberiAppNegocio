using BarberiAppNegocio.Models;

namespace BarberiAppNegocio.Interface
{
    public interface IBarberia
    {
        List<Barberia> ObtenerListaBarberias();
        Barberia ObtenerBarberiaPorId(int id);
        void CrearBarberia(Barberia barberia);
        void ActualizarBarberia(Barberia barberia);
        Barberia EliminarBarberia(int id);
        bool ValidarBarberia(int id);
    }
}
