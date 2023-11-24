using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;
using tl2_tp10_2023_alvaroad29.ViewModels;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index() // listo los usuarios
    {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        if(!IsAdmin()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(new ListaUsuariosViewModel(_usuarioRepository.GetAll()));
    }

    [HttpGet]
    public IActionResult Create() // vista del form para ingresar
    {
        //if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult Create(Usuario usuario) // dsp de enviar el form
    {
        if(!ModelState.IsValid) return RedirectToAction("Create");
        _usuarioRepository.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id) // tiene que coincidir con el asp-route-{nombre} 
    {  
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        return View(new ActualizarUsuarioViewModel(_usuarioRepository.GetById(id)));
    }

    [HttpPost]
    public IActionResult Update(Usuario usuario) {
        if(!ModelState.IsValid) return RedirectToAction("Update");
        _usuarioRepository.Update(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        if(!EstaLogeado()) return RedirectToRoute(new {controller = "Login", action="Index"});
        _usuarioRepository.Remove(id);
        return RedirectToAction("Index");
    }

    private bool IsAdmin() => HttpContext.Session.GetString("NivelDeAcceso") == enumRol.admin.ToString();
    private bool EstaLogeado() => !String.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"));

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
