using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog1
{
    //Exception is already built in in .NET as a baseclass
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email) : base($"Ogiltig e-postadress: {email}") { }
    }

    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string email) : base($"E-postadressen finns redan: {email}") { }
    }
}
