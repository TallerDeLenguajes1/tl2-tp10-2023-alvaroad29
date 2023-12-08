using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index() 
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});

            if (!IsAdmin())
            {
                return View(new ListaTablerosViewModel(_tableroRepository.GetAllById(Convert.ToInt32(HttpContext.Session.GetString("Id"))), _usuarioRepository.GetAll()));
            }else{
                return View(new ListaTablerosViewModel(_tableroRepository.GetAll(), _usuarioRepository.GetAll()));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a los tableros {ex.ToString()}");
            return RedirectToAction("Error"); //?
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if (IsAdmin())
            {
                var tablero = new CrearTableroViewModel(_usuarioRepository.GetAll());
                tablero.IdUsuarioPropietario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                tablero.NivelDeAcceso = HttpContext.Session.GetString("NivelDeAcceso");
                return View(tablero);
            }else
            {
                var tablero = new CrearTableroViewModel(_usuarioRepository.GetAll());
                tablero.IdUsuarioPropietario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                tablero.NivelDeAcceso = HttpContext.Session.GetString("NivelDeAcceso");
                return View(tablero); 
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear tablero{ex.ToString()}");
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult Create(CrearTableroViewModel t)
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!ModelState.IsValid) return RedirectToAction("Create");
            Tablero tablero = new Tablero(t);
            _tableroRepository.Create(tablero);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear tablero {ex.ToString()}");
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            Tablero nuevoTablero= _tableroRepository.GetById(id); 
            if (IsAdmin() || nuevoTablero.IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id"))) 
            {
                return View(new ActualizarTableroViewModel(nuevoTablero));
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

    [HttpPost]
    public IActionResult Update(ActualizarTableroViewModel t) {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Update");
            
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"}); // son necesarios estos controles en el post?
            if (IsAdmin() || t.IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                Tablero tablero = new Tablero(t);
                _tableroRepository.Update(tablero.Id, tablero);
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

    [HttpGet]
    public IActionResult Delete(int id) {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if (IsAdmin() || _tableroRepository.GetById(id).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                _tableroRepository.Remove(id);
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


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
