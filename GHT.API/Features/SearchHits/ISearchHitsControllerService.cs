using System.Collections.Generic;
using GHT.API.Features.SearchHits.Models;

namespace GHT.API.Features.SearchHits
{
    public interface ISearchHitsControllerService
    {
        List<SearchRankResponse> GetSearchRankings(string seachTerm, string testUrl);
    }

    public class SearchHitsControllerService : ISearchHitsControllerService
    {
        public List<SearchRankResponse> GetSearchRankings(string seachTerm, string testUrl)
        {
            return new List<SearchRankResponse> {
                new SearchRankResponse {
                    SearchEngine = "Google",
                    SearchTerm = "Hello",
                    TestUrl = "World",
                    MatchingResults = new List<SearchRankResponse.SearchResult> {
                        new SearchRankResponse.SearchResult {
                            Address = "www.google.com",
                            Ranking = 0,
                            Title = "World"
                        }
                    }
                }
            };
        }
    }
}