using ContactCatalog1.Models;
using ContactCatalog1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1.Validators
{
    public class ContactValidator
    {
        private readonly IContactRepository _repo;

        public ContactValidator(IContactRepository repo)
        {
            _repo = repo;
        }

        public void Validate(Contact contact)
        {
            if (!EmailValidator.IsValidEmail(contact.Email))
                throw new InvalidEmailException(contact.Email);

            if (_repo.GetAll().Any(c => c.Email.Equals(contact.Email, StringComparison.OrdinalIgnoreCase)))
                throw new DuplicateEmailException(contact.Email);
        }        
    }
}

