using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMgmtSystem.DAL.Model
{
    /// <summary>Contains list of Book Categories.</summary>
    public class BookCategories
    {
        public BookCategories()
        {
            Shelves = new HashSet<Shelves>();

        }
        /// <summary>Gets or sets the identifier.</summary>
        public long Id { get; set; }
        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }
        /// <summary>Gets or sets the list of Shelves.</summary>
        public virtual ICollection<Shelves> Shelves { get; set; }


    }
}
