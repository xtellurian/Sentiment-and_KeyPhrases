using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SentimentalNews.Model
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("score")]
        public double Score { get; set; }
        [JsonProperty("detail")]
        public Detail Detail { get; set; }
    }

    public class Caption
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("confidence")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double Confidence { get; set; }
    }

    public class Description
    {
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("captions")]
        public List<Caption> Captions { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
    }

    public class AnalyseImageResponse
    {
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }
        [JsonProperty("description")]
        public Description Description { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; }
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

        
    public class FaceRectangle
    {
        [JsonProperty("left")]
        public int Left { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class Celebrity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("faceRectangle")]
        public FaceRectangle FaceRectangle { get; set; }
        [JsonProperty("confidence")]
        public double Confidence { get; set; }
    }

    public class Detail
    {
        [JsonProperty("celebrities")]
        public List<Celebrity> Celebrities { get; set; }
    }

}
