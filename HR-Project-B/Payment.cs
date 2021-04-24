using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HR_Project_B.Components;

namespace HR_Project_B

{
    class Payment
    {
        public static string[] GetUserPaymentInformation(int userRole) //Program.account.role
        {
            if (userRole == 0)
            {
                Text line1 = new Text("Please fill in the information below:\n", ConsoleColor.Yellow); line1.Display();

                Input nameInput = new Input(new Text("\nName:"), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z "));
                string userName = nameInput.Display();

                string userEmail;
                while (true)
                {
                    Input emailInput = new Input(new Text("\nEmail:"), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z0-9_.-@"));
                    userEmail = emailInput.Display();
                    if (!Regex.IsMatch(userEmail, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                    {
                        Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red); error.Display();
                        continue;
                    }
                    else { break; }
                }

                Input phoneInput = new Input(new Text("\nPhone Number:"), new Text("\nPlease enter a valid phone number!", ConsoleColor.Red), new InputSettings(false, 10, 10, "0-9"));
                string userPhone = "+31" + phoneInput.Display();

                return new string[] { Program.account.id, userName, userEmail, userPhone };
            }
            else { return new string[] { Program.account.id, Program.account.name, Program.account.email, Program.account.phone, };}
        }

        public static double CalculateTotalPrice(Tuple<string, int, double>[] itemInfo) //Takes array with tuples containing <iten name, item amount, item price>
        {
            double totalPrice = 0;

            for (int i = 0; i < itemInfo.Length; i++)
            {
                totalPrice += itemInfo[i].Item3 * itemInfo[i].Item2;
            }
            return totalPrice;
        }

        public static void ShowReceipt(Tuple<string, int, double>[] itemInfo) //Should take array with tuples containing <iten name, item amount, item price>
        {
            //Dummy Values
            Tuple<string, int, double> item1 = Tuple.Create("Steak", 2, 7.50);
            Tuple<string, int, double> item2 = Tuple.Create("Pasta", 1, 6.0);
            Tuple<string, int, double> item3 = Tuple.Create("Coca Cola", 3, 2.0);
            Tuple<string, int, double> item4 = Tuple.Create("Ice Cream", 3, 4.50);
            //Tuple<string, int, double>[] pickedMenuItemInfo = itemInfo;
            Tuple<string, int, double>[] pickedMenuItemInfo = new Tuple<string, int, double>[] { item1, item2, item3, item4 };

            for (int i = 0; i < pickedMenuItemInfo.Length; i++)
            {
                Console.WriteLine("{0,-20} {1,7}", $"{pickedMenuItemInfo[i].Item1} ({pickedMenuItemInfo[i].Item2}x)", $"{ pickedMenuItemInfo[i].Item3 * pickedMenuItemInfo[i].Item2} $");
            }
            Console.WriteLine("----------------- +"); Console.WriteLine($"Total price = {CalculateTotalPrice(pickedMenuItemInfo)} $");
        }
    }
}