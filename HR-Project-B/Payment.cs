using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HR_Project_B.Components;

namespace HR_Project_B

{
    class Payment
    {
        private static MenuCategory[] categories;
        private static FileManager fm = new FileManager("Menu.json");
        private static ShoppingBasket basket = new ShoppingBasket();
        private static FileManager pf = new FileManager("PayFood.json");

        public static void Start(ShoppingBasket _basket, MenuCategory[] _categories)
        {
            categories = _categories;
            basket = _basket;
            Display();
        }

        public static void Pay()
        {
            string name = Program.account.name;
            string email = Program.account.email;
            if (Program.account.role == 0)
            {
                Input nameInput = new Input(new Text("\nName: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z ", "", false));
                name = nameInput.Display();

                while (true)
                {
                    Input emailInput = new Input(new Text("\nEmail: "), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z0-9_.-@", "", false));
                    email = emailInput.Display();
                    if (!Regex.IsMatch(email, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                    {
                        Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red); error.Display();
                        continue;
                    }
                    else { break; }
                }
            }

            CustomerReservations.LoadReservation();
            while (true)
            {
                Input reservationInput = new Input(new Text("\nReservation Code: "), new Text("\nPlease enter a valid reservation code!", ConsoleColor.Red), new InputSettings(false, 8, 8, "A-Za-z0-9-", "", false));
                string id = reservationInput.Display();

                Reservation[] reservations = CustomerReservations.reservations;
                Reservation foundReservation = null;
                foreach (Reservation reservation in reservations)
                {
                    if(reservation.orderID == id && reservation.status == "Canceled")
                    {
                        foundReservation = reservation;
                        break;
                    }
                }

                if (foundReservation == null)
                {
                    Text error = new Text("\nPlease enter a valid Reservation code. If the reservation has been canceled you cant pay for it", ConsoleColor.Red);
                    error.Display();
                    continue;
                }

                foundReservation.status = "Paid";
                CustomerReservations.SaveReservation();
                break;
            }

            //complete payment
            Console.WriteLine("");
            Menu menu = new Menu(new Text("Order has successfully been paid. Press enter to continue."));
            menu.Display();
        }

        public static bool ValidateCreditCard(string creditcard)
        {
            if (creditcard == null) { return false; }
            int[] parts = new int[creditcard.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = (int)(creditcard[i] - '0');
            }

            for (int i = parts.Length - 2; i >= 0; i -= 2)
            {
                int temp = parts[i];
                temp *= 2;
                
                if(temp > 9)
                {
                    temp = temp % 10 + 1;
                }

                parts[i] = temp;
            }

            int total = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                total += parts[i];
            }

            return total % 10 == 0;
        }

        private static void Display()
        {
            Program.ClearConsole();
            Console.WriteLine("\nReceipt:");
            Console.OutputEncoding = Encoding.UTF8;

            Dictionary<string, int> items = basket.GetBasket();
            foreach (string id in items.Keys)
            {
                MenuItem item = IdToItem(id);
                Console.WriteLine("{0,-20} {1,7}", $"{item.name} ({items[id]}x)", $"€{ Math.Round(item.price * items[id],2):0.00}"); //:0.00 = 5.9 --> 5.90
            }

            Console.WriteLine("----------------- +");
            Console.WriteLine($"Total price = €{Math.Round(CalculatePrice(),2):0.00} "); //:0.00 = 5.9 --> 5.90
        }

        private static double CalculatePrice()
        {
            double result = 0;

            Dictionary<string, int> items = basket.GetBasket();
            foreach (string id in items.Keys)
            {
                MenuItem item = IdToItem(id);
                result += item.price * items[id];
            }

            return result;
        }

        private static MenuItem IdToItem(string id)
        {
            foreach (MenuCategory category in categories)
            {
                foreach (MenuItem item in category.items)
                {
                    if(id == item.id)
                    {
                        return item;
                    }
                }
            }

            return null;
        }
        private static void SavePayFood(PayFood data)
        {

            dynamic[] found = pf.ReadJSON();
            PayFood[] orderedFood = new PayFood[found.Length];
            for (int i = 0; i < found.Length; i++)
            {
                PayFood item = new PayFood(found[i]);

                orderedFood[i] = item;
            }

            found = new dynamic[orderedFood.Length + 1];
            for (int i = 0; i < orderedFood.Length; i++)
            {
                found[i] = orderedFood[i];
            }
            found[^1] = data;

            pf.WriteJSON(found);
        }
        //old
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
            Console.Clear();
            //Dummy Values
            Tuple<string, int, double> item1 = Tuple.Create("Steak", 2, 7.50);
            Tuple<string, int, double> item2 = Tuple.Create("Pasta", 1, 6.0);
            Tuple<string, int, double> item3 = Tuple.Create("Coca Cola", 3, 2.0);
            Tuple<string, int, double> item4 = Tuple.Create("Ice Cream", 3, 4.50);
            //Tuple<string, int, double>[] pickedMenuItemInfo = itemInfo;
            Tuple<string, int, double>[] pickedMenuItemInfo = new Tuple<string, int, double>[] { item1, item2, item3, item4 };

            Console.WriteLine("");
            Console.WriteLine("Receipt:");
            for (int i = 0; i < pickedMenuItemInfo.Length; i++)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("{0,-20} {1,7}", $"{pickedMenuItemInfo[i].Item1} ({pickedMenuItemInfo[i].Item2}x)", $"€{ pickedMenuItemInfo[i].Item3 * pickedMenuItemInfo[i].Item2:0.00}");
            }
            Console.WriteLine("----------------- +"); Console.WriteLine($"Total price = €{CalculateTotalPrice(pickedMenuItemInfo):0.00}");
        }
    
    }
}
