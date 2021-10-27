using LibraryMgmtSystem.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmtSystem.Service.Interfaces
{
    public interface IBookCategoryService
    {
        Task<int> SeedingData();
        Task<string> Create(string name);
        Task<List<BookCategories>> GetAll();
        Task<BookCategories> Get(string name);
    }
}
