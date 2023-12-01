using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListarTareasViewModel
    {
        public string NombreTablero { get; set; }
        public int Id_tablero { get; set; }
        List<TareaViewModel> tareasVM;
        public List<TareaViewModel> TareasVM { get => tareasVM; set => tareasVM = value; }

        public ListarTareasViewModel(List<Tarea> tareas, List<Usuario> usuarios, TableroViewModel tablero)
        {
            TareasVM = new List<TareaViewModel>();
            this.NombreTablero = tablero.Nombre;
            Id_tablero = tablero.Id;
            foreach (var t in tareas)
            {
                TareaViewModel tareaVM = new TareaViewModel(t);
                tareaVM.NombreUsuarioPropietario = "Sin asignar";
                tareaVM.NombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tareaVM.IdUsuarioAsignado)?.NombreDeUsuario;
                TareasVM.Add(tareaVM);
            }
        }
    }
}