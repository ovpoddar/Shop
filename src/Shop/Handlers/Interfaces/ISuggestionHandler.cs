using Shop.Models;
using System.Collections.Generic;

namespace Shop.Handlers.Interfaces
{
    public interface ISuggestionHandler
    {
        List<Suggestion> GetSuggestions(string name);
    }
}
