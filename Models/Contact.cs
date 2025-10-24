using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1.Models
{
    public class Contact
    {
        //Properties thats needed when a new Contact is created
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tags { get; set; }

        //Constructor
        public Contact(int id, string name, string email, string tags)
        {
            Id = id;
            Name = name;
            Email = email;
            Tags = tags;
        }
    }
}
