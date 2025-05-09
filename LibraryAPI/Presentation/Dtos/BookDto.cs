namespace LibraryAPI.Presentation.Dtos
{
    public record BookDto(
        int Id = default,
        string Title = "",
        int AuthorId = default,
        int Quantity = default,
        DateTime? PublishedDate = null
    );
}
