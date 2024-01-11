using tl2_tp10_2023_alvaroad29.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ActualizarUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Este campo debe contener solo letras y números.")]
        [Remote(action: "VerifyUserName", controller: "Usuario")]
        [Display(Name = "Nombre de usuario")]
        public string NombreDeUsuario{get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Este campo debe contener solo letras y números.")]
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