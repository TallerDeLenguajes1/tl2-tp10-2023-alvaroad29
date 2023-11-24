using tl2_tp10_2023_alvaroad29.Models;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ActualizarUsuarioViewModel
    {
        [Display(Name = "Nombre de Usuario")]
        public string NombreDeUsuario{get;set;}

        [Display(Name = "Contraseña")]
        public string Contrasenia{get;set;}

        [Display(Name = "Rol")]
        public enumRol Rol{get;set;}

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