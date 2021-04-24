using System;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class Program
    {
        /*
            Role id's:
            0 = Guest
            1 = Customer
            2 = Chef
            3 = Manager
            4 = Admin 
        */
        public static Account[] accounts;
        public static Account account;
        public static Text restaurantText = new Text(@"
             _____  ______  __  __   ____        ____    ______  ____    ____     __    __ 
            /\___ \/\  _  \/\ \/\ \ /\  _`\     /\  _`\ /\  _  \/\  _`\ /\  _`\  /\ \  /\ \
            \/__/\ \ \ \L\ \ \ \/'/'\ \ \L\_\   \ \ \/\ \ \ \L\ \ \ \L\ \ \ \/\_\\ `\`\\/'/
               _\ \ \ \  __ \ \ , <  \ \  _\L    \ \ \ \ \ \  __ \ \ ,  /\ \ \/_/_`\ `\ /' 
              /\ \_\ \ \ \/\ \ \ \\`\ \ \ \L\ \   \ \ \_\ \ \ \/\ \ \ \\ \\ \ \L\ \ `\ \ \ 
              \ \____/\ \_\ \_\ \_\ \_\\ \____/    \ \____/\ \_\ \_\ \_\ \_\ \____/   \ \_\
               \/___/  \/_/\/_/\/_/\/_/ \/___/      \/___/  \/_/\/_/\/_/\/ /\/___/     \/_/
        ", ConsoleColor.Yellow);

        private static void Main(string[] args)
        {
            LoadAccounts();
            Console.CursorVisible = false;
          
            while (true)
            {
                //Clear on start
                ClearConsole();
                //Show login screen
                Register.Start();
                //If succesfull login
                if (account != null)
                {
                    //Show blank page
                    ClearConsole();
                    //Show menu based on role
                    Dashboard.Start();
                }
            }
        }

        public static void ClearConsole()
        {
            Console.Clear();
            restaurantText.Display(true);
        }

        // Load accounts from the file
        private static void LoadAccounts()
        {
            FileManager fm = new FileManager("Accounts.json");

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
            FileManager fm = new FileManager("Accounts.json");

            dynamic[] foundAccounts = new dynamic[accounts.Length];
            for (int i = 0; i < accounts.Length; i++)
            {
                foundAccounts[i] = accounts[i];
            }

            fm.WriteJSON(foundAccounts);
        }
    }
}
