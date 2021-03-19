using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Project_B
{

    class menuDrawing
    {
        public static int index = 0;
        public static string drawMenu(List<string> items)
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();
            // ceck wich case is hoverd
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {

                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {

                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
   
                return items[index];
            }
            else
            {
                return "";
            }

            return "";

        }
    }   
}
