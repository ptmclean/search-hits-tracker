using System.Collections.Generic;

namespace GHT.API.Features.SearchHits.Models
{
    public class SearchRankResponse
    {
        public string SearchTerm { get; set; }
        public string TestUrl { get; set; }
        public List<SearchResult> MatchingResults { get; set; }
        public string SearchEngine { get; set; }

        public class SearchResult
        {
            public int Ranking { get; set; }
            public string Title { get; set; }
            public string Address { get; set; }
        }
    }
}