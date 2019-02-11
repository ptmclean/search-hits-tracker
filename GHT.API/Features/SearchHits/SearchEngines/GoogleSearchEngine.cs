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
  public class GoogleSearchEngine : ISearchEngine
  {
    public string Name => "Google";
    public List<SearchResult> GetSearchResultsForTerm(string searchTerm)
    {
      var chromeOptions = new ChromeOptions();
      chromeOptions.AddArgument("--headless");
      using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
      {
        driver.Navigate().GoToUrl($@"https://www.google.com/search?q={searchTerm}&num=100");
        var searchResultItems = driver.FindElementsByXPath("//a//h3//parent::a");
        return searchResultItems.Select((current, index) => new SearchResult
        {
          Ranking = index,
          Address = new Uri(current.GetAttribute("href")),
          Title = current.FindElement(By.TagName("h3")).Text
        })
        .ToList();
      }
    }
  }
}