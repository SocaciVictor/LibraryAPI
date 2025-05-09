using LibraryAdminPage.Components.Interfaces;
using LibraryAdminPage.Components.Models;

namespace LibraryAdminPage.Components.Services
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient _http;
        public LoanService(HttpClient http) => _http = http;

        public async Task<LoanDto?> BorrowAsync(LoanRequestDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/loans", dto);
            return resp.IsSuccessStatusCode
                ? await resp.Content.ReadFromJsonAsync<LoanDto>()
                : null;
        }

        public async Task<bool> ReturnAsync(int loanId)
        {
            var resp = await _http.PostAsync($"api/loans/{loanId}/return", null);
            return resp.IsSuccessStatusCode;
        }

        public Task<LoanDto?> GetByIdAsync(int id)
            => _http.GetFromJsonAsync<LoanDto?>($"api/loans/{id}");
    }
}
