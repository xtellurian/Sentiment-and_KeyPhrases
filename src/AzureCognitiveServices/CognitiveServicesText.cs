using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rian.Cognitive {
        

    /// <summary>
    /// Implementation of https://www.microsoft.com/cognitive-services/en-us/text-analytics/documentation
    /// </summary>
    public class CognitiveServicesTextAnalysis : ICognitiveServicesTextAnalysis
    {
        #region Requests
        private class TextRequest
        {
            public TextRequest()
            {
                Documents = new List<TextDocument>();
            }
            [JsonProperty("documents")]
            public List<TextDocument> Documents { get; set; }
        }
        private class TextDocument
        {
            public TextDocument(string id, string text, string language)
            {
                Id = id;
                Language = language;
                Text = text;
            }
            [JsonProperty("language")]
            public string Language { get; private set; }
            [JsonProperty("id")]
            public string Id { get; private set; }
            [JsonProperty("text")]
            public string Text { get; private set; }
        } 
        #endregion

        private readonly HttpClient _httpClient;
        /// <summary>
        /// Cognitive Text service endpoint
        /// </summary>
        private const string serviceEndpoint = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/";
        public CognitiveServicesTextAnalysis(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
        }


        private readonly string[] supportedLanguages = {"en","es","fr","pt"};
        public async Task<IEnumerable<Article>> SetSentiments(IEnumerable<Article> articles)
        {

            var request = new TextRequest();
            foreach(var a in articles){
                if(supportedLanguages.Contains(a.Language)){
                    if(!string.IsNullOrEmpty(a.Description)){
                            request.Documents.Add(new TextDocument(a.Id.ToString(), a.Description, a.Language));
                    }
                }
            }

            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"{serviceEndpoint}sentiment", content).ConfigureAwait(false);

            var response = JObject.Parse(await result.Content.ReadAsStringAsync());
            CatchAndThrow(response, request);
            var ff = response["documents"].Children().ToList();
            foreach(var f in ff){
                var id = f["id"];
                var sentiment = f["score"];

                articles.FirstOrDefault(a=>a.Id.ToString() == id.ToString()).Sentiment
                            = sentiment.Value<double>();

            }
            return articles;
        }

        public async Task<IEnumerable<Article>> SetKeyPhrases (IEnumerable<Article> articles)
        {
            var request = new TextRequest();
            foreach(var a in articles){
                if(supportedLanguages.Contains(a.Language)){
                    request.Documents.Add(
                        new TextDocument(a.Id.ToString(), a.Description.ToString(), a.Language));
                }
                else{
                //   Manager.WriteLine($"{a.Language} not supported");
                    a.KeyPhrases = new List<string>();
                }
            }
            var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"{serviceEndpoint}keyPhrases", content).ConfigureAwait(false);

            var response = JObject.Parse(await result.Content.ReadAsStringAsync());
            CatchAndThrow(response, request);
            foreach(var doc in response["documents"].Children()){
                var id = doc["id"];
                var phrases = doc.Value<JArray>("keyPhrases").ToObject<List<string>>();

                articles.FirstOrDefault(a=>a.Id.ToString() == id.ToString()).KeyPhrases = phrases;
                
            }

            return articles;
        }
    

        private void CatchAndThrow(JObject response, TextRequest request)
        {
            if (response["errors"] != null && response["errors"].Children().Any())
            {
                throw new Exception(response["errors"].Children().First().Value<string>("message"));
            }
            if(response["code"] != null && response["message"] != null)
            {
                throw new Exception(response["message"].Value<string>());
            }
        }
    }
}
