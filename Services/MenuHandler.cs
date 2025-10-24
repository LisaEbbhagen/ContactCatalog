using ContactCatalog1.Models;
using ContactCatalog1.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactCatalog1.Services
{
    //Handling all interactions with user
    public class MenuHandler
    {

        private readonly ContactService _service;
        public MenuHandler(ContactService service)
        {
            _service = service;
        }

        public void Run()
        {
            SeedContacts();
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PromptContact();
                        Pause();
                        break;

                    case "2":
                        ListContacts();
                        Pause();
                        break;

                    case "3":
                        SearchByName();
                        Pause();
                        break;

                    case "4":
                        FilterByTag();
                        Pause();
                        break;

                    case "5":
                        ExportToCsv();
                        Pause();
                        break;

                    case "6":
                        running = false;
                        Console.WriteLine("Avslutar..");
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.\n");
                        Console.WriteLine("Tryck på valfri tangent för att försöka igen...\n");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public void ShowMenu()
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("Välkommen till kontaktkatalogen");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Välj ett av följande alternativ: ");
            Console.WriteLine("-------------------------------------------------\n");
            Console.WriteLine("1. Lägg till");
            Console.WriteLine("2. Lista");
            Console.WriteLine("3. Sök (Namn innehåller)");
            Console.WriteLine("4. Filtrera via Tag");
            Console.WriteLine("5. Exportera CSV");
            Console.WriteLine("6. Avsluta");
        }
        
        private void SeedContacts()
        {
            _service.AddContact(new Contact(1, "Lisa Ebbhagen", "lisa.ebbhagen@chasacademy.se", "friend, school, children"));
            _service.AddContact(new Contact(2, "Liza Hjortling", "liza.hjortling@chasacademy.se", "friend, school, cat, children"));
            _service.AddContact(new Contact(3, "Robin Grahn", "robin.grahn@chasacademy.se", "friend, school, c-sharp"));
            _service.AddContact(new Contact(4, "Rolf Andersson", "rolf.andersson@chasacademy.se", "friend, school, bank, children"));
        }    

        public void PromptContact()
        {
            Console.Write("Ange ID: ");
            string inputID = Console.ReadLine();
            int id;
            if (!int.TryParse(inputID, out id) || string.IsNullOrEmpty(inputID))
            {
                Console.WriteLine("Ogiltigt ID. Ange ett heltal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ange namn: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Namn krävs.");
                Console.ReadKey();
                return;
            }
            Console.Write("Ange e-post: ");
            string email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Email krävs.");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Ange taggar (kommaseparerade): ");
            string tags = Console.ReadLine();

            try
            {
                _service.AddContact(new Contact(id, name, email, tags));
                Console.WriteLine("Kontakt tillagd!");
            }
            catch (InvalidEmailException)
            {
                Console.WriteLine($"Ogiltig e-postadress: {email}");
            }
            catch (DuplicateEmailException)
            {
                Console.WriteLine($"E-postadressen finns redan: {email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }

        private void ListContacts()
        {

            Console.WriteLine("\nKontakter:");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Id",-5} {"Namn",-18} {"Email",-32} {"Taggar", -20}"); 
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            foreach (var contact in _service.GetAll())
            {
                Console.WriteLine($"{contact.Id, -5} | {contact.Name, -15} | {contact.Email, -30} | {contact.Tags, -20}");
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
        }

        public void SearchByName()
        {
            Console.Write("Ange sökterm för namn: ");
            string inputName = Console.ReadLine();
            var hitsByName = _service.SearchByName(inputName);
            
            if (hitsByName.Any())
            {
                Console.WriteLine("Sökträffar:");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Id",-5} {"Namn",-18} {"Email",-32} {"Taggar",-20}");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                foreach (var h in hitsByName)
                {
                    Console.WriteLine($"{h.Id,-5} | {h.Name,-15} | {h.Email,-30} | {h.Tags,-20}");
                }
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }
            else Console.WriteLine("Inga träffar.");
        }

        public void FilterByTag()
        {
            Console.Write("Filtrera via tagg: ");
            string inputTag = Console.ReadLine();
            var hitsByTag = _service.FilterByTag(inputTag);
            if (hitsByTag.Any())
            {
                Console.WriteLine("Sökträffar:");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Id",-5} {"Namn",-18} {"Email",-32} {"Taggar",-20}");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                foreach (var h in hitsByTag)
                {
                    Console.WriteLine($"{h.Id,-5} | {h.Name,-15} | {h.Email,-30} | {h.Tags,-20}");
                }
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }

            else Console.WriteLine("Inga träffar.");
        }

        public void ExportToCsv()
        {
            var csv = _service.ToCsv(_service.GetAll());
            Console.WriteLine(csv);
        }


        private void Pause()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear(); 
        }
    }

}

        
   

