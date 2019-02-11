using System.Collections.Generic;
using System.Linq;
using SearchHitTracker.API.Features.SearchHits.Models;
using SearchHitTracker.API.Features.SearchHits.SearchEngines;

namespace SearchHitTracker.API.Features.SearchHits
{
    public interface ISearchHitsControllerService
    {
        List<SearchRankResponse> GetSearchRankings(string seachTerm, string testUrl);
    }

    public class SearchHitsControllerService : ISearchHitsControllerService
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;
        public SearchHitsControllerService(IEnumerable<ISearchEngine> searchEngines)
        {
            _searchEngines = searchEngines;
        }
        public List<SearchRankResponse> GetSearchRankings(string searchTerm, string testUrl)
        {
            var rankings = new List<SearchRankResponse>();
            foreach (var searchEngine in _searchEngines)
            {
                var searchResults = searchEngine.GetSearchResultsForTerm(searchTerm);
                rankings.Add(new SearchRankResponse
                {
                    SearchTerm = searchTerm,
                    TestUrl = testUrl,
                    SearchEngine = searchEngine.Name,
                    MatchingResults = searchResults
                        .Where(r => r.Address.AbsoluteUri.Contains(testUrl))
                        .Select(r => new SearchRankResponse.SearchResult
                        {
                            Ranking = r.Ranking,
                            Title = r.Title,
                            Address = r.Address.ToString()
                        })
                        .ToList()
                });
            }
            return rankings;
        }
    }
}