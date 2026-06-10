using Microsoft.AspNetCore.Mvc;
using Interface.Dtos;
using Interface.Services;

namespace Deteleschuer.nl.Controllers;

public class HomeController : Controller
{
    private readonly IAbonnementService _abonnementService;

    public HomeController(IAbonnementService abonnementService) // ik geef weer het juiste interface pakketje mee vanuit de service 
    {
        _abonnementService = abonnementService;
    }

    public IActionResult Index(string? provider = null) // IAction is de abstractie die ervoor zorgt dat alles word teruggestuurd naar de view het hele plaatje niet alleen een ruwe lijst 
    {
        List<AbonnementDto> abonnementen = _abonnementService.HaalOverzichtOp(provider);// controller maakt lijst 
        return View(abonnementen);// stuurt data naar view
    }

}   

   
    