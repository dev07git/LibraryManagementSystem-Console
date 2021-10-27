using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using LibraryMgmtSystem.DAL.Model;

namespace LibraryMgmtSystem.Service.Interfaces
{
   public interface IBookService
    {
        Task<string> Create(string name, string shelfName, string bookCategoryName);
        Task<Books> Validate(string name, string shelfName, string bookCategoryName);
        Task<List<Books>> GetAll();
        Task<List<Books>> Get(string name, string shelfName, string bookCategoryName);
        Task<IList<Contracts.LibrarySearchResultViewModel>> AdvanceVehicleSearch(string searchText);

    }
}
