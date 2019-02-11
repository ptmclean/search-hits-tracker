using System.Linq;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using SearchHitTracker.API.Features.SearchHits.SearchEngines;
using SearchHitTracker.API.Features.SearchHits.SearchEngines.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace SearchHitTracker.API.Tests
{
    [TestFixture]
    public class SearchEngineCacheDecoratorTests
    {
        private IMemoryCache _memoryCache;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            _memoryCache = serviceProvider.GetService<IMemoryCache>();
        }

        [Test]
        public void GivenASingleCall_ThenUnderlyingServiceShouldBeCalled()
        {
            var underlyingCacheDecorator = A.Fake<ISearchEngine>();
            var memoryCache = A.Fake<IMemoryCache>();
            var cacheDecorator = new SearchEngineCachingDecorator(underlyingCacheDecorator, memoryCache);
            cacheDecorator.GetSearchResultsForTerm("Hello");
            A.CallTo(() => underlyingCacheDecorator.GetSearchResultsForTerm(A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GivenTwoCallsWithSameArgument_ThenUnderlyingServiceShouldBeCalledOnce()
        {
            var underlyingCacheDecorator = A.Fake<ISearchEngine>();
            var cacheDecorator = new SearchEngineCachingDecorator(underlyingCacheDecorator, _memoryCache);
            cacheDecorator.GetSearchResultsForTerm("Hello");
            cacheDecorator.GetSearchResultsForTerm("Hello");
            A.CallTo(() => underlyingCacheDecorator.GetSearchResultsForTerm(A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GivenTwoCallsWithSameArgument_TheSameObjectShouldBeReturnedBothTimes()
        {
            var underlyingCacheDecorator = A.Fake<ISearchEngine>();
            List<SearchResult> expected = new List<SearchResult> {
                    new SearchResult {
                        Title = "The Dude abides"
                    }
                };
            A.CallTo(() => underlyingCacheDecorator.GetSearchResultsForTerm(A<string>._)).Returns(
                expected
            );
            var cacheDecorator = new SearchEngineCachingDecorator(underlyingCacheDecorator, _memoryCache);
            var responseOne = cacheDecorator.GetSearchResultsForTerm("Hello");
            var responseTwo = cacheDecorator.GetSearchResultsForTerm("Hello");
            responseOne.Should().BeEquivalentTo(expected);
            responseTwo.Should().BeEquivalentTo(expected);
        }


        [Test]
        public void GivenACallToNameProperty_ThenUnderlyingServiceNameShouldBeReturned()
        {
            var underlyingCacheDecorator = A.Fake<ISearchEngine>();
            A.CallTo(() => underlyingCacheDecorator.Name).Returns("The Dude");
            var memoryCache = A.Fake<IMemoryCache>();
            var cacheDecorator = new SearchEngineCachingDecorator(underlyingCacheDecorator, memoryCache);
            var name = cacheDecorator.Name;
            name.Should().Be("The Dude");
        }
    }
}