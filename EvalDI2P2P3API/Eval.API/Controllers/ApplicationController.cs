using Eval.Business.DTO;
using Eval.Business.Services.Interfaces;
using Eval.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eval.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ILogger<ApplicationController> _logger;
        
        public ApplicationController(ILogger<ApplicationController> logger, IApplicationService applicationService)
        {
            _logger = logger;
            _applicationService = applicationService;
        }
        
        // GET: api/<ApplicationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var applications = await _applicationService.GetApplications();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving applications.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // POST api/<ApplicationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateApplicationDTO createApplicationDto)
        {
            try
            {
                await _applicationService.AddApplication(createApplicationDto);
            
                var response = new
                {
                    message = "Application created successfully.",
                    application = createApplicationDto
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the application.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        
    }
}
