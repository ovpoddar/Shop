using Shop.Models;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface ISuggestionHandler
    {
        List<SuggestionModel> GetSuggestions(string name);
        SuggestionModel SelectSuggestion(string name);
    }
}
