using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GHT.API.Features.SearchHits.SearchEngines.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GHT.API.Features.SearchHits.SearchEngines
{
    public class BingSearchEngine : ISearchEngine
    {
        public string Name => "Bing";
        public List<SearchResult> GetSearchResultsForTerm(string searchTerm)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            var searchResults = new List<SearchResult>();
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                for (int i = 0; i < 10; i++)
                {
                    driver.Navigate().GoToUrl($@"https://www.bing.com/search?q={searchTerm}&first={i * 10 + 1}");
                    var searchResultItems = driver.FindElementsByCssSelector("li.b_algo");
                    searchResults.AddRange(searchResultItems.Select((current, index) => new SearchResult
                    {
                        Ranking = i * 10 + index,
                        Address = new Uri(current.FindElement(By.CssSelector("a"))?.GetAttribute("href")),
                        Title = current.FindElement(By.TagName("h2"))?.Text
                    }));
                }
            }
            return searchResults;
        }
    }
}