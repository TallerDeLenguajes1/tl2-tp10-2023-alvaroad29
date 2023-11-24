using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository _tareaRepository;
    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index(int id) 
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(_tareaRepository.GetAllByIdTablero(id)); 
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
