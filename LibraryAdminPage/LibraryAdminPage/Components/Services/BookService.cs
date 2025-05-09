using LibraryAdminPage.Components.Interfaces;
using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _http;
        public BookService(HttpClient http) => _http = http;

        public Task<List<BookDto>> GetAllAsync()
            => _http.GetFromJsonAsync<List<BookDto>>("api/books")!;

        public Task<BookDto?> GetByIdAsync(int id)
            => _http.GetFromJsonAsync<BookDto?>($"api/books/{id}");

        public async Task<BookDto?> CreateAsync(CreateBookDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/books", dto);
            return resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<BookDto>()
                : null;
        }

        public async Task<bool> UpdateAsync(int id, UpdateBookDto dto)
        {
            var resp = await _http.PutAsJsonAsync($"api/books/{id}", dto);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/books/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
