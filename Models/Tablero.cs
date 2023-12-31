using tl2_tp10_2023_alvaroad29.ViewModels; // para reconocer el viewModel a castear

namespace tl2_tp10_2023_alvaroad29.Models
{
    public class Tablero
    {
        // int id;
        // int idUsuarioPropietario;
        // string nombre;
        // string descripcion;

        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Tablero() // Poner siempre constructor vacio
        {
        }

        public Tablero(CrearTableroViewModel t)
        {
            IdUsuarioPropietario = t.IdUsuarioPropietario;
            Nombre = t.Nombre;
            Descripcion = t.Descripcion;
        }

        public Tablero(ActualizarTableroViewModel t)
        {
            IdUsuarioPropietario = t.IdUsuarioPropietario;
            Nombre = t.Nombre;
            Descripcion = t.Descripcion;
            Id = t.Id;
        }
    }
}