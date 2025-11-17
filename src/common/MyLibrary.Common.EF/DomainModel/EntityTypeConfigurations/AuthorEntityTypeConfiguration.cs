using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.EF.DomainModel.EntityTypeConfigurations;

internal class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Устанавливаем автоинкремент для первичного ключа
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        // Устанавливаем длину строкового поля и характеристику NOT NULL
        builder.Property(a => a.Name).HasMaxLength(50);

        builder.Property(a => a.Name).IsRequired();

        // Устанавливаем ссылочную целостность для связи "один-ко-многим"
        // между таблицами Authors и Books и внешний ключ в таблице Books
        builder
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorFk)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
