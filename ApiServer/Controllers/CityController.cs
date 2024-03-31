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
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly olympicsContext _olympicsContext;


        public CityController(ILogger<CityController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Get()
        {
            return await _olympicsContext.Cities.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Get(int id)
        {
            var item = await _olympicsContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<City>> Post(City item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.Cities.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<City>> Put(City item)
        {
            if (item == null)
                return BadRequest();
            if (!_olympicsContext.Cities.Any(x => x.Id == item.Id))
                return NotFound();

            _olympicsContext.Update(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> Delete(int id)
        {
            var item = _olympicsContext.Cities.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            _olympicsContext.Cities.Remove(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
