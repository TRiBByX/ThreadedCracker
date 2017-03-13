using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ThreadedCracker.Model;

namespace ThreadedCracker.Handlers
{
    public class StringHandler
    {
        public static List<UserDataCleatTxt> WordController(string word, List<UserData> userDatas)
        {
            List<UserDataCleatTxt> result = new List<UserDataCleatTxt>();

            //Check plain word
            string possiblePassword = word;
            List<UserDataCleatTxt> partialResult = CheckSingleWord(userDatas, possiblePassword);
            result.AddRange(partialResult);

            //Checks all capital letters.
            string possibleCapitalizedPassword = word.ToUpper();
            partialResult = CheckSingleWord(userDatas, possibleCapitalizedPassword);
            result.AddRange(partialResult);

            //Checks capitalized first letter.
            string possibleCapitalizedFirstLetterPassword = word.First().ToString().ToUpper() + word.Substring(1);
            partialResult = CheckSingleWord(userDatas, possibleCapitalizedFirstLetterPassword);
            result.AddRange(partialResult);

            //Checks a reversed password.
            string possibleReversedPassword = ReverseString(word);
            partialResult = CheckSingleWord(userDatas, possibleReversedPassword);
            result.AddRange(partialResult);

            //Checks if the password has a digit at the end.
            for (int i = 0; i < 100; i++)
            {
                string possiblePasswordDigit = word + i;
                partialResult = CheckSingleWord(userDatas, possiblePasswordDigit);
                result.AddRange(partialResult);
            }

            //Checks if the password has a digit at the start
            for (int i = 0; i < 100; i++)
            {
                string possibleDigitPassword = i + word;
                partialResult = CheckSingleWord(userDatas, possibleDigitPassword);
                result.AddRange(partialResult);
            }
            //checks if the password has a digit at the end and the start.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    string possibleDigitPasswordDigit = i + word + j;
                    partialResult = CheckSingleWord(userDatas, possibleDigitPasswordDigit);
                    result.AddRange(partialResult);
                }
            }
            return result;
        }


        private static List<UserDataCleatTxt> CheckSingleWord(List<UserData> userInfos, string possiblePassword)
        {
            var PassCount = 0;
            char[] charArray = possiblePassword.ToCharArray();

            byte[] passwordAsBytes = Array.ConvertAll(charArray, StringHandler.GetConverter());

            byte[] hashedPassword = new SHA1CryptoServiceProvider().ComputeHash(passwordAsBytes);

            List<UserDataCleatTxt> results = new List<UserDataCleatTxt>();

            foreach (var encryptedUserInfo in userInfos)
            {
                if (CompareBytes(encryptedUserInfo.EncryptedPass, hashedPassword))
                {
                    results.Add(new UserDataCleatTxt(encryptedUserInfo.Username, possiblePassword));
                    PassCount++;
                    Console.WriteLine($"{encryptedUserInfo.Username} {possiblePassword}");
                }
            }
            return results;
        }

        /// <summary>
        /// Validates whether the password has been found.
        /// </summary>
        /// <param name="firstArray"></param>
        /// <param name="secondArray"></param>
        /// <returns></returns>
        private static bool CompareBytes(IList<byte> firstArray, IList<byte> secondArray)
        {
            if (firstArray.Count != secondArray.Count) return false;
            for (int i = 0; i < firstArray.Count; i++)
            {
                if (firstArray[i] != secondArray[i]) return false;
            }
            return true;
        }

        private static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return chars.ToString();
        }

        private static readonly Converter<char, byte> Converter = CharToByte;

        private static byte CharToByte(char ch)
        {
            return Convert.ToByte(ch);
        }

        public static Converter<char, byte> GetConverter()
        {
            return Converter;
        }
    }
}