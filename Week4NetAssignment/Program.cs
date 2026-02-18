using NLog;

namespace Week4NetAssignment;

class Program
{
    static Logger logger;

    static void Main()
    {
        

        // NLog setup
        string path = Directory.GetCurrentDirectory() + "//nlog.config";
        logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

        logger.Info("Program started");

        string file = "mario.csv";
        Console.WriteLine(Directory.GetCurrentDirectory());
        try
        {
            if (!File.Exists(file))
            {
                logger.Error("File does not exist: {File}", file);
                Console.WriteLine("File does not exist");
                return;
            }

            while (true)
            {
                Menu();
                if (!int.TryParse(Console.ReadLine(), out int answer))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Task.Delay(1000).Wait();
                    continue;
                }

                switch (answer)
                {
                    case 1:
                        ViewCharacters(file);
                        break;
                    case 2:
                        AddCharacter(file);
                        break;
                    case 3:
                        Console.WriteLine("Exiting the program. Goodbye!");
                        Task.Delay(1000).Wait();
                        logger.Info("Program ended by user");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        Task.Delay(1000).Wait();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Unhandled exception in Main");
            Console.WriteLine("An unexpected error occurred. Check log_file.txt for details.");
        }
        finally
        {
            logger.Info("Program ended");
        }
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("-----------------------------");
        Console.WriteLine("- Mario Bros Character Data -");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("1. View all characters");
        Console.WriteLine("2. Add a character");
        Console.WriteLine("3. Exit");
        Console.Write("Please select an option: ");
    }

    static void ViewCharacters(string file)
    {
        Console.Clear();
        Console.WriteLine("Viewing all characters...\n");

        try
        {
            string[] lines = File.ReadAllLines(file);

            if (lines.Length <= 1)
            {
                Console.WriteLine("No characters found.");
            }
            else
            {
                // optional: show header once
                Console.WriteLine("File header: " + lines[0]);
                Console.WriteLine(new string('-', 40));

                // each remaining line is one character
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(','); // id,name,description,species,first-appearance,year-created [web:42][web:45][web:48]

                    if (parts.Length < 6)
                    {
                        Console.WriteLine("Invalid line: " + line);
                        Console.WriteLine();
                        continue;
                    }

                    Console.WriteLine($"id: {parts[0]}");
                    Console.WriteLine($"name: {parts[1]}");
                    Console.WriteLine($"description: {parts[2]}");
                    Console.WriteLine($"species: {parts[3]}");
                    Console.WriteLine($"first-appearance: {parts[4]}");
                    Console.WriteLine($"year-created: {parts[5]}");
                    Console.WriteLine(new string('-', 40));
                }

                logger.Info("Viewed characters with labeled output from file {File}", file);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error viewing characters from {File}", file);
            Console.WriteLine("Error reading file. Check log for details.");
        }

        Console.Write("\nPress Enter to return to menu...");
        Console.ReadLine();
    }

    static void AddCharacter(string file)
    {
        Console.Clear();
        Console.WriteLine("Adding a new character...\n");

        try
        {
            Console.Write("Id: ");
            string? id = Console.ReadLine();

            Console.Write("Name: ");
            string? name = Console.ReadLine();

            Console.Write("Description: ");
            string? description = Console.ReadLine();

            Console.Write("Species: ");
            string? species = Console.ReadLine();

            Console.Write("First appearance (game): ");
            string? firstAppearance = Console.ReadLine();

            Console.Write("Year created: ");
            string? yearCreated = Console.ReadLine();

            // Build CSV line in same order as header
            string newLine = $"{id},{name},{description},{species},{firstAppearance},{yearCreated}";

            // Append with newline
            File.AppendAllText(file, Environment.NewLine + newLine);

            logger.Info("Added character {Name} with id {Id} to {File}", name, id, file);
            Console.WriteLine("\nCharacter added successfully!");
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error adding character to {File}", file);
            Console.WriteLine("Error writing to file. Check log for details.");
        }

        Console.Write("\nPress Enter to return to menu...");
        Console.ReadLine();
    }
}