using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Rian.AzureStorage
{
    public class UploadToBlobStorage
{
    private const string CONTENT_TYPE = "application/json";
 
    private readonly object obj;
    private readonly string containerPath;
    private readonly string blobAddressUri;
    private readonly Dictionary<string, string> metadata;
 
    public UploadToBlobStorage(object obj,
                                        string containerPath,
                                        string blobAddressUri,
                                        Dictionary<string, string> metadata)
    {
        this.obj = obj;
        this.containerPath = containerPath;
        this.blobAddressUri = blobAddressUri;
        this.metadata = metadata;
    }
 
    public async Task Apply(CloudStorageAccount model)
    {
        var client = model.CreateCloudBlobClient();
 
        var container = client.GetContainerReference(containerPath);
        try{
             if (! await container.ExistsAsync()){
                  await container.CreateAsync();
             }
          
            var blobReference = container.GetBlockBlobReference(blobAddressUri);
 
            var blockBlob = blobReference;
            await UploadToContainer(blockBlob); 
        }
        catch (Exception ex){

            Console.WriteLine(ex);
        }
       
    }
 
    private async Task UploadToContainer(CloudBlockBlob blockBlob)
    {
        SetBlobProperties(blockBlob);
 
        using (var ms = new MemoryStream())
        {
            LoadStreamWithJson(ms);
            await blockBlob.UploadFromStreamAsync(ms);
        }
    }
 
    private void SetBlobProperties(CloudBlockBlob blobReference)
    {
        blobReference.Properties.ContentType = CONTENT_TYPE;
        foreach (var meta in metadata)
        {
            blobReference.Metadata.Add(meta.Key, meta.Value);
        }
    }
 
    private void LoadStreamWithJson(Stream ms)
    {
        var json = JsonConvert.SerializeObject(obj);
        StreamWriter writer = new StreamWriter(ms);
        writer.Write(json);
        writer.Flush();
        ms.Position = 0;
    }
}
}
