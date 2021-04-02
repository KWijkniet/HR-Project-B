﻿using System;
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
            string userUsername, userPassword, userPhonenumber, userEmail, userRole;
            int userChoice;

            string[] menuItems = new string[] {
            "Login",
            "Register an account",
            "Continue as guest",
            "Exit"};
            // check whice case is pressed
            Console.CursorVisible = false;
            while (true) //Loops until user gives valid input
            {
                OptionMenu menu = new OptionMenu("Welcome user, select one of the options below.\n", menuItems);
                userChoice = menu.Display();
                if (userChoice == 3)
                {
                    Environment.Exit(0);
                }
                if (userChoice == 0 || userChoice == 1 || userChoice == 2)
                {
                    if (userChoice == 2)
                    {
                        userRole = "guest";
                        foreach (Account acc in Program.accounts)
                        {
                            if (acc.name == "Guest" && acc.password == "" && acc.id == "0")
                            {
                                Program.account = acc;
                                return;
                            }
                        }
                    }
                    Console.Clear();
                    break;
                }
                else { TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true); }
            }
            
            //Login
            if (userChoice == 0)
            {
                while (true)
                {
                    TextTool.TextColor("Enter your email: ", ConsoleColor.Yellow, false);
                    userEmail = Console.ReadLine();
                    TextTool.TextColor("Enter your password: ", ConsoleColor.Yellow, false);
                    userPassword = Console.ReadLine();
                    //Check if username exists > if false raise error else check if password matches > if false raise error else login complete 

                    if(userEmail.Length <= 0 || userPassword.Length <= 0)
                    {
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                        continue;
                    }

                    foreach (Account acc in Program.accounts)
                    {
                        if (acc.email == userEmail && acc.password == userPassword)
                        {
                            Program.account = acc;
                            break;
                        }
                    }

                    if(Program.account != null)
                    {
                        break;
                    }
                    else
                    {
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                        continue;
                    }
                }
            }
            
            //Registration
            else if (userChoice == 1) 
            {
                //loops until valid username is given
                while (true) {
                    TextTool.TextColor("Enter your username (can only contain letters): ", ConsoleColor.Yellow, false);
                    userUsername = Console.ReadLine();
                    if (Regex.IsMatch(userUsername, "^[A-Za-z ]{3,15}$")) {      //can only contain letters, no special characters or spaces, 3-15 characters long
                        break;
                    }
                    else {
                        userUsername = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                    }
                }

                //loops until valid password is given
                while (true) {
                    TextTool.TextColor("Enter your password (atleast 8 characters long and needs to contain atleast 1 number): ", ConsoleColor.Yellow, false);
                    userPassword = Console.ReadLine();
                    if (Regex.IsMatch(userPassword, "^[a-zA-Z0-9]{8,128}$") && Regex.IsMatch(userPassword, @"\d")) {        //atleast 8 characters long, needs to contain atleast 1 numbers
                        break;
                    }
                    else {
                        userPassword = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                    }
                }
                
                //loops until valid phonenumber is given
                while (true) {
                    TextTool.TextColor("Enter your phonenumber (+31 already included): ", ConsoleColor.Yellow, false);
                    userPhonenumber = Console.ReadLine();
                    if (Regex.IsMatch(userPhonenumber, "^[0-9]{10}$")) {        //needs to have valid phonenumber format
                        userPhonenumber = "+31 " + userPhonenumber; break;
                    }
                    else { 
                        userPhonenumber = ""; 
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                    }
                }

                //loops until valid email is given
                while (true) {
                    TextTool.TextColor("Enter your email: ", ConsoleColor.Yellow, false);
                    userEmail = Console.ReadLine();

                    bool isUnique = true;
                    foreach (Account account in Program.accounts)
                    {
                        if(account.email == userEmail)
                        {
                            isUnique = false;
                            break;
                        }
                    }

                    //needs to have valid email format
                    if (Regex.IsMatch(userEmail, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$") && isUnique) {
                        break;
                    }
                    else {
                        userEmail = "";
                        if (!isUnique)
                        {
                            TextTool.TextColor("Email already in use. Please try again.", ConsoleColor.Red, true);
                        }
                        else
                        {
                            TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                        }
                    }
                }

                //loops until valid role is given
                while (true) {
                   TextTool.TextColor("Enter your role (customer,admin,chef,manager): ", ConsoleColor.Yellow, false); //temporary!
                    userRole = Console.ReadLine();
                    if (userRole == "customer" || userRole == "admin" || userRole == "chef" || userRole == "manager") {
                        break;
                    }
                    else {
                        userRole = "";
                        TextTool.TextColor("Your input was invalid. Please try again.", ConsoleColor.Red, true);
                    }
                }

                int roleIndex = userRole == "customer" ? 1 : userRole == "chef" ? 2 : userRole == "manager" ? 3 : userRole == "admin" ? 4 : 0;
                Program.account = new Account(userUsername, roleIndex, userEmail, userPhonenumber, userPassword);

                TextTool.TextColor("\nYour account has been created succesfully.", ConsoleColor.Green ,true); Console.WriteLine("Press enter to continue"); Console.Read(); Console.Clear();

                Account[] temp = new Account[Program.accounts.Length + 1];
                for (int i = 0; i < Program.accounts.Length; i++)
                {
                    temp[i] = Program.accounts[i];
                }
                temp[^1] = Program.account;
                Program.accounts = temp;

                Program.SaveAccounts();
            }
        }
    }
}