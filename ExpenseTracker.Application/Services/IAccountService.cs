using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Application.Services
{
    public interface IAccountService
    {
        // interface methods
        Task<RegisterResultDTO> RegisterAsync(AccountRequest request);
    }
}