using Microsoft.EntityFrameworkCore;
using NetPC.Data;
using System.ComponentModel.DataAnnotations;

namespace NetPC.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Contact : IEntity
    {
        // Kontakt : imie, nazwisko, email, haslo, kategoria ( lista - służbowy, prywatny, inny) --> dla służbopwy, moze byc klient/szef.. , telefon, data urodzenia

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

        public Categories Category {  get; set; }

        public CompanyCategories? Company { get; set; }

        public string? OtherCategory { get; set; }

        public DateOnly Birth_Date { get; set; }
    
    
    }
}
