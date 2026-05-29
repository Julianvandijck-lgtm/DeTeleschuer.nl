using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Interface.Repositories;
using Interface.Models;

namespace Deteleschuer.nl.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IAanvraagRepository _aanvraagRepository;
    private readonly INotitieRepository _notitieRepository;

    public DashboardController(IAanvraagRepository aanvraagRepository, INotitieRepository notitieRepository)
    {
        _aanvraagRepository = aanvraagRepository;
        _notitieRepository = notitieRepository;
    }

    public IActionResult Index()
    {
        var aanvragen = _aanvraagRepository.HaalAlleOp();
        return View(aanvragen);
    }

    public IActionResult Detail(int id)
    {
        var aanvraag = _aanvraagRepository.HaalDetailOp(id);
        if (aanvraag == null) return RedirectToAction("Index");
        aanvraag.Notities = _notitieRepository.HaalOpVoorAanvraag(id);
        return View(aanvraag);
    }

    [HttpPost]
    public IActionResult StatusBijwerken(int id, string status)
    {
        _aanvraagRepository.StatusBijwerken(id, status);
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