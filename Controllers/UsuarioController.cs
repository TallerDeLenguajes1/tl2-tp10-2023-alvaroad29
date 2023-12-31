using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class UsuarioController : Controller // hereda de controller
{
    private readonly ILogger<UsuarioController> _logger; // Ilogger
    private readonly IUsuarioRepository _usuarioRepository; // para usar los repos
    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository) // injeccion
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index() // listo los usuarios
    {
        try
        {
            if (!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"}); 
            if (!IsAdmin()) return RedirectToRoute(new {controller = "Login", action="Index"}); // o tiro un error??
            return View(new ListaUsuariosViewModel(_usuarioRepository.GetAll()));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a los usuarios {ex.ToString()}"); // loggeo el error
            return RedirectToAction("Error"); //?
        }
    }

    [HttpGet]
    public IActionResult Create() // vista del form para ingresar
    {
        try
        {
            // if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            // if(!IsAdmin()) return RedirectToAction("Error");
            return View(new CrearUsuarioViewModel());
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear usuario {ex.ToString()}");
            return RedirectToAction("Error");
        }
       
    }

    [HttpPost]
    public IActionResult Create(CrearUsuarioViewModel u) // dsp de enviar el form
    {
        try
        {
            if(_usuarioRepository.ExistUser(u.NombreDeUsuario))
            {
                ModelState.AddModelError("NombreDeUsuario", "El nombre de usuario ingresado ya existe.");
            }
            
            if(!ModelState.IsValid) return View(u); // validación
        
            Usuario usuario = new Usuario(u); // casteo
            _usuarioRepository.Create(usuario); // creo
            return RedirectToAction("Index"); // redirecciono
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear usuario {ex.ToString()}");
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult Update(int id) // tiene que coincidir con el asp-route-{nombre} 
    {  
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            if (IsAdmin() || id == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                return View(new ActualizarUsuarioViewModel(_usuarioRepository.GetById(id)));
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToAction("Error");
        }
        
    }

    [HttpPost]
    public IActionResult Update(ActualizarUsuarioViewModel u) {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Create");
        
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            if (IsAdmin() || u.Id == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                Usuario usuario = new Usuario(u);
                _usuarioRepository.Update(usuario.Id, usuario);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToAction("Error");
        }
        
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if (IsAdmin() || id == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                _usuarioRepository.Remove(id);
                if (id == Convert.ToInt32(HttpContext.Session.GetString("Id")))
                {
                    return RedirectToRoute(new {controller = "Login", action="Logout"});
                }else
                {
                    return RedirectToAction("Index");
                }
            }else
            {
                return RedirectToAction("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult Configuracion() // listo los usuarios
    {
        try
        {
            if (!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"}); 
                int id =  Convert.ToInt32(HttpContext.Session.GetString("Id"));
                string usuario = HttpContext.Session.GetString("Usuario");
                string rol = HttpContext.Session.GetString("NivelDeAcceso");
                UsuarioViewModel u = new UsuarioViewModel(usuario,rol,id);
                return View(u);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a configuración de usuario {ex.ToString()}"); // loggeo el error
            return RedirectToAction("Error"); 
        }
        
    }
    
    
    private bool IsAdmin() => HttpContext.Session.GetString("NivelDeAcceso") == enumRol.admin.ToString();
    private bool EstaLogeado() => !String.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"));

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
