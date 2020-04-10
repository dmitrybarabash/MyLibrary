using System.ComponentModel.DataAnnotations.Schema;
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

        // ��� ������ Fluent API ��������� �� ������ ������,
        // ������� ������� �� ��������� ��������� Entity Framework
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            // Authors
            //

            // ������������� ������������� ��� ���������� �����
            modelBuilder.Entity<Author>().Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // ������������� ����� ���������� ���� � �������������� NOT NULL
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Name).IsRequired();
            // ������������� ��������� ����������� ��� ����� "����-��-������"
            // ����� ��������� Authors � Books � ������� ���� � ������� Books
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithRequired(b => b.Author)
                .HasForeignKey(b => b.AuthorFk)
                .WillCascadeOnDelete();

            //
            // Presses
            //

            // ������������� ������������� ��� ���������� �����
            modelBuilder.Entity<Press>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // ������������� ����� ���������� ���� � �������������� NOT NULL
            modelBuilder.Entity<Press>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Press>().Property(p => p.Name).IsRequired();
            // ������������� ��������� ����������� ��� ����� "����-��-������"
            // ����� ��������� Presses � Books � ������� ���� � ������� Books
            modelBuilder.Entity<Press>()
                .HasMany(p => p.Books)
                .WithRequired(b => b.Press)
                .HasForeignKey(b => b.PressFk)
                .WillCascadeOnDelete();

            //
            // Books
            //

            // ������������� ������������� ��� ���������� �����
            modelBuilder.Entity<Book>().Property(b => b.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // ������������� ����� ���������� ���� � �������������� NOT NULL
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(b => b.Name).IsRequired();
            // ��������� �������� ��� ���� ���� decimal
            modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
    }
}