#r "Newtonsoft.Json"

using System;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;

public async static Task<string> Run(string myQueueItem, TraceWriter log)
{
    log.Info($"C# Queue trigger function processed: {myQueueItem}");

    var key = ConfigurationManager.AppSettings["cognitive_api_key"];

    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

    var content = await GetContent(httpClient, myQueueItem, log);

    return content;
}

private async static Task<string> GetContent(HttpClient httpClient, string location, TraceWriter log)
{
    var count = 0;
    string content = "";
    while(true)
    {
        var topics = await httpClient.GetAsync(location);
        content = await topics.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response>(content);
        log.Info("Response: " + response.status);
        if(string.Equals(response.status, "Succeeded")) break;
        if(++count > 50) throw new Exception("Exceeeded Retry Count");
        await Task.Delay(30000); // wait 30 seconds
    }
    
    return content;
}

public class Response 
{
    public string status { get; set; }
}

    