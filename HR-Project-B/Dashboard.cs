using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HR_Project_B
{
    class Dashboard
    {
        public static void Start()
        {
            while (true)
            {
                int role = Program.account.role;

                switch (role)
                {
                    case 0:
                        if (!GuestMenu())
                        {
                            return;
                        }
                        break;
                    case 1:
                        if (!CustomerMenu())
                        {
                            return;
                        }
                        break;
                    case 2:
                        if (!ChefMenu())
                        {
                            return;
                        }
                        break;
                    case 3:
                        if (!ManagerMenu())
                        {
                            return;
                        }
                        break;
                    case 4:
                        if (!AdminMenu())
                        {
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private static bool CustomerMenu()
        {
            string[] menuItems = new string[] {
            "Menu",
            "Reservation",
            "Info",
            "Logout"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                OptionMenu menu = new OptionMenu("  Welcome " + Program.account.name + ", select one of the options below.\n", menuItems);
                int index = menu.Display();
                switch (index)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Menu"); Console.Read();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Reservation"); Console.Read();
                        break;
                    case 2:
                        Console.Clear();
                        Information.Info(); Console.Read();
                        break;
                    case 3:
                        Program.account = null;
                        return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool GuestMenu()
        {
            string[] menuItems = new string[] {
            "Menu",
            "Reservation",
            "Info",
            "Logout",};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                OptionMenu menu = new OptionMenu("  Welcome " + Program.account.name + ", select one of the options below.\n", menuItems);
                int index = menu.Display();
                switch (index)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Menu"); Console.Read();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Reservation"); Console.Read();
                        break;
                    case 2:
                        Console.Clear();
                        Information.Info(); Console.Read();
                        break;
                    case 3:
                        Program.account = null;
                        return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool ChefMenu()
        {
            string[] menuItems = new string[] {
            "Menu", //Also gets to edit menu
            "Reservation",
            "Info",
            "Logout",};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                OptionMenu menu = new OptionMenu("  Welcome " + Program.account.name + ", select one of the options below.\n", menuItems);
                int index = menu.Display();
                switch (index)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Menu"); Console.Read();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Reservation"); Console.Read();
                        break;
                    case 2:
                        Console.Clear();
                        Information.Info(); Console.Read();
                        break;
                    case 3:
                        Program.account = null;
                        return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool ManagerMenu()
        {
            string[] menuItems = new string[] {
            "Menu",
            "Reservation",
            "Bookings", //Able to see and edit bookings
            "Info",
            "Logout",};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                OptionMenu menu = new OptionMenu("  Welcome " + Program.account.name + ", select one of the options below.\n", menuItems);
                int index = menu.Display();
                switch (index)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Menu"); Console.Read();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Reservation"); Console.Read();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Bookings"); Console.Read();
                        break;
                    case 3:
                        Console.Clear();
                        Information.Info(); Console.Read();
                        break;
                    case 4:
                        Program.account = null;
                        return false;
                }
                Console.Clear();
            }
            return true;
        }
        private static bool AdminMenu()
        {
            string[] menuItems = new string[] {
            "Menu", //Can edit everything
            "Reservation",
            "Bookings", //Can edit everything
            "Finance", //Overview
            "Info",
            "Logout",};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true)
            {
                OptionMenu menu = new OptionMenu("  Welcome " + Program.account.name + ", select one of the options below.\n", menuItems);
                int index = menu.Display();
                switch (index)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Menu"); Console.Read();
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Reservation"); Console.Read();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Bookings"); Console.Read();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Finance"); Console.Read();
                        break;
                    case 4:
                        Console.Clear();
                        Information.Info(); Console.Read();
                        break;
                    case 5:
                        Program.account = null;
                        return false;
                }
                Console.Clear();
            }
            return true;
        }
    }
}
