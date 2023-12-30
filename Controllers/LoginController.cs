using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
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
    public IActionResult Login(LoginViewModel usuario) // LoginViewModel
    {
        try
        {
            //existe el usuario?
            if(!ModelState.IsValid) return RedirectToAction("Index");

            var usuarioLogeado = _usuarioRepository.Login(usuario.NombreDeUsuario, usuario.Contrasenia);

            // si el usuario no existe devuelvo al index
            if (usuarioLogeado == null){
                _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NombreDeUsuario + " Clave ingresada: " + usuario.Contrasenia);
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                return View("Index", usuario); //el formulario
            }    

            _logger.LogInformation("El usuario: " + usuario.NombreDeUsuario + " Ingreso correctamente"); // logs

            //Registro el usuario (creo la sesion)
            logearUsuario(usuarioLogeado);

            //Devuelvo el usuario al Home
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar logear un usuario {ex.ToString()}");
            ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo de nuevo.");
            return View("Index", usuario); 
            //return BadRequest();
        }
        return RedirectToRoute(new { controller = "Home", action = "Index" });
        
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void logearUsuario(Usuario user) // seteo los campos 
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
