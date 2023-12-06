using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.ViewModels
{
    public class ListaUsuariosViewModel
    {
        private List<UsuarioViewModel> usuariosVM;

        public ListaUsuariosViewModel(List<Usuario> usuarios)
        {
            UsuariosVM = new List<UsuarioViewModel>();
            foreach (var u in usuarios)
            {
                UsuarioViewModel usuarioVM = new UsuarioViewModel(u);
                UsuariosVM.Add(usuarioVM);
            }
        }

        public ListaUsuariosViewModel(Usuario usuario)
        {
            UsuariosVM = new List<UsuarioViewModel>();
            UsuarioViewModel usuarioVM = new UsuarioViewModel(usuario);
            UsuariosVM.Add(usuarioVM);
        }
        public List<UsuarioViewModel> UsuariosVM { get => usuariosVM; set => usuariosVM = value; }
    }
}