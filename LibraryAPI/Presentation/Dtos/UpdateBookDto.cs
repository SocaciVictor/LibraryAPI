namespace LibraryAPI.Presentation.Dtos
{
    public record UpdateBookDto(
        string Title = default!,
        string Author = default!,
        int Quantity = default!,
        DateTime PublishedDate = default!
    );
}
