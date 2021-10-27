using LibraryMgmtSystem.DAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LibraryMgmtSystem
{
    public interface ILibraryManagementSystem
    {
        Task<int> SeedingData();

        #region Book Category service Methods
        Task<string> CreateBookCategory(string name);
        Task<List<BookCategories>> GetAllBookCategories();
        Task<BookCategories> FindBookCategory(string name);
        #endregion


        #region Shelf service Methods
        Task<string> CreateShelf(string name, string bookCategoryName);
        Task<List<Shelves>> GetAllShelves();
        Task<List<Shelves>> FindShelf(string name, string bookCategoryName);
        #endregion

        #region Book service methods
        Task<string> CreateBook(string name, string shelfName, string bookCategoryName);
        Task<List<Books>> GetAllBooks();
        Task<List<Books>> FindBooks(string name, string shelfName, string bookCategoryName);
        #endregion

        #region Searching method
        Task<IList<Service.Contracts.LibrarySearchResultViewModel>> AdvanceVehicleSearch(string searchText);
        #endregion
    }
}