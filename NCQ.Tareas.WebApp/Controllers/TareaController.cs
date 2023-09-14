using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NCQ.Tareas.WebApp.Models;
using NCQ.Tareas.WebApp.Services.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;

namespace NCQ.Tareas.WebApp.Controllers
{
    public class TareaController : Controller
    {
        private readonly IConsumirApiService _consumirApiService;

        private IEnumerable<ListaSeleccion> _estados;
        private IEnumerable<ListaSeleccion> _prioridades;

        public TareaController(IConsumirApiService consumirApiService)
        {
            _consumirApiService = consumirApiService;
        }

        public async Task<IActionResult> Index(string filtroColaborador, string filtroEstado ,string filtroPrioridad,
                                                string filtroFechaInicio, string filtroFechaFin)
        {
            ViewData["filtroFechaInicio"] = filtroFechaInicio;
            ViewData["filtroFechaFin"] = filtroFechaFin;

            // Carga Combo de colaboradores
            var colaboradores = await _consumirApiService.ObtenerColaboradores();
            ViewData["ColaboradorId"] = new SelectList(colaboradores, "Id", "NombreCompleto",filtroColaborador);

            CargarList();
            // Carga Lista de estados
            ViewData["Estado"] = new SelectList(_estados, "Id", "Nombre", filtroEstado);

            // Carga lista de Prioridades
            ViewData["Prioridad"] = new SelectList(_prioridades, "Id", "Nombre", filtroPrioridad);


            //var tareas = await _consumirApiService.ObtenerTareas();
            var tareas = await _consumirApiService.ObtTareas();

            // Filtro de colaborador
            if (!String.IsNullOrEmpty(filtroColaborador))
            {
                if (filtroColaborador != "0")
                    tareas = tareas.Where(filtro => filtro.ColaboradorId == Convert.ToInt32(filtroColaborador)).ToList();
            }

            // Filtro de Estado
            if (!String.IsNullOrEmpty(filtroEstado) )
            {
                if (filtroEstado != "0")
                    tareas = tareas.Where(filtro => filtro.Estado == Convert.ToSByte(filtroEstado)).ToList();
            }
            // Filtro de Prioridad
            if (!String.IsNullOrEmpty(filtroPrioridad))
            {
                if (filtroPrioridad != "0")
                    tareas = tareas.Where(filtro => filtro.Prioridad == Convert.ToSByte(filtroPrioridad)).ToList();
            }

            // Filtro de Fecha Inicio
            if (!String.IsNullOrEmpty(filtroFechaInicio))
            {
                tareas = tareas.Where(filtro => filtro.FechaInicio >= Convert.ToDateTime(filtroFechaInicio)).ToList();
            }

            // Filtro de Fecha Fin
            if (!String.IsNullOrEmpty(filtroFechaFin))
            {
                tareas = tareas.Where(filtro => filtro.FechaFin <= Convert.ToDateTime(filtroFechaFin)).ToList();
            }

            return View(tareas);
        }

        public async Task<IActionResult> Crear()
        {
            // Carga Combo de colaboradores
            var colaboradores = await _consumirApiService.ObtenerColaboradores();
            ViewData["ColaboradorId"] = new SelectList(colaboradores, "Id","NombreCompleto",null);

            CargarList();
            // Carga Lista de estados
            ViewData["Estado"] = new SelectList(_estados, "Id", "Nombre",1);

            // Carga lista de Prioridades
            ViewData["Prioridad"] = new SelectList(_prioridades, "Id", "Nombre",3);

            ViewData["Fecha"] = DateTime.Now;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Tarea tarea)
        {
            // Carga Combo de colaboradores
            var colaboradores = await _consumirApiService.ObtenerColaboradores();
            ViewData["ColaboradorId"] = new SelectList(colaboradores, "Id", "NombreCompleto");

            CargarList();
            // Carga Lista de estados
            ViewData["Estado"] = new SelectList(_estados, "Id", "Nombre");
            // Carga lista de Prioridades
            ViewData["Prioridad"] = new SelectList(_prioridades, "Id", "Nombre");

            if (ModelState.IsValid)
            {
                if (tarea.ColaboradorId == 0) tarea.ColaboradorId = null;

                if ( tarea.ColaboradorId == null && (tarea.Estado == 2 || tarea.Estado == 3))
                {
                    ViewBag.Mensaje = "Tarea con estado diferente a 'Pendiente', se requiere el colaborador";
                    ViewBag.ClaseAlert = "alert alert-danger col-sm-6";
                    return View(tarea);
                }

                await _consumirApiService.AgregarTarea(tarea);

                return RedirectToAction(nameof(Index));
            }

            return View(tarea);
        }

