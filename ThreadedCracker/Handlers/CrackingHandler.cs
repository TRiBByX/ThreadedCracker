using System;
using System.Collections.Generic;
using ThreadedCracker.Model;

namespace ThreadedCracker.Handlers
{
    public class CrackingHandler
    {
        private static List<UserDataCleatTxt> UserData = new List<UserDataCleatTxt>();


        public static void RunCracking(List<UserData> passwords, List<string> dictionary)
        {
            var start = DateTime.Now;
            for (int i = 0; i < dictionary.Count; i++)
            {
                string word = dictionary[i];
                List<UserDataCleatTxt> partialResult = StringHandler.WordController(word, passwords);
            }
            var end = DateTime.Now;
            Console.WriteLine(TimeCalculation(start, end));
        }

        public static string TimeCalculation(DateTime start, DateTime end)
        {
            TimeSpan elapsedTime = end - start;
            return $"Time taken: {elapsedTime}";
        }
    }
}