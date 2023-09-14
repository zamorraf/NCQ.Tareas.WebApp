using System.ComponentModel.DataAnnotations;

namespace NCQ.Tareas.WebApp.Models
{
    public class Colaborador
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre Colaborador")]
        [Required(ErrorMessage = "El nombre del colaborador es requerido")]
        public string NombreCompleto { get; set; }
    }
}
