using System;
using System.Text.RegularExpressions;

namespace HR_Project_B.Components
{
    class Menu
    {
        public Text title = null;
        public Text[] options = null;
        public string prefix = "> ";
        public ConsoleColor foregroundColor = ConsoleColor.White;
        public ConsoleColor backgroundColor = ConsoleColor.Black;
        public ConsoleColor highlightColor = ConsoleColor.Gray;

        private int selectedIndex = 0;
        private bool firstLoad = true;
        private string clearPrefix = "  ";

        public Menu(Text title, Text[] options, string prefix = "> ")
        {
            title.text += "\n";
            this.title = title;
            this.options = options;
            this.prefix = prefix;
            this.clearPrefix = "";

            for (int i = 0; i < prefix.Length; i++)
            {
                this.clearPrefix += " ";
            }
            FixOptionsStringLength();
        }

        public Menu(Text returnText, string prefix = "> ")
        {
            this.title = new Text("");
            this.options = new Text[]
            {
                returnText
            };
            this.prefix = prefix;
            this.clearPrefix = "";

            for (int i = 0; i < prefix.Length; i++)
            {
                this.clearPrefix += " ";
            }

            FixOptionsStringLength();
        }

        public int Display()
        {
            bool wasCursorVisible = Console.CursorVisible;
            while (true)
            {
                Console.CursorVisible = false;
                UpdateList();
                firstLoad = false;

                ConsoleKeyInfo pressed = Console.ReadKey(true);
                switch (pressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = selectedIndex - 1 >= 0 ? selectedIndex - 1 : options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = selectedIndex + 1 < options.Length ? selectedIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = wasCursorVisible;
                        return selectedIndex;
                    default:
                        break;
                }
            }
        }

        private void UpdateList()
        {
            Console.ResetColor();
            if (!firstLoad)
            {
                int amount = options.Length;
                if(title.text.Length > 0)
                {
                    amount += Regex.Matches(title.text, "\n").Count;
                }
                Console.SetCursorPosition(0, Console.CursorTop - amount);
            }

            title.Display();
            for (int i = 0; i < options.Length; i++)
            {
                Text option = options[i];

                if (selectedIndex == i)
                {
                    option.text = prefix + option.text.Substring(clearPrefix.Length);
                    option.Display(true, true);
                }
                else
                {
                    option.text = clearPrefix + option.text.Substring(clearPrefix.Length);
                    option.Display(true, false);
                }
            }
        }

        private void FixOptionsStringLength()
        {
            int length = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if(length <= options[i].text.Length)
                {
                    length = options[i].text.Length + 1;
                }
            }

            for (int i = 0; i < options.Length; i++)
            {
                Text option = options[i];
                int currentLength = option.text.Length;
                for (int j = 0; j < length - currentLength; j++)
                {
                    option.text += " ";
                }
                option.text = clearPrefix + option.text;
            }
        }
    }
}
