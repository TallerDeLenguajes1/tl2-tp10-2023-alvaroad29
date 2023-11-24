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
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});

        if (!IsAdmin())
        {
            return View(tableroRepository.GetAllById(Convert.ToInt32(HttpContext.Session.GetString("Id"))));
        }else{
            return View(tableroRepository.GetAll());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        Tablero t = new Tablero();
        t.IdUsuarioPropietario = 99;
        return View(t); 
    }

    [HttpPost]
    public IActionResult Create(Tablero tablero)
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(IsAdmin()) return View(tableroRepository.GetById(id));
        if (tableroRepository.GetById(id).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id"))) // que el usuario que modifique el tablero sea al cual pertenece el tablero
        {
            return View(tableroRepository.GetById(id));
        }else
        {
            return RedirectToAction("Error");
        }
        
    }

    [HttpPost]
    public IActionResult Update(Tablero tablero) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        tableroRepository.Update(tablero.Id, tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        tableroRepository.Remove(id);
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
