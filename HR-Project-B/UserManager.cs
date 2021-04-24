using System;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class UserManager
    {
        public static void Start() {

            while (true)
            {
                Program.ClearConsole();

                Text message = new Text("Select a user:");
                Text[] messages = new Text[Program.accounts.Length + 1];

                // search
                for (int i = 0; i < Program.accounts.Length; i++)
                {
                    messages[i] = new Text(Program.accounts[i].name);
                }
                messages[^1] = new Text("Back");

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();
                if (selected == messages.Length - 1) { return; }

                Account selectUser = Program.accounts[selected];

                Program.ClearConsole();
                if (selectUser.id == Program.account.id)
                {
                    Text error = new Text("you can't edit your own account!", ConsoleColor.Red);
                    error.Display();
                    Console.ReadKey();
                    continue;
                }
                
                if (selectUser.id == "0")
                {
                    Text error = new Text("you can't edit the guest account!", ConsoleColor.Red);
                    error.Display();
                    Console.ReadKey();
                    continue;
                }

                EditUser(selectUser);
            }
        }

        private static void EditUser(Account selectUser)
        {
            string roleName = selectUser.role == 0 ? "Guest" :
                selectUser.role == 1 ? "Customer" :
                selectUser.role == 2 ? "Chef" :
                selectUser.role == 3 ? "Manager" :
                selectUser.role == 4 ? "Admin" : "";
            
            while (true)
            {
                Program.ClearConsole();
                string userInfo = $"Name: {selectUser.name}\nEmail: {selectUser.email}\nPhone: {selectUser.phone}\nRole: {roleName}";
                Text userInfoText = new Text(userInfo + "\n\n");
                userInfoText.Display();

                Text roleText = new Text("Select a new role:");
                Text[] roles = new Text[]
                {
                        new Text("Customer"),
                        new Text("Chef"),
                        new Text("Manager"),
                        new Text("Admin"),
                        new Text("Back"),
                };

                Menu roleMenu = new Menu(roleText, roles);
                int selectedRole = roleMenu.Display();
                if (selectedRole == roles.Length - 1) { return; }

                selectUser.role = selectedRole + 1;
                for (int i = 0; i < Program.accounts.Length; i++)
                {
                    if (Program.accounts[i].id == selectUser.id)
                    {
                        Program.accounts[i].role = selectUser.role;
                    }
                }
                Program.SaveAccounts();
                return;
            }
        }
    }
}
