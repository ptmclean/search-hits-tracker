using System.Collections.Generic;
using SearchHitTracker.API.Features.SearchHits.Models;
using Microsoft.AspNetCore.Mvc;

namespace SearchHitTracker.API.Features.SearchHits
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHitsController : ControllerBase
    {
        private readonly ISearchHitsControllerService _searchHitsControllerService;
        public SearchHitsController(ISearchHitsControllerService searchHitsControllerService)
        {
            _searchHitsControllerService = searchHitsControllerService;
        }

        [HttpGet]
        public List<SearchRankResponse> Get([FromQuery]string searchTerm, [FromQuery]string testUrl)
        {
            return _searchHitsControllerService.GetSearchRankings(searchTerm, testUrl);
        }
    }
}