using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListarMisTareasViewModel
    {
        public string NombreUsuarioAsignado { get; set; }
        List<TareaViewModel> tareasVM;
        public List<TareaViewModel> TareasVM { get => tareasVM; set => tareasVM = value; }

        public ListarMisTareasViewModel(List<Tarea> tareas, List<Tablero> tableros, Usuario usuario)
        {
            NombreUsuarioAsignado = usuario.NombreDeUsuario;
            TareasVM = new List<TareaViewModel>();
            foreach (var t in tareas)
            {
                TareaViewModel tareaVM = new TareaViewModel(t);  
                
                Tablero tableroVM =  tableros.FirstOrDefault(t => t.Id == tareaVM.Id_tablero);
                
                tareaVM.NombreTablero = tableroVM.Nombre;
                
                if(tableroVM.IdUsuarioPropietario == usuario.Id)
                {
                    tareaVM.Modificable = true;
                }else
                {
                    tareaVM.Modificable = false;
                }
                TareasVM.Add(tareaVM);
            }
        }
    }
}