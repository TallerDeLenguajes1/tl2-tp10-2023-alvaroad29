using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class Tarea
    {
        int id;
        int id_tablero;
        string nombre;
        string descripcion;
        string color;
        EstadoTarea estado;
        int? idUsuarioAsignado;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Color { get => color; set => color = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
        public int Id_tablero { get => id_tablero; set => id_tablero = value; }

        public Tarea(ActualizarTareaViewModel tareaVM)
        {
            Color = tareaVM.TareaVM.Color;
            Descripcion = tareaVM.TareaVM.Descripcion;
            Estado = tareaVM.TareaVM.Estado;
            Nombre = tareaVM.TareaVM.Nombre;
            Id_tablero = tareaVM.TareaVM.Id_tablero;
            IdUsuarioAsignado = tareaVM.TareaVM.IdUsuarioAsignado;
            Id = tareaVM.TareaVM.Id;
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