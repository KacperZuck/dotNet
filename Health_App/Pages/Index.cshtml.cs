using Health_App.Models;
using Health_App.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

public class IndexModel : PageModel
{
    [BindProperty] 
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
            new Claim(ClaimTypes.Name, user.email),
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            new Claim("UserName", user.name)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2) 
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToPage("/UserView");
    }
}