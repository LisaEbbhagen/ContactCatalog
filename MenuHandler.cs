using ContactCatalog1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactCatalog1
{
    //Handling all interactions with user
   
    public class MenuHandler
    {

        private readonly ContactManager _manager;
        public MenuHandler(ContactManager manager)
        {
            _manager = manager;
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
                        AddContact();
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
            _manager.AddContact(new Contact(1, "Lisa Ebbhagen", "lisa.ebbhagen@chasacademy.se", "friend, school, children"));
            _manager.AddContact(new Contact(2, "Liza Hjortling", "liza.hjortling@chasacademy.se", "friend, school, cat, children"));
            _manager.AddContact(new Contact(3, "Robin Grahn", "robin.grahn@chasacademy.se", "friend, school, c-sharp"));
            _manager.AddContact(new Contact(4, "Rolf Andersson", "rolf.andersson@chasacademy.se", "friend, school, bank, children"));
        }    

        public void AddContact()
        {
            Console.Write("Ange ID: ");
            string inputID = Console.ReadLine();
            int id;
            if (!int.TryParse(inputID, out id))
            {
                Console.WriteLine("Ogiltigt ID. Ange ett heltal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Ange namn: ");
            string name = Console.ReadLine();

            Console.Write("Ange e-post: ");
            string email = Console.ReadLine();

            Console.Write("Ange taggar (kommaseparerade): ");
            string tags = Console.ReadLine();

            //Contact newContact = new Contact(id, name, email, tags);

            try
            {
                _manager.AddContact(new Contact(id, name, email, tags));
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
            //Snygga till alla listutskrifter 
            //Console.WriteLine("\nResultat:");
            //Console.WriteLine("--------------------------------------------------");
            //Console.WriteLine($"{"Kontonummer",-15} {"Ägare",-20} {"Saldo",10}"); // -15 -20=Vänsterjustera och reservera 15 resp 20 tecken, 10=högerjustera o reservera 10 tecken. :C = formaterar som valuta
            //Console.WriteLine("--------------------------------------------------");

            //foreach (var acc in results)
            //{
            //    Console.WriteLine($"{acc.AccountNumber,-15} {acc.Owner.Name,-20} {acc.Balance,10:C}");
            //}
            foreach (var contact in _manager.GetAll())
            {
                Console.WriteLine($"ID: {contact.Id} | Namn: {contact.Name} | Email: {contact.Email} | Taggar: {contact.Tags}");
            }
        }

        public void SearchByName()
        {
            Console.Write("Ange sökterm för namn: ");
            string inputName = Console.ReadLine();
            var hitsByName = _manager.SearchByName(inputName);
            
            if (hitsByName.Any())
            {
                Console.WriteLine("Sökträffar:");
                foreach (var h in hitsByName)
                {
                    Console.WriteLine($"- ID: {h.Id} | Namn: {h.Name} | Email: {h.Email} | Taggar: {h.Tags}");
                }
            }

            else Console.WriteLine("Inga träffar.");
        }

        public void FilterByTag()
        {
            Console.Write("Filtrera via tagg: ");
            string inputTag = Console.ReadLine();
            var hitsByTag = _manager.FilterByTag(inputTag);
            if (hitsByTag.Any())
            {
                Console.WriteLine("Sökträffar:");
                foreach (var h in hitsByTag)
                {
                    Console.WriteLine($"ID: {h.Id} | Namn: {h.Name} | Email: {h.Email} | Taggar: {h.Tags}");
                }
            }

            else Console.WriteLine("Inga träffar.");
        }

        public string ExportToCsv()
        {

            var sb = new StringBuilder();
            sb.AppendLine("Id,Name,Email,Tags");
            foreach (var c in _manager.GetAll())
            {
                var tags = string.Join('|', c.Tags);
                sb.AppendLine($"{c.Id},{c.Name},{c.Email},{tags}");
            }
            return sb.ToString();
        }


        private void Pause()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear(); // Rensar konsolen för en fräsch meny
        }

        //public List<Contact> SearchByName(string inputName)
        //{
        //    return _byId.Values
        //        .Where(c => c.Name.Contains(inputName, StringComparison.OrdinalIgnoreCase))
        //        .OrderBy(c => c.Name)
        //        .ToList();
        //}

        //public List<Contact> FilterByTag(string inputTag)
        //{
        //    return _byId.Values
        //        .Where(c => c.Tags.Contains(inputTag, StringComparison.OrdinalIgnoreCase))
        //        .OrderBy(c => c.Tags)
        //        .ToList();
        //}

        ////Private because its only used internal
        //



    }

}

        
   

