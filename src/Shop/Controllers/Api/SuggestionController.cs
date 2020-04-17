using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionHandler _suggestion;

        public SuggestionController(ISuggestionHandler suggestion) =>
            _suggestion = suggestion;
        
        [HttpGet("{name}")]
        public List<Suggestion> Get(string name)
        {
            return _suggestion.GetSuggestions(name);
        }

        [HttpGet("select/{name}")]
        public Suggestion Select(string name)
        {
            return _suggestion.SelectSuggestion(name);
        }
    }
}