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
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly olympicsContext _olympicsContext;


        public EventController(ILogger<EventController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return await _olympicsContext.Events.Include(b => b.Sport).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var item = await _olympicsContext.Events.Include(b => b.Sport).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Post(Event item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.Events.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Event>> Put(Event item)
        {
            if (item == null)
                return BadRequest();
            if (!_olympicsContext.Events.Any(x => x.Id == item.Id))
                return NotFound();

            _olympicsContext.Update(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> Delete(int id)
        {
            var item = _olympicsContext.Events.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            _olympicsContext.Events.Remove(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}

