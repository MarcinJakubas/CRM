using CRM_Api.Data;
using CRM_Api.Models;
using CRM_Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CRM_Api.Controllers
{
    [Route("api/errors")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly CRMDbContext _context;
        private readonly ErrorLogger _errorLogger;

        public ErrorController(CRMDbContext context, ErrorLogger errorLogger)
        {
            _context = context;
            _errorLogger = errorLogger;
        }

        [HttpPost]
        public async Task<IActionResult> LogError([FromBody] ErrorLog error)
        {
            await _errorLogger.LogErrorAsync(error.ErrorMessage, error.StackTrace, error.UserId, error.Username);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<ErrorLog>>> GetErrors()
        {
            return await _context.Errors.ToListAsync();
        }
    }
}
