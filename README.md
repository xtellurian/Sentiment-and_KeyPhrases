# Sentimental News
Playing around with MS Cognitive Services. 
Taking news article summaries from around the world and generating summaries using key phrases and sentiment detection.

# We're Live!

http://news-sentiment.azurewebsites.net/

# Getting Started

1) `git pull https://github.com/xtellurian/Sentiment-and_KeyPhrases`

2) Create Azure Functions (code provided)

3) Add secrets for development

```
dotnet user-secrets set NewsApiKey <YOUR-KEY>
dotnet user-secrets set CognitiveServicesTextApiKey <YOUR-KEY>
dotnet user-secrets set PollAndStoreV2FunctionUri <YOUR-URI>
dotnet user-secrets set LatestDataV2Uri <YOUR-URI>
```


4) `dotnet restore`

5) `dotnet run`



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
- [x] Image Analysis
- [ ] Fix Function Workflow so data still available during refresh
- [ ] Entity Linking to merge topics
- [ ] Bing News Search API
- [ ] Azure KES?
- [ ] Front End Improvements
- [ ] Better logging in Azure Functions
- [ ] Store data to discover trends
- [ ] Move data calls to JS front end for faster page load times


