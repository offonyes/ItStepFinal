using System;

class GuessTheNumber
{
    private const int MinNumber = 1;
    private const int MaxNumber = 1000;

    static void Main(string[] args)
    {
        DisplayHeader();

        Random random = new Random();
        int secretNumber = random.Next(MinNumber, MaxNumber); 
        int attempts = 0;
        List<int> numbers = new List<int>();

        // Infinite loop until the player guesses the correct number
        while (true)
        {
            attempts++;
            Console.Write("Enter your guess: ");
            int guess = ValidateInput();
            numbers.Add(guess);

            if (guess == secretNumber)
            {
                DisplaySuccess(secretNumber, attempts, numbers);
                break;
            }
            else if (guess < secretNumber)
            {
                PrintHint("Higher");
            }
            else
            {
                PrintHint("Lower");
            }
        }
    }

    static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║     WELCOME TO GUESS THE NUMBER!     ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"I have chosen a number between {MinNumber} and {MaxNumber}.");
        Console.WriteLine("Your goal is to guess the number!");
        Console.WriteLine("You will receive hints: 'Higher' or 'Lower'.");
        Console.WriteLine("Let's begin!");
        Console.ResetColor();
        Console.WriteLine();
    }

    // Prints a hint to the player indicating whether to guess higher or lower
    static void PrintHint(string hint)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Hint: {hint}");
        Console.ResetColor();
    }

    static void DisplaySuccess(int number, int attempts, List<int> numbers)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("========================================");
        Console.WriteLine($"Congratulations! You guessed the number: {number}");
        Console.WriteLine($"It took you {attempts} attempts.");
        Console.WriteLine("Your attempts:");
        foreach (var i in numbers)
        {
            Console.Write($"{i}  ");
        }
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.ResetColor();
    }

    // Validates the input to ensure it's a valid number within the range
    static int ValidateInput()
    {
        int value;
        while (!int.TryParse(Console.ReadLine(), out value) || value < MinNumber || value > MaxNumber)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid input. Please enter a number between {MinNumber} and {MaxNumber}");
            Console.ResetColor();
            Console.Write("Try again: ");
        }
        return value;
    }
}
