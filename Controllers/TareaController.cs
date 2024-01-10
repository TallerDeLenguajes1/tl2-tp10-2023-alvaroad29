using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _tareaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    private readonly ITableroRepository _tableroRepository;
    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository,  IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
    }

    [HttpGet]
    public IActionResult Index(int id) 
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            
            Tablero tablero = _tableroRepository.GetById(id);
            List<Tarea> tareas = _tareaRepository.GetAllByIdTablero(id);
            List<Usuario> usuarios = _usuarioRepository.GetAll();
            TableroViewModel tableroVM = new TableroViewModel(tablero);

            if (IsAdmin() || tablero.IdUsuarioPropietario == userIdInSession) // admin o operador dueño del tab.
            {
                return View(new ListarTareasViewModel(tareas, usuarios, tableroVM)); 
            }else
            {
                return View("ListarTareasAsignadas", new ListarTareasAsignadasViewModel(tareas, usuarios, tableroVM, userIdInSession));
            }     
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a las tareas{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
    }

    [HttpGet]
    public IActionResult MisTareas()
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<Tarea> misTareas = _tareaRepository.GetAllByIdUsuario(userIdInSession);
            List<Tablero> tableros = _tableroRepository.GetAll();
            Usuario usuario = _usuarioRepository.GetById(userIdInSession);
            return View(new ListarMisTareasViewModel(misTareas, tableros, usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a las tareas{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
    }

    [HttpGet]
    public IActionResult Create(int id)
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if (IsAdmin() || _tableroRepository.GetById(id).IdUsuarioPropietario == userIdInSession)
            {
                var tarea = new CrearTareaViewModel(_usuarioRepository.GetAll());
                tarea.Id_tablero = id;
                return View(tarea); 
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear tarea {ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
        
    }

    [HttpPost]
    public IActionResult Create(CrearTareaViewModel t)
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!ModelState.IsValid) return RedirectToAction("Create");
            Tarea tarea = new Tarea(t);
            _tareaRepository.Create(tarea.Id_tablero, tarea);
            return RedirectToAction("Index", new{id = tarea.Id_tablero});
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear tarea {ex.ToString()}");
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
            Tarea nuevaTarea = _tareaRepository.GetById(id);
            if (IsAdmin() || _tableroRepository.GetById(nuevaTarea.Id_tablero).IdUsuarioPropietario == userIdInSession) // solo admin y propietario de la tarea
            {
                return View(new ActualizarTareaViewModel(nuevaTarea, _usuarioRepository.GetAll()));
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
    public IActionResult Update(ActualizarTareaViewModel t) {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Update");
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});

            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if(IsAdmin() || _tableroRepository.GetById(t.Id_tablero).IdUsuarioPropietario == userIdInSession){
                Tarea tarea = new Tarea(t);
                _tareaRepository.Update(tarea.Id, tarea);
                return RedirectToAction("Index", new{id = tarea.Id_tablero});
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
    public IActionResult UpdateEstado(int id, int idTablero, EstadoTarea estado) {
        try
        {
            // if(!ModelState.IsValid) return RedirectToAction("Update");
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            Tarea tarea = _tareaRepository.GetById(id);
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if(IsAdmin() || _tableroRepository.GetById(idTablero).IdUsuarioPropietario == userIdInSession || tarea.IdUsuarioAsignado == userIdInSession)
            {
                tarea.Estado = estado;
                _tareaRepository.Update(id, tarea);
                return RedirectToAction("Index", new{id = idTablero});
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

            Tarea tareaEliminar = _tareaRepository.GetById(id);
            if (IsAdmin() || _tableroRepository.GetById(tareaEliminar.Id_tablero).IdUsuarioPropietario == userIdInSession)
            {
                _tareaRepository.Remove(id);
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
}
