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
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly olympicsContext _olympicsContext;


        public PersonController(ILogger<PersonController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await _olympicsContext.People.ToListAsync();

        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var item = await _olympicsContext.People.Include(b => b.GamesCompetitors).FirstOrDefaultAsync(x => x.Id == id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person item)
        {
            if (item == null)
                return BadRequest();

            _olympicsContext.People.Add(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<Person>> Put(Person item)
        {
            if (item == null)
                return BadRequest();
            if (!_olympicsContext.People.Any(x => x.Id == item.Id))
                return NotFound();

            _olympicsContext.Update(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            var item = _olympicsContext.People.FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();
            _olympicsContext.People.Remove(item);
            await _olympicsContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
