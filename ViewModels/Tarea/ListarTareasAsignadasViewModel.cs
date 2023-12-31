using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListarTareasAsignadasViewModel
    {
        public string NombreTablero { get; set; }
        public string UsuarioPropietario { get; set; }
        public int Id_tablero { get; set; }
        private List<TareaViewModel> tareasVM;
        public List<TareaViewModel> TareasVM { get => tareasVM; set => tareasVM = value; }
        public ListarTareasAsignadasViewModel(List<Tarea> tareas, List<Usuario> usuarios, TableroViewModel tablero, int idUsuario)
        {
            TareasVM = new List<TareaViewModel>();
            this.NombreTablero = tablero.Nombre;
            UsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tablero.IdUsuarioPropietario)?.NombreDeUsuario;
            Id_tablero = tablero.Id;
            foreach (var t in tareas)
            {
                TareaViewModel tareaVM = new TareaViewModel(t);
                if(t.IdUsuarioAsignado == idUsuario) //  || tablero.IdUsuarioPropietario == idUsuario
                {
                    tareaVM.Modificable = true;
                    tareaVM.NombreUsuarioAsignado = usuarios.FirstOrDefault(u => u.Id == tareaVM.IdUsuarioAsignado)?.NombreDeUsuario;
                }else
                {
                    tareaVM.Modificable = false;
                    if (tareaVM.IdUsuarioAsignado == 0)
                    {
                        tareaVM.NombreUsuarioAsignado = "Sin asignar";
                    }else
                    {
                        tareaVM.NombreUsuarioAsignado = usuarios.FirstOrDefault(u => u.Id == tareaVM.IdUsuarioAsignado)?.NombreDeUsuario;
                    }
                }

                TareasVM.Add(tareaVM);
            }
        }

        ListarTareasAsignadasViewModel(){}
    }
}