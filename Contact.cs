using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tags { get; set; }

        public Contact(int id, string name, string email, string tags)
        {
            Id = id;
            Name = name;
            Email = email;
            Tags = tags;
        }


        //lägg till Microsoft.Extensions.Logging ?



        //public Dictionary<int, Contact> contactRegister = contacts.ToDictionary(c => c.Id, c => c);



        //public HashSet<string> allEmail = new HashSet<string>();
        //    emails.Add(contacts.Email)


        public void PrintList()
        {

        }

        public void SearchName()
        {

        }

        public void FilterByTag()
        {

        }


    }
}
