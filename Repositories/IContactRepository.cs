using ContactCatalog1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1.Repositories
{
    public interface IContactRepository
    {
        //Moqed repository
        void Save(Contact contact); //Ser annorlunda ut hos Malin-.-
      
        IEnumerable<Contact> GetAll();
    }
}
