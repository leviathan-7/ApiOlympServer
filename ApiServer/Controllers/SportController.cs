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
            return await _olympicsContext.Sports.Include(b => b.Events).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sport>> Get(int id)
        {
            var item = await _olympicsContext.Sports.Include(b => b.Events).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<Sport>> Post(Sport item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.Sports.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Sport>> Put(Sport item)
        {
            if (item == null)
                return BadRequest();
            if (!_olympicsContext.Sports.Any(x => x.Id == item.Id))
                return NotFound();

            _olympicsContext.Update(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Sport>> Delete(int id)
        {
            var item = _olympicsContext.Sports.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            _olympicsContext.Sports.Remove(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
