using ApiServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonRegionController : ControllerBase
    {
        private readonly ILogger<PersonRegionController> _logger;
        private readonly olympicsContext _olympicsContext;


        public PersonRegionController(ILogger<PersonRegionController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        [HttpGet]
        public IEnumerable<PersonRegion> Get()
        {
            return _olympicsContext.PersonRegions;

        }
    }
}
