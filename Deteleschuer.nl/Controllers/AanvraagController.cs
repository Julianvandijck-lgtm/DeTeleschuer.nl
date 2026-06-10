using Microsoft.AspNetCore.Mvc;
using Deteleschuer.nl.Helpers;
using Deteleschuer.nl.ViewModels;
using Interface.Models;
using Interface.Services;

namespace Deteleschuer.nl.Controllers;

public class AanvraagController : Controller
{
    private readonly IWebHostEnvironment _env; // geeft toegang tot de map op de server waar bestanden worden opgeslagen
    private readonly IAanvraagService _aanvraagService;

    public AanvraagController(IWebHostEnvironment env, IAanvraagService aanvraagService) // niet gebonden aan concrete implementatie
    {
        _env = env;
        _aanvraagService = aanvraagService;
    }

    public IActionResult Starten(int abonnementId)
    {
        if (abonnementId <= 0) // ongeldige id stuur terug naar home
            return RedirectToAction("Index", "Home");

        HttpContext.Session.SetInt32("abonnementId", abonnementId); // id opslaan in sessie zodat het beschikbaar blijft tijdens alle stappen
        return RedirectToAction("Persoonsgegevens");
    }

    [HttpGet] // pagina ophalen, toont het lege formulier
    public IActionResult Persoonsgegevens()
    {
        var model = HttpContext.Session.Get<PersoonsgegevensViewModel>("persoonsgegevens")
                    ?? new PersoonsgegevensViewModel(); // als al ingevuld haal op uit sessie anders leeg model
        return View(model);
    }

    [HttpGet]
    public IActionResult Bedankt()
    {
        return View();
    }

    [HttpPost] // formulier versturen, gegevens opslaan in sessie en doorsturen naar documenten
    public IActionResult Persoonsgegevens(PersoonsgegevensViewModel model)
    {
        if (!ModelState.IsValid) // als er validatiefouten zijn toon het formulier opnieuw
            return View(model);

        HttpContext.Session.Set("persoonsgegevens", model); // persoonsgegevens bewaren voor later gebruik bij bevestigen
        return RedirectToAction("Documenten");
    }

    [HttpGet]
    public IActionResult Documenten()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Documenten(DocumentenViewModel model) // async omdat bestanden uploaden tijd kost
    {
        if (!ModelState.IsValid)
            return View(model);

        if (string.IsNullOrEmpty(model.DigitaleHandtekening))
        {
            ModelState.AddModelError("DigitaleHandtekening", "Handtekening is verplicht.");
            return View(model);
        }

        var uploadsMap = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsMap); // map aanmaken als die nog niet bestaat

        var legitNaam = $"{Guid.NewGuid()}{Path.GetExtension(model.FotoLegitimatie.FileName)}"; // unieke naam zodat bestanden van verschillende klanten elkaar niet overschrijven
        using (var stream = new FileStream(Path.Combine(uploadsMap, legitNaam), FileMode.Create))
            await model.FotoLegitimatie.CopyToAsync(stream);

        var bankNaam = $"{Guid.NewGuid()}{Path.GetExtension(model.FotoBankpas.FileName)}"; // zelfde principe voor bankpas
        using (var stream = new FileStream(Path.Combine(uploadsMap, bankNaam), FileMode.Create))
            await model.FotoBankpas.CopyToAsync(stream);

        HttpContext.Session.SetString("fotoLegitimatie", legitNaam); // bestandsnamen opslaan in sessie zodat bevestigen ze kan ophalen
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
    [ActionName("Bevestigen")] // twee methodes heten bevestigen dus dit onderscheidt de post van de get
    public IActionResult BevestigenPost()
    {
        var persoonsgegevens = HttpContext.Session.Get<PersoonsgegevensViewModel>("persoonsgegevens"); // alles ophalen uit sessie
        var abonnementId = HttpContext.Session.GetInt32("abonnementId");

        if (persoonsgegevens == null || abonnementId == null) // als sessie verlopen is terug naar home
            return RedirectToAction("Index", "Home");

        var klant = new Klant // klant object opbouwen vanuit sessiedata
        {
            Naam = persoonsgegevens.Naam,
            Adres = persoonsgegevens.Adres,
            GeboorteDatum = DateOnly.FromDateTime(persoonsgegevens.Geboortedatum),
            Email = persoonsgegevens.Email,
            Telefoon = persoonsgegevens.Telefoon,
            FotoID = HttpContext.Session.GetString("fotoLegitimatie") ?? "",
            FotoBankpas = HttpContext.Session.GetString("fotoBankpas") ?? ""
        };

        var aanvraag = new Aanvraag // aanvraag object opbouwen, status altijd nieuw bij aanmaken
        {
            AbonnementId = abonnementId.Value,
            AanvraagDatum = DateTime.Now,
            Status = "Nieuw",
            NummerBehouden = persoonsgegevens.NummerBehouden,
            DigitaleHandtekening = HttpContext.Session.GetString("handtekening") ?? "",
            HandtekeningDatum = DateTime.Now
        };

        _aanvraagService.AanvraagOpslaan(klant, aanvraag); // eerst klant dan aanvraag opslaan via service

        HttpContext.Session.Clear(); // sessie leegmaken want alles is opgeslagen

        return RedirectToAction("Bedankt");
    }
}
