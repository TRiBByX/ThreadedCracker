using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ThreadedCracker.Model;

namespace ThreadedCracker.Handlers
{
    public class StringHandler
    {
        /// <summary>
        /// Takes in a word alteres it and checks sends it to checksingle word
        /// Takes a string word and a userdata object list.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="userDatas"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Takes in an altered word to then see if it's the same hash value as the password
        /// Takes a list of userdata objects and a string possible password
        /// </summary>
        /// <param name="userInfos"></param>
        /// <param name="possiblePassword"></param>
        /// <returns></returns>
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
        /// Checks each byte and compares to the possbile password and the user password.
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

        /// <summary>
        /// Reverses a string.
        /// Example: 'abc' = 'cba'
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string ReverseString(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return chars.ToString();
        }
        //Converts to a char array to byte array.
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