using Microsoft.Extensions.Logging;
using System.Text;

namespace ContactCatalog1
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create logger
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            var logger = loggerFactory.CreateLogger<ContactManager>();

            // Create ContactManager (that stores contacts)
            var manager = new ContactManager(logger);

            // Skapa validator och koppla till manager
            var validator = new ContactValidator(manager); // manager fungerar som repository

            // Koppla validator till manager
            manager.SetValidator(validator); // Du behöver lägga till denna metod i ContactManager

            // Starta meny
            var menu = new MenuHandler(manager);
            menu.Run();

        }
    }
}
//var contactsById = new Dictionary<int, Contact>();
//var emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
//var repo = new SimpleRepository(contactsById, emails);
