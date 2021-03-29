using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Project_B
{
    class TextTool
    {
        public static void TextColor(string text, ConsoleColor color, bool doWriteLine) //Takes text to print, type of color and bool for writeline/write 
        {
            Console.ForegroundColor = color;
            if (doWriteLine) { Console.WriteLine(text); } else { Console.Write(text); }
            Console.ResetColor();
        }
    }
}
