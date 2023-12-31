using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListaTablerosViewModel
    {
        public List<TableroViewModel> MisTablerosVM{ get; set;}
        public List<TableroViewModel> TablerosTareasVM{ get; set;}
        public List<TableroViewModel> TodosTablerosVM{ get; set;}
        public List<UsuarioViewModel> usuarios{get; set; }
        public ListaTablerosViewModel(List<Tablero> todosTableros, List<Usuario> usuarios)
        {
            TodosTablerosVM = new List<TableroViewModel>();   
            foreach (var t in todosTableros)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;
                tableroVM.Modificable = true;
                TodosTablerosVM.Add(tableroVM);
            }
            MisTablerosVM = new List<TableroViewModel>();
            TablerosTareasVM = new List<TableroViewModel>();
        }

        public ListaTablerosViewModel(List<Tablero> misTableros, List<Tablero> tablerosTarea, List<Usuario> usuarios)
        {
            TodosTablerosVM = new List<TableroViewModel>();   
            
            MisTablerosVM = new List<TableroViewModel>();
            foreach (var t in misTableros)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;

                tableroVM.Modificable = true;
                MisTablerosVM.Add(tableroVM);
            }

            TablerosTareasVM = new List<TableroViewModel>();
            foreach (var t in tablerosTarea)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;

                tableroVM.Modificable = false;
                TablerosTareasVM.Add(tableroVM);
            }

        }

        public ListaTablerosViewModel(){}
    }
}
