using System;
using System.Collections.Generic;

namespace EcommercePrototype
{
    public class UserAuthentication
    {
        // To store the details of users
        private Dictionary<string, string> users;

        // Constructor for the class where initialization is done 
        public UserAuthentication()
        {
            users = new Dictionary<string, string>();
        }

        // Function to add users to the 
        public bool RegisterUser(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                return false; // Username already exists
            }

            users.Add(username, password);
            return true; // User registered successfully
        }

        public bool AuthenticateUser(string username, string password)
        {
            if (users.TryGetValue(username, out string storedPassword))
            {
                return storedPassword == password; // Return true if password matches
            }

            return false; // Username not found
        }

        public List<string> ListUsers()
        {
            return new List<string>(users.Keys); // Return the list of usernames
        }
    }
}
