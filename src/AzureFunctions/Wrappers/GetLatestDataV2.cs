using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rian.Cognitive;

namespace Rian.AzureFunctions
{
    public class GetLatestDataV2
    {
        private HttpClient _client;
        private string _function;
        public GetLatestDataV2(string function)
        {
            _client = new HttpClient();
            _function = function;
        }

        public async Task<ArticleDataAggregate> Run()
        {
            var res = await _client.GetAsync(_function);
            var text = await res.Content.ReadAsStringAsync();
            
            try
            {
                var other = JsonConvert.DeserializeObject<ArticleDataAggregate>(text);

                return other;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
            
        }

        public ArticleDataAggregate AverageSentimentsOverTopcs(ArticleDataAggregate data)
        {
            foreach(var topic in data.Topics)
                {
                    var articles = data.Articles.Where(a => a.TopicAssignments.Any(t=>t.TopicId == topic.Id));
                    double average = 0;
                    foreach(var a in articles)
                    {
                        average += a.Sentiment;
                    }
                    topic.AverageSentiment = average / articles.Count();
                }

            return data;
        }
    }
}