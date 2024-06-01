namespace BarberiAppNegocio.Models
{
    public class Servicio
    {
        public int ServicioID { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }
        public string? Tipo { get; set; }
        public string? Precio { get; set; }
    }
}
