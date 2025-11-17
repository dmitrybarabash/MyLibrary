using Microsoft.EntityFrameworkCore;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.EF.DomainModel;

public class MyLibraryContext : DbContext
{
    public MyLibraryContext(DbContextOptions<MyLibraryContext> options) : base(options)
    {
        // Если БД нет, то создаем ее
        Database.EnsureCreated();
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Press> Presses { get; set; }
    public DbSet<Book> Books { get; set; }

    // При помощи Fluent API описываем те детали модели,
    // которые выходят за дефолтные установки Entity Framework
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Задаем правила сопоставления классов модели с таблицами БД с помощью Fluent API.
        // Чтобы не загромождать метод OnModelCreating, используем раздельные реализации
        // интерфейса IEntityTypeConfiguration<Entity> для каждой таблицы.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyLibraryContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}