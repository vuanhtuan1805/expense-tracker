using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Application.DTOs
{
    public class RegisterResultDTO
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}