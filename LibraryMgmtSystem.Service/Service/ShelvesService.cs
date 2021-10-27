using LibraryMgmtSystem.DAL.Model;
using LibraryMgmtSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmtSystem.Service
{
  public class ShelvesService: IShelvesService
    {
        private readonly LibrarySystemDbContext _librarySystemDbContext;
        private readonly IBookCategoryService _bookCategoryService;

        public ShelvesService(LibrarySystemDbContext librarySystemDbContext,IBookCategoryService bookCategoryService)
        {
            _librarySystemDbContext = librarySystemDbContext;
            this._bookCategoryService = bookCategoryService;
        }
        /// <summary>creates a shelf.</summary>
        /// <param name="name">shelfName.</param>
        /// <param name="bookCategoryName">bookCategoryName.</param>
        /// <returns>string response message.</returns>
        public async Task<string> Create(string name,string bookCategoryName)
        {
            Shelves shelf = await Validate(name,bookCategoryName);
            if (shelf != null)
                return string.Format(Service.Constants.RecordAlreadyExist, Service.Constants.lblShelf);

            BookCategories bookCategories= await _bookCategoryService.Get(bookCategoryName);
            if(bookCategories==null)
                return string.Format(Service.Constants.InvalidRecord, Service.Constants.lblBookCategory);
            
            shelf = new Shelves() { Name = name, BookCategoryId=bookCategories.Id};
            await _librarySystemDbContext.Shelves.AddAsync(shelf);
            await _librarySystemDbContext.SaveChangesAsync();
            return string.Format(Service.Constants.RecordCreatedSuccessfully, Service.Constants.lblShelf);

        }
        /// <summary>Get all shelves from database.</summary>
        /// <returns>Collection of Shelves.</returns>
        public async Task<List<Shelves>> GetAll()
        {
            return _librarySystemDbContext.Shelves.ToList();

        }
        /// <summary>Validate a shelf in database.</summary>
        /// <param name="name">shelfName.</param>
        /// <param name="bookCategoryName">bookCategoryName.</param>
        /// <returns>Shelves object.</returns>
        public async Task<Shelves> Validate(string name, string bookCategoryName)
        {
            return _librarySystemDbContext.Shelves.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && x.BookCategory.Name.Equals(bookCategoryName, StringComparison.OrdinalIgnoreCase));

        }
        /// <summary>Find shelves in database.</summary>
        /// <param name="name">shelfName.</param>
        /// <param name="bookCategoryName">bookCategoryName.</param>
        /// <returns>collection of Shelves.</returns>
        public async Task<List<Shelves>> Get(string name, string bookCategoryName)
        {

            IQueryable<Shelves> query = _librarySystemDbContext.Shelves.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(bookCategoryName))
                query = query.Where(x => x.BookCategory.Name.Equals(bookCategoryName, StringComparison.OrdinalIgnoreCase));

            return await Task.Run(() => query.ToList());
        }
    }
}
