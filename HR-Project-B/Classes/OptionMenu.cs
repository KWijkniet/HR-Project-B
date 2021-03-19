using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HR_Project_B
{
    class OptionMenu
    {
        private string title;
        private string[] options;
        private int index = 0;

        public OptionMenu(string _title, string[] _options)
        {
            this.title = _title;
            this.options = _options;
        }

        public int Display()
        {
            //show first time
            UpdateList();
            //Loop until user pressed enter
            bool awaitingResponse = true;
            while (awaitingResponse)
            {
                //check if the user pressed the correct key
                ConsoleKeyInfo pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index < 0)
                    {
                        index = options.Length - 1;
                    }
                }
                else if (pressed.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index > options.Length - 1)
                    {
                        index = 0;
                    }
                }
                else if (pressed.Key == ConsoleKey.Enter)
                {
                    awaitingResponse = false;
                }
                UpdateList();
            }
            return index;
        }

        private void UpdateList()
        {
            string result = title + "\n";
            for (int i = 0; i < options.Length; i++)
            {
                string option = options[i];
                string prefix = "  ";

                if (index == i)
                {
                    prefix = "> ";
                }
                result += prefix + option + "\n";
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(result);
        }
    }
}
