using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interface.Models;
using Interface.Repositories;
using Logic.Mappers;
using Logic.Services;
using Deteleschuer.nl.Mappers;
using Deteleschuer.nl.ViewModels;

namespace Deteleschuer.nl.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly AanvraagService _aanvraagService;
    private readonly INotitieRepository _notitieRepository;

    public DashboardController(AanvraagService aanvraagService, INotitieRepository notitieRepository)
    {
        _aanvraagService = aanvraagService;
        _notitieRepository = notitieRepository;
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

        dto.Notities = _notitieRepository.HaalOpVoorAanvraag(id);
        return View(AanvraagViewModelMapper.NaarDetailViewModel(dto));
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
        _notitieRepository.Opslaan(new Notitie
        {
            AanvraagId = aanvraagId,
            Tekst = tekst,
            DatumAangemaakt = DateTime.Now
        });
        return RedirectToAction("Detail", new { id = aanvraagId });
    }

    [HttpPost]
    public IActionResult NotitieBijwerken(int id, int aanvraagId, string tekst)
    {
        _notitieRepository.Bijwerken(new Notitie { Id = id, AanvraagId = aanvraagId, Tekst = tekst });
        return RedirectToAction("Detail", new { id = aanvraagId });
    }
}
