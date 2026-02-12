
while (true)
{
    Menu();
    int answer;
    if (!int.TryParse(Console.ReadLine(), out answer))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (answer)
    {
        case 1:
            ViewCharacters();
            break;
        case 2:
            AddCharacter();
            break;
        case 3:
            Console.WriteLine("Exiting the program. Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
            break;
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


static void ViewCharacters()
{
    Console.Clear();
    Console.WriteLine("Viewing all characters...");
    // Code to display characters goes here
    Console.Write("press enter to leave");
    Console.ReadLine();
}

static void AddCharacter()
{
    Console.Clear();
    Console.WriteLine("Adding a new character...");
    // Code to add a new character goes here
    Console.Write("press enter to leave");
    Console.ReadLine();
    
}