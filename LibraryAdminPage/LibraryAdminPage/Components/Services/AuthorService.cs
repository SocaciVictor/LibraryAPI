using LibraryAdminPage.Components.Interfaces;
using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _http;
        public AuthorService(HttpClient http) => _http = http;

        public Task<List<AuthorDto>> GetAllAsync()
            => _http.GetFromJsonAsync<List<AuthorDto>>("api/authors")!;

        public Task<AuthorDto?> GetByIdAsync(int id)
            => _http.GetFromJsonAsync<AuthorDto?>($"api/authors/{id}");

        public async Task<AuthorDto?> CreateAsync(CreateAuthorDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/authors", dto);
            return resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<AuthorDto>()
                : null;
        }

        public async Task<bool> UpdateAsync(int id, UpdateAuthorDto dto)
        {
            var resp = await _http.PutAsJsonAsync($"api/authors/{id}", dto);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/authors/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
