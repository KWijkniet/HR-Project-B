using System;
using System.Text.RegularExpressions;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class Register
    {
        public static void Start()
        {
            Text message = new Text("Welcome to Jake Darcy's restaurant app! Please select one of the options below.");
            Text[] messages = new Text[]
            {
                new Text("Login"),
                new Text("Register an account"),
                new Text("Continue as guest")
            };

            Menu menu = new Menu(message, messages);
            int selected = menu.Display();

            Program.ClearConsole();
            switch (selected)
            {
                case 0:
                    Login();
                    break;
                case 1:
                    RegisterAccount();
                    break;
                case 2:
                    Guest();
                    break;
                default:
                    break;
            }

            return;
        }

        public static void Login()
        {
            while (true)
            {
                Input emailInput = new Input(new Text("\nEmail:"), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z0-9_.-@"));
                string email = emailInput.Display();
                if (email == null)
                {
                    return;
                }
                else if (!Regex.IsMatch(email, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                {
                    Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red);
                    error.Display();
                    continue;
                }

                Input passwordInput = new Input(new Text("\nPassword:"), new Text("\nPlease enter a valid password!", ConsoleColor.Red), new InputSettings(false, 1, 999, "A-Za-z0-9"));
                string password = passwordInput.Display();
                if (password == null)
                {
                    return;
                }

                bool hasFound = false;
                foreach (Account item in Program.accounts)
                {
                    if (item.email == email && item.password == password)
                    {
                        Program.account = item;
                        return;
                    }
                }

                if (!hasFound)
                {
                    Text error = new Text("\nIncorrect email or password. Please try again.", ConsoleColor.Red);
                    error.Display();
                    continue;
                }
            }
        }

        public static void RegisterAccount()
        {
            while (true)
            {
                Input emailInput = new Input(new Text("\nEmail:"), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z0-9_.-@"));
                string email = emailInput.Display();
                if (email == null)
                {
                    return;
                }
                else if (!Regex.IsMatch(email, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                {
                    Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red);
                    error.Display();
                    continue;
                }

                bool hasFound = false;
                foreach (Account item in Program.accounts)
                {
                    if(item.email == email)
                    {
                        hasFound = true;
                        break;
                    }
                }

                if (hasFound)
                {
                    Text error = new Text("\nEmail already in use. Please try again.", ConsoleColor.Red);
                    error.Display();
                    continue;
                }

                Input nameInput = new Input(new Text("\nName:"), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z "));
                string name = nameInput.Display();
                if(name == null)
                {
                    return;
                }

                Input passwordInput = new Input(new Text("\nPassword:"), new Text("\nPlease enter a valid password!", ConsoleColor.Red), new InputSettings(false, 8, 128, "A-Za-z0-9"));
                string password = passwordInput.Display();
                if (password == null)
                {
                    return;
                }

                Input phoneInput = new Input(new Text("\nPhone Number:"), new Text("\nPlease enter a valid phone number!", ConsoleColor.Red), new InputSettings(false, 10, 10, "0-9"));
                string phone = "+31" + phoneInput.Display();
                if (phone == null)
                {
                    return;
                }

                Account account = new Account(name, 1, email, phone, password);

                Account[] temp = new Account[Program.accounts.Length + 1];
                for (int i = 0; i < Program.accounts.Length; i++)
                {
                    temp[i] = Program.accounts[i];
                }
                temp[^1] = account;
                
                Program.accounts = temp;
                Program.account = account;

                Program.SaveAccounts();
                return;
            }
        }

        public static void Guest()
        {
            foreach (Account acc in Program.accounts)
            {
                if (acc.name == "Guest" && acc.password == "" && acc.id == "0")
                {
                    Program.account = acc;
                    return;
                }
            }
        }
    }
}