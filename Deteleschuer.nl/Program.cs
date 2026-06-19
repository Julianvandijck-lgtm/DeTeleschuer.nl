using Interface.Repositories;
using Interface.Services;
using Dal.Repositories;
using Logic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' niet gevonden.");

builder.Services.AddScoped<IAbonnementRepository>(sp => new AbonnementRepository(connectionString));

builder.Services.AddScoped<AbonnementService>();

builder.Services.AddSession();

builder.Services.AddScoped<IKlantRepository>(sp => new KlantRepository(connectionString));

builder.Services.AddScoped<IAanvraagRepository>(sp => new AanvraagRepository(connectionString));

builder.Services.AddScoped<AanvraagService>();

builder.Services.AddScoped<IGebruikerRepository>(sp => new GebruikerRepository(connectionString));
builder.Services.AddScoped<IInlogService, InlogService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inlog/Inloggen";
        options.AccessDeniedPath = "/Inlog/Inloggen";
    });

builder.Services.AddScoped<INotitieRepository>(sp => new NotitieRepository(connectionString));

var app = builder.Build();

app.UseAuthentication();

using (var scope = app.Services.CreateScope())
{
    var gebruikerRepo = scope.ServiceProvider.GetRequiredService<IGebruikerRepository>();
    if (gebruikerRepo.HaalOpViaGebruikersnaam("Roland") == null)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("Roland123");
        gebruikerRepo.Aanmaken("Roland", hash);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();