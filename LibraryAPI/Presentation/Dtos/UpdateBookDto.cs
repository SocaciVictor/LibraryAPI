namespace LibraryAPI.Presentation.Dtos
{
    public record UpdateBookDto(
        string Title = default!,
        int AuthorId = default!,
        int Quantity = default!,
        DateTime PublishedDate = default!
    );
}
