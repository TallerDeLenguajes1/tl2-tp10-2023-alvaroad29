using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;


namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ActualizarTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de tablero")]
        public string Nombre{ get; set; }
        public int IdUsuarioPropietario{ get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int Id{get;set;}
        public List<UsuarioViewModel> usuarios;
        public ActualizarTableroViewModel(Tablero tablero)
        {
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        }

        public ActualizarTableroViewModel()
        {
        }
    }
}