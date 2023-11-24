using tl2_tp10_2023_alvaroad29.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ActualizarUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreDeUsuario{get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Contraseña")]
        [PasswordPropertyText]
        public string Contrasenia{get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]
        public enumRol Rol{get;set;}

        [Required]
        public int Id{get;set;}
        public ActualizarUsuarioViewModel(Usuario usuario)
        {
            NombreDeUsuario = usuario.NombreDeUsuario;
            Contrasenia = usuario.Contrasenia;
            Rol = usuario.Rol;
            Id = usuario.Id;
        }

        public ActualizarUsuarioViewModel()
        {
        }
    }
}