using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HR_Project_B.Components
{
    /*
     * The Input settings are extra settings for the input fields.
     * These settings help expand the functionallity of the input component by adding input regex, string requirements and more.
     * 
     * The default settings for this class are often enough to work with. In case you do want to customize them you can add your own data to the constructor.
     * new InputSettings(newLine, minLength, maxLength, allowedCharacters, customRegex, canEspace)
     * 
     */
    class InputSettings
    {
        public int minLength;                   //Min length of the string
        public int maxLength;                   //Max length of the string
        public string allowedCharacters;        //A regex of the characters that are allowed to be pressed.
        public string customRegex;              //Custom regex to override the allowedCharacters
        public bool newLine = false;            //Show the input below the text
        public bool canEscape = true;           //can you press escape to cancel the input (returns null)

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
            if (customRegex.Length > 0)
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
}
