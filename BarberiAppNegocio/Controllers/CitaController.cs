using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            try
            {
                // Obtener el nombre de usuario del contexto de la solicitud HTTP
                var userName = User.Identity.Name;

                // Registro de inicio de la creación de cita con el usuario que realizó la acción
                _logger.LogWarning($"Inicio de creación de cita por el usuario {userName}: {JsonConvert.SerializeObject(cita)}");

                // Creación de la cita
                _ICita.CrearCita(cita);

                // Registro de éxito de la creación de cita con el usuario que realizó la acción
                _logger.LogWarning($"Cita creada exitosamente por el usuario {userName}: {JsonConvert.SerializeObject(cita)}");

                return await Task.FromResult(cita);
            }
            catch (Exception ex)
            {
                var userName = User.Identity.Name;
                // Registro de error en caso de excepción con el usuario que realizó la acción
                _logger.LogError(ex, $"Error al crear cita por el usuario {userName}: {JsonConvert.SerializeObject(cita)}");
                return StatusCode(500, "Error interno del servidor");
            }
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
            try
            {
                // Obtener el nombre de usuario del contexto de la solicitud HTTP
                var userName = User.Identity.Name;

                // Registro de inicio de la eliminación de cita
                _logger.LogWarning($"Inicio de eliminación de cita con ID {id} por el usuario {userName}");

                // Eliminación de la cita
                var cita = _ICita.EliminarCita(id);

                // Registro de éxito de la eliminación de cita
                _logger.LogWarning($"Cita con ID {id} eliminada exitosamente por el usuario {userName}");

                return await Task.FromResult(cita);
            }
            catch (Exception ex)
            {
                // Registro de error en caso de excepción
                _logger.LogError(ex, $"Error al eliminar cita con ID {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        private bool CitaExists(int id)
        {
            return _ICita.ValidarCita(id);
        }
    }
}
