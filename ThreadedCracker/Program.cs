using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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

//            Console.WriteLine(dict[6].Count);

//            List<Task> tasks = new List<Task>();
//
//            for (int i = 0; i < dict.Count; i++)
//            {
//                var t = new Task(() => { CrackingHandler.RunCracking(pass, dict[i]); });
//                tasks.Add(t);
//                t.Start();
//                Console.WriteLine($"Task: {i}, has been started");
//                t.Wait();
//            }

            List<ThreadStart> threads = new List<ThreadStart>();
            for (int i = 0; i < dict.Count; i++)
            {
                threads.Add(new ThreadStart( () => CrackingHandler.RunCracking(pass, dict[i]) ));
                new Thread(threads[i]).Start();
                Console.WriteLine($"Thread: {i}, has been started");
            }
        }
    }
}