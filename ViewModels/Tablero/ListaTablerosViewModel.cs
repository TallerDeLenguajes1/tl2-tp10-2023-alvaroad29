using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListaTablerosViewModel
    {
        private List<TableroViewModel> tablerosVM;

        public ListaTablerosViewModel(List<Tablero> tableros)
        {
            TablerosVM = new List<TableroViewModel>();
            foreach (var t in tableros)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                TablerosVM.Add(tableroVM);
            }
        }

        public List<TableroViewModel> TablerosVM { get => tablerosVM; set => tablerosVM = value; }
    }
}
