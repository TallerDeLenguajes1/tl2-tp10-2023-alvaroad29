using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_alvaroad29.Models;
namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class CrearTareaViewModel
    {
        int id_tablero;
        string nombre;
        string descripcion;
        string color;
        EstadoTarea estado;
        int? idUsuarioAsignado;

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Descripcion { get => descripcion; set => descripcion = value; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public string Color { get => color; set => color = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
        public int Id_tablero { get => id_tablero; set => id_tablero = value; }

        public CrearTareaViewModel()
        {

        }
        
    }
}