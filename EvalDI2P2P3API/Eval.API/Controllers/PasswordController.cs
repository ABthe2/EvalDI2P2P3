using Eval.Business.DTO;
using Eval.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eval.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IPasswordService _passwordService;
        
        public PasswordController(ILogger<ApplicationController> logger, IPasswordService passwordService)
        {
            _logger = logger;
            _passwordService = passwordService;
        }
        
        // GET: api/<PasswordController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var passwords = await _passwordService.GetPasswords();
                return Ok(passwords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving passwords.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        
        // POST api/<PasswordController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountDTO createAccountDto)
        {
            try
            {
                await _passwordService.AddPassword(createAccountDto);
            
                var response = new
                {
                    message = "Password created successfully."
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the password.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // DELETE api/<PasswordController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _passwordService.DeletePassword(id);
                return Ok(new { message = "Password deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the password.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
