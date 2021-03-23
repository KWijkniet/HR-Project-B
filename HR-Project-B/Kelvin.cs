using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace HR_Project_B
{
    class Kelvin
    {
        private static Account[] accounts;
        public static void Start()
        {
            Console.CursorVisible = false;
            while (true)
            {
                DebugMode();
            }
        }

        #region Debug Accounts
        // Show Option menu
        private static void DebugMode()
        {
            string title = "What do you want to do?";
            string[] options = new string[]
            {
                "Create random users",
                "Save accounts",
                "Load accounts",
                "Show accounts",
                "Update account",
                "Create account"
            };
            OptionMenu menu = new OptionMenu(title, options);
            int response = menu.Display();

            switch (response)
            {
                case 0:
                    GenerateAccounts();
                    Console.WriteLine("Accounts have been generated");
                    break;
                case 1:
                    SaveAccounts();
                    Console.WriteLine("Accounts have been saved to the JSON");
                    break;
                case 2:
                    LoadAccounts();
                    Console.WriteLine("Accounts have been loaded from the JSON");
                    break;
                case 3:
                    ShowAccounts();
                    break;
                case 4:
                    UpdateAccount();
                    break;
                case 5:
                    CreateAccount();
                    Console.WriteLine("Account has been created!");
                    break;
                default:
                    break;
            }
        }

        // Randomly generate accounts (resets current list)
        private static void GenerateAccounts()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random();

            int amount = random.Next(1, 10);
            accounts = new Account[amount];

            for (int i = 0; i < amount; i++)
            {
                int nameLength = random.Next(4, 10);
                string name = new string(Enumerable.Repeat(chars, nameLength)
                  .Select(s => s[random.Next(s.Length)]).ToArray());

                accounts[i] = new Account(name);
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
        private static void SaveAccounts()
        {
            FileManager fm = new FileManager("Accounts.json", new string[] { "Data" });

            dynamic[] foundAccounts = new dynamic[accounts.Length];
            for (int i = 0; i < accounts.Length; i++)
            {
                foundAccounts[i] = accounts[i];
            }

            fm.WriteJSON(foundAccounts);
        }

        // Display a list of users
        private static void ShowAccounts()
        {
            string title = "Accounts:\n";
            string[] options = new string[] { "back" };

            if(accounts != null && accounts.Length > 0)
            {
                for (int i = 0; i < accounts.Length; i++)
                {
                    title += (i + 1) + ". " + accounts[i].name + "\n";
                }
            }
            else
            {
                title += "NO ACCOUNTS FOUND\n";
            }

            Console.Clear();
            OptionMenu menu = new OptionMenu(title, options);
            menu.Display();
            Console.Clear();
            return;
        }

        // Select an account and updates its name
        private static void UpdateAccount()
        {
            string title = "What account would you like to edit?\n";
            string[] options = new string[accounts.Length + 1];
            options[options.Length - 1] = "Back";

            if (accounts != null && accounts.Length > 0)
            {
                for (int i = 0; i < accounts.Length; i++)
                {
                    options[i] = accounts[i].name;
                }
            }
            else
            {
                title += "NO ACCOUNTS FOUND\n";
            }

            Console.Clear();
            OptionMenu menu = new OptionMenu(title, options);
            int index = menu.Display();

            Console.WriteLine("What should the new name be?");
            string name = Console.ReadLine();

            Account target = accounts[index];
            target.name = name;

            SaveAccounts();
            Console.Clear();
        }

        // Create a new account
        private static void CreateAccount()
        {
            //Console.WriteLine("What should the name be?");
            //string name = Console.ReadLine();
            //Console.WriteLine("What should the password be?");
            //string password = Console.ReadLine();
            //Console.Clear();

            //string title = "Are you sure you want to create this account?\n";
            //string[] options = new string[] { "Confirm", "Back" };
            //OptionMenu menu = new OptionMenu(title, options);
            //menu.Display();
            //Console.Clear();

            //Account account = new Account(name, password);

            //Account[] temp = new Account[accounts.Length + 1];
            //for (int i = 0; i < accounts.Length; i++)
            //{
            //    temp[i] = accounts[i];
            //}
            //temp[^1] = account;
            //accounts = temp;

            //SaveAccounts();
        }
        #endregion
    }
}
