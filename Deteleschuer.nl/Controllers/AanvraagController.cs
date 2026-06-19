using Microsoft.AspNetCore.Mvc;
using Deteleschuer.nl.Helpers;
using Deteleschuer.nl.ViewModels;
using Interface.Models;
using Logic.Services;

namespace Deteleschuer.nl.Controllers;

public class AanvraagController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly AanvraagService _aanvraagService;

    public AanvraagController(IWebHostEnvironment env, AanvraagService aanvraagService)
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

        var klant = new Klant(
            naam: persoonsgegevens.Naam,
            straatnaam: persoonsgegevens.Straatnaam,
            huisnummer: persoonsgegevens.Huisnummer,
            geboorteDatum: DateOnly.FromDateTime(persoonsgegevens.Geboortedatum!.Value),
            email: persoonsgegevens.Email,
            telefoon: persoonsgegevens.Telefoon,
            fotoId: HttpContext.Session.GetString("fotoLegitimatie") ?? "",
            fotoBankpas: HttpContext.Session.GetString("fotoBankpas") ?? ""
        );

        _aanvraagService.AanvraagOpslaan(
            klant,
            abonnementId.Value,
            persoonsgegevens.NummerBehouden,
            HttpContext.Session.GetString("handtekening") ?? ""
        );

        HttpContext.Session.Clear();

        return RedirectToAction("Bedankt");
    }

    [HttpGet]
    public IActionResult Bedankt()
    {
        return View();
    }
}
