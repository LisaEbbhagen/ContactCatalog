using System.Text;

namespace ContactCatalog1
{
    public class Program
    {
        static void Main(string[] args)
        {

            var byId = new Dictionary<int, Contact>();
            var emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var manager = new ContactManager();

            manager.AddContact(new Contact(1, "Lisa Ebbhagen", "lisa.ebbhagen@chasacademy.se", "friend, school, children"));
            manager.AddContact(new Contact(2, "Liza Hjortling", "liza.hjortling@chasacademy.se", "friend, school, cat, children"));
            manager.AddContact(new Contact(3, "Robin Grahn", "robin.grahn@chasacademy.se", "friend, school, c-sharp"));
            manager.AddContact(new Contact(4, "Rolf Andersson", "rolf.andersson@chasacademy.se", "friend, school, bank, children"));

            ShowMenu();

            bool running = true;
            while (running)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Ange ID: ");
                        string inputID = Console.ReadLine();
                        int id;
                        if (!int.TryParse(inputID, out id))
                        {
                            Console.WriteLine("Ogiltigt ID. Ange ett heltal.");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Ange namn: ");
                        string name = Console.ReadLine();

                        Console.Write("Ange e-post: ");
                        string email = Console.ReadLine();

                        Console.Write("Ange taggar (kommaseparerade): ");
                        string tags = Console.ReadLine();

                        Contact newContact = new Contact(id, name, email, tags);

                        try
                        {
                            manager.AddContact(newContact);
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
                        break;

                    case "2":
                        foreach (var contact in byId.Values)
                        {
                            Console.WriteLine($"ID: {contact.Id} | Namn: {contact.Name} | Email: {contact.Email} | Taggar: {contact.Tags}");
                        }
                        break;

                    case "3":
                        Console.Write("Ange sökterm för namn: ");
                        string inputName = Console.ReadLine();

                        var hitsByName = manager.SearchByName(inputName);
                        if (hitsByName.Any())
                        {
                            Console.WriteLine("Sökträffar:");
                            foreach (var h in hitsByName)
                            {
                                Console.WriteLine($"- ID: {h.Id} | Namn: {h.Name} | Email: {h.Email} | Taggar: {h.Tags}");
                            }
                        }

                        else Console.WriteLine("Inga träffar.");
                        break;

                    case "4":
                        //sök o filtrera via tagg: 
                        Console.Write("Filtrera via tagg: ");
                        string inputTag = Console.ReadLine();
                        var hitsByTag = manager.FilterByTag(inputTag);
                        if (hitsByTag.Any())
                        {
                            Console.WriteLine("Sökträffar:");
                            foreach (var h in hitsByTag)
                            {
                                Console.WriteLine($"ID: {h.Id} | Namn: {h.Name} | Email: {h.Email} | Taggar: {h.Tags}");
                            }
                        }

                        else Console.WriteLine("Inga träffar.");
                        break;

                    case "5":                       

                        string csvData = manager.ToCsv(byId.Values);
                        string filePath = "kontakter.csv";

                        try
                        {
                            File.WriteAllText(filePath, csvData);
                            Console.WriteLine($"CSV-fil skapad: {filePath}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fel: {ex.Message}");
                        }
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

        static void ShowMenu()
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
    }
}

