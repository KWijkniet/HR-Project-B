using System;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class Information
    {
        public static void Start()
        {
            Text line01 = new Text("Information about Jake Darcy's restaurant\n\n", ConsoleColor.Green);
            Text line02 = new Text("About us", ConsoleColor.Yellow);
            Text line03 = new Text("Opening times", ConsoleColor.Yellow);
            Text line04 = new Text("Where can you find us", ConsoleColor.Yellow);

            line01.Display();
            line02.Display();
            Console.WriteLine("Here at Jake Darcy's we have a wide variety of items for you to choose from. ");
            Console.WriteLine("You will be able to choose from different types of menu's, including vegetarian, vegan, fish, meat and more.");
            Console.WriteLine("We also have a vast assortment of drinks you can order at the bar, which is always opened.");
            Console.WriteLine("We always strife to give our customers the best experience they can have.\n");
            line03.Display();
            Console.WriteLine("The restaurant is open at any day of the week, from midday till midnight.");
            Console.WriteLine("We serve lunch till 4pm and dinner from 6pm to 10pm.");
            Console.WriteLine("Our bar is always open.\n");
            line04.Display();
            Console.WriteLine("We are located at Wijnhaven 107, 3011 WN in Rotterdam.\n\n\n");
            
            Menu menu = new Menu(new Text("Press enter to go back"));
            menu.Display();
            return;
        }
    }
}
