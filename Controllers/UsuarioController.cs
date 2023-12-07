using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index() // listo los usuarios
    {
        try
        {
            if (!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"}); // afuera del try?
    
            if (IsAdmin())
            {
                return View(new ListaUsuariosViewModel(_usuarioRepository.GetAll()));
            }else
            {
                ListaUsuariosViewModel usuarios = new ListaUsuariosViewModel(_usuarioRepository.GetById(Convert.ToInt32(HttpContext.Session.GetString("Id"))));
                return View(usuarios);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a los usuarios {ex.ToString()}");
            return RedirectToAction("Error"); //?
        }
        
    }

    [HttpGet]
    public IActionResult Create() // vista del form para ingresar
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!IsAdmin()) return RedirectToAction("Error");
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
            if(!ModelState.IsValid) return RedirectToAction("Create");
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!IsAdmin()) return RedirectToRoute(new {controller = "Login", action="Index"});

            Usuario usuario = new Usuario(u);
            _usuarioRepository.Create(usuario);
            return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
