using System;
using System.IO;
using System.Linq;

class Hangman
{
    static void Main(string[] args)
    {
        string filePath = "..\\..\\..\\words.txt";

        string[] words = File.ReadAllLines(filePath)
                             .Where(word => !string.IsNullOrWhiteSpace(word)) 
                             .Select(word => word.Trim().ToLower()) 
                             .ToArray();

        Random random = new Random();
        string selectedWord = words[random.Next(words.Length)];
        char[] hiddenWord = new string('_', selectedWord.Length).ToCharArray();
        int attemptsLeft = 6;
        string guessedLetters = "";

        DisplayHeader();

        Console.WriteLine("The word has been chosen. Let's begin!");
        Console.WriteLine($"The word has {selectedWord.Length} letters.");
        Console.WriteLine();

        // Loop until word is guessed or attempts run out
        while (attemptsLeft > 0 && hiddenWord.Contains('_'))
        {
            Console.WriteLine($"Word: {new string(hiddenWord)}");
            Console.WriteLine($"Attempts left: {attemptsLeft}");
            Console.WriteLine($"Guessed letters: {guessedLetters}");
            Console.Write("Enter a letter: ");
            char guess = ValidateLetter();

            if (guessedLetters.Contains(guess))
            {
                PrintError("You have already guessed this letter.");
                continue;
            }

            guessedLetters += guess + " ";

            if (selectedWord.Contains(guess))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct! The letter is in the word.");
                Console.ResetColor();

                // Reveal the guessed letter in the hidden word
                for (int i = 0; i < selectedWord.Length; i++)
                {
                    if (selectedWord[i] == guess)
                    {
                        hiddenWord[i] = guess;
                    }
                }
            }
            else
            {
                PrintError("Wrong! The letter is not in the word.");
                attemptsLeft--;
            }

            Console.WriteLine();
        }

        if (!hiddenWord.Contains('_'))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=============================================");
            Console.WriteLine($"Congratulations! You guessed the word: {selectedWord}");
            Console.WriteLine("You have won the game!");
            Console.WriteLine("=============================================");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=============================================");
            Console.WriteLine($"Game Over! The correct word was: {selectedWord}");
            Console.WriteLine("Better luck next time.");
            Console.WriteLine("=============================================");
            Console.ResetColor();
        }

        DisplayFooter();
    }

    static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║          WELCOME TO HANGMAN!         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void DisplayFooter()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Thank you for playing Hangman!");
        Console.WriteLine("Come back soon for another challenge.");
        Console.ResetColor();
    }

    // Validates the player's input, ensuring it's a single letter
    static char ValidateLetter()
    {
        char value;
        while (!char.TryParse(Console.ReadLine(), out value) ||
               !(value >= 'a' && value <= 'z') && !(value >= 'A' && value <= 'Z'))
        {
            PrintError("Invalid input. Please enter a letter.\nTry again: ");
        }
        return char.ToLower(value);
    }

    static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
