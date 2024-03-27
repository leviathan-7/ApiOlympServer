using ApiServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SportController : ControllerBase
    {
        private readonly ILogger<SportController> _logger;
        private readonly olympicsContext _olympicsContext;


        public SportController(ILogger<SportController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sport>>> Get()
        {
            return await _olympicsContext.Sports.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sport>> Get(int id)
        {
            var item = await _olympicsContext.Sports.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }
    }
}
