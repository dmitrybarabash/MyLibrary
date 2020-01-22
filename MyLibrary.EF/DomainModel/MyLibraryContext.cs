using System.Data.Entity;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.EF.DomainModel
{
    public class MyLibraryContext : DbContext
    {
        public MyLibraryContext() : base("MyLibrary") { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Press> Presses { get; set; }
        public DbSet<Book> Books { get; set; }

        // ѕри помощи Fluent API описываем те детали модели,
        // которые выход€т за дефолтные установки Entity Framework
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            // Authors
            //

            // ”станавливаем длину строкового пол€ и характеристику NOT NULL
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Name).IsRequired();
            // ”станавливаем ссылочную целостность дл€ св€зи "один-ко-многим"
            // между таблицами Authors и Books и внешний ключ в таблице Books
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithRequired(b => b.Author)
                .HasForeignKey(b => b.AuthorFk);

            //
            // Presses
            //

            // ”станавливаем длину строкового пол€ и характеристику NOT NULL
            modelBuilder.Entity<Press>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Press>().Property(p => p.Name).IsRequired();
            // ”станавливаем ссылочную целостность дл€ св€зи "один-ко-многим"
            // между таблицами Presses и Books и внешний ключ в таблице Books
            modelBuilder.Entity<Press>()
                .HasMany(p => p.Books)
                .WithRequired(b => b.Press)
                .HasForeignKey(b => b.PressFk);

            //
            // Books
            //

            // ”станавливаем длину строкового пол€ и характеристику NOT NULL
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired();
            // ”казываем точность дл€ пол€ типа decimal
            modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
    }
}