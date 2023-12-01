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
    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index() 
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});

        if (!IsAdmin())
        {
            return View(new ListaTablerosViewModel(_tableroRepository.GetAllById(Convert.ToInt32(HttpContext.Session.GetString("Id"))), _usuarioRepository.GetAll()));
        }else{
            return View(new ListaTablerosViewModel(_tableroRepository.GetAll(), _usuarioRepository.GetAll()));
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        List<Usuario> usuarios = _usuarioRepository.GetAll();
        return View(new CrearTableroViewModel(new ListaUsuariosViewModel(usuarios).UsuariosVM)); 
    }

    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tablero)
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(!ModelState.IsValid) return RedirectToAction("Create");
        //_tableroRepository.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {  
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(IsAdmin()) return View(new ActualizarTableroViewModel(_tableroRepository.GetById(id)));
        if (_tableroRepository.GetById(id).IdUsuarioPropietario == Convert.ToInt32(HttpContext.Session.GetString("Id"))) // que el usuario que modifique el tablero sea al cual pertenece el tablero
        {
            return View(new ActualizarTableroViewModel(_tableroRepository.GetById(id)));
        }else
        {
            return RedirectToAction("Error");
        }
        
    }

    [HttpPost]
    public IActionResult Update(Tablero tablero) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(!ModelState.IsValid) return RedirectToAction("Update");
        _tableroRepository.Update(tablero.Id, tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        _tableroRepository.Remove(id);
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
