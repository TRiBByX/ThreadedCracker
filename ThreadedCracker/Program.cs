using System;
using System.Collections.Generic;
using ThreadedCracker.Handlers;

namespace ThreadedCracker
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            FileHandler.LoadDictionary();
            Console.WriteLine("Dictionary Loaded");
            FileHandler.LoadPasswords("passwords.txt");
            Console.WriteLine("Passwords Loaded");

            var pass = FileHandler.Passwords;
            var dict = FileHandler.Dictionaries;

            for (int i = 0; i < dict.Count; i++)
            {
                for (int j = 0; j < dict[i].Count; j++)
                {
                    CrackingHandler.RunCracking(pass, dict[i]);
                }
            }
        }
    }
}