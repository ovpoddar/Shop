using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Models;
using Shop.Utilities;
using System.Collections.Generic;

namespace Shop.Controllers.Api
{
    [AuthorizeToken]
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