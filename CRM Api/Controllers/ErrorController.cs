using CRM_Api.Data;
using CRM_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRM_Api.Controllers
{
    [Route("api/errors")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly CRMDbContext _context;

        public ErrorController(CRMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> LogError([FromBody] ErrorLog error)
        {
            _context.Errors.Add(error);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
