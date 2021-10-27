using LibraryMgmtSystem.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmtSystem.Service.Interfaces
{
    public interface IShelvesService
    {
        Task<string> Create(string name, string bookCategoryName);
        Task<List<Shelves>> GetAll();
        Task<List<Shelves>> Get(string name, string bookCategoryName);
    }
}
