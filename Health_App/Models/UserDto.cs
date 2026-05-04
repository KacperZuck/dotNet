using Health_App.Common;

namespace Health_App.Models
{
    public class UserDto : IDto
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string pasword { get; set; }
        public DateOnly birth_date { get; set; }
    }
}
