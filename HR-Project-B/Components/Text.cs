using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B.Components
{
    class Text
    {
        public string text = "";
        public ConsoleColor color = ConsoleColor.White;
        public ConsoleColor background = ConsoleColor.Black;
        public ConsoleColor highlight = ConsoleColor.Gray;
        public ConsoleColor highlightText = ConsoleColor.Black;

        public Text(string text, ConsoleColor color = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black, ConsoleColor highlight = ConsoleColor.Gray, ConsoleColor highlightText = ConsoleColor.Black)
        {
            this.text = text;
            this.color = color;
            this.background = background;
            this.highlight = highlight;
            this.highlightText = highlightText;
        }

        public void Display(bool newLine = false, bool showHighlight = false)
        {
            Console.ResetColor();
            if (showHighlight)
            {
                Console.ForegroundColor = highlightText;
                Console.BackgroundColor = highlight;
            }
            else
            {
                Console.ForegroundColor = color;
                Console.BackgroundColor = background;
            }

            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
            Console.ResetColor();
        }
    }
}
