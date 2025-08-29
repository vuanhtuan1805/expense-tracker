using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.Services;
using ExpenseTracker.Application.DTOs;

namespace ExpenseTracker.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Registration successful");
        }
    }
}