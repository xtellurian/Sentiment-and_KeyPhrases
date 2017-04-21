using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Source : SourceBase 
{
    private const string sourcesUrl = "https://newsapi.org/v1/sources";
    private const string articlesUrl = "https://newsapi.org/v1/articles";
    private static string _apiKey;

    public List<Article> Articles {get; private set;}

    public async Task<int> LoadArticles()
    {
        if(string.IsNullOrEmpty(_apiKey)) throw new NullReferenceException("Api Key is Null");

        var reqUrl = articlesUrl + "?source=" + id;
       // Create a New HttpClient object.
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
        // Call asynchronous network methods in a try/catch block to handle exceptions
        try 
        {
            // Above three lines can be replaced with new helper method below
            string responseBody = await client.GetStringAsync(reqUrl);


            var result = JsonConvert.DeserializeObject<ArticleResponse>(responseBody);
            client.Dispose();
            Articles = result.articles;
            foreach(var a in Articles){
                a.Language = language;
            }
            Console.WriteLine($"Loaded {Articles.Count} articles from {name}");
            return Articles.Count;
         }  
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");    
            Console.WriteLine("Message :{0} ",e.Message);
        }

        client.Dispose();
        throw new Exception("Failed to Load Articles in Source Id: " + id);
    }

    public static void SetApiKey(string apiKey)
    {
        _apiKey = apiKey;
    }

    public static async Task<SourceResponse> GetSourcesAsync(Language language = Language.all)
    {
       // Create a New HttpClient object.
        HttpClient client = new HttpClient();
        string url = sourcesUrl;
        if(language!=Language.all){
            url += "?language=" + Enum.GetName(typeof(Language), language);
        }
        
        // Call asynchronous network methods in a try/catch block to handle exceptions
        try 
        {
            // Above three lines can be replaced with new helper method below
            string responseBody = await client.GetStringAsync(url);


            var result = JsonConvert.DeserializeObject<SourceResponse>(responseBody);
            client.Dispose();
            return result;
         }  
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");    
            Console.WriteLine("Message :{0} ",e.Message);
        }

        client.Dispose();
        return null;
    }
}