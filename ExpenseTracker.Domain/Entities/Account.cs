
namespace ExpenseTracker.Domain.Entities;
public class Account : BaseEntity
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public decimal Balance { get; set; }

    public string Email { get; set; } = string.Empty;

    public Account() : base()
    {
    }
}