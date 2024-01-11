using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;
namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class CrearTareaViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 1000 caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public EstadoTarea Estado { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public int Id_tablero { get; set; }
        public string Color { get; set; }

        private List<Usuario> usuarios = new List<Usuario>();
        public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
        public CrearTareaViewModel(List<Usuario> usuarios)
        {
            this.usuarios = usuarios;
        }

        public CrearTareaViewModel(){}
        
    }
}