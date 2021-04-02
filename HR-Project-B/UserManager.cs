using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class UserManager
    {
        public static void Start() {
            string titel = "Select a user";
            string[] options = new string[Program.accounts.Length + 1];

            // search
            for (int i = 0; i < Program.accounts.Length; i++)
            {
                options[i] = Program.accounts[i].name;
                    
            }
            options[^1] = "Back";


            while (true)
            {
                Console.Clear();
                OptionMenu menu = new OptionMenu(titel, options);
                int response = menu.Display();
                if (response == options.Length)
                {
                    return;
                }
                Account selectUser = Program.accounts[response];
                
                if (selectUser.id == Program.account.id)
                {
                    Console.Clear();
                    TextTool.TextColor("you can't edit your own account!", ConsoleColor.Red, true);
                    Console.ReadKey();
                    continue;
                }
                
                if (selectUser.id == "0")
                {
                    Console.Clear();
                    TextTool.TextColor("you can't edit the guest account!", ConsoleColor.Red, true);
                    Console.ReadKey();
                    continue;
                }

                string roleName = "";
                if(selectUser.role == 0)
                {
                    roleName = "Guest";

                }
                else if (selectUser.role == 1)
                {
                    roleName = "Customer";

                }
                else if (selectUser.role == 2)
                {
                    roleName = "Chef";

                }
                else if (selectUser.role == 3)
                {
                    roleName = "Manager";

                }
                else if (selectUser.role == 4)
                {
                    roleName = "Admin";

                }



                while (true)
                {
                    Console.Clear();
                    TextTool.TextColor($"Name: {selectUser.name}\nEmail: {selectUser.email}\nPhone: {selectUser.phone}\nRole: {roleName}\n", ConsoleColor.Gray, true);
                    TextTool.TextColor("Select a role number:\n1 = customer \n2 = chef \n3 = manager \n4 = admin", ConsoleColor.Green, true);
                    string tempRole = Console.ReadLine();
                    if (tempRole.Length > 0)
                    {
                        try
                        {
                      
                            int temp = int.Parse(tempRole);
                            if(temp < 1 || temp > 4)
                            {
                                throw new Exception();
                            
                            }
                            selectUser.role = temp;
                            for (int i = 0; i < Program.accounts.Length; i++)
                            {
                                if(Program.accounts[i].id == selectUser.id)
                                {
                                    Program.accounts[i].role = selectUser.role;

                                }

                            }
                            Program.SaveAccounts();
                            break;
                        }
                        catch (Exception)
                        {
                            TextTool.TextColor("Please enter a valid role!", ConsoleColor.Red, true);
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        TextTool.TextColor("Please enter a valid role!", ConsoleColor.Red, true);
                        Console.ReadKey();
                    }
                }


            }

        
        
        }



    }
}
