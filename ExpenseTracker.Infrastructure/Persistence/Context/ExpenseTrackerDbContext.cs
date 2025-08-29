using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Infrastructure.Persistence.Context
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
    }
}