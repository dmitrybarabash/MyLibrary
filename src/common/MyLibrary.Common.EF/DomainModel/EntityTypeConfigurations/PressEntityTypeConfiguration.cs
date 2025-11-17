using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.EF.DomainModel.EntityTypeConfigurations;

internal class PressEntityTypeConfiguration : IEntityTypeConfiguration<Press>
{
    public void Configure(EntityTypeBuilder<Press> builder)
    {
        // Устанавливаем автоинкремент для первичного ключа
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        // Устанавливаем длину строкового поля и характеристику NOT NULL
        builder.Property(p => p.Name).HasMaxLength(50);

        builder.Property(p => p.Name).IsRequired();

        // Устанавливаем ссылочную целостность для связи "один-ко-многим"
        // между таблицами Presses и Books и внешний ключ в таблице Books
        builder
            .HasMany(p => p.Books)
            .WithOne(b => b.Press)
            .HasForeignKey(b => b.PressFk)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
