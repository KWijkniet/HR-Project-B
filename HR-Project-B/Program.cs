using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HR_Project_B
{
    class Program
    {
        public static Account[] accounts;
        public static Account account;

        private static void Main(string[] args)
        {
            LoadAccounts();

            while (true)
            {
                //Clear on start
                Console.Clear();
                //Show login screen
                Register.Start();
                //If succesfull login
                if(account != null)
                {
                    //Show blank page
                    Console.Clear();
                    //Show menu based on role
                    Karan.Start();
                }
                //Kelvin.Start();
            }
        }

        // Load accounts from the file
        private static void LoadAccounts()
        {
            FileManager fm = new FileManager("Accounts.json", new string[] { "Data" });

            dynamic[] foundAccounts = fm.ReadJSON();
            accounts = new Account[foundAccounts.Length];
            for (int i = 0; i < foundAccounts.Length; i++)
            {
                Account account = new Account(foundAccounts[i]);

                accounts[i] = account;
            }
        }

        // Save accounts to the file
        public static void SaveAccounts()
        {
            FileManager fm = new FileManager("Accounts.json", new string[] { "Data" });

            dynamic[] foundAccounts = new dynamic[accounts.Length];
            for (int i = 0; i < accounts.Length; i++)
            {
                foundAccounts[i] = accounts[i];
            }

            fm.WriteJSON(foundAccounts);
        }
    }
}
