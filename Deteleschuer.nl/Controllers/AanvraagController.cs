using Microsoft.AspNetCore.Mvc;
using Deteleschuer.nl.Helpers;
using Deteleschuer.nl.ViewModels;
using Interface.Models;
using Interface.Services;

namespace Deteleschuer.nl.Controllers;

public class AanvraagController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly IAanvraagService _aanvraagService;

    public AanvraagController(IWebHostEnvironment env, IAanvraagService aanvraagService)
    {
        _env = env;
        _aanvraagService = aanvraagService;
    }

    public IActionResult Starten(int abonnementId)
    {
        if (abonnementId <= 0)
            return RedirectToAction("Index", "Home");

        HttpContext.Session.SetInt32("abonnementId", abonnementId);
        return RedirectToAction("Persoonsgegevens");
    }

    [HttpGet]
    public IActionResult Persoonsgegevens()
    {
        var model = HttpContext.Session.Get<PersoonsgegevensViewModel>("persoonsgegevens")
                    ?? new PersoonsgegevensViewModel();
        return View(model);
    }

    [HttpGet]
    public IActionResult Bedankt()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Persoonsgegevens(PersoonsgegevensViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        HttpContext.Session.Set("persoonsgegevens", model);
        return RedirectToAction("Documenten");
    }

    [HttpGet]
    public IActionResult Documenten()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Documenten(DocumentenViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (string.IsNullOrEmpty(model.DigitaleHandtekening))
        {
            ModelState.AddModelError("DigitaleHandtekening", "Handtekening is verplicht.");
            return View(model);
        }

        var uploadsMap = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsMap);

        var legitNaam = $"{Guid.NewGuid()}{Path.GetExtension(model.FotoLegitimatie.FileName)}";
        using (var stream = new FileStream(Path.Combine(uploadsMap, legitNaam), FileMode.Create))
            await model.FotoLegitimatie.CopyToAsync(stream);

        var bankNaam = $"{Guid.NewGuid()}{Path.GetExtension(model.FotoBankpas.FileName)}";
        using (var stream = new FileStream(Path.Combine(uploadsMap, bankNaam), FileMode.Create))
            await model.FotoBankpas.CopyToAsync(stream);

        HttpContext.Session.SetString("fotoLegitimatie", legitNaam);
        HttpContext.Session.SetString("fotoBankpas", bankNaam);
        HttpContext.Session.SetString("handtekening", model.DigitaleHandtekening);
        HttpContext.Session.SetString("handtekeningDatum", DateTime.Now.ToString("o"));

        return RedirectToAction("Bevestigen");
    }

    [HttpGet]
    public IActionResult Bevestigen()
    {
        return View();
    }
    [HttpPost]
    [ActionName("Bevestigen")]
    public IActionResult BevestigenPost()
    {
        var persoonsgegevens = HttpContext.Session.Get<PersoonsgegevensViewModel>("persoonsgegevens");
        var abonnementId = HttpContext.Session.GetInt32("abonnementId");

        if (persoonsgegevens == null || abonnementId == null)
            return RedirectToAction("Index", "Home");

        var klant = new Klant
        {
            Naam = persoonsgegevens.Naam,
            Adres = persoonsgegevens.Adres,
            GeboorteDatum = DateOnly.FromDateTime(persoonsgegevens.Geboortedatum),
            Email = persoonsgegevens.Email,
            Telefoon = persoonsgegevens.Telefoon,
            FotoID = HttpContext.Session.GetString("fotoLegitimatie") ?? "",
            FotoBankpas = HttpContext.Session.GetString("fotoBankpas") ?? ""
        };

        var aanvraag = new Aanvraag
        {
            AbonnementId = abonnementId.Value,
            AanvraagDatum = DateTime.Now,
            Status = "Nieuw",
            NummerBehouden = persoonsgegevens.NummerBehouden,
            DigitaleHandtekening = HttpContext.Session.GetString("handtekening") ?? "",
            HandtekeningDatum = DateTime.Now
        };

        _aanvraagService.AanvraagOpslaan(klant, aanvraag);

        HttpContext.Session.Clear();

        return RedirectToAction("Bedankt");
    }
}




