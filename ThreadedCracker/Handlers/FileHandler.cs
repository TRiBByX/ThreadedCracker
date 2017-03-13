﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ThreadedCracker.Model;

namespace ThreadedCracker.Handlers
{
    public static class FileHandler
    {
        public static Collection<List<string>> Dictionaries = new Collection<List<string>>();
        public static List<UserData> Passwords = new List<UserData>();


        private const int MaxChunckSize = 50000;

        public static void LoadDictionary()
        {
            var chunk = new List<string>();
            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    while (!r.EndOfStream)
                    {
                        chunk.Add(r.ReadLine());
                        if (chunk.Count == MaxChunckSize)
                        {
                            Dictionaries.Add(chunk);
                            chunk = new List<string>();
                        }
                    }
                    Dictionaries.Add(chunk);
                }
            }
        }

        /// <summary>
        /// Loads in a passwordfile by filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static void LoadPasswords(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(":".ToCharArray());
                    UserData usrinf = new UserData(parts[0], parts[1]);
                    Passwords.Add(usrinf);
                }
            }
        }

        public static void SaveCrackedPasswords()
        {
            FileStream fs = new FileStream("CrackedPasswords.txt", FileMode.Create, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                
            }
        }


    }
}