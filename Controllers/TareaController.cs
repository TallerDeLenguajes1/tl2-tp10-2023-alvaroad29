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
        return View(tareaRepository.GetAllByIdTablero(1));
    }

    [HttpGet]
    public IActionResult Create()
    {
        var tarea = new Tarea();
        tarea.Id_tablero = 1;
        return View(tarea); 
    }

    [HttpPost]
    public IActionResult Create(Tarea tarea)
    {
        tareaRepository.Create(tarea.Id_tablero, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        return View(tareaRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(Tarea tarea) {
        tareaRepository.Update(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        tareaRepository.Remove(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
