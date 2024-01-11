using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;


namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class CrearTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 100 caracteres.")]
        [Display(Name = "Nombre de tablero")]
        public string Nombre{ get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 1000 caracteres.")]
        [Display(Name = "Usuario propietario")]
        public int IdUsuarioPropietario{ get; set; }    

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        private List<Usuario> usuarios = new List<Usuario>();
        public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

        public CrearTableroViewModel(List<Usuario> usuarios)
        {
            this.usuarios = usuarios;
        }
        public CrearTableroViewModel()
        {
        }
    }
}