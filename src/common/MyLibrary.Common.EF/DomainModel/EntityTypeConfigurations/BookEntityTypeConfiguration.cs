using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.EF.DomainModel.EntityTypeConfigurations;

internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Устанавливаем автоинкремент для первичного ключа
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        // Устанавливаем длину строкового поля и характеристику NOT NULL
        builder.Property(b => b.Name).HasMaxLength(100);

        builder.Property(b => b.Name).IsRequired();

        // Указываем точность для поля типа decimal
        builder.Property(b => b.Price).HasColumnType("decimal(18,2)");
    }
}
