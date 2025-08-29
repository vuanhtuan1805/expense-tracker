using Microsoft.Extensions.DependencyInjection;
using ExpenseTracker.Application.Services;
namespace ExpenseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
