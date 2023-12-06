using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id{get;set;} 
        public string NombreDeUsuario{get;set;} 
        public enumRol Rol {get;set;}

        public UsuarioViewModel(Usuario usuario){

            NombreDeUsuario = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
            Id = usuario.Id;
        }
    }
}