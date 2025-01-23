namespace JournalApp
{
    class Program
    {
        static List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        static Timer dailyReminder;

        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool exit = false;

            // Set daily reminder (e.g., 8 PM every day)
            DateTime scheduledTime = DateTime.Today.AddHours(20);
            if (DateTime.Now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }
            TimeSpan timeToGo = scheduledTime - DateTime.Now;
            dailyReminder = new Timer(x => DailyReminder(journal), null, timeToGo, TimeSpan.FromHours(24));

            while (!exit)
            {
                Console.WriteLine("Journal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Add a new prompt");
                Console.WriteLine("6. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        WriteNewEntry(journal);
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        SaveJournal(journal);
                        break;
                    case "4":
                        LoadJournal(journal);
                        break;
                    case "5":
                        AddNewPrompt();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void WriteNewEntry(Journal journal)
        {
            Random random = new Random();
            int index = random.Next(prompts.Count);
            string prompt = prompts[index];
            
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine("Enter your response:");
            string response = Console.ReadLine();

            journal.AddEntry(prompt, response);
        }

        static void SaveJournal(Journal journal)
        {
            Console.WriteLine("Enter the filename to save the journal:");
            string filename = Console.ReadLine();
            journal.SaveToFile(filename);
            Console.WriteLine("Journal saved.");
        }

        static void LoadJournal(Journal journal)
        {
            Console.WriteLine("Enter the filename to load the journal:");
            string filename = Console.ReadLine();
            journal.LoadFromFile(filename);
            Console.WriteLine("Journal loaded.");
        }

        static void AddNewPrompt()
        {
            Console.WriteLine("Enter a new journal prompt:");
            string newPrompt = Console.ReadLine();
            prompts.Add(newPrompt);
            Console.WriteLine("New prompt added.");
        }

        static void DailyReminder(Journal journal)
        {
            Console.WriteLine("It's time to write in your journal!");
            WriteNewEntry(journal);
        }
    }

    internal class Journal
    {
        internal void AddEntry(string prompt, string response)
        {
            throw new NotImplementedException();
        }

        internal void DisplayEntries()
        {
            throw new NotImplementedException();
        }

        internal void LoadFromFile(string filename)
        {
            throw new NotImplementedException();
        }

        internal void SaveToFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
