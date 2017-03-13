using System;

namespace ThreadedCracker.Model
{
    public class UserData
    {
        /// <summary>
        /// Overall this is just a standard class. Made with the mvvm pattern in mind.
        /// </summary>
        public string Username { get; set; }
        public string Base64EncyptedPass { get; set; }
        public byte[] EncryptedPass { get; set; }


        public UserData(string username, string encryptedPassBase64)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (encryptedPassBase64 == null) throw new ArgumentNullException("encryptedPass");

            Username = username;
            Base64EncyptedPass = encryptedPassBase64;
            EncryptedPass = Convert.FromBase64String(encryptedPassBase64);
        }

        public override string ToString()
        {
            return $"{Username} : {Base64EncyptedPass}";
        }
    }
}