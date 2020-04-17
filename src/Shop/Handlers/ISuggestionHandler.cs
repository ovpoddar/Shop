using Shop.Models;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public interface ISuggestionHandler
    {
        List<Suggestion> GetSuggestions(string name);
        Suggestion SelectSuggestion(string name);
    }
}
