using CRM_Api.Data;
using CRM_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_Api.Controllers
{
    [Route("api/audit")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly CRMDbContext _context;

        public AuditController(CRMDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuditLog>>> GetAuditLogs()
        {
            return await _context.AuditLogs.ToListAsync();
        }
    }
}
