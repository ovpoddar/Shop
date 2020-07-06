using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionHandler _suggestion;

        public SuggestionController(ISuggestionHandler suggestion) =>
            _suggestion = suggestion;

        [HttpGet("{name}")]
        [EnableCors("All")]
        public List<Suggestion> Get(string name)
        {
            return _suggestion.GetSuggestions(name);
        }
    }
}