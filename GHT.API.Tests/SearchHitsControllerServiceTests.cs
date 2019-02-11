using NUnit.Framework;
using FakeItEasy;
using GHT.API.Features.SearchHits.SearchEngines;
using GHT.API.Features.SearchHits;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GHT.API.Features.SearchHits.Models;
using GHT.API.Features.SearchHits.SearchEngines.Models;
using System;

namespace GHT.API.Tests
{
    public class SearchHitsControllerServiceTests
    {
        [Test]
        public void GivenNoSearchEngines_ThenEmptyListReturned()
        {
            var _searchHitsControllerService = new SearchHitsControllerService(new List<ISearchEngine> ());
            var result = _searchHitsControllerService.GetSearchRankings("tests", "best");
            result.Count().Should().Be(0);
        }

        [Test]
        public void GivenTwoSearchEngines_ThenTwoSearchResultsReturned()
        {
            var googleSearchEngine = A.Fake<ISearchEngine>();
            A.CallTo(() => googleSearchEngine.Name).Returns("Google");

            var bingSearchEngine = A.Fake<ISearchEngine>();
            A.CallTo(() => googleSearchEngine.Name).Returns("Bing");

            var _searchHitsControllerService = new SearchHitsControllerService(new List<ISearchEngine> { googleSearchEngine, bingSearchEngine });
            var result = _searchHitsControllerService.GetSearchRankings("tests", "best");
            result.Count().Should().Be(2);
        }

        [Test]
        public void GivenSearchResult_ThenModelMappedCorrectly()
        {
            const string Address = "https://address.com/";
            const string PageTitle = "Title";
            const int Ranking = 42;
            const string SearchEngineName = "SearchEngine";
            const string SearchTerm = "SearchTerm";

            var expected = new SearchRankResponse
            {
                MatchingResults = new List<SearchRankResponse.SearchResult> {
                    new SearchRankResponse.SearchResult {
                        Address = Address,
                        Ranking = Ranking,
                        Title = PageTitle
                    }
                },
                SearchEngine = SearchEngineName,
                SearchTerm = SearchTerm,
                TestUrl = Address
            };

            var googleSearchEngine = A.Fake<ISearchEngine>();
            A.CallTo(() => googleSearchEngine.Name).Returns(expected.SearchEngine);
            A.CallTo(() => googleSearchEngine.GetSearchResultsForTerm(A<string>._)).Returns(
                new List<SearchResult> {
                    new SearchResult {
                        Address = new Uri(Address),
                        Ranking = Ranking,
                        Title = PageTitle
                    }
                }
            );

            var _searchHitsControllerService = new SearchHitsControllerService(new List<ISearchEngine> { googleSearchEngine });
            var result = _searchHitsControllerService.GetSearchRankings(SearchTerm, Address);
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GivenSearchResult_ThenResultsFilteredByTestUrl()
        {
            const string Address = "https://address.com/";
            const string TestUrl = "https://NotTheSameUrl";
            const string PageTitle = "Title";
            const int Ranking = 42;
            const string SearchEngineName = "SearchEngine";
            const string SearchTerm = "SearchTerm";

            var expected = new SearchRankResponse
            {
                MatchingResults = new List<SearchRankResponse.SearchResult> (),
                SearchEngine = SearchEngineName,
                SearchTerm = SearchTerm,
                TestUrl = TestUrl
            };

            var googleSearchEngine = A.Fake<ISearchEngine>();
            A.CallTo(() => googleSearchEngine.Name).Returns(expected.SearchEngine);
            A.CallTo(() => googleSearchEngine.GetSearchResultsForTerm(A<string>._)).Returns(
                new List<SearchResult> {
                    new SearchResult {
                        Address = new Uri(Address),
                        Ranking = Ranking,
                        Title = PageTitle
                    }
                }
            );

            var _searchHitsControllerService = new SearchHitsControllerService(new List<ISearchEngine> { googleSearchEngine });
            var result = _searchHitsControllerService.GetSearchRankings(SearchTerm, TestUrl);
            result.Should().BeEquivalentTo(expected);
        }
    }
}