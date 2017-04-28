// using System;

// namespace Rian.Cognitive 
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {   

//            var manager = new Manager();

//            manager.RunTopicDetectionSynchronous();

//          //   var returned = manager.DownloadLastTopicDetection();
            
//         //    Console.WriteLine("Returned: ");
//         //    PrintInfo(returned.Result, 10);


//         //   Console.Read();
          
//         }

//         private static void PrintInfo(TopicDetectionResponse response, int count){
//             response.Result.Topics.Sort( (r1, r2) => r2.Score.CompareTo(r1.Score));
            
//             for(int i = 0 ; i < count; i++){
//                 Console.WriteLine($"{response.Result.Topics[i].KeyPhrase} has score {response.Result.Topics[i].Score}");
//             }
//         }
//     }
// }
