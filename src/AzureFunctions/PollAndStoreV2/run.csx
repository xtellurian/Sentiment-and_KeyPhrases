#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"
using System;
using System.Text;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage.Blob;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, Stream output)
{    
    var headerValues = req.Headers.GetValues("x-location");
    var dataLocation = headerValues.FirstOrDefault();
    log.Info($"Adding serialised data to queue");

    var content = await req.Content.ReadAsStringAsync();
    var jObj = JObject.Parse(content);
    jObj.Add("dataLocation", new JValue(dataLocation));
    var result = JsonConvert.SerializeObject(jObj);
    var bytes = Encoding.UTF8.GetBytes(result);
    await output.WriteAsync(bytes, 0, bytes.Length);

    // return message
    var response = new HttpResponseMessage();
    response.Content = new StringContent(result);
    return response;
}
