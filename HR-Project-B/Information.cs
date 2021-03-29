using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class Information
    {
        public static void Info()
        {
            TextTool.TextColor("Information about Jake Darcy's restaurant\n\n", ConsoleColor.Green, true);
            TextTool.TextColor("About us", ConsoleColor.Yellow , true);
            Console.WriteLine("Here at Jake Darcy's we have a wide variety of items for you to choose from. ");
            Console.WriteLine("You will be able to choose from different types of menu's, including vegetarian, vegan, fish, meat and more.");
            Console.WriteLine("We also have a vast assortment of drinks you can order at the bar, which is always opened.");
            Console.WriteLine("We always strife to give our customers the best experience they can have.\n");
            TextTool.TextColor("Opening times", ConsoleColor.Yellow, true);
            Console.WriteLine("The restaurant is open at any day of the week, from midday till midnight.");
            Console.WriteLine("We serve lunch till 4pm and dinner from 6pm to 10pm.");
            Console.WriteLine("Our bar is always open.\n");
            TextTool.TextColor("Where can you find us", ConsoleColor.Yellow, true);
            Console.WriteLine("We are located at Wijnhaven 107, 3011 WN in Rotterdam.\n\n\n");
            Console.WriteLine("Press enter to go back");
            Console.Read();
        }
    }
}
