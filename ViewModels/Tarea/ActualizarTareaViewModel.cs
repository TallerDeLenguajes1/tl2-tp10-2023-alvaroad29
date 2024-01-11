using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ActualizarTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 1000 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public EstadoTarea Estado { get; set; }
        public string Color { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public int Id_tablero { get; set; }
        public int Id{ get; set; }
        // private List<Usuario> usuarios = new List<Usuario>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public ActualizarTareaViewModel(Tarea tarea, List<Usuario> usuarios)
        {
            Usuarios = usuarios;
    
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Estado = tarea.Estado;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            Id_tablero = tarea.Id_tablero;
            Id = tarea.Id;
        }
        public ActualizarTareaViewModel(){}
    }
}