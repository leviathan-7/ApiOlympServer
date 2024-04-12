using ApiServer.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NocRegion>>> Get()
        {
            return await _olympicsContext.NocRegions.ToListAsync();

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<NocRegion>> Get(int id)
        {
            var item = await _olympicsContext.NocRegions.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<NocRegion>> Post(NocRegion item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.NocRegions.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult<NocRegion>> Put(NocRegion item)
        {
            if (item == null)
                return BadRequest();
            if (!_olympicsContext.NocRegions.Any(x => x.Id == item.Id))
                return NotFound();

            _olympicsContext.Update(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<NocRegion>> Delete(int id)
        {
            var item = _olympicsContext.NocRegions.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            _olympicsContext.NocRegions.Remove(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
