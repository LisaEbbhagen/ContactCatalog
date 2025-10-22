using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1
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
            if (!IsValidEmail(contact.Email))
                throw new InvalidEmailException(contact.Email);

            if (_repo.GetAll().Any(c => c.Email.Equals(contact.Email, StringComparison.OrdinalIgnoreCase)))
                throw new DuplicateEmailException(contact.Email);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            { 
                return false; 
            }
        }
        //_byId.Add(c.Id, c);
        //if (string.IsNullOrWhiteSpace(contact.Email) || !contact.Email.Contains("@"))
        //    throw new InvalidEmailException();

        //if (_repo.GetAll().Any(c => c.Email.Equals(contact.Email, StringComparison.OrdinalIgnoreCase)))
        //    throw new DuplicateEmailException();

    }
}

