# search-hits-tracker

Thanks for taking the time to take a look at this! There application is in two parts. There is a dotnet web API and a React website. To run the solution you can run these commands in 2 terminals from the location of this file...
```
dotnet restore ./SearchHitTracker.API
dotnet run -p ./SearchHitTracker.API
```
```
npm --prefix ./search-hit-tracker-ui i
npm --prefix ./search-hit-tracker-ui start 
```

After running the second command there should be a website opened in your default browser.

To run the tests...
```
dotnet test ./SearchHitTracker.API.Tests/
```

## Future performance, availabilty and reliability
Currently this project is at a POC stage. For internal by a member of the IT team it is useful but for use by a braoder user base there are some investments needed. In particular...
* At this point there is no build/deployment to reliable infrastructure. The appliction is a simple static website and .NET core Web API so a deployment to Fargate and S3 would allow users to access the site easily
* Scraping websites is not a reliable way to source this information. *If* this method is to be continued then logging and alerting needs to be in place. I like to use Serilog as a wrapper around logs and a log aggregation tool like Sumo, Splunk or ELK to parse and collect logs. These tools can be used to set up dashboards and alerts for when things do go wrong.
* Selenium is crazy slow. Again, *if* scraping is to continue to be the way to source the info, then a library like HtmlAgility would help with performance.
* The pipeline build should run tests including unit, integration and performance tests to ensure that future changes don't effect performance, availability or reliability.

## Pre-requisits
* .NET core SDK 2.2+
* A recentish version of Node/npm

## Assummptions and Notes
* I took the line 'Note: Third party testing frameworks and DI libraries are acceptable to use.' as a suggestion to use Selenium/Puppeteer to parse the HTML(I've used Selenium) that may have not been your intention, if you'd like me to refactor to parse the HTML by hand let me know and I can re-work.
* The UI doesn't have much in the way of logic. It takes the model given by the API and displays it. I haven't written tests for it, if you would like me to please let me know.

