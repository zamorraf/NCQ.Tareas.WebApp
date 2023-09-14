using System.ComponentModel.DataAnnotations;

namespace NCQ.Tareas.WebApp.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = null;

        [Display(Name = "Colaborador")]
        public int? ColaboradorId { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar un estado")]
        public byte Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una prioridad")]
        public byte Prioridad { get; set; }

        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0: yyyy-MM-dd}")]
        [Required(ErrorMessage = "Debe indicar una fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha Finalización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: yyyy-MM-dd}")]
        [Required(ErrorMessage = "Debe indicar una fecha de finalización")]
        public DateTime FechaFin { get; set; }

        public string? Notas { get; set; } 

        //public virtual Colaborador Colaborador { get; set; }
    }
}
