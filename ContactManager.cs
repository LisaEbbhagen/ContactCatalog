using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactCatalog1
{
    //Handling all contactlogics
    //Validates and store contacts
    public class ContactManager : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contactsById = new();
        private readonly HashSet<string> _emails = new(StringComparer.OrdinalIgnoreCase);
        private readonly ILogger<ContactManager> _logger;
        private ContactValidator _validator; 

        public ContactManager(ILogger<ContactManager> logger)
        {
            _logger = logger;
        }

        public void SetValidator(ContactValidator validator)
        {
            _validator = validator;
        }

        public void Save(Contact contact)
        {
            _contactsById[contact.Id] = contact;
            _emails.Add(contact.Email);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactsById.Values;
        }

        public void AddContact(Contact contact)
        {
            try
            {
                _validator.Validate(contact);
                Save(contact);
                _logger.LogInformation("Kontakt sparad: {Name}", contact.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fel vid sparande av kontakt.");
                throw;
            }
        }

            //_validator.Validate(contact); // kastar exception om något är fel

            //_contactsById[contact.Id] = contact;
            //_emails.Add(contact.Email);

        public IEnumerable<Contact> SearchByName(string name)
        {
            return _contactsById.Values
                .Where(c => c.Name
                .Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Contact> FilterByTag(string tag)
        {
            return _contactsById.Values
                .Where(c => c.Tags.Split(',')
                .Select(t => t.Trim())
                .Contains(tag, StringComparer.OrdinalIgnoreCase));
        }

        public string ToCsv(IEnumerable<Contact> contacts)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,Namn,Email,Taggar");

            foreach (var c in contacts)
            {
                sb.AppendLine($"{c.Id},{c.Name},{c.Email},{c.Tags}");
            }

            return sb.ToString();
        }
    }
}