using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Controllers

{
    [Authorize]
    [Route("api/barberia")]
    [ApiController]
    public class BarberiaController : Controller
    {
        private readonly IBarberia _IBarberia;
        private ActionResult<Cita> barberia;

        public BarberiaController(IBarberia IBarberia)
        {
            _IBarberia = IBarberia;
        }

        //Roles (1 'SU') (2 'Admin') (3 'Barbero') (4 'Cliente')    
        // GET: CitaController
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Barberia>>> Get()
        {
            return await Task.FromResult(_IBarberia.ObtenerListaBarberias());
        }

        // GET: CitaController/Details/5
        [HttpGet("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Barberia>> Get(int id)
        {
            var barberia = await Task.FromResult(_IBarberia.ObtenerBarberiaPorId(id));
            if (barberia == null)
            {
                return NotFound();
            }
            return barberia;
        }

        // POST: CitaController/Create
        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Barberia>> Post(Barberia barberia)
        {
            _IBarberia.CrearBarberia(barberia);
            return await Task.FromResult(barberia);
        }

        // GET: CitaController/Edit/5
        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Barberia>> Put(int id, Barberia barberia)
        {
            if (id != barberia.BarberiaID)
            {
                return BadRequest();
            }
            try
            {
                _IBarberia.ActualizarBarberia(barberia);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarberiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(barberia);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<Barberia>> Delete(int id)
        {
            var barberia = _IBarberia.EliminarBarberia(id);
            return await Task.FromResult(barberia);
        }

        private bool BarberiaExists(int id)
        {
            return _IBarberia.ValidarBarberia(id);
        }
    }
}
