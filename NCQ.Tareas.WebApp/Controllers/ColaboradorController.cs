using Microsoft.AspNetCore.Mvc;
using NCQ.Tareas.WebApp.Models;
using NCQ.Tareas.WebApp.Services.Interfaces;
using System.Text.Json;

namespace NCQ.Tareas.WebApp.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly IConsumirApiService _consumirApiService;

        public ColaboradorController(IConsumirApiService consumirApiService)
        {
            _consumirApiService = consumirApiService;
        }

        public async Task<IActionResult> Index(string filtroNombre)
        {
            ViewData["filtroNombre"] = filtroNombre;

            var colaborador = await _consumirApiService.ObtenerColaboradores2();

            if (!String.IsNullOrEmpty(filtroNombre))
            {
                colaborador = colaborador.Where(filtro => filtro.NombreCompleto.Contains(filtroNombre)).ToList();
            }

            return View(colaborador);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Colaborador colaborador) 
        {
            if (ModelState.IsValid)
            {
                await _consumirApiService.AgregarColaborador(colaborador);

                return RedirectToAction(nameof(Index));
            }

            return View(colaborador);
        }

        // GET: localhost:puerto/Colaborador/Modificar
        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            if (id == 0)
                return NotFound();

            var colaborador = await _consumirApiService.ObtenerColaborador(id);
            if (colaborador == null)
                return NotFound();

            return View(colaborador);
        }

        // POST: localhost:puerto/Colaborador/Edit/2
        [HttpPost]
        public async Task<IActionResult> Modificar(int id, Colaborador colaborador)
        {
            if (id != colaborador.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Grabar en BD
                await _consumirApiService.ModificarColaborador(colaborador);

                return RedirectToAction(nameof(Index));
            }

            return View(colaborador);
        }

        // GET: localhost:puerto/Colaborador/Delete/
        public async Task<IActionResult> Eliminar(int id)
        {
            if (id == 0)
                return NotFound();

            var colaborador = await  _consumirApiService.ObtenerColaborador(id);
            if (colaborador == null)
                return NotFound();

            return View(colaborador);
        }

        // POST: localhost:puerto/Colaborador/Delete/2
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirm(int id)
        {
                       
            await _consumirApiService.EliminarColaborador(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
