using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListarTareasViewModel
    {
        public string NombreTablero { get; set; }
        public string UsuarioPropietario { get; set; }
        public int Id_tablero { get; set; }
        public List<TareaViewModel> TareasVM { get; set; }
        
        public Dictionary<EstadoTarea, List<TareaViewModel>> TareasPorEstado { get; set; }

        public ListarTareasViewModel(List<Tarea> tareas, List<Usuario> usuarios, TableroViewModel tablero)
        {
            TareasVM = new List<TareaViewModel>();
            NombreTablero = tablero.Nombre;
            Id_tablero = tablero.Id;
            UsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tablero.IdUsuarioPropietario)?.NombreDeUsuario;
            foreach (var t in tareas)
            {
                TareaViewModel tareaVM = new TareaViewModel(t);
                if(tareaVM.IdUsuarioAsignado == null)
                {
                    tareaVM.NombreUsuarioAsignado = "Sin asignar";  
                }else
                {        
                    tareaVM.NombreUsuarioAsignado = usuarios.FirstOrDefault(u => u.Id == tareaVM.IdUsuarioAsignado)?.NombreDeUsuario;
                }
                TareasVM.Add(tareaVM);
            }

            TareasPorEstado = new Dictionary<EstadoTarea, List<TareaViewModel>>();
            foreach (EstadoTarea estado in Enum.GetValues(typeof(EstadoTarea)))
            {
                TareasPorEstado[estado] = TareasVM.Where(t => t.Estado == estado).ToList();
            }
        }

        public ListarTareasViewModel(){}
    }
}