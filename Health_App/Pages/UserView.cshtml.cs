using Health_App.Models;
using Health_App.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Health_App.Pages
{
    [Authorize]
    public class UserViewModel : PageModel
    {
        private readonly IUserService<User, UserDto> _userService;

        public UserViewModel(IUserService<User, UserDto> userService)
        {
            _userService = userService;
        }

        public ICollection<UserDto> _usersList { get; set; }
        public string _currentUserName { get; set; }

        public async Task OnGetAsync()
        {
            _currentUserName = User.FindFirst("UserName")?.Value ?? "Użytkowniku";
            _usersList = await _userService.GetAll();
        }
    }
}