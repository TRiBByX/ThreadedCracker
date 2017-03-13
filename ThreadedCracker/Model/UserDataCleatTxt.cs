using System;

namespace ThreadedCracker.Model
{
    public class UserDataCleatTxt
    {
        /// <summary>
        /// Basic unectrypted version of the userinfo from file.
        /// </summary>
        public string Username { get; set; }
        public string Password { get; set; }

        public UserDataCleatTxt(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");

            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return $"{Username} : {Password}";
        }
    }
}