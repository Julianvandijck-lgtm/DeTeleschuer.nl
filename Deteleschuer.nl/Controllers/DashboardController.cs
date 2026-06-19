using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Logic.Mappers;
using Logic.Services;
using Deteleschuer.nl.Mappers;
using Deteleschuer.nl.ViewModels;

namespace Deteleschuer.nl.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly AanvraagService _aanvraagService;
    private readonly NotitieService _notitieService;

    public DashboardController(AanvraagService aanvraagService, NotitieService notitieService)
    {
        _aanvraagService = aanvraagService;
        _notitieService = notitieService;
    }

    public IActionResult Index()
    {
        var viewModel = new AanvraagOverzichtViewModel
        {
            Aanvragen = _aanvraagService.HaalOverzicht()
                .Select(AanvraagViewModelMapper.NaarRegelViewModel)
                .ToList()
        };
        return View(viewModel);
    }

    public IActionResult Detail(int id)
    {
        var dto = _aanvraagService.HaalDetail(id);
        if (dto == null) return RedirectToAction("Index");

        var notities = _notitieService.HaalVoorAanvraag(id);
        return View(AanvraagViewModelMapper.NaarDetailViewModel(dto, notities));
    }

    [HttpPost]
    public IActionResult StatusBijwerken(int id, string status)
    {
        try
        {
            _aanvraagService.WerkStatusBij(id, AanvraagMapper.NaarStatusEnum(status));
        }
        catch (ArgumentException)
        {
            // Ongeldige statuswaarde (gemanipuleerde POST) — stil negeren
        }
        return RedirectToAction("Detail", new { id });
    }

    [HttpPost]
    public IActionResult NotitieOpslaan(int aanvraagId, string tekst)
    {
        _notitieService.Toevoegen(aanvraagId, tekst);
        return RedirectToAction("Detail", new { id = aanvraagId });
    }

    [HttpPost]
    public IActionResult NotitieBijwerken(int id, int aanvraagId, string tekst)
    {
        _notitieService.Bijwerken(id, tekst);
        return RedirectToAction("Detail", new { id = aanvraagId });
    }
}
