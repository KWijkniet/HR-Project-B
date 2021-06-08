using System;
using System.Text.RegularExpressions;

namespace HR_Project_B.Components
{
    /*
     * The menu component gives the user an easy way of creating a interactable menu. With the use of the arrow keys you can select one of the given options.
     * 
     * This component requires a text component as the title and a array of text components for the options.
     * You can also enter your custom prefix if you dont want to use the default one.
     * 
     * It also has an option for the user to enter just 1 text component. This will wait for a user to press enter without having other options to choose from. example:

        "We have received the payment. Press enter to continue"
     */
    class Menu
    {
        public Text title = null;               //The title displayed above the menu
        public Text[] options = null;           //All the options the user can choose from
        public string prefix = "> ";            //The prefix that shows before the selected option

        private int selectedIndex = 0;          //Which option is selected
        private bool firstLoad = true;          //Is this the first time loading the menu? if so calculate its starting position so it doesnt update the full console.
        private string clearPrefix = "  ";      //Same as the prefix but it only contains spaces. This is used for none selected options

        public Menu(Text title, Text[] options, string prefix = "> ")
        {
            //Fix the given title so the user doesnt have to do this
            title.text += "\n";
            this.title = title;

            //Set the options
            this.options = options;

            //Set the prefix
            this.prefix = prefix;
            this.clearPrefix = "";

            //Make the clear prefix the same length as the prefix (but as spaces)
            for (int i = 0; i < prefix.Length; i++)
            {
                this.clearPrefix += " ";
            }
          
            //Make all the options the same length (using spaces) so the highlighted areas match with the other options
            FixOptionsStringLength();
        }

        public Menu(Text returnText, string prefix = "> ")
        {
            //Set an empty title and use the only given text component as the option
            this.title = new Text("");
            this.options = new Text[]
            {
                returnText
            };
          
            //Set prefix and clear prefix
            this.prefix = prefix;
            this.clearPrefix = "";

            //Fix the clear prefix
            for (int i = 0; i < prefix.Length; i++)
            {
                this.clearPrefix += " ";
            }

            //Make all the options the same length (using spaces) so the highlighted areas match with the other options
            FixOptionsStringLength();
        }

        public int Display()
        {
            bool wasCursorVisible = Console.CursorVisible;
            //show list until option has been selected
            while (true)
            {
                Console.CursorVisible = false;
                UpdateList();
                firstLoad = false;

                //read the pressed key
                ConsoleKeyInfo pressed = Console.ReadKey(true);
                switch (pressed.Key)
                {
                    case ConsoleKey.UpArrow:
                        //if the user presses up while at the top it will go to the bottom of the menu
                        selectedIndex = selectedIndex - 1 >= 0 ? selectedIndex - 1 : options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        //if the user presses down while at the bottom it will go to the top of the menu
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
                //calculate its position
                int amount = options.Length;
                if(title.text.Length > 0)
                {
                    amount += Regex.Matches(title.text, "\n").Count;
                }
                //set cursor to start of the menu to just update the menu
                Console.SetCursorPosition(0, Console.CursorTop - amount);
            }

            //Update the list
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
            //Get the longest string and store its length
            int length = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if(length <= options[i].text.Length)
                {
                    length = options[i].text.Length + 1;
                }
            }

            //Add spaces all options untill they have the same length as the longest option
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
