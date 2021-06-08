using System;
using System.Text.RegularExpressions;

namespace HR_Project_B.Components
{
    /*
     * The input component makes it easier to ask for a input without going through the troubles of adding alot of while loops and unclean code to check if the value is accaptable.
     * The input allows for a quick 2 line of simple code to have the functionallity that you require.
     * To use it all you need are 2 text components (the input label and the error message). The settings are optional.
     */
    class Input
    {
        public Text text;
        public Text error;
        public InputSettings settings = new InputSettings();

        //Create an input. Will use default settings if none given.
        public Input(Text text, Text error, InputSettings settings = null)
        {
            this.text = text;
            this.error = error;
            if(settings != null)
            {
                this.settings = settings;
            }
        }

        //Display the input (will return the result when the users presses enter)
        public string Display()
        {
            bool wasCursorVisible = Console.CursorVisible;
            while (true)
            {
                Console.CursorVisible = true;
                text.Display(settings.newLine);
                string result = ReadLineOrEsc();
                if(result == null)
                {
                    //cancel input
                    Console.CursorVisible = wasCursorVisible;
                    return null;
                }
                if (Regex.IsMatch(result, settings.GetRegex()))
                {
                    Console.CursorVisible = wasCursorVisible;
                    return result;
                }
                else
                {
                    error.Display();
                }
            }
        }

        //Validates the users input and executes extra lines where needed (escape = cancel, enter = submit)
        private string ReadLineOrEsc()
        {
            string result = "";
            int curIndex = 0;
            while(true)
            {
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);

                switch (readKeyResult.Key)
                {
                    case ConsoleKey.Escape:
                        if (!settings.canEscape)
                        {
                            break;
                        }
                        return null;
                    case ConsoleKey.Enter:
                        return result;
                    case ConsoleKey.Backspace:
                        if (curIndex > 0)
                        {
                            result = result.Remove(result.Length - 1);
                            Console.Write(readKeyResult.KeyChar);
                            Console.Write(' ');
                            Console.Write(readKeyResult.KeyChar);
                            curIndex--;
                        }
                        break;
                    default:
                        if (Regex.IsMatch(readKeyResult.KeyChar.ToString(), settings.GetRegex(true)))
                        {
                            result += readKeyResult.KeyChar;
                            Console.Write(readKeyResult.KeyChar);
                            curIndex++;
                        }
                        break;
                }
            }
        }
    }
}
