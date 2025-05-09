using LibraryAdminPage.Components.Interfaces;
using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        public UserService(HttpClient http) => _http = http;

        public Task<List<UserDto>> GetAllAsync()
            => _http.GetFromJsonAsync<List<UserDto>>($"api/users")!;

        public Task<UserDto?> GetByIdAsync(int id)
            => _http.GetFromJsonAsync<UserDto?>($"api/users/{id}");

        public async Task<UserDto?> CreateAsync(CreateUserDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/users", dto);
            return resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<UserDto>()
                : null;
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var resp = await _http.PutAsJsonAsync($"api/users/{id}", dto);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/users/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
