using System.Collections.Generic;
using ThreadedCracker.Model;

namespace ThreadedCracker.Handlers
{
    public class CrackingHandler
    {
        private static List<UserDataCleatTxt> UserData = new List<UserDataCleatTxt>();


        public static void RunCracking(List<UserData> passwords, List<string> dictionary)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                string word = dictionary[i];
                List<UserDataCleatTxt> partialResult = StringHandler.WordController(word, passwords);
            }
        }
    }
}