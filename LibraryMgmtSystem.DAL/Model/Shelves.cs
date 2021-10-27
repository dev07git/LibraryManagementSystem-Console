using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMgmtSystem.DAL.Model
{
    /// <summary>Contains list of Shelves.</summary>
    public class Shelves
    {
        public Shelves()
        {
            Books = new HashSet<Books>();

        }
        /// <summary>Gets or sets the identifier.</summary>
        public long Id { get; set; }
        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }
        public long BookCategoryId { get; set; }
        /// <summary>Gets or sets the list of BookCategory.</summary>
        public virtual BookCategories BookCategory { get; set; }
        /// <summary>Gets or sets the list of Books.</summary>
        public virtual ICollection<Books> Books { get; set; }


    }
}
