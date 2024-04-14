namespace BarberiAppNegocio.Models
{
    public class Servicio
    {
        public int ServicioID { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public string? Estado { get; set; }
        public string? Tipo { get; set; }
        public string? Precio { get; set; }
        public int BarberiaId { get; set; }
    }
}
