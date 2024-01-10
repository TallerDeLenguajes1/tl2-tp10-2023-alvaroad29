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
    private readonly ITareaRepository _tareaRepository;
    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        _tareaRepository = tareaRepository;
    }

    [HttpGet]
    public IActionResult Index() 
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            List<Tablero> misTableros = _tableroRepository.GetAllById(userIdInSession);

            List<Tablero> tablerosTareas = tablerosTareasAsignadas(userIdInSession);

            List<Usuario> usuarios = _usuarioRepository.GetAll();

            return View(new ListaTablerosViewModel(misTableros,tablerosTareas,usuarios));

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a los tableros {ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"}); //?
        }
    }

    [HttpGet]
    public IActionResult TodosTableros() 
    {
        try
        {
            if (!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"}); 
            if (!IsAdmin()) return RedirectToRoute(new {controller = "Login", action="Index"}); // o tiro un error??

            List<Tablero> todosTableros = _tableroRepository.GetAll();
            List<Usuario> usuarios = _usuarioRepository.GetAll();
            return View(new ListaTablerosViewModel(todosTableros, usuarios));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a todos los tableros {ex.ToString()}"); // loggeo el error
            return RedirectToRoute(new {controller = "Home", action="Error"}); //?
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
                return View(tablero);
            }else
            {
                var tablero = new CrearTableroViewModel(_usuarioRepository.GetAll());
                tablero.IdUsuarioPropietario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                return View(tablero); 
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear tablero{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
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
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            Tablero nuevoTablero= _tableroRepository.GetById(id); 
            if (IsAdmin() || nuevoTablero.IdUsuarioPropietario == userIdInSession) 
            {
                return View(new ActualizarTableroViewModel(nuevoTablero));
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
    }

    [HttpPost]
    public IActionResult Update(ActualizarTableroViewModel t) {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Update");
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            if (IsAdmin() || t.IdUsuarioPropietario == userIdInSession)
            {
                Tablero tablero = new Tablero(t);
                _tableroRepository.Update(tablero.Id, tablero);
                return RedirectToAction("Update");
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            if (IsAdmin() || _tableroRepository.GetById(id).IdUsuarioPropietario == userIdInSession)
            {
                _tableroRepository.Remove(id);
                return RedirectToAction("Index");
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
    }

    private bool IsAdmin() => HttpContext.Session.GetString("NivelDeAcceso") == enumRol.admin.ToString();
    private bool EstaLogeado() => !String.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"));

    private List<Tablero> tablerosTareasAsignadas(int idUsuario)
    {
        List<Tarea> misTareasAsignadas = _tareaRepository.GetAllByIdUsuario(idUsuario);

        List<Tablero> tableros = _tableroRepository.GetAll();

        List<Tablero> tablerosTareasAsignadas = new List<Tablero>(); 

        foreach (var tablero in tableros)
        {
            if (tablero.IdUsuarioPropietario != idUsuario && misTareasAsignadas.Any(tarea => tarea.Id_tablero == tablero.Id))
            {
                tablerosTareasAsignadas.Add(tablero);
            }
        }

        return tablerosTareasAsignadas;
    }
}
