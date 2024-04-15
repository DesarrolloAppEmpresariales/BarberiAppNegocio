using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Controllers
{
    [Authorize]
    [Route("api/cita")]
    [ApiController]
    public class CitaController : Controller
    {

        private readonly ICita _ICita;
        private readonly ILogger<CitaController> _logger;

        public CitaController(ICita ICita, ILogger<CitaController> logger)
        {
            _ICita = ICita;
            _logger = logger;
        }

        //Roles (1 'SU') (2 'Admin') (3 'Barbero') (4 'Cliente')    
        // GET: CitaController
        [HttpGet]
        [Authorize(Roles = "1,2,3")]
        public async Task<ActionResult<IEnumerable<Cita>>> Get()
        {
            _logger.LogWarning("Se realiza la consulta de citas");
            return await Task.FromResult(_ICita.ObtenerListaCitas());
        }

        // GET: CitaController/Details/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1,4")]
        public async Task<ActionResult<Cita>> Get(int id)
        {
            var cita = await Task.FromResult(_ICita.ObtenerCitaPorId(id));
            if (cita == null)
            {
                return NotFound();
            }
            return cita;
        }

        // POST: CitaController/Create
        [HttpPost]
        [Authorize(Roles = "1,4")]
        public async Task<ActionResult<Cita>> Post(Cita cita)
        {
            _ICita.CrearCita(cita);
            return await Task.FromResult(cita);
        }

        // GET: CitaController/Edit/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Cita>> Put(int id, Cita cita)
        {
            if (id != cita.CitaID)
            {
                return BadRequest();
            }
            try
            {
                _ICita.ActualizarCita(cita);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(cita);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Cita>> Delete(int id)
        {
            var cita = _ICita.EliminarCita(id);
            return await Task.FromResult(cita);
        }

        private bool CitaExists(int id)
        {
            return _ICita.ValidarCita(id);
        }
    }
}
