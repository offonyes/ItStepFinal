using System;

class FinalCalculator
{
    static void Main(string[] args)
    {
        DisplayHeader();

        double num1 = ValidateInput("Enter the first number: ");
        double num2 = ValidateInput("Enter the second number: ");
        Console.WriteLine();

        DisplayMenu();

        char choice;

        // Checks input and accepts only +, -, *, /
        while (!(char.TryParse(Console.ReadLine(), out choice) && (choice == '+' || choice == '-' || choice == '*' || choice == '/')))
        {
            PrintError("Invalid input! Please select a valid option (+, -, *, /).");
        }

        double result = 0;

        switch (choice)
        {
            case '+':
                result = num1 + num2;
                break;
            case '-':
                result = num1 - num2;
                break;
            case '*':
                result = num1 * num2;
                break;
            case '/':
                if (num2 == 0)
                {
                    PrintError("Error: Division by zero is not allowed!");
                    return;
                }
                result = num1 / num2;
                break;
        }


        DisplayResult(num1, num2, choice, result);
    }

    static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         Welcome to Calculator        ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Select an operation:" +
            "\n   Addition (+)" +
            "\n   Subtraction (-)" +
            "\n   Multiplication (*)" +
            "\n   Division (/)");
        Console.WriteLine();
        Console.ForegroundColor= ConsoleColor.Yellow;
        Console.Write("Your choice (+, -, *, /): ");
        Console.ResetColor();
    }

    static void DisplayResult(double num1, double num2, char operation, double result)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("========================================");
        Console.WriteLine($"    Result: {num1} {operation} {num2} = {result}");
        Console.WriteLine("========================================");
        Console.ResetColor();
    }

    // Validate input. Accepts only numbers
    static double ValidateInput(string text)
    {
        Console.Write(text);
        double value;
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            PrintError("Invalid input! Please enter a valid number.");
            Console.Write(text);
        }
        return value;
    }

    static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($" {message}");
        Console.ResetColor();
    }
}
