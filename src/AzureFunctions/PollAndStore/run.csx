using System;
using System.Net;
using System.Configuration;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log, ICollector<string> outputQueue)
{    
    var headerValues = req.Headers.GetValues("x-location");
    var dataLocation = headerValues.FirstOrDefault();

    log.Info($"Adding to queue: {dataLocation}");
    outputQueue.Add(dataLocation);

    return new HttpResponseMessage();
}
