using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel
{
    public class Press
    {
        public long Id { get; set; }
        public string Name { get; set; }

        // Используется только в Entity Framework
        public ICollection<Book> Books { get; set; }   // Книги данного издательства
    }
}