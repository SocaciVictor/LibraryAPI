namespace LibraryAPI.Presentation.Dtos
{
    public record LoanRequestDto(
        int BookId = default!,
        int UserId = default!
    );
}
