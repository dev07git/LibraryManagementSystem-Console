using LibraryMgmtSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryMgmtSystem.DAL.Model;
using System.Linq;

namespace LibraryMgmtSystem.Service
{
   public class BookService :IBookService
    {
        private readonly LibrarySystemDbContext _librarySystemDbContext;
        private readonly IBookCategoryService _bookCategoryService;

        public BookService(LibrarySystemDbContext librarySystemDbContext, IBookCategoryService bookCategoryService)
        {
            _librarySystemDbContext = librarySystemDbContext;
            this._bookCategoryService = bookCategoryService;
        }
        /// <summary>creates a Book.</summary>
        /// <param name="name">Book name.</param>
        /// <param name="shelfName">shelfName.</param>
        /// <param name="bookCategoryName">bookCategoryName.</param>
        /// <returns>string response message.</returns>
        public async Task<string> Create(string name, string shelfName, string bookCategoryName)
        {
            Books book = await Validate(name, shelfName, bookCategoryName);
            if (book != null)
                return string.Format(Service.Constants.RecordAlreadyExist, Service.Constants.lblBook);

            // NOTE : validate book category
            BookCategories bookCategories = await _bookCategoryService.Get(bookCategoryName);
            if (bookCategories == null)
                return string.Format(Service.Constants.InvalidRecord, Service.Constants.lblBookCategory);

            // NOTE : validate Shelf under book category
            Shelves shelf = bookCategories.Shelves.Where(x => x.Name.Equals(shelfName,StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            
            if (shelf == null)
                return string.Format(Service.Constants.InvalidRecord, Service.Constants.lblShelf);

            book = new Books() { Name = name, ShelfId= shelf.Id };
            await _librarySystemDbContext.Books.AddAsync(book);
            await _librarySystemDbContext.SaveChangesAsync();
            return string.Format(Service.Constants.RecordCreatedSuccessfully, Service.Constants.lblBook);

        }
        /// <summary>Validate a Book in database.</summary>
        /// <param name="name">Book name.</param>
        /// <param name="shelfName">shelfName.</param>
        /// <param name="bookCategoryName">bookCategoryName.</param>
        /// <returns>string response message.</returns>
        public async Task<Books> Validate(string name,string shelfName, string bookCategoryName)
        {
            return _librarySystemDbContext.Books.FirstOrDefault(x => 
            x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) 
            && x.Shelf.Name.Equals(shelfName, StringComparison.OrdinalIgnoreCase)
            && x.Shelf.BookCategory.Name.Equals(bookCategoryName, StringComparison.OrdinalIgnoreCase));

        }
        /// <summary>Get all Books from database.</summary>
        /// <returns>Collection of Books.</returns>
        public async Task<List<Books>> GetAll()
        {
            return _librarySystemDbContext.Books.ToList();

        }
        /// <summary>Find Books in database.</summary>
        /// <param name="name">Book name.</param>
        /// <param name="shelfName">shelfName.</param>
        /// <param name="bookCategoryName">bookCategoryName.</param>
        /// <returns>Collection of Books.</returns>
        public async Task<List<Books>> Get(string name, string shelfName, string bookCategoryName)
        {

            IQueryable<Books> query = _librarySystemDbContext.Books.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(shelfName) && !string.IsNullOrEmpty(bookCategoryName))
                query = query.Where(x => x.Shelf.Name.Equals(shelfName, StringComparison.OrdinalIgnoreCase) && x.Shelf.BookCategory.Name.Equals(bookCategoryName, StringComparison.OrdinalIgnoreCase));

            else if (!string.IsNullOrEmpty(shelfName) && string.IsNullOrEmpty(bookCategoryName))
                query = query.Where(x => x.Shelf.Name.Equals(shelfName, StringComparison.OrdinalIgnoreCase));

            else if (string.IsNullOrEmpty(shelfName) && !string.IsNullOrEmpty(bookCategoryName))
                query = query.Where(x => x.Shelf.BookCategory.Name.Equals(bookCategoryName, StringComparison.OrdinalIgnoreCase));

            return await Task.Run(() => query.ToList());
        }

        /// <summary>Find BookCategories or Shelves or Books in database.</summary>
        /// <param name="searchText">search text.</param>
        /// <returns>Collection of LibrarySearchResultViewModel.</returns>
        public async Task<IList<Contracts.LibrarySearchResultViewModel>> AdvanceVehicleSearch(string searchText)
        {
            IQueryable<Books> booksQuery = _librarySystemDbContext.Books;
            IList<Contracts.LibrarySearchResultViewModel> books=null;
            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.ToLower();
                string[] searchArray = searchText.Split(' ');

                foreach (var searchItem in searchArray)
                {
                    string text = searchItem;
                    booksQuery = booksQuery.Where(w =>
                    w.Name.ToLower().Contains(text)
                   || w.Shelf.Name.ToLower().Contains(text)
                   || w.Shelf.BookCategory.Name.ToLower().Contains(text)
                    );
                }
                books =await Task.Run(()=> booksQuery.Select(s => new Contracts.LibrarySearchResultViewModel()
                {
                    BookCategoryName = s.Shelf.BookCategory.Name,
                    ShelfName = s.Shelf.Name,
                    BookName = s.Name
                }).Distinct().ToList());
            }


            return books;
        }

        

    }
}
