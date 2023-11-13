using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepository;
    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    public IActionResult Index() 
    {
        return View(tableroRepository.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        Tablero t = new Tablero();
        t.IdUsuarioPropietario = 99;
        return View(t); 
    }

    [HttpPost]
    public IActionResult Create(Tablero tablero)
    {
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        return View(tableroRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(Tablero tablero) {
        tableroRepository.Update(tablero.Id, tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        tableroRepository.Remove(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
