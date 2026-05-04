
using Health_App.Data;
using Health_App.Models;
using Health_App.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
//SQLitePCL.Batteries.Init();

builder.Services.AddDbContext<ConfigDbContext>(options =>
    options.UseSqlite("Data Source=HealthApp.db"));
// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Index";     // Gdzie przekierować niezalogowanych
        //options.LogoutPath = "/Logout";   // Ścieżka wylogowania
        //options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorization();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IUserRepository<User>, UserRepository>();
builder.Services.AddScoped<IUserService<User, UserDto>, UserService<User, UserDto>>();

var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<MappingProfile>();
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets(); 
app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");// Lub tam gdzie masz główne wejście Blazora DO SPRAWDZENIA

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ConfigDbContext>();
    // To sprawi, że baza powstanie automatycznie przy pierwszym uruchomieniu
    dbContext.Database.EnsureCreated();
}

app.Run();
