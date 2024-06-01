namespace BarberiAppNegocio.Models
{
    public class Barberia
    {
        public int BarberiaID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int ServicioBarberiaId { get; set; }
        public int ProductoBarberiaId { get; set; }
        public int UsuarioAdminNegocio { get; set; }
        public int EmpBarberiaId { get; set; }
        public int AdminPlatId { get; set; }
    }
}
