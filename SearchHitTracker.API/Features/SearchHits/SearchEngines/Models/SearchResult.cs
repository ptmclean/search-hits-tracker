using System;

namespace SearchHitTracker.API.Features.SearchHits.SearchEngines.Models
{
    public class SearchResult
    {
        public int Ranking { get; set; }
        public Uri Address { get; set; }
        public string Title { get; set; }
    }
}