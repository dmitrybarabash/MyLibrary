using Microsoft.EntityFrameworkCore;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.EF.DomainModel
{
    public class MyLibraryContext : DbContext
    {
        public MyLibraryContext(DbContextOptions<MyLibraryContext> options) : base(options)
        {
            // ���� �� ���, �� ������� ��
            Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Press> Presses { get; set; }
        public DbSet<Book> Books { get; set; }

        // ��� ������ Fluent API ��������� �� ������ ������,
        // ������� ������� �� ��������� ��������� Entity Framework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            // Authors
            //

            // ������������� ������������� ��� ���������� �����
            modelBuilder.Entity<Author>().Property(a => a.Id).ValueGeneratedOnAdd();
            // ������������� ����� ���������� ���� � �������������� NOT NULL
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Name).IsRequired();
            // ������������� ��������� ����������� ��� ����� "����-��-������"
            // ����� ��������� Authors � Books � ������� ���� � ������� Books
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorFk)
                .OnDelete(DeleteBehavior.Cascade);

            //
            // Presses
            //

            // ������������� ������������� ��� ���������� �����
            modelBuilder.Entity<Press>().Property(p => p.Id).ValueGeneratedOnAdd();
            // ������������� ����� ���������� ���� � �������������� NOT NULL
            modelBuilder.Entity<Press>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Press>().Property(p => p.Name).IsRequired();
            // ������������� ��������� ����������� ��� ����� "����-��-������"
            // ����� ��������� Presses � Books � ������� ���� � ������� Books
            modelBuilder.Entity<Press>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Press)
                .HasForeignKey(b => b.PressFk)
                .OnDelete(DeleteBehavior.Cascade);

            //
            // Books
            //

            // ������������� ������������� ��� ���������� �����
            modelBuilder.Entity<Book>().Property(b => b.Id).ValueGeneratedOnAdd();
            // ������������� ����� ���������� ���� � �������������� NOT NULL
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired();
            // ��������� �������� ��� ���� ���� decimal
            modelBuilder.Entity<Book>().Property(b => b.Price).HasColumnType("decimal(18,2)");


            base.OnModelCreating(modelBuilder);
        }
    }
}