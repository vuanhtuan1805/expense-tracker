using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Persistence.Context;
using ExpenseTracker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ExpenseTrackerDb"), 
    new MySqlServerVersion(new Version(8, 0, 42))));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // No database = create one
    var dbContext = scope.ServiceProvider.GetRequiredService<ExpenseTrackerDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
