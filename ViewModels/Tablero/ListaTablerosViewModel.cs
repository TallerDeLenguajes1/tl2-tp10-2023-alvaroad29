using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListaTablerosViewModel
    {
        public List<TableroViewModel> TablerosVM{ get; set;}
        public List<UsuarioViewModel> usuarios{get; set; }
        public ListaTablerosViewModel(List<Tablero> tableros, List<Usuario> usuarios)
        {
            TablerosVM = new List<TableroViewModel>();
            foreach (var t in tableros)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;
                // if (String.IsNullOrEmpty(tableroVM.nombreUsuarioPropietario))
                // {
                //     tableroVM.nombreUsuarioPropietario = "-";
                // }
                tableroVM.Modificable = true;
                TablerosVM.Add(tableroVM);
            }
        }

        public ListaTablerosViewModel(List<Tablero> misTableros, List<Tablero> tablerosTarea, List<Usuario> usuarios)
        {
            TablerosVM = new List<TableroViewModel>();
            foreach (var t in misTableros)
            {
                
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;

                tableroVM.Modificable = true;
                TablerosVM.Add(tableroVM);
            }

            //TablerosTareaVM = new List<TableroViewModel>();
            foreach (var t in tablerosTarea)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.nombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreDeUsuario;


                tableroVM.Modificable = false;
                 TablerosVM.Add(tableroVM);
            }

        }

        public ListaTablerosViewModel(){}
    }
}
