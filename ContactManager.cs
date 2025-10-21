using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1
{
    public class ContactManager
    {
        private readonly Dictionary<int, Contact> _byId = new();
        private readonly HashSet<string> _emails = new(StringComparer.OrdinalIgnoreCase);



        public void AddContact(Contact c)
        {
            if (!IsValidEmail(c.Email)) throw new InvalidEmailException(c.Email);
            if (!_emails.Add(c.Email)) throw new DuplicateEmailException(c.Email);
            _byId.Add(c.Id, c);
        }

        public List<Contact> SearchByName(string inputName)
        {
            return _byId.Values
                .Where(c => c.Name.Contains(inputName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Name)
                .ToList();
        }

        public List<Contact> FilterByTag(string inputTag)
        {
            return _byId.Values
                .Where(c => c.Tags.Contains(inputTag, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Tags)
                .ToList();
        }
                        
//Private because its only used internal
private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        public string ToCsv(IEnumerable<Contact> contacts)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,Name,Email,Tags");
            foreach (var c in contacts)
            {
                var tags = string.Join('|', c.Tags);
                sb.AppendLine($"{c.Id},{c.Name},{c.Email},{tags}");
            }
            return sb.ToString();
        }
    }
}
