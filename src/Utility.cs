using System;

namespace Rian.Cognitive {
        
    public static class Utility 
    {
        private static string _newsApiKey;
        public static void SetNewsApiKey(string key){
            _newsApiKey = key;
        }
        private static string _cognitiveServicesTextApiKey;
        public static void SetCognitiveServicesTextApiKey(string key){
            _cognitiveServicesTextApiKey = key;
        }


        private static string CognitiveServicesApiKeyFileName = "CognitiveServicesTextApi.key";
        private static string NewsApiKeyFileName = "NewsApi.key";
        public static string LoadCognitiveServicesTextApiKey() 
        {
            if (!string.IsNullOrEmpty(_cognitiveServicesTextApiKey)) {
                return _cognitiveServicesTextApiKey;
            }
            
            try
            {
                string text = System.IO.File.ReadAllText(CognitiveServicesApiKeyFileName);
            return text;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to read file with name " + CognitiveServicesApiKeyFileName, ex);
            }
            
        }

        public static string LoadNewsApiKey() 
        {
            if (!string.IsNullOrEmpty(_newsApiKey)){
                return _newsApiKey;
            }
            try
            {
                string text = System.IO.File.ReadAllText(NewsApiKeyFileName);
                return text;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to read file with name " + NewsApiKeyFileName, ex);
            }
            
        }

        private static string StorageConnectionStringFileName = "StorageConnectionString.key";
        private static string StorageConnectionString;
        public static string LoadStorageConnectionString() 
        {
            if (!string.IsNullOrEmpty(StorageConnectionString)){
                return StorageConnectionString;
            }
            try
            {
                string text = System.IO.File.ReadAllText(StorageConnectionStringFileName);
                return text;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to read file with name " + StorageConnectionStringFileName, ex);
            }
        }
    }
}
