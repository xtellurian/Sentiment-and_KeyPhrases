# Sentiment-and_KeyPhrases
Playing around with MS Cognitive Services. 
Taking news article summaries from around the world and generating summaries using key phrases and sentiment detection.

# Getting Started

1) git pull https://github.com/xtellurian/Sentiment-and_KeyPhrases

2) Place Api Keys in 'CognitiveServicesTextApi.key' and NewsApi.key

3) dotnet restore

4) dotnet run



# Requirements 

* DotNetCore
* Json.Net
* News Api Key
* Ms Cognitive Services - Text Analytics Api Key

Place the Api Keys in their respective files. See Utility.cs

Thanks to :

https://github.com/ealsur/CognitiveServicesText

https://newsapi.org


# Output

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
