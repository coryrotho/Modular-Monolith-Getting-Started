using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    internal static readonly Guid Book1Guid = new Guid("0f5c53d2-dd5d-48aa-aa90-262326771174");
    internal static readonly Guid Book2Guid = new Guid("03c03ded-592a-4881-ac36-649aaac1fdef");
    internal static readonly Guid Book3Guid = new Guid("e49ed99a-849b-4c6a-8b8f-2514ac22675a");
        
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(p => p.Author)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasData(GetSampleBookData());
    }
    private IEnumerable<Book> GetSampleBookData()
    {
        var cory = "Cory Rothenberger";
        yield return new Book(Book1Guid, "Book 1", cory, 10.99m);
        yield return new Book(Book2Guid, "Book 2", cory, 11.49m);
        yield return new Book(Book3Guid, "Book 3", cory, 12.99m);
    }
}

