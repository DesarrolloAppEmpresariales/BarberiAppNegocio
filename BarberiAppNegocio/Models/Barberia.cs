namespace BarberiAppNegocio.Models
{
    public class Barberia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int StockProducto { get; set; }
        public int CitaId { get; set; }
        public int ServicioId { get; set; }
        public int ProductoId { get; set; }
        public int AdministradorNegocioId { get; set; }
        public int EmpleadoBarberiaId { get; set; }
    }
}
