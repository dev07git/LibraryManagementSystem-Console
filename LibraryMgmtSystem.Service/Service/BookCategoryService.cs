using LibraryMgmtSystem.DAL.Model;
using LibraryMgmtSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmtSystem.Service
{
   public class BookCategoryService:IBookCategoryService
    {
        private readonly LibrarySystemDbContext _librarySystemDbContext;
        
        public BookCategoryService(LibrarySystemDbContext librarySystemDbContext)
        {
            _librarySystemDbContext = librarySystemDbContext;
        }
        /// <summary>creates a Book Category.</summary>
        /// <param name="name">Book Category name.</param>
        /// <returns>string response message.</returns>
        public async Task<string> Create(string name)
        {
            BookCategories bookCategories = await Validate(name);
            if (bookCategories != null)
                return string.Format(Service.Constants.RecordAlreadyExist, Service.Constants.lblBookCategory);

            var bookCategory= new BookCategories() { Name = name };
            try
            {
                await _librarySystemDbContext.BookCategories.AddAsync(bookCategory);
                await _librarySystemDbContext.SaveChangesAsync();
                return string.Format(Service.Constants.RecordCreatedSuccessfully, Service.Constants.lblBookCategory);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            
        }
        /// <summary>Get all BookCategories from database.</summary>
        /// <returns>Collection of BookCategories.</returns>
        public async Task<List<BookCategories>> GetAll()
        {
            return _librarySystemDbContext.BookCategories.ToList();

        }
        /// <summary>Find Book Category in database.</summary>
        /// <param name="name">Book Category name.</param>
        /// <returns>Collection of Books.</returns>
        public async Task<BookCategories> Get(string name)
        {
            return _librarySystemDbContext.BookCategories.FirstOrDefault(x=>x.Name.Equals(name,StringComparison.OrdinalIgnoreCase));

        }
        /// <summary>Validate a Book Category in database.</summary>
        /// <param name="name">Category name.</param>
        /// <returns>string response message.</returns>
        public async Task<BookCategories> Validate(string name)
        {

            IQueryable<BookCategories> query = _librarySystemDbContext.BookCategories.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
           

           return  await Task.Run(() => query.FirstOrDefault());
        }

        /// <summary>Seeding sample data in database.</summary>
        /// <returns>int.</returns>
        public async Task<int> SeedingData()
        {
            var bookCategories = new List<BookCategories>(){
                new BookCategories() {  Name = "Information Technology & Networking",
                Shelves=new List<Shelves>(){
                new Shelves(){
                   
                    Name="S1",
                    Books=new List<Books>(){
                        new Books(){ Name="Computer Networking: A Top-Down Approach (6th Edition)"},
                        new Books(){ Name="Computer Networking: A Top-Down Approach (5th Edition)"},
                    }
                },
                new Shelves(){
                  
                    Name="S2",
                    Books=new List<Books>(){
                        new Books(){ Name="Routing TCP/IP, Volume 1 (2nd Edition)"},
                        new Books(){ Name="Routing TCP/IP, Volume II (2nd Edition)"},
                    }
                },
                new Shelves(){
                   
                    Name="S4",
                    Books=new List<Books>(){
                        new Books(){ Name="Networking All-in-One For Dummies"},
                        new Books(){ Name="Cisco Networking All-in-One For Dummies"},
                    }
                }
                }
                },
               new BookCategories() {  Name = "Programing Languages",
                Shelves=new List<Shelves>(){
                new Shelves(){
                    
                    Name="S1",
                    Books=new List<Books>(){
                        new Books(){ Name="Programming in ANSI C - Balagurusamy"},
                        new Books(){ Name="Object-Oriented Programming with C++ | 7th Edition"},
                    }
                },
                new Shelves(){
                    
                    Name="S2",
                    Books=new List<Books>(){
                        new Books(){ Name="The C# Player's Guide"},
                        new Books(){ Name="Core Java An Integrated Approach (Black Book)"},
                    }
                },
                new Shelves(){
                   
                    Name="S3",
                    Books=new List<Books>(){
                        new Books(){ Name="Head First Java"},
                        new Books(){ Name="Automating Boring Stuff with Python"},
                         new Books(){ Name="Learn Python 3 The Hard Way, by Zed A. Shaw"},
                    }
                }
                } }
            };



            await _librarySystemDbContext.BookCategories.AddRangeAsync(bookCategories);
            return await _librarySystemDbContext.SaveChangesAsync();
        }
    }
}
