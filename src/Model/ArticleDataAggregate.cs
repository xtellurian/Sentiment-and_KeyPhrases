// this class is the final class to be returned and used in views
using System.Collections.Generic;
using System;

namespace Rian.Cognitive
{
    public class ArticleDataAggregate
    {
        public MetaData Meta {get;set;}

        public List<Article> Articles {get;protected set;}

        public List<SourceBase> Sources {get;protected set;}

        public List<Topic> Topics {get;protected set;}

        private object _articleLock = new object();
        public void AddArticlesThreadsafe (IEnumerable<Article> articles)
        {
            lock(_articleLock){
                if(Articles == null) Articles = new List<Article>();
                Articles.AddRange(articles);
            }
        }

        private object _sourceLock = new object();
        public void AddSourcesThreadsafe(IEnumerable<SourceBase> sources)
        {
            lock(_sourceLock){
                if (Sources== null) Sources = new List<SourceBase>();
                Sources.AddRange(sources);
            }
        }

        private object _topicLock = new object();
        public void AddTopicsThreadsafe(IEnumerable<Topic> topics)
        {
            lock(_topicLock){
                if(Topics==null) Topics = new List<Topic>();
                Topics.AddRange(topics);
            }
        }

    }


    public class MetaData 
    {
        public string DataLocation {get;set;}
        public DateTime? DateCreated {get;set;}
    }
}



