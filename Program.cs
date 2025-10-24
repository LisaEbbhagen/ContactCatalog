using ContactCatalog1.Services;
using ContactCatalog1.Validators;
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
            });

            var logger = loggerFactory.CreateLogger<ContactService>();
            logger.LogInformation("Programmet startar");

            // Create service (that store contacts)
            var service = new ContactService(logger);

            // Create validator (service works as a repository)
            var validator = new ContactValidator(service); 

            // Connect validator with service
            service.SetValidator(validator); 

            // Start menu
            var menu = new MenuHandler(service);
            menu.Run();

        }
    }
}
