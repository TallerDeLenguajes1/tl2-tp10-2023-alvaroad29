using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_alvaroad29.ViewModels;
namespace tl2_tp10_2023_alvaroad29.Models
{
    public class Usuario
    {
        public int Id{get;set;} 
        public string NombreDeUsuario{get;set;} 
        public string Contrasenia{get;set;}
        public enumRol Rol{get;set;}

        public Usuario(CrearUsuarioViewModel u)
        {          
            NombreDeUsuario = u.NombreDeUsuario;
            Contrasenia = u.Contrasenia;
            Rol = u.Rol;
        }

        public Usuario(ActualizarUsuarioViewModel u)
        {          
            NombreDeUsuario = u.NombreDeUsuario;
            Contrasenia = u.Contrasenia;
            Rol = u.Rol;
            Id = u.Id;
        }

        public Usuario()
        {
        }
    }

    

    public enum enumRol
    {
        admin,operador
    }
}

