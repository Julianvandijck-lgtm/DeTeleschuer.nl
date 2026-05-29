using Microsoft.AspNetCore.Mvc;
using Interface.Dtos;
using Interface.Services;

namespace Deteleschuer.nl.Controllers;

public class HomeController : Controller
{
    private readonly IAbonnementService _abonnementService;

    public HomeController(IAbonnementService abonnementService)
    {
        _abonnementService = abonnementService;
    }

    public IActionResult Index(string? provider = null)
    {
        List<AbonnementDto> abonnementen = _abonnementService.HaalOverzichtOp(provider);
        return View(abonnementen);
    }

}   

   
    