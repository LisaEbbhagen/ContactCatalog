using ContactCatalog1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1
{
    public interface IContactRepository
    {
        //Moqed repository
        void Save(Contact contact); //Ser annorlunda ut hos Malin-.-
      
        IEnumerable<Contact> GetAll();
    }
}
