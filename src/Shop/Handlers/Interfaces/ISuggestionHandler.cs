using System.Collections.Generic;
using Shop.Models;

namespace Shop.Handlers.Interfaces
{
    public interface ISuggestionHandler
    {
        List<Suggestion> GetSuggestions(string name);
    }
}
