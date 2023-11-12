using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_alvaroad29.Models;

namespace tl2_tp10_2023_alvaroad29.Controllers;
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepository;
    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index() // listo los usuarios
    {
        return View(usuarioRepository.GetAll());
    }

    [HttpGet]
    public IActionResult Create() // vista del form para ingresar
    {
        return View(new Usuario()); //??
    }

    [HttpPost]
    public IActionResult Create(Usuario usuario) // dsp de enviar el form
    {
        usuarioRepository.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id) // tiene que coincidir con el asp-route-{nombre} del button
    {  
        return View(usuarioRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(Usuario usuario) {
        usuarioRepository.Update(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) {
        usuarioRepository.Remove(id);
        return RedirectToAction("Index");
    }

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
