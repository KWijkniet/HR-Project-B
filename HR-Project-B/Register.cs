using System;
using System.Text.RegularExpressions; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Project_B
{
    class Register //Jelmer
    {
        public static void Start()
        {
            string userChoice, userUsername, userPassword, userPhonenumber, userEmail, userRole;

            TextTool.TextColor("Welcome to Jake Darcy’s restaurant\n", ConsoleColor.Green, true);
            Console.WriteLine("1) Login\n2) Register an account\n3) Continue as guest\n");
            
            while (true) //Loops until user gives valid input
            {
                userChoice = Console.ReadLine();
                if (userChoice == "1" || userChoice == "2" || userChoice == "3") { if (userChoice == "3") { userRole = "guest"; } Console.Clear(); break; }
                else { TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, false); }
            }

            if (userChoice == "1") //goes to login
            {
                TextTool.TextColor("Enter your username: ", ConsoleColor.Yellow, false);
                userUsername = Console.ReadLine();
                TextTool.TextColor("Enter your password: ", ConsoleColor.Yellow, false);
                userPassword = Console.ReadLine();
                //Check if username exists > if false raise error else check if password matches > if false raise error else login complete 
            }
            else if (userChoice == "2") //goes to account creation
            {
                while (true) {      //loops until valid username is given
                    TextTool.TextColor("Enter your username (can only contain letters): ", ConsoleColor.Yellow, false);
                    userUsername = Console.ReadLine();
                    if (Regex.IsMatch(userUsername, "^[A-Za-z]{3,15}$")) {      //can only contain letters, no special characters or spaces, 3-15 characters long
                        break;
                    }
                    else {
                        userUsername = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, false);
                    }
                }
                while (true) {    //loops until valid password is given
                    TextTool.TextColor("Enter your password (atleast 8 characters long and needs to contain atleast 1 number): ", ConsoleColor.Yellow, false);
                    userPassword = Console.ReadLine();
                    if (Regex.IsMatch(userPassword, "^[a-zA-Z0-9]{8,128}$") && Regex.IsMatch(userPassword, @"\d")) {        //atleast 8 characters long, needs to contain atleast 1 numbers
                        break;
                    }
                    else {
                        userPassword = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, false);
                    }
                }
                while (true) {      //loops until valid phonenumber is given
                    TextTool.TextColor("Enter your phonenumber (+31 already included): ", ConsoleColor.Yellow, false);
                    userPhonenumber = Console.ReadLine();
                    if (Regex.IsMatch(userPhonenumber, "^[0-9]{10}$")) {        //needs to have valid phonenumber format
                        userPhonenumber = "+31 " + userPhonenumber; break;
                    }
                    else {
                        userPhonenumber = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, false);
                    }
                }
                while (true) {  //loops until valid email is given
                    TextTool.TextColor("Enter your email: ", ConsoleColor.Yellow, false);
                    userEmail = Console.ReadLine();
                    if (Regex.IsMatch(userEmail, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$")) {     //needs to have valid email format
                        break;
                    }
                    else {
                        userEmail = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, false);
                    }
                }
                while (true) {   //loops until valid role is given
                   TextTool.TextColor("Enter your role (customer,admin,chef,manager): ", ConsoleColor.Yellow, false); //temporary!
                    userRole = Console.ReadLine();
                    if (userRole == "customer" || userRole == "admin" || userRole == "chef" || userRole == "manager") {
                        break;
                    }
                    else {
                        userRole = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, false);
                    }
                }
                //create account with class template using given information
                Console.Clear();
                Console.WriteLine($"username: {userUsername}\npassword: {userPassword}\nphonenumber: {userPhonenumber}\nemail: {userEmail}\nrole: {userRole}");
            }
        }
    }
}