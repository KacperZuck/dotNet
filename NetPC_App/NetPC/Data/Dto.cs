using NetPC.Models;
using System.ComponentModel.DataAnnotations;

namespace NetPC.Data
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required] 
        public string Email { get; set; }
        public int Phone { get; set; }
        public Categories Category { get; set; }
        public int? Company { get; set; }
        public string? OtherCategory { get; set; }
        public string Birth_Date { get; set; }
    }

    public class CreateContactDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public int Phone { get; set; }


        public Categories Category { get; set; }
        public CompanyCategories? Company { get; set; }

        public string? OtherCategory { get; set; }
        public DateOnly Birth_Date { get; set; }
    }
}
