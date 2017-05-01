using System.Collections.Generic;

namespace Rian.Cognitive
{
    public class TopicDetectionAggregate : TopicDetectionResponse 
    {
        public List<SourceBase> Sources {get;set;}
    }
}