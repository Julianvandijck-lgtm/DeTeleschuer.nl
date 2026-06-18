using Microsoft.AspNetCore.Mvc;
using Interface.Enums;
using Logic.Services;
using Deteleschuer.nl.Mappers;
using Deteleschuer.nl.ViewModels;

namespace Deteleschuer.nl.Controllers;

public class HomeController : Controller
{
    private readonly AbonnementService _abonnementService;
    private readonly AbonnementViewModelMapper _mapper = new();

    public HomeController(AbonnementService abonnementService)
    {
        _abonnementService = abonnementService;
    }

    public IActionResult Index(string? provider = null)
    {
        var gekozenProvider = Enum.TryParse<Provider>(provider, out var parsed) ? parsed : (Provider?)null;

        var viewModel = new AbonnementOverzichtViewModel
        {
            Abonnementen = _abonnementService
                .HaalOverzichtOp(gekozenProvider)
                .Select(_mapper.NaarViewModel)
                .ToList(),
            GekozenProvider = provider
        };

        return View(viewModel);
    }
}

    