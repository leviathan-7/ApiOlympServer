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
    }
}
