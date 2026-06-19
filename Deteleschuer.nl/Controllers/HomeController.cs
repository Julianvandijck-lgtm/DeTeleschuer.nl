using Microsoft.AspNetCore.Mvc;
using Interface.Enums;
using Logic.Services;
using Deteleschuer.nl.Mappers;
using Deteleschuer.nl.ViewModels;

namespace Deteleschuer.nl.Controllers;

public class HomeController : Controller
{
    private readonly AbonnementService _abonnementService;

    public HomeController(AbonnementService abonnementService)
    {
        _abonnementService = abonnementService;
    }

    public IActionResult Index(string? provider = null)
    {
        Provider? gekozenProvider = provider != null
            && Enum.TryParse<Provider>(provider, ignoreCase: true, out var parsed)
            && Enum.IsDefined(parsed)
                ? parsed
                : null;

        var viewModel = new AbonnementOverzichtViewModel
        {
            Abonnementen = _abonnementService
                .HaalOverzichtOp(gekozenProvider)
                .Select(AbonnementViewModelMapper.NaarViewModel)
                .ToList(),
            GekozenProvider = provider
        };

        return View(viewModel);
    }
}

    