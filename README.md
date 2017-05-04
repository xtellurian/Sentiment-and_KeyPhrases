# Sentimental News
Playing around with MS Cognitive Services. 
Taking news article summaries from around the world and generating summaries using key phrases and sentiment detection.

# We're Live!

http://news-sentiment.azurewebsites.net/

# Getting Started

1) git pull https://github.com/xtellurian/Sentiment-and_KeyPhrases

2) Create Azure Functions (code provided)

3) Create appsettings.json

    a) Add Cognitive Services Key & News API Key

    b) Add URLs for Azure Functions

```
{
  "NewsApiKey" : "<YOUR-KEY>",
  "CognitiveServicesTextApiKey" : "<YOUR-KEY>",
  "PollAndStoreV2FunctionUri": "<URL + code>",
  "LatestDataV2Uri": "<URL + code>"
}
```

4) dotnet restore

5) dotnet run



# Requirements 

* DotNetCore
* News Api Key
* Ms Cognitive Services - Text Analytics Api Key
* Azure Functions Account

Thanks to :

https://github.com/ealsur/CognitiveServicesText

https://newsapi.org

# To Do

- [x] Refactor Data Model
- [x] Cache machanism in ASP.NET for improved performance
- [x] Extend response from LastTopicDetection to include articles that are returned
- [x] Implement Azure Topic Detection
- [ ] Front End Improvements
- [ ] Better logging in Azure Functions
- [ ] Store data to discover trends
- [ ] Move data calls to JS front end for faster page load times

# Sample Output (Original)

COUNT x PHRASE -- SENTIMENT

```
7 x North Korea -- 45%
6 x police officer -- 12%
5 x President Donald Trump -- 41%
5 x Manchester United -- 68%
4 x central Paris -- 26%
4 x Champs Elysees -- 9%
4 x Fox News -- 26%
4 x general election -- 49%
3 x presidential election -- 53%
3 x French election -- 26%
```
