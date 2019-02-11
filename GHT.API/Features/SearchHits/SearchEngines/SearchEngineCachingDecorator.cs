using System;
using System.Collections.Generic;
using GHT.API.Features.SearchHits.SearchEngines.Models;
using Microsoft.Extensions.Caching.Memory;

namespace GHT.API.Features.SearchHits.SearchEngines
{
    public class SearchEngineCachingDecorator : ISearchEngine
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISearchEngine _searchEngine;

        public SearchEngineCachingDecorator(ISearchEngine searchEngine, IMemoryCache memoryCache)
        {
            _searchEngine = searchEngine;
            _memoryCache = memoryCache;
        }
        public string Name => _searchEngine.Name;

        public List<SearchResult> GetSearchResultsForTerm(string searchTerm)
        {
            var cacheKey = $"{_searchEngine.Name}:{searchTerm}";
            if(!_memoryCache.TryGetValue<List<SearchResult>>(cacheKey, out var cachedValue))
            {
                cachedValue = _searchEngine.GetSearchResultsForTerm(searchTerm);
                _memoryCache.Set(cacheKey, cachedValue, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1)));
            }

            return cachedValue;
        }
    }
}