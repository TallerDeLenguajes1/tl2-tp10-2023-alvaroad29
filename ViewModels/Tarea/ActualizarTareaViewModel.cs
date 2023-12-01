using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ActualizarTareaViewModel
    {
        public TareaViewModel TareaVM { get; set; }
        public ListaUsuariosViewModel UsuariosVM { get; set; }

        public ActualizarTareaViewModel(Tarea tarea, List<Usuario> usuarios)
        {
            TareaVM = new TareaViewModel(tarea);
            UsuariosVM = new ListaUsuariosViewModel(usuarios); 
        }

        public ActualizarTareaViewModel()
        {

        }
    }
}