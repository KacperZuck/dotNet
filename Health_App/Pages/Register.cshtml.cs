using Health_App.Models;
using Health_App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Health_App.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService<Models.User, UserDto> _userService;

        [BindProperty]
        public InputModel Input { get; set; }

        public RegisterModel(IUserService<Models.User, UserDto> userService)
        {
            _userService = userService;
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Imię jest wymagane")]
            public string name { get; set; }

            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress(ErrorMessage = "Błędny format adresu email")]
            public string email { get; set; }

            [Required(ErrorMessage = "Data urodzenia jest wymagana")]
            [DataType(System.ComponentModel.DataAnnotations.DataType.Date)] // To wygeneruje kalendarz w przeglądarce
            public DateOnly birth_date { get; set; }

            [Required(ErrorMessage = "Hasło jest wymagane")]
            [DataType(System.ComponentModel.DataAnnotations.DataType.Password)] // Poprawione: Password (nie pasword)
            [MinLength(6, ErrorMessage = "Hasło musi mieć minimum 6 znaków")]
            public string pasword { get; set; } // Nazwa Twojego pola w DTO

            [Required(ErrorMessage = "Musisz powtórzyć hasło")]
            [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
            [Compare("pasword", ErrorMessage = "Hasła nie są identyczne")] // Musi pasować do nazwy pola powyżej
            public string repPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newUserDto = new UserDto
            {
                name = Input.name,
                email = Input.email,
                birth_date = Input.birth_date,
                pasword = Input.pasword
            };

            await _userService.Add(newUserDto);

            return RedirectToPage("/Index");
        }
    }

  
}
