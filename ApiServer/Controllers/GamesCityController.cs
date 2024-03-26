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
    public class GamesCityController : ControllerBase
    {
        private readonly ILogger<GamesCityController> _logger;
        private readonly olympicsContext _olympicsContext;


        public GamesCityController(ILogger<GamesCityController> logger, olympicsContext olympicsContext)
        {
            _logger = logger;
            _olympicsContext = olympicsContext;
        }

        [HttpGet]
        public IEnumerable<GamesCity> Get()
        {
            return _olympicsContext.GamesCities;

        }
    }
}
