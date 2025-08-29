using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Services;
using ExpenseTracker.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;

namespace ExpenseTracker.Infrastructure.ServiceImpl
{
    public class AccountService : IAccountService
    {
        // Implementation of account-related services
        private readonly ExpenseTrackerDbContext _context;

        public AccountService(ExpenseTrackerDbContext context)
        {
            _context = context;
        }
        public async Task<RegisterResultDTO> RegisterAsync(AccountRequest request)
        {
            var exist = await _context.Accounts.AnyAsync(a => a.Username == request.Username || a.Email == request.Email);
            if (exist)
            {
                return new RegisterResultDTO
                {
                    Success = false,
                    Errors = new List<string> { "Username or Email already exists." }
                };
            }

            var passwordHash = HashPassword(request.Password);
            _context.Accounts.Add(new()
            {
                Username = request.Username,
                Email = request.Email,
                Password = passwordHash
            });

            return new RegisterResultDTO
            {
                Success = await _context.SaveChangesAsync() > 0
            };
        }
        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 4,
                MemoryCost = 1 << 18, // 1^18 = 256 MB
                Lanes = Environment.ProcessorCount,
                Threads = Environment.ProcessorCount,
                Password = System.Text.Encoding.UTF8.GetBytes(password),
                Salt = salt,
                HashLength = 32
            };

            using var argon2 = new Argon2(config);

            return config.EncodeString(argon2.Hash().Buffer);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return Argon2.Verify(hashedPassword, password);
        }
    }
}