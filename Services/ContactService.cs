using ContactCatalog1.Models;
using ContactCatalog1.Repositories;
using ContactCatalog1.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactCatalog1.Services
{
    //Handling all contactlogics
    //Validates and store contacts
    public class ContactService : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contactsById = new();
        private readonly HashSet<string> _emails = new(StringComparer.OrdinalIgnoreCase);
        private readonly ILogger<ContactService> _logger;
        private ContactValidator _validator; 

        public ContactService(ILogger<ContactService> logger)
        {
            _logger = logger;
        }

        public void SetValidator(ContactValidator validator)
        {
            _validator = validator;
        }

        public void Save(Contact contact)
        {
            _contactsById[contact.Id] = contact; //saves contact i the dictionary
            _emails.Add(contact.Email); //adds email to hashset
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactsById.Values;
        }

        //Adds contact with validation and logging
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

        public IEnumerable<Contact> SearchByName(string name)
        {
            return _contactsById.Values
                .Where(c => c.Name
                .Contains(name, StringComparison.OrdinalIgnoreCase)); //compare searchinput with names in dictionary,
                                                                      //doesnt care about big or small character
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

            try
            {
                _logger?.LogInformation("CSV exporterad");
                Console.WriteLine("CSV-fil har exporterats.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fel vid exportering av fil.");
                Console.WriteLine("Fel vid exportering av fil.");
                throw;
            }

            return sb.ToString();
        }
    }
}