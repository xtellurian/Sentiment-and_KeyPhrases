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
    public class GetLatestData
    {
        private HttpClient _client;
        private string _function;
        public GetLatestData(string function)
        {
            _client = new HttpClient();
            _function = function;
        }

        public async Task<TopicDetectionAggregate> Run()
        {
            var res = await _client.GetAsync(_function);
            var text = await res.Content.ReadAsStringAsync();
            
            try
            {
                var other1 = JsonConvert.DeserializeObject<string>(text);
                var other = JsonConvert.DeserializeObject<TopicDetectionAggregate>(other1);
                // weird - its getting serialised twice somehow...
                return other;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
            
        }

        public ArticleDataAggregate Convert(TopicDetectionAggregate old)
        {
            var aggregate = new ArticleDataAggregate();
            var meta = new MetaData();
            meta.DataLocation = old.DataLocation;
            meta.DateCreated = old.CreatedDateTime;
            aggregate.Meta = meta;
            aggregate.AddSourcesThreadsafe(old.Sources);
            foreach(var s in old.Sources){
                aggregate.AddArticlesThreadsafe(s.Articles);
            }
            aggregate.AddTopicsThreadsafe(old.Result.Topics);

            foreach(var t in old.Result.TopicAssignments)
            {
                var a = aggregate.Articles.FirstOrDefault(x => x.Id?.ToString() == t.DocumentId);
                if(a.TopicAssignments == null) a.TopicAssignments = new List<TopicAssignment>();
                a.TopicAssignments.Add(t);
                
            }
            foreach(var a in aggregate.Articles){
                if(a.TopicAssignments==null) a.TopicAssignments = new List<TopicAssignment>();
            }

            return aggregate;

        }
    }
}