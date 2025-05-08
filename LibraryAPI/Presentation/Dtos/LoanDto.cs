using System.Text.Json.Serialization;

namespace LibraryAPI.Presentation.Dtos
{
    public record LoanDto(
       int Id = default!,
       int BookId = default!,
       int UserId = default!,
       DateTime LoanedAt = default!,
       DateTime? ReturnedAt = default!
   );
}
