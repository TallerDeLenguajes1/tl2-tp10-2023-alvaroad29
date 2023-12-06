using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class Tablero
    {
        int id;
        int idUsuarioPropietario;
        string nombre;
        string descripcion;

        public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public Tablero()
        {
        }

        public Tablero(CrearTableroViewModel t)
        {
            idUsuarioPropietario = t.IdUsuarioPropietario;
            nombre = t.Nombre;
            descripcion = t.Descripcion;
        }

        public Tablero(ActualizarTableroViewModel t)
        {
            idUsuarioPropietario = t.IdUsuarioPropietario;
            nombre = t.Nombre;
            descripcion = t.Descripcion;
            id = t.Id;
        }
    }
}