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
    public class NocRegionController : ControllerBase
    {
        private readonly ILogger<NocRegionController> _logger;
        private readonly olympicsContext _olympicsContext;


        public NocRegionController(ILogger<NocRegionController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NocRegion>>> Get()
        {
            return await _olympicsContext.NocRegions.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NocRegion>> Get(int id)
        {
            var item = await _olympicsContext.NocRegions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }
    }
}
