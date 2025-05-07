using System.Text.Json.Serialization;

namespace LibraryAPI.Presentation.Dtos
{
    public record CreateBookDto(
          string Title = default!,
          int AuthorId = default!,
          int Quantity = default!,
          DateTime PublishedDate = default!
      );
}
