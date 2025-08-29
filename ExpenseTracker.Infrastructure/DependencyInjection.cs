using ExpenseTracker.Application.Services;
using ExpenseTracker.Infrastructure.Persistence.Context;
using ExpenseTracker.Infrastructure.ServiceImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExpenseTrackerDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("ExpenseTrackerDb"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("ExpenseTrackerDb"))
            ));

        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
