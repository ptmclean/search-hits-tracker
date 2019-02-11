using System.Collections.Generic;
using GHT.API.Features.SearchHits.SearchEngines.Models;

namespace GHT.API.Features.SearchHits.SearchEngines
{
    public interface ISearchEngine
    {
        string Name { get; }
        List<SearchResult> GetSearchResultsForTerm(string searchTerm);
    }
}