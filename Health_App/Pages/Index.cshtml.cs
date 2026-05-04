using Health_App.Models;
using Health_App.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

public class IndexModel : PageModel
{
    [BindProperty] // Ten atrybut mówi: "weź dane z formularza i włóż je tutaj"
    public UserDto LoginData { get; set; }

    private readonly IUserService<User, UserDto> _userService;

    public IndexModel(IUserService<User, UserDto> userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userService.Login(LoginData.email, LoginData.pasword);

        if (user == null)
        {
            ModelState.AddModelError("", "Błędny login lub hasło");
            return Page();
        }

        var claims = new List<Claim>
        {
            // ClaimTypes.Name zazwyczaj przechowuje unikalny login/email
            new Claim(ClaimTypes.Name, user.email),
            // ClaimTypes.NameIdentifier to zazwyczaj ID z bazy danych
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            // Możesz dodać własne dane, np. imię
            new Claim("UserName", user.name)
        };

        // 2. Stwórz tożsamość (Identity) używając schematu ciasteczek
        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // 3. Opcjonalne ustawienia ciasteczka (np. czy ma pamiętać użytkownika po zamknięciu przeglądarki)
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // "Zapamiętaj mnie"
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2) // Czas życia ciacha
        };

        // 4. WYGENERUJ CIASTECZKO i zaloguj użytkownika
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToPage("/UserView");
    }
}