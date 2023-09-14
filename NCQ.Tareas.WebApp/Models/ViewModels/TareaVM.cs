using System.ComponentModel.DataAnnotations;

namespace NCQ.Tareas.WebApp.Models.ViewModels
{
    public class TareaVM
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Colaborador")]
        public int ColaboradorId { get; set; }
        public byte Estado { get; set; }
        public byte Prioridad { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Finalización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        public DateTime FechaFin { get; set; }
        public string Notas { get; set; }

        [Display(Name = "Colaborador")]
        public string ColaboradorNombre { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDesc { get; set; }

        [Display(Name = "Prioridad")]
        public string PrioridadDesc { get; set; }
    }
}
