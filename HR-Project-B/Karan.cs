using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HR_Project_B
{
    class Karan
    {
        public static void Start()
        {
            while (true)
            {
                int role = Program.account.role;

                switch (role)
                {
                    case 0:
                        if (!guestMenu())
                        {
                            return;
                        }
                        break;
                    case 1:
                        if (!customerMenu())
                        {
                            return;
                        }
                        break;
                    case 2:
                        if (!chefMenu())
                        {
                            return;
                        }
                        break;
                    case 3:
                        if (!mangerMenu())
                        {
                            return;
                        }
                        break;
                    case 4:
                        if (!adminMenu())
                        {
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private static bool customerMenu()
        {
            List<string> menuItems = new List<string>() {
            "Log in",
            "Sign up",
            "Menu",
            "Order",
            "Logout"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = menuDrawing.drawMenu(menuItems);
                if (selectedMenuItem == "Log in")
                {
                    Console.Clear();
                    Console.WriteLine("Log Hier In !!"); Console.Read();
                }
                else if (selectedMenuItem == "Sign up")
                {
                    Console.Clear();
                    Console.WriteLine("Vul Hier U Gegevens In"); Console.Read();
                }
                else if (selectedMenuItem == "Menu")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier Het Menu"); Console.Read();
                }

                else if (selectedMenuItem == "Order")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier U Order"); Console.Read();

                }

                else if (selectedMenuItem == "Logout")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool guestMenu()
        {
            List<string> menuItems = new List<string>() {
            "Log in",
            "Sign up",
            "Menu",
            "Logout"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = menuDrawing.drawMenu(menuItems);
                if (selectedMenuItem == "Log in")
                {
                    Console.Clear();
                    Console.WriteLine("Log Hier In !!"); Console.Read();
                }
                else if (selectedMenuItem == "Sign up")
                {
                    Console.Clear();
                    Console.WriteLine("Vul Hier U Gegevens In"); Console.Read();
                }
                else if (selectedMenuItem == "Menu")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier Het Menu"); Console.Read();
                }

                else if (selectedMenuItem == "Logout")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool chefMenu()
        {
            List<string> menuItems = new List<string>() {
            "Log in",
            "Sign up",
            "Menu",
            "Order",
            "Logout"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = menuDrawing.drawMenu(menuItems);
                if (selectedMenuItem == "Log in")
                {
                    Console.Clear();
                    Console.WriteLine("Log Hier In !!"); Console.Read();
                }
                else if (selectedMenuItem == "Sign up")
                {
                    Console.Clear();
                    Console.WriteLine("Vul Hier U Gegevens In"); Console.Read();
                }
                else if (selectedMenuItem == "Menu")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier Het Menu or change the menu"); Console.Read();
                }

                else if (selectedMenuItem == "Order")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier U Order"); Console.Read();

                }

                else if (selectedMenuItem == "Logout")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool mangerMenu()
        {
            List<string> menuItems = new List<string>() {
            "Log in",
            "Sign up",
            "Menu",
            "Order",
            "Logout"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = menuDrawing.drawMenu(menuItems);
                if (selectedMenuItem == "Log in")
                {
                    Console.Clear();
                    Console.WriteLine("Log Hier In !!"); Console.Read();
                }
                else if (selectedMenuItem == "Sign up")
                {
                    Console.Clear();
                    Console.WriteLine("Vul Hier U Gegevens In"); Console.Read();
                }
                else if (selectedMenuItem == "Menu")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier Het Menu"); Console.Read();
                }

                else if (selectedMenuItem == "Order")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier U Order or change the orders"); Console.Read();

                }

                else if (selectedMenuItem == "Logout")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool adminMenu()
        {
            List<string> menuItems = new List<string>() {
            "Log in",
            "Sign up",
            "Menu",
            "Order",
            "Logout"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = menuDrawing.drawMenu(menuItems);
                if (selectedMenuItem == "Log in")
                {
                    Console.Clear();
                    Console.WriteLine("Log Hier In !!"); Console.Read();
                }
                else if (selectedMenuItem == "Sign up")
                {
                    Console.Clear();
                    Console.WriteLine("Vul Hier U Gegevens In"); Console.Read();
                }
                else if (selectedMenuItem == "Menu")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier Het Menu"); Console.Read();
                }

                else if (selectedMenuItem == "Order")
                {
                    Console.Clear();
                    Console.WriteLine("Zie Hier U Order or change the orders"); Console.Read();

                }

                else if (selectedMenuItem == "Logout")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }


    }
}
