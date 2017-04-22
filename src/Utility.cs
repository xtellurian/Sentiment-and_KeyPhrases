using System;

namespace Rian.Cognitive {
        
    public static class Utility 
    {
        private static string CognitiveServicesApiKeyFileName = "CognitiveServicesTextApi.key";
        private static string NewsApiKeyFileName = "NewsApi.key";
        public static string LoadCognitiveServicesTextApiKey() 
        {
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
    }
}