        // GET: localhost:puerto/Tarea/Modificar
        [HttpGet]
        public async Task<IActionResult> Modificar(int id)
        {
            if (id == 0)
                return NotFound();

            var tarea = await _consumirApiService.ObtenerTarea(id);
            if (tarea == null)
                return NotFound();

            // Carga Combo de colaboradores
            var colaboradores = await _consumirApiService.ObtenerColaboradores();
            ViewData["ColaboradorId"] = new SelectList(colaboradores, "Id", "NombreCompleto", tarea.ColaboradorId);

            CargarList();
            // Carga Lista de estados
            ViewData["Estado"] = new SelectList(_estados, "Id", "Nombre", tarea.Estado);

            // Carga lista de Prioridades
            ViewData["Prioridad"] = new SelectList(_prioridades, "Id", "Nombre", tarea.Prioridad);

            return View(tarea);
        }

        // POST: localhost:puerto/Tarea/Edit/2
        [HttpPost]
        public async Task<IActionResult> Modificar(int id , Tarea tarea)
        {
            if (id != tarea.Id)
                return NotFound();

            // Carga Combo de colaboradores
            var colaboradores = await _consumirApiService.ObtenerColaboradores();
            ViewData["ColaboradorId"] = new SelectList(colaboradores, "Id", "NombreCompleto", tarea.ColaboradorId);

            CargarList();
            // Carga Lista de estados
            ViewData["Estado"] = new SelectList(_estados, "Id", "Nombre", tarea.Estado);

            // Carga lista de Prioridades
            ViewData["Prioridad"] = new SelectList(_prioridades, "Id", "Nombre", tarea.Prioridad);

            var tarea2 = await _consumirApiService.ObtenerTarea(id);
            //Validar que no se pueda editar una tarea en estado Finalizada
            if (tarea != null && tarea2.Estado == 3) 
            {
                ViewBag.Mensaje = "Tarea con estado 'Finalizada', no se puede modificar";
                ViewBag.ClaseAlert = "alert alert-danger col-sm-6";
                return View(tarea);
            }

            if (tarea.ColaboradorId == 0 && (tarea.Estado == 2 || tarea.Estado == 3))
            {
                ViewBag.Mensaje = "Tarea con estado diferente a 'Pendiente', se requiere el colaborador";
                ViewBag.ClaseAlert = "alert alert-danger col-sm-6";
                return View(tarea);
            }


            if (ModelState.IsValid)
            {
                if (tarea.ColaboradorId == 0) tarea.ColaboradorId = null;
                // Grabar en BD
                await _consumirApiService.ModificarTarea(tarea);

                return RedirectToAction(nameof(Index));
            }

            return View(tarea);
        }


        // GET: localhost:puerto/Tarea/Eliminar/
        public async Task<IActionResult> Eliminar(int id)
        {
            if (id == 0)
                return NotFound();

            var tarea = await _consumirApiService.ObtTarea(id);
            if (tarea == null)
                return NotFound();

            return View(tarea);
                
        }

        // POST: localhost:puerto/Tarea/Eliminar/2
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirm(int id)
        {
            @ViewBag.ClaseAlert = "";
            var tarea = await _consumirApiService.ObtenerTarea(id);
            if (tarea != null && tarea.Estado != 2)
            {
                //CargarList();

                await _consumirApiService.EliminarTarea(id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var tarea2 = await _consumirApiService.ObtTarea(id);
                ViewBag.Mensaje = "Tarea con estado 'En proceso', no se pede eliminar";
                @ViewBag.ClaseAlert = "alert alert-danger col-sm-6";
                return View(tarea2);
            }
            
        }

        public void CargarList() 
        {
            // Carga Lista de estados
            _estados = new[]
            {
                new ListaSeleccion{Id = 1, Nombre = "Pendiente"},
                new ListaSeleccion{Id = 2, Nombre = "En proceso"},
                new ListaSeleccion{Id = 3, Nombre = "Finalizada"}
            };


            // Carga lista de Prioridades
            _prioridades = new[]
            {
                new ListaSeleccion{Id = 1, Nombre = "Alta"},
                new ListaSeleccion{Id = 2, Nombre = "Media"},
                new ListaSeleccion{Id = 3, Nombre = "Baja"}
            };
        }

    }
}
