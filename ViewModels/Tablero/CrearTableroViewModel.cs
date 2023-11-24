using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;


namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class CrearTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de tablero")]
        public string Nombre{ get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Usuario Propietario")]
        public int IdUsuarioPropietario{ get; set; }    

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        public List<UsuarioViewModel> usuarios;
        public CrearTableroViewModel(List<UsuarioViewModel> usuarios)
        {
            //Nombre = tablero.Nombre;
            //IdUsuarioPropietario = usuario.Id;
            //Descripcion = tablero.Descripcion;
            this.usuarios = usuarios;
        }
        public CrearTableroViewModel()
        {
        }
    }
}