using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraryMgmtSystem.DAL.Model;
using LibraryMgmtSystem.Service.Interfaces;
using LibraryMgmtSystem.Service;
using System.Collections.Generic;

namespace LibraryMgmtSystem
{
    class Program
    {
        public ILibraryManagementSystem _libraryManagementSystem;

        public Program(ILibraryManagementSystem libraryManagementSystem)
        {
            _libraryManagementSystem = libraryManagementSystem;
        }


        static void Main(string[] args)
        {
            // NOTE: Configuring DI
            var host = Host.CreateDefaultBuilder(args)
             .ConfigureServices((hostContext, services) =>
             {
                 services.AddDbContext<LibrarySystemDbContext>(context => context.UseInMemoryDatabase("LibrarySystemDb").EnableSensitiveDataLogging());
                 services.AddSingleton<IBookCategoryService, BookCategoryService>();
                 services.AddSingleton<IShelvesService, ShelvesService>();
                 services.AddSingleton<IBookService, BookService>();
                 services.AddSingleton<ILibraryManagementSystem, LibraryManagementSystem>();
                 services.AddSingleton<Program>();

             }).Build();
            // NOTE: Get Instance of Program class invoke Run method 
            host.Services.GetRequiredService<Program>().Run();




            Console.WriteLine("*****-----Thanks for using our Library Management System-----*****");

        }

        public void Run()
        {
            // NOTE: Adding some sample Data
            _libraryManagementSystem.SeedingData();
            Console.Clear();
            Console.WriteLine("\n\n*****-----Welcome To Library Management System-----*****");
            string bookCategoryName = null, shelfName = null, bookName = null;
            List<BookCategories> bookCategories;
            while (true)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("*****-----Please choose below option carefully-----*****");
                Console.WriteLine("Enter 1 to create new Book Category");
                Console.WriteLine("Enter 2 to See all Book Categories");
                Console.WriteLine("Enter 3 to create new Shelf");
                Console.WriteLine("Enter 4 to see all Shelves");
                Console.WriteLine("Enter 7 to create new Book");
                Console.WriteLine("Enter 8 to see all Book");
                Console.WriteLine("Enter 9 to search Book-Categories/Shelves/Books in Library");
                Console.WriteLine("Enter 0 to Exist");
                var intChoice = -1;
                Console.WriteLine("Enter your choice:");
                var choice = Console.ReadLine();
                int.TryParse(choice, out intChoice);
                switch (intChoice)
                {
                    case -1:
                        Console.WriteLine("Invalid selection, please try again with valid choice");
                        break;
                    case 1:
                        Console.WriteLine("\nEnter Category Book name:");
                        bookCategoryName = Console.ReadLine();
                        Console.WriteLine(_libraryManagementSystem.CreateBookCategory(bookCategoryName).Result);
                        break;

                    case 2:
                        bookCategories = _libraryManagementSystem.GetAllBookCategories().Result;
                        Console.WriteLine("**--Following are the available Book Categories**--");

                        foreach (var item in bookCategories)
                        {
                            Console.WriteLine($"Category#: {item.Id}-----Book Category: {item.Name}");
                        }
                        break;

                    case 3:
                        Console.WriteLine("\nEnter existing Book Category name:");
                        bookCategoryName = Console.ReadLine();
                        Console.WriteLine("\nEnter new Shelf name:");
                        shelfName = Console.ReadLine();
                        Console.WriteLine(_libraryManagementSystem.CreateShelf(shelfName, bookCategoryName).Result);
                        break;

                    case 4:
                        Console.WriteLine("**--Following are the available Shelves in the Library.**--");
                        bookCategories = _libraryManagementSystem.GetAllBookCategories().Result;

                        foreach (var bookCategory in bookCategories)
                        {
                            Console.WriteLine($"--Book Category: {bookCategory.Name}");
                            foreach (var shelf in bookCategory.Shelves)
                            {
                                Console.WriteLine($"\t-----Shelf#: {shelf.Id}-----Shelf: {shelf.Name}");
                            }

                        }
                        break;

                    case 7:
                        Console.WriteLine("\nEnter existing Book category name:");
                        bookCategoryName = Console.ReadLine();
                        Console.WriteLine("\nEnter existing Shelf name under above Book category:");
                        shelfName = Console.ReadLine();
                        Console.WriteLine("\nEnter Book name:");
                        bookName = Console.ReadLine();
                        Console.WriteLine(_libraryManagementSystem.CreateBook(bookName, shelfName, bookCategoryName).Result);
                        break;

                    case 8:
                        Console.WriteLine("**--Following are the available Books in the Library.**--");
                        bookCategories = _libraryManagementSystem.GetAllBookCategories().Result;

                        foreach (var bookCategory in bookCategories)
                        {
                            Console.WriteLine($"--Book Category: {bookCategory.Name}");
                            foreach (var shelf in bookCategory.Shelves)
                            {
                                Console.WriteLine($"\t-----Shelf#: {shelf.Id}-----Shelf: {shelf.Name}");
                                foreach (var book in shelf.Books)
                                {
                                    Console.WriteLine($"\t\t-----Book#: {book.Id}-----Book: {book.Name}");
                                }
                            }

                        }
                        break;
                    case 9:
                        Console.WriteLine("\n Type any thing to search for Book-Categories or Shelves or Books:");
                        var searchText= Console.ReadLine();
                        IList<Service.Contracts.LibrarySearchResultViewModel> librarySearchResultViewModel = _libraryManagementSystem.AdvanceVehicleSearch(searchText).Result;
                        foreach (var item in librarySearchResultViewModel)
                        {
                            Console.WriteLine($"Book Category: {item.BookCategoryName}\tShelf: {item.ShelfName}\tBook: {item.BookName}");

                        }
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid selection, please try again by entering option#");
                        break;
                }
            }

        }


      

    }
}
