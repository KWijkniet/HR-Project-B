using System;

namespace HR_Project_B.Components
{
    /*
     * The text component is a component created to make it easier to use alot of options in just a few lines of code.
     * The component supports stuff such as text and background color and seperate color options for when it is highlighted.
     * 
     * To create a text component you call the constructor (requires atleast 1 string, the text, to be send with the constructor. this will make the component use the default colors)
     * To display the component you call the Display function. This has the option to show it on a new line as well as if the user wants the highlight colors or the normal ones.
     */
    class Text
    {
        public string text = "";
        public ConsoleColor color = ConsoleColor.White;
        public ConsoleColor background = ConsoleColor.Black;
        public ConsoleColor highlight = ConsoleColor.Gray;
        public ConsoleColor highlightText = ConsoleColor.Black;

        //Constructor has the default color values so it only requires a string and the rest will be optional
        public Text(string text, ConsoleColor color = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black, ConsoleColor highlight = ConsoleColor.Gray, ConsoleColor highlightText = ConsoleColor.Black)
        {
            this.text = text;
            this.color = color;
            this.background = background;
            this.highlight = highlight;
            this.highlightText = highlightText;
        }

        //display the string with the correct options
        public void Display(bool newLine = false, bool showHighlight = false)
        {
            //Reset color
            Console.ResetColor();
            //Should it show the highlighted version
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
          
            //Should it be displayed on a new line
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }

            //Reset color
            Console.ResetColor();
        }
    }
}
