using NetPC.Models;

namespace NetPC.Data
{
    public static class Extension
    {
        public static ContactDto AsDto(this Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Password = contact.Password,
                Phone = contact.Phone,
                Email = contact.Email,
                Category = contact.Category,
                Company = contact.Company.HasValue
                    ? (int)contact.Company.Value
                    : null,
                OtherCategory = contact.OtherCategory,
                Birth_Date = contact.Birth_Date.ToString("yyyy-MM-dd")
            };
        }
    }

}
