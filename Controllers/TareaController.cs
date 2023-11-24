using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;
    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    public IActionResult Index() 
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(tareaRepository.GetAllByIdTablero(1)); // ???
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
        tareaRepository.Create(tarea.Id_tablero, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(tareaRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(Tarea tarea) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        tareaRepository.Update(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        tareaRepository.Remove(id);
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
