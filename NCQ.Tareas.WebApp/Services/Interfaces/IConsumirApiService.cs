using NCQ.Tareas.WebApp.Models;
using NCQ.Tareas.WebApp.Models.ViewModels;

namespace NCQ.Tareas.WebApp.Services.Interfaces
{
    public interface IConsumirApiService
    {
        // Colaborador
        Task<List<Colaborador>> ObtenerColaboradores();
        Task<Colaborador> ObtenerColaborador(int id);

        Task<List<Colaborador>> ObtenerColaboradores2();

        Task AgregarColaborador(Colaborador colaborador);
        Task ModificarColaborador(Colaborador colaborador);
        Task EliminarColaborador(int id);


        // Tarea
        Task<List<Tarea>> ObtenerTareas();
        Task<List<TareaVM>> ObtTareas();
        Task<Tarea> ObtenerTarea(int id);
        Task<TareaVM> ObtTarea(int id);

        Task AgregarTarea(Tarea tarea);
        Task ModificarTarea(Tarea tarea);
        Task EliminarTarea(int id);

    }
}
