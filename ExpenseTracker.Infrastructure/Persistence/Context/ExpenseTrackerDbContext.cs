using Microsoft.EntityFrameworkCore;

public class ExpenseTrackerDbContext : DbContext
{
    public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
        : base(options)
    {
    }
    public DbSet<Account> Users { get; set; }
}