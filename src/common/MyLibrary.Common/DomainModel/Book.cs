namespace MyLibrary.Common.DomainModel;

public class Book
{
    public long Id { get; set; }
    public long AuthorFk { get; set; }
    public string Name { get; set; }
    public int Pages { get; set; }
    public decimal Price { get; set; }
    public long PressFk { get; set; }

    // Используются только в Entity Framework
    public virtual Author Author { get; set; }   // Навигационное свойство для AuthorFk
    public virtual Press Press { get; set; }     // Навигационное свойство для PressFk
}
