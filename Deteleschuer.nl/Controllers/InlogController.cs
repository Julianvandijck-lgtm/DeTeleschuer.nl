using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Deteleschuer.nl.ViewModels;
using Interface.Services;

namespace Deteleschuer.nl.Controllers;

public class InlogController : Controller
{
    private readonly IInlogService _inlogService;

    public InlogController(IInlogService inlogService)
    {
        _inlogService = inlogService;
    }

    [HttpGet]
    public IActionResult Inloggen() => View();

    [HttpPost]
    public async Task<IActionResult> Inloggen(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (!_inlogService.ControleerInloggegevens(model.Gebruikersnaam, model.Wachtwoord))
        {
            ModelState.AddModelError("", "Onjuiste gebruikersnaam of wachtwoord.");
            return View(model);
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Gebruikersnaam) };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); // gebruikers naam word in een coockie gestopt en asp.net stuurt deze steeds mee bij inlog scherm 
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                 // alvast voorbereiding voor latere stappen 
        return RedirectToAction("Index", "Dashboard");
    }

    public async Task<IActionResult> Uitloggen()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Inloggen");
    }
}