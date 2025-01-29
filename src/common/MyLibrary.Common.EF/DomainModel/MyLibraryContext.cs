using Microsoft.EntityFrameworkCore;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.EF.DomainModel
{
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
            //
            // Authors
            //

            // Устанавливаем автоинкремент для первичного ключа
            modelBuilder.Entity<Author>().Property(a => a.Id).ValueGeneratedOnAdd();
            // Устанавливаем длину строкового поля и характеристику NOT NULL
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Name).IsRequired();
            // Устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Authors и Books и внешний ключ в таблице Books
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorFk)
                .OnDelete(DeleteBehavior.Cascade);

            //
            // Presses
            //

            // Устанавливаем автоинкремент для первичного ключа
            modelBuilder.Entity<Press>().Property(p => p.Id).ValueGeneratedOnAdd();
            // Устанавливаем длину строкового поля и характеристику NOT NULL
            modelBuilder.Entity<Press>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Press>().Property(p => p.Name).IsRequired();
            // Устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Presses и Books и внешний ключ в таблице Books
            modelBuilder.Entity<Press>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Press)
                .HasForeignKey(b => b.PressFk)
                .OnDelete(DeleteBehavior.Cascade);

            //
            // Books
            //

            // Устанавливаем автоинкремент для первичного ключа
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();
            // Устанавливаем длину строкового поля и характеристику NOT NULL
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired();
            // Указываем точность для поля типа decimal
            modelBuilder.Entity<Book>().Property(b => b.Price).HasColumnType("decimal(18,2)");


            base.OnModelCreating(modelBuilder);
        }
    }
}