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
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly olympicsContext _olympicsContext;


        public CityController(ILogger<CityController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Get()
        {
            return await _olympicsContext.Cities.ToListAsync();

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Get(int id)
        {
            var item = await _olympicsContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<City>> Post(City item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.Cities.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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
