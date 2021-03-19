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
                int role = roleMenu();
                Console.Clear();
                if (role == 1)
                {
                    if (!customerMenu()) // moeten de rollen komen te staan 
                    {
        
                        continue;
                    }
                    
                }
                if (role == 2)
                {
                    if (!guestMenu()) // moeten de rollen komen te staan 
                    {

                        continue;
                    }

                }
                if (role == 3)
                {
                    if (!chefMenu()) // moeten de rollen komen te staan 
                    {

                        continue;
                    }

                }
                if (role == 4)
                {
                    if (!mangerMenu()) // moeten de rollen komen te staan 
                    {

                        continue;
                    }

                }
                if (role == 5)
                {
                    if (!adminMenu()) // moeten de rollen komen te staan 
                    {

                        continue;
                    }
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
            "Back"};
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

                else if (selectedMenuItem == "Back")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static int roleMenu()
        {
            List<string> menuItems = new List<string>() {
            "Customer",
            "Guest",
            "Chef",
            "Manager",
            "Admin"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = menuDrawing.drawMenu(menuItems);
                if (selectedMenuItem == "Customer")
                {
                    Console.Clear();
                    Console.WriteLine("U bent nu customer"); 
                    return 1;
                }
                else if (selectedMenuItem == "Guest") 
                {
                    Console.Clear();
                    Console.WriteLine("U bent nu Guest"); return 2;
                }
                else if (selectedMenuItem == "Chef")
                {
                    Console.Clear();
                    Console.WriteLine("U bent nu chef"); return 3;
                }
                else if (selectedMenuItem == "Manager")
                {
                    Console.Clear();
                    Console.WriteLine("U bent nu manger"); return 4;
                }

                else if (selectedMenuItem == "Admin")
                {
                    Console.Clear();
                    Console.WriteLine("U bent nu admin"); return 5;

                }


                Console.Clear();
            }
            return 0;
        }
        private static bool guestMenu()
        {
            List<string> menuItems = new List<string>() {
            "Log in",
            "Sign up",
            "Menu",
            "Exit"};
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

                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
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
            "Back"};
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

                else if (selectedMenuItem == "Back")
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
            "Back"};
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

                else if (selectedMenuItem == "Back")
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
            "Back"};
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

                else if (selectedMenuItem == "Back")
                {
                    return false;
                }
                Console.Clear();
            }
            return true;
        }


    }
}
