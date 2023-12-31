using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class Tarea
    {
        // int id;
        // int id_tablero;
        // string nombre;
        // string descripcion;
        // string color;
        // EstadoTarea estado;
        // int? idUsuarioAsignado;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public EstadoTarea Estado { get; set; }
        public int? IdUsuarioAsignado { get; set; }
        public int Id_tablero { get; set; }

        public Tarea(ActualizarTareaViewModel tareaVM)
        {
            Color = tareaVM.Color;
            Descripcion = tareaVM.Descripcion;
            Estado = tareaVM.Estado;
            Nombre = tareaVM.Nombre;
            Id_tablero = tareaVM.Id_tablero;
            IdUsuarioAsignado = tareaVM.IdUsuarioAsignado;
            Id = tareaVM.Id;
        }

        public Tarea(CrearTareaViewModel tareaVM)
        {
            Color = tareaVM.Color;
            Descripcion = tareaVM.Descripcion;
            Estado = tareaVM.Estado;
            Nombre = tareaVM.Nombre;
            Id_tablero = tareaVM.Id_tablero;
            IdUsuarioAsignado = tareaVM.IdUsuarioAsignado;
        }
        public Tarea()
        {
        }
    }

    public enum EstadoTarea
    {
        Ideas, ToDo, Doing, Review, Done
    }
}