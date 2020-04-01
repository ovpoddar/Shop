using Microsoft.AspNetCore.Mvc;
using Shop.Handlers;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Controllers.Api
{
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionHandler _suggestion;

        public SuggestionController(ISuggestionHandler suggestion)
        {
            _suggestion = suggestion ?? throw new System.ArgumentNullException(nameof(_suggestion));
        }

        [HttpGet]
        [Route("api/Get")]
        public List<SuggestionModel> Get(string name)
        {
            return _suggestion.GetSuggestions(name);
        }

        [HttpGet]
        [Route("api/Select")]
        public SuggestionModel Select(string name)
        {
            return _suggestion.SelectSuggestion(name);
        }
    }
}