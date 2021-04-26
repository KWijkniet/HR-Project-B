using System;
using System.Linq;
using HR_Project_B.Components;

namespace HR_Project_B
{
    public class DashboardOption
    {
        public string name;
        public int id;
        public int[] allowedRoles;

        public DashboardOption(int id, string name, int[] options)
        {
            this.id = id;
            this.name = name;
            this.allowedRoles = options;
        }

        public bool HasAccess(int i)
        {
            if(allowedRoles.Length <= 0)
            {
                return true;
            }
            return allowedRoles.Contains<int>(i);
        }
    }

    class Dashboard
    {
        private static DashboardOption[] options = new DashboardOption[]
        {
            new DashboardOption(0, "Menu", new int[0]),
            new DashboardOption(1, "Manage Users", new int[]{4}),
            new DashboardOption(2, "Reservations", new int[]{0,1,3,4}),
            new DashboardOption(3, "Take-away", new int[]{0,1}),
            new DashboardOption(4, "Cancel reservation", new int[]{0,1}),
            new DashboardOption(5, "Bookings", new int[]{3,4}),
            new DashboardOption(6, "Finance", new int[]{3,4}),
            new DashboardOption(7, "Info", new int[0]),
            new DashboardOption(8, "Payment", new int[0]),
            new DashboardOption(9, "Logout", new int[0]),
        };

        public static void Start()
        {
            while (true)
            {
                Program.ClearConsole();
                int role = Program.account.role;

                DashboardOption[] menuOptions = AllowedOptions(role);

                Text message = new Text("Welcome " + Program.account.name + "!\nWhat can we do for you?");
                Text[] messages = new Text[menuOptions.Length];

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    messages[i] = new Text(menuOptions[i].name);
                }

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                switch (menuOptions[selected].id)
                {
                    case 0:
                        MenuManager.Start();
                        break;
                    case 1:
                        UserManager.Start();
                        break;
                    case 2:
                        Reservations.Start();
                        break;
                    case 3: //Take-Away
                        break;
                    case 4: //Cancel reservation
                        break;
                    case 5: //Bookings
                        break;
                    case 6: //Finance
                        break;
                    case 7:
                        Information.Start();
                        break;
                    case 8:
                        Payment.GetUserPaymentInformation(Program.account.role);
                        Payment.ShowReceipt(null);
                        Console.ReadKey();
                        break;
                    case 9:
                        Program.account = null;
                        return;
                    default:
                        break;
                }
            }
        }

        private static DashboardOption[] AllowedOptions(int role)
        {
            int count = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].HasAccess(role))
                {
                    count++;
                }
            }

            DashboardOption[] result = new DashboardOption[count];
            int index = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].HasAccess(role))
                {
                    result[index] = options[i];
                    index++;
                }
            }
            return result;
        }
    }
}
