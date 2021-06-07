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

        public static bool ValidateCreditCard(string creditcard) //Validates if creditcard info is valid according to creditcard rules
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

        private static void Display() //Displays order in a receipt format
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

        private static double CalculatePrice() //Calculates the total price in basket
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
    }
}
