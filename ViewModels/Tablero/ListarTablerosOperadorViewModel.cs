using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    // descartado
    public class ListarTablerosOperadorViewModel
    {
        public List<TableroViewModel> MisTablerosVM{ get; set;}
        public List<TableroViewModel> TablerosTareaVM{ get; set;}
        public List<UsuarioViewModel> usuarios{get; set; }

        public ListarTablerosOperadorViewModel(List<Tablero> misTableros, List<Tablero> tablerosTarea, List<Usuario> usuarios)
        {
            MisTablerosVM = new List<TableroViewModel>();
            foreach (var t in misTableros)
            {
                
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;

                MisTablerosVM.Add(tableroVM);
            }

            TablerosTareaVM = new List<TableroViewModel>();
            foreach (var t in tablerosTarea)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;

                TablerosTareaVM.Add(tableroVM);
            }

        }

        public ListarTablerosOperadorViewModel()
        {
        }
        
    }
}