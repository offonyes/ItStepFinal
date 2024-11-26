using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BookManagement
{
    // 1. Book Class
    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Year: {Year}";
        }
    }

    // 2. BookManager Class
    class BookManager
    {
        private const string FilePath = "..\\..\\..\\books.json";
        private List<Book> books = new List<Book>();

        public BookManager()
        {
            LoadBooks();
        }

        // Add a new book and save the updated list
        public void AddBook(Book book)
        {
            books.Add(book);
            SaveBooks();
        }

        // Get all books
        public List<Book> GetBooks()
        {
            return books;
        }

        // Search for books by title
        public List<Book> SearchBooks(string title)
        {
            return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Save books to the JSON file
        private void SaveBooks()
        {
            try
            {
                string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving books: {ex.Message}");
            }
        }

        // Load books from the JSON file
        private void LoadBooks()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading books: {ex.Message}");
            }
        }
    }

    // 3. BookUI Class (Handles all user interactions)
    class BookUI
    {
        private BookManager bookManager;

        public BookUI(BookManager manager)
        {
            bookManager = manager;
        }

        public void Start()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("--- Book Management System ---");
                Console.WriteLine("1. Add a New Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search for a Book");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AddBook();
                        break;

                    case "2":
                        Console.Clear();
                        ShowBooks();
                        break;

                    case "3":
                        Console.Clear();
                        SearchBooks();
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Adding new book
        private void AddBook()
        {
            Console.Write("Enter book title: ");
            string title = ValidateNonEmptyInput();

            Console.Write("Enter book author: ");
            string author = ValidateNonEmptyInput();

            Console.Write("Enter year of publication: ");
            int year = ValidateYearInput();

            Book newBook = new Book(title, author, year);
            bookManager.AddBook(newBook);
            Console.WriteLine("Book added successfully.");
        }

        //Show all books
        private void ShowBooks()
        {
            var books = bookManager.GetBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\nList of Books:");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

        }

        // Search book by title
        private void SearchBooks()
        {
            Console.Write("Enter title to search: ");
            string title = ValidateNonEmptyInput();

            var foundBooks = bookManager.SearchBooks(title);
            if (foundBooks.Count == 0)
            {
                Console.WriteLine($"No books found with title containing: {title}");
                return;
            }

            Console.WriteLine("\nSearch Results:");
            foreach (var book in foundBooks)
            {
                Console.WriteLine(book);
            }

        }

        // Checks input
        private string ValidateNonEmptyInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("Invalid input. Please enter a valid value.");
            }
        }

        // Check year
        private int ValidateYearInput()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int year) && year > 0)
                {
                    return year;
                }
                Console.WriteLine("Invalid input. Please enter a valid year.");
            }
        }
    }

    // 4. Main Program
    class Program
    {
        static void Main(string[] args)
        {
            BookManager bookManager = new BookManager();
            BookUI bookUI = new BookUI(bookManager);
            bookUI.Start();
        }
    }
}
