using tl2_tp10_2023_alvaroad29.Models;
namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class TableroViewModel
    {
        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string nombreUsuarioPropietario { get; set; }
        public TableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }

        public TableroViewModel()
        {

        }
    }
}