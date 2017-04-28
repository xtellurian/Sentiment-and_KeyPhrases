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


        private static string GetLatestDataFileName = "GetLatestDataFunction.key";
        public static string GetLatestDataFunction() 
        {
             try
            {
                string text = System.IO.File.ReadAllText(GetLatestDataFileName);
                return text;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to read file with name " + GetLatestDataFileName, ex);
            }
        }

        private static string PollAndStoreV2FunctionFileName = "PollAndStoreV2Function.key";
        public static string GetPollAndStoreV2AzureFunction() 
        {
             try
            {
                string text = System.IO.File.ReadAllText(PollAndStoreV2FunctionFileName);
                return text;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to read file with name " + PollAndStoreV2FunctionFileName, ex);
            }
        }

        private static string PollAndStoreFunctionFileName = "PollAndStoreFunction.key";
        public static string GetPollAndStoreAzureFunction() 
        {
             try
            {
                string text = System.IO.File.ReadAllText(PollAndStoreFunctionFileName);
                return text;
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to read file with name " + PollAndStoreFunctionFileName, ex);
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
