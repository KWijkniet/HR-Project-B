using System;
using System.Text.RegularExpressions;

namespace HR_Project_B.Components
{
    class InputSettings
    {
        public int minLength;                   //Min length of the string
        public int maxLength;                   //Max length of the string
        public string allowedCharacters;        //A regex of the characters that are allowed to be pressed.
        public string customRegex;              //Custom regex to override the allowedCharacters
        public bool newLine = false;            //Show the input below the text
        public bool canEscape = true;           //can you press escape to cancel the input

        //set the settings when creating the input settings (with default values)
        public InputSettings(bool newLine = false, int minLength = 1, int maxLength = 999, string allowedCharacters = "A-Za-z0-9 ", string customRegex = "", bool canEscape = true)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
            this.allowedCharacters = allowedCharacters;
            this.newLine = newLine;
            this.customRegex = customRegex;
            this.canEscape = canEscape;
        }

        //Get the regex that should be used to validate the input value
        public string GetRegex(bool ignoreLength = false)
        {
            if(customRegex.Length > 0)
            {
                if (ignoreLength)
                {
                    return Regex.Replace(customRegex, "{.*?}", string.Empty);
                }
                return customRegex;
            }

            if (ignoreLength)
            {
                return "^[" + allowedCharacters + "]$";
            }
            return "^[" + allowedCharacters + "]{" + minLength + "," + maxLength + "}$";
        }
    }

    class Input
    {
        public Text text;
        public Text error;
        public InputSettings settings = new InputSettings();

        //Create an input. Will
        public Input(Text text, Text error, InputSettings settings = null)
        {
            this.text = text;
            this.error = error;
            if(settings != null)
            {
                this.settings = settings;
            }
        }

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
