using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMgmtSystem.DAL.Model
{
    /// <summary>Contains list of Books.</summary>
    public class Books
    {

        /// <summary>Gets or sets the identifier.</summary>
        public long Id { get; set; }
        /// <summary>Gets or sets the name.</summary>
        public string Name { get; set; }
        public long ShelfId { get; set; }
        /// <summary>Gets or sets the list of Shelf.</summary>
        public virtual Shelves Shelf { get; set; }

    }
}
