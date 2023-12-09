using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class TareaViewModel
    {
        int id;
        int id_tablero;
        string nombre;
        string descripcion;
        string color;
        EstadoTarea estado;
        int? idUsuarioAsignado;
        string nombreUsuarioAsignado;
        string nombreTablero;
        bool modificable;

        public int Id { get => id; set => id = value; }

        // [Required(ErrorMessage = "Este campo es requerido.")]
        // [Display(Name = "Nombre ")]
        public string Nombre { get => nombre; set => nombre = value; }

        public string Descripcion { get => descripcion; set => descripcion = value; }

        public string Color { get => color; set => color = value; }

        // [Required(ErrorMessage = "Este campo es requerido.")]
        // [Display(Name = "Estado")]
        
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
        public int Id_tablero { get => id_tablero; set => id_tablero = value; }
        public string NombreUsuarioAsignado { get => nombreUsuarioAsignado; set => nombreUsuarioAsignado = value; }
        public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
        public bool Modificable { get => modificable; set => modificable = value; }

        public TareaViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }

        public TareaViewModel()
        {
        }
    }
}