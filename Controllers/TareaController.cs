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
            return View(new ListarTareasViewModel(_tareaRepository.GetAllByIdTablero(id), _usuarioRepository.GetAll(),new TableroViewModel(_tableroRepository.GetById(id)))); 
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
            return View(new ListarMisTareasViewModel(_tareaRepository.GetAllByIdUsuario(Convert.ToInt32(HttpContext.Session.GetString("Id"))), _tableroRepository.GetAll(),_usuarioRepository.GetById(Convert.ToInt32(HttpContext.Session.GetString("Id")))));
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
            var tarea = new CrearTareaViewModel(_usuarioRepository.GetAll());
            tarea.Id_tablero = id;
            return View(tarea); 
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
