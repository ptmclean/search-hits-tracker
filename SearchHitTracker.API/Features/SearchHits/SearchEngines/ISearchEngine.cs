using System.Collections.Generic;
using SearchHitTracker.API.Features.SearchHits.SearchEngines.Models;

namespace SearchHitTracker.API.Features.SearchHits.SearchEngines
{
    public interface ISearchEngine
    {
        string Name { get; }
        List<SearchResult> GetSearchResultsForTerm(string searchTerm);
    }
}