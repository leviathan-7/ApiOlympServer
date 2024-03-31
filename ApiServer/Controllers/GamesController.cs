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
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly olympicsContext _olympicsContext;


        public GamesController(ILogger<GamesController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            return await _olympicsContext.Games.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var item = await _olympicsContext.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Post(Game item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.Games.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Game>> Put(Game item)
        {
            if (item == null)
                return BadRequest();
            if (!_olympicsContext.Games.Any(x => x.Id == item.Id))
                return NotFound();

            _olympicsContext.Update(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> Delete(int id)
        {
            var item = _olympicsContext.Games.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            _olympicsContext.Games.Remove(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
