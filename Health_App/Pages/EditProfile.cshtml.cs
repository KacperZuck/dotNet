using Health_App.Models;
using Health_App.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Health_App.Pages
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        private readonly IUserService<User, UserDto> _userService;

        public EditProfileModel(IUserService<User, UserDto> userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserDto UserData { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return RedirectToPage("/Index");

            var userId = Guid.Parse(userIdClaim);
            UserData = await _userService.Get(userId);

            if (UserData == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) {
                TempData["UnvalidMessage"] = "Formularz wymaga poprawy";
                return Page();
            }

            await _userService.Update(UserData);

            TempData["SuccessMessage"] = "Zmiany zostały zapisane pomyślnie!";
            return RedirectToPage();
        }
    }
}
