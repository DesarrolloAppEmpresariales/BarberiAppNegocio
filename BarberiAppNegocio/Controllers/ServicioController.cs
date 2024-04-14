using BarberiApp.WebApi.Interface;
using BarberiAppNegocio.Interface;
using BarberiAppNegocio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IServicio _IServicio;

        public ServicioController(IServicio IServicio)
        {
            _IServicio = IServicio;
        }

        //Roles (1 'SU') (2 'Admin') (3 'Barbero') (4 'Cliente')    
        // GET: CitaController
        [HttpGet]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<IEnumerable<Servicio>>> Get()
        {
            return await Task.FromResult(_IServicio.ObtenerListaServicios());
        }

        // GET: CitaController/Details/5
        [HttpGet("{id}")]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<Servicio>> Get(int id)
        {
            var servicio = await Task.FromResult(_IServicio.ObtenerServicioPorId(id));
            if (servicio == null)
            {
                return NotFound();
            }
            return servicio;
        }

        // POST: CitaController/Create
        [HttpPost]
        [Authorize(Roles = "3,4")]
        public async Task<ActionResult<Servicio>> Post(Servicio servicio)
        {
            _IServicio.CrearServicio(servicio);
            return await Task.FromResult(servicio);
        }

        // POST: CitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CitaController/Edit/5
        [HttpPut("{id}")]
        [Authorize(Roles = "3, 4")]
        public async Task<ActionResult<Servicio>> Put(int id, Servicio servicio)
        {
            if (id != servicio.ServicioID)
            {
                return BadRequest();
            }
            try
            {
                _IServicio.ActualizarServicio(servicio);
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
            return await Task.FromResult(servicio);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1, 4")]
        public async Task<ActionResult<Servicio>> Delete(int id)
        {
            var servicio = _IServicio.EliminarServicio(id);
            return await Task.FromResult(servicio);
        }

        private bool ServicioExists(int id)
        {
            return _IServicio.ValidarServicio(id);
        }
    }
}
