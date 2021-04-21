using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
        public static Text restaurantText = new Text("Jake Darcy's restaurant\n", ConsoleColor.Yellow);

        private static void Main(string[] args)
        {
            ClearConsole();
            Text message = new Text("Hello there! Welcome to my restaurant. What is your name?");
            Text error = new Text("Sorry but i dont think that is real name.", ConsoleColor.Red);
            Input input = new Input(message, error, new InputSettings(true));
            string name = input.Display();

            while (true)
            {
                ClearConsole();
                message = new Text("Welcome " + name + "!\nWhat can we do for you?");
                Text[] messages = new Text[]
                {
                    new Text("View menu"),
                    new Text("View information"),
                    new Text("Make reservation"),
                    new Text("Order take-away"),
                    new Text("Leave"),
                };

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                switch (selected)
                {
                    case 4:
                        ClearConsole();
                        message = new Text("Goodbye!\n");
                        message.Display(true);

                        menu = new Menu(new Text("Press enter to leave", default, default, ConsoleColor.Gray, ConsoleColor.Red));
                        menu.Display();
                        return;
                    default:
                        ClearConsole();
                        message = new Text("Coming soon!\n");
                        message.Display(true);

                        menu = new Menu(new Text("Press enter to leave"));
                        menu.Display();
                        break;
                }
            }

            return;

            LoadAccounts();
            Console.CursorVisible = false;
          
            while (true)
            {
                //Clear on start
                Console.Clear();
                //Show login screen
                Register.Start();
                //If succesfull login
                if (account != null)
                {
                    //Show blank page
                    Console.Clear();
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
