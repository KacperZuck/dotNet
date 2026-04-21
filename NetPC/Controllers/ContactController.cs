using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetPC;
using NetPC.Data;
using NetPC.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace NetPC.Controllers
{
    public class ContactController : Controller
    {

        private readonly IRepository<Contact> contactRepository;
        
        public ContactController(IRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await contactRepository.GetAll();
            return View(contacts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        private bool GoodPassword(string password)
        {
            return Regex.IsMatch(password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");
        }
        private void AddCompany(Contact contact, CompanyCategories company)
        {
            contact.Company = company;
        }

        private void AddOtherCategory(Contact contact, string value)
        {
            contact.OtherCategory = value;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAll()
        {
            
            var contacts = await contactRepository.GetAll();
            return Ok(contacts.Select( x => x.AsDto()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> Get(Guid id)
        {
            var contact = await contactRepository.GetbyId(id); // ( await ContactRepository.GetbyId(id)).AsDTO();
            
            if( contact == null) return NotFound();

            return Ok(contact.AsDto());
        }

        // for update
        [HttpPut("/Contact/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ContactDto update)
        {
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(update));
            Console.WriteLine(ModelState.IsValid);
            Console.WriteLine(ModelState);
            try
            {
                if (update == null)
                    return BadRequest("Brak danych");

                var contact = await contactRepository.GetbyId(id);

                if (contact == null)
                    return NotFound("Brak kontaktu w bazie");

                if (string.IsNullOrWhiteSpace(update.Email))
                    return BadRequest("Email wymagany");

                if (!GoodPassword(update.Password))
                    return BadRequest("Hasło musi mieć min. 6 znaków, dużą literę i cyfrę");

                var contacts = await contactRepository.GetAll();

                if (contacts.Any(x =>
                    x.Email == update.Email &&
                    x.Id != id))
                {
                    return BadRequest("Email musi być unikalny");
                }

                if (!DateOnly.TryParse(update.Birth_Date, out var birthDate))
                {
                    return BadRequest("Niepoprawna data urodzenia");
                }

                contact.Name = update.Name;
                contact.Surname = update.Surname;
                contact.Email = update.Email;
                contact.Password = update.Password;
                contact.Phone = update.Phone;
                contact.Category = update.Category;
                contact.Birth_Date = DateOnly.Parse(update.Birth_Date);

                if (update.Category == Categories.Służbowy && update.Company.HasValue)
                {
                    AddCompany(contact, (CompanyCategories)update.Company.Value);
                }
                else if (update.Category == Categories.Inny &&
                    !string.IsNullOrWhiteSpace(update.OtherCategory))
                {
                    AddOtherCategory(contact, update.OtherCategory);
                }

                await contactRepository.Update(contact);

                return Ok("Zapisano");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<ContactDto>> Create(CreateContactDto newContact)
        {
            if (string.IsNullOrWhiteSpace(newContact.Email))
                return BadRequest("Wymagany email");
            
            if (!GoodPassword(newContact.Password))
                return BadRequest("Wymagane lepsze haslo");

            var contacts = await contactRepository.GetAll();
            if (contacts.Any(x => x.Email == newContact.Email))
            {
                return BadRequest("Wymagany unikalny email");
            }

            if (!DateOnly.TryParse(newContact.Birth_Date.ToString(), out var birthDate))
                return BadRequest("Niepoprawna data urodzenia");

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = newContact.Name,
                Surname = newContact.Surname,
                Email = newContact.Email,
                Password = newContact.Password,
                Phone = newContact.Phone,
                Birth_Date = newContact.Birth_Date,
                Category = newContact.Category
            };

            if (newContact.Category == Categories.Służbowy && newContact.Company.HasValue)
            {
                contact.Company = newContact.Company.Value;
            }
            else if (newContact.Category == Categories.Inny)
            {
                contact.OtherCategory = newContact.OtherCategory;
            }

            await contactRepository.Create(contact);

            return RedirectToAction("Index");
        }

        [HttpDelete("/Contact/{id}")]
        public async Task<ActionResult<DBContext>> Delete(Guid id)
        {
            var contact = await contactRepository.GetbyId(id);

            if ( contact == null) return NotFound();

            await contactRepository.Delete(id);
            return Ok();
        }
    }
}
