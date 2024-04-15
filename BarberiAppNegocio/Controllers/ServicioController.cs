using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Controllers
{
    [Authorize]
    [Route("api/Servicio")]
    [ApiController]
    public class ServicioController : Controller
    {

        private readonly IServicio _IServicio;
        private readonly ILogger<ServicioController> _logger;

        public ServicioController(IServicio IServicio, ILogger<ServicioController> logger)
        {
            _IServicio = IServicio;
            _logger = logger;
        }

        //Roles (1 'SU') (2 'Admin') (3 'Barbero') (4 'Cliente')    
        // GET: ServicioController
        [HttpGet]
        [Authorize(Roles = "1,2,3")]
        public async Task<ActionResult<IEnumerable<Servicio>>> Get()
        {
            _logger.LogWarning("Se realiza la consulta de Servicios");
            return await Task.FromResult(_IServicio.ObtenerListaServicios());
        }

        // GET: ServicioController/Details/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1,4")]
        public async Task<ActionResult<Servicio>> Get(int id)
        {
            var Servicio = await Task.FromResult(_IServicio.ObtenerServicioPorId(id));
            if (Servicio == null)
            {
                return NotFound();
            }
            return Servicio;
        }

        // POST: ServicioController/Create
        [HttpPost]
        [Authorize(Roles = "1,4")]
        public async Task<ActionResult<Servicio>> Post(Servicio Servicio)
        {
            _IServicio.CrearServicio(Servicio);
            return await Task.FromResult(Servicio);
        }

        // GET: ServicioController/Edit/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Servicio>> Put(int id, Servicio Servicio)
        {
            if (id != Servicio.ServicioID)
            {
                return BadRequest();
            }
            try
            {
                _IServicio.ActualizarServicio(Servicio);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(Servicio);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Servicio>> Delete(int id)
        {
            var Servicio = _IServicio.EliminarServicio(id);
            return await Task.FromResult(Servicio);
        }

        private bool ServicioExists(int id)
        {
            return _IServicio.ValidarServicio(id);
        }
    }
}
