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
            Tablero tablero = _tableroRepository.GetById(id);
            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<Tarea> tareas = _tareaRepository.GetAllByIdTablero(id);
            List<Usuario> usuarios = _usuarioRepository.GetAll();
            TableroViewModel tableroVM = new TableroViewModel(tablero);

            if (IsAdmin() || tablero.IdUsuarioPropietario == idUsuario) // admin o operador dueño del tab.
            {
                return View(new ListarTareasViewModel(tareas, usuarios, tableroVM)); 
            }else
            {
                return View("ListarTareasAsignadas", new ListarTareasAsignadasViewModel(tareas, usuarios, tableroVM, idUsuario));
            }     
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a las tareas{ex.ToString()}");
            return RedirectToAction("Error");
        }
        
    }

    public IActionResult MisTareas()
    {
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});

            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<Tarea> misTareas = _tareaRepository.GetAllByIdUsuario(idUsuario);

            List<Tablero> tableros = _tableroRepository.GetAll();

            Usuario usuario = _usuarioRepository.GetById(idUsuario);

            return View(new ListarMisTareasViewModel(misTareas, tableros, usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a las tareas{ex.ToString()}");
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult Create(int id)
    {
        try // esta de mas??
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            if (IsAdmin() || _tableroRepository.GetById(id).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                var tarea = new CrearTareaViewModel(_usuarioRepository.GetAll());
                tarea.Id_tablero = id;
                return View(tarea); 
            }else
            {
                return RedirectToAction("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear tarea {ex.ToString()}");
            return RedirectToAction("Error");
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
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        try
        {
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            Tarea nuevaTarea = _tareaRepository.GetById(id);
            if (IsAdmin() || _tableroRepository.GetById(nuevaTarea.Id_tablero).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id"))) // solo admin y propietario de la tarea
            {
                return View(new ActualizarTareaViewModel(nuevaTarea, _usuarioRepository.GetAll()));
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
    public IActionResult Update(ActualizarTareaViewModel t) {
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Update");
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(IsAdmin() || _tableroRepository.GetById(t.Id_tablero).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id"))){
                Tarea tarea = new Tarea(t);
                _tareaRepository.Update(tarea.Id, tarea);
                return RedirectToAction("Index", new{id = tarea.Id_tablero});
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
    public IActionResult UpdateEstado(int id, int idTablero, EstadoTarea estado) {
        try
        {
            // if(!ModelState.IsValid) return RedirectToAction("Update");
            if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            Tarea tarea = _tareaRepository.GetById(id);
            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            if(IsAdmin() || _tableroRepository.GetById(idTablero).IdUsuarioPropietario == idUsuario || tarea.IdUsuarioAsignado == idUsuario)
            {
                
                tarea.Estado = estado;
                _tareaRepository.Update(id, tarea);
                return RedirectToAction("Index", new{id = idTablero});
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

            Tarea tareaEliminar = _tareaRepository.GetById(id);
            if (IsAdmin() || _tableroRepository.GetById(tareaEliminar.Id_tablero).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                _tareaRepository.Remove(id);
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
