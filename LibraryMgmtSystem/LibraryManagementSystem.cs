using LibraryMgmtSystem.DAL.Model;
using LibraryMgmtSystem.Service.Interfaces;
using LibraryMgmtSystem.Service;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LibraryMgmtSystem
{
  public class LibraryManagementSystem: ILibraryManagementSystem
    {
        private readonly IBookCategoryService _bookCategoryService;
        private readonly IShelvesService _shelvesService;
        private readonly IBookService _bookService;
        public LibraryManagementSystem(IBookCategoryService bookCategoryService, IShelvesService shelvesService, IBookService bookService)
        {
            _bookCategoryService = bookCategoryService;
            _shelvesService = shelvesService;
            _bookService = bookService;
        }


        public async Task<int> SeedingData()
        { 
            return await _bookCategoryService.SeedingData();

        }
        public async Task<string> CreateBookCategory(string name)
        {
            return await  _bookCategoryService.Create(name);
        }

        public async Task<List<BookCategories>> GetAllBookCategories()
        {
            return await _bookCategoryService.GetAll();
        }

        public async Task<BookCategories> FindBookCategory(string name)
        {
            return await _bookCategoryService.Get(name);
        }


        public async Task<string> CreateShelf(string name,string bookCategoryName)
        {
            return await _shelvesService.Create(name, bookCategoryName);
        }

        public async Task<List<Shelves>> GetAllShelves()
        {
            return await _shelvesService.GetAll();
        }

        public async Task<List<Shelves>> FindShelf(string name, string bookCategoryName)
        {
            return await _shelvesService.Get(name, bookCategoryName);
        }

        public async Task<string> CreateBook(string name,string shelfName, string bookCategoryName)
        {
            return await _bookService.Create(name, shelfName, bookCategoryName);
        }

        public async Task<List<Books>> GetAllBooks()
        {
            return await _bookService.GetAll();
        }

        public async Task<List<Books>> FindBooks(string name, string shelfName, string bookCategoryName)
        {
            return await _bookService.Get(name,shelfName, bookCategoryName);
        }

        public async Task<IList<Service.Contracts.LibrarySearchResultViewModel>> AdvanceVehicleSearch(string searchText)
        {
            return await _bookService.AdvanceVehicleSearch(searchText);

        }
    }
}
