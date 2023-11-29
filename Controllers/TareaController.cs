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

    public IActionResult Index(int id) 
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(new ListarTareasViewModel(_tareaRepository.GetAllByIdTablero(id), _usuarioRepository.GetAll(),_tableroRepository.GetById(id)?.Nombre)); 
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        var tarea = new Tarea();
        tarea.Id_tablero = 1;
        return View(tarea); 
    }

    [HttpPost]
    public IActionResult Create(Tarea tarea)
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(!ModelState.IsValid) return RedirectToAction("Create");
        _tareaRepository.Create(tarea.Id_tablero, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(!ModelState.IsValid) return RedirectToAction("Update");
        return View(_tareaRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(Tarea tarea) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        _tareaRepository.Update(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        _tareaRepository.Remove(id);
        return RedirectToAction("Index");
    }

    private bool IsAdmin() => HttpContext.Session.GetString("NivelDeAcceso") == enumRol.admin.ToString();
    private bool EstaLogeado() => !String.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"));


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
