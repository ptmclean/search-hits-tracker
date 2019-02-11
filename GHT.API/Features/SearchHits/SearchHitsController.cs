using System.Collections.Generic;
using GHT.API.Features.SearchHits.Models;
using Microsoft.AspNetCore.Mvc;

namespace GHT.API.Features.SearchHits
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