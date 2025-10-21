using System.Text;

namespace ContactCatalog1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var byId = new Dictionary<int, Contact>();
            var emails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            AddContact(new Contact(1, "Lisa Ebbhagen", "lisa.ebbhagen@chasacademy.se", "friend, school, children"), byId, emails);
            AddContact(new Contact(2, "Liza Hjortling", "liza.hjortling@chasacademy.se", "friend, school, cat, children"), byId, emails);
            AddContact(new Contact(3, "Robin Grahn", "robin.grahn@chasacademy.se", "friend, school, c-sharp"), byId, emails);
            AddContact(new Contact(4, "Rolf Andersson", "rolf.andersson@chasacademy.se", "friend, school, bank, children"), byId, emails);

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
                            AddContact(newContact, byId, emails);
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
                        catch (Exception)
                        {
                            Console.WriteLine($"Något gick fel");
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
                        var hitsByName = byId.Values.Where(c => c.Name.Contains(inputName, StringComparison.OrdinalIgnoreCase)).OrderBy(c => c.Name).ToList();
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
                        var hitsByTag = byId.Values.Where(c => c.Tags.Contains(inputTag, StringComparison.OrdinalIgnoreCase)).OrderBy(c => c.Tags).ToList();
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

                        string ToCsv(IEnumerable<Contact> contacts)
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine("Id,Name,Email,Tags");
                            foreach (var c in contacts)
                            {
                                var tags = string.Join('|', c.Tags);
                                sb.AppendLine($"{c.Id},{c.Name},{c.Email},{tags}");
                            }
                            return sb.ToString();
                        }

                        string csvData = ToCsv(byId.Values);

                        string filePath = "kontakter.csv";

                        try
                        {
                            File.WriteAllText(filePath, csvData);
                            Console.WriteLine($"CSV-fil skapad: {filePath}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fel vid filskrivning. Ingen fil skapad.");
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


            void AddContact(Contact c, Dictionary<int, Contact> byId, HashSet<string> emails)
            {
                if (!IsValidEmail(c.Email)) throw new InvalidEmailException(c.Email);
                if (!emails.Add(c.Email)) throw new DuplicateEmailException(c.Email);
                byId.Add(c.Id, c);
            }
        }

        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }
    }
}

