using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _usuarioRepository;
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(Usuario usuario) // LoginViewModel
    {
        //existe el usuario?
        var usuarios = _usuarioRepository.GetAll();

        var usuarioLogeado = usuarios.FirstOrDefault(u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Contrasenia == usuario.Contrasenia);

        // si el usuario no existe devuelvo al index
        if (usuarioLogeado == null){
            _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NombreDeUsuario + " Clave ingresada: " + usuario.Contrasenia);
            return RedirectToAction("Index"); //el formulario
        }    
        
        _logger.LogInformation("El usuario: " + usuario.NombreDeUsuario + " Ingreso correctamente");

        //Registro el usuario (creo la sesion)
        logearUsuario(usuarioLogeado);
        
        //Devuelvo el usuario al Home
        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("NivelDeAcceso", user.Rol.ToString());
        HttpContext.Session.SetString("Id", user.Id.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
