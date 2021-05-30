using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HR_Project_B.Components;
using System.Text.RegularExpressions;

namespace HR_Project_B
{
    class CustomerReservations

    {
        public static ReservationOptions[] tables;
        public static Reservation[] reservations;
        public static void Start()
        {
            LoadReservation();
            LoadTables();


            while (true)
            {
                Program.ClearConsole();
                Text reservation = new Text("Reservation options");
                Text[] options = new Text[]
                {
                    new Text("Make"),
                    new Text("View"),
                    new Text("Back")
                };

                Menu setupReservation = new Menu(reservation, options);
                int response = setupReservation.Display();

                Program.ClearConsole();
                switch (response)
                {
                    case 0:
                        //Make Reservation
                        CreateReservation();
                        break;
                    case 1:
                        //View Category
                        ViewReservation();
                        break;
                    case 2:
                        //back
                        return;
                    default:
                        break;
                }
            }
        }

        public static void LoadReservation()

        {
            FileManager fm = new FileManager("Reservations.json");

            dynamic[] foundOptions = fm.ReadJSON();
            reservations = new Reservation[foundOptions.Length];
            for (int i = 0; i < foundOptions.Length; i++)
            {
                Reservation newReservation = new Reservation(foundOptions[i]);

                reservations[i] = newReservation;
            }
        }

        // Save accounts to the file
        private static void SaveReservation()
        {
            FileManager fm = new FileManager("Reservations.json");

            dynamic[] foundOptions = new dynamic[reservations.Length];
            for (int i = 0; i < reservations.Length; i++)
            {
                foundOptions[i] = reservations[i];
            }

            fm.WriteJSON(foundOptions);
        }


        private static void LoadTables()
        {
            Reservations.LoadReservation();
            tables = Reservations.reservationOptions;


        }

        private static void CreateReservation()
        {
            string name;
            string email;
            if (Program.account.role == 0)
            {
                Input emailInput = new Input(new Text("\nEmail: "), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 6, 225, "A-Za-z0-9_.-@", "", false));
                email = emailInput.Display();
                if (email == null)
                {
                    return;
                }
                else if (!Regex.IsMatch(email, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                {
                    Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red);
                    error.Display();
                   
                }
                
                Input nameInput = new Input(new Text("\nName: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z "));
                name = nameInput.Display();
                if (name == null)
                {
                    return;
                }
                
            }
            else
            {
                name = Program.account.name;
                email = Program.account.email;
            }
            //make resevation here
            Program.ClearConsole();

            Text message = new Text("Select a table: ");
            Text[] messages = new Text[tables.Length + 1];

            // search
            for (int i = 0; i < tables.Length; i++)
            {
                messages[i] = new Text(tables[i].name);
            }
            messages[^1] = new Text("Back");

            Menu menu = new Menu(message, messages);
            int selected = menu.Display();

            if (selected == messages.Length - 1)
            {
                return;

            }
            ReservationOptions table = tables[selected];


            //Payment

            string creditCard;

            while (true)
            {
                Input creditInput = new Input(new Text("\nCredit card: "), new Text("\nPlease enter a credit card!", ConsoleColor.Red), new InputSettings(false, 13, 16, "0-9", "", false));
                creditCard = creditInput.Display();

                if (Payment.ValidateCreditCard(creditCard))
                {
                    break;
                }
                else
                {
                    Text error = new Text("\nPlease enter a valid Credit card.", ConsoleColor.Red);
                    error.Display();
                    continue;
                }
            }

            Reservation reservation = new Reservation(creditCard, "Table reservation", table.id, Program.account.role == 1 ? "" : Program.account.id, name, email);
           
            Reservation[] temp = new Reservation[reservations.Length + 1];
            for (int i = 0; i < reservations.Length; i++)
            {
                temp[i] = reservations[i];
            }
            temp[^1] = reservation;
            reservations = temp;
            SaveReservation();
        }

        public static void CreateTakeaway() //should be private
        {
            LoadReservation();

            string name;
            string email;
            if (Program.account.role == 0)
            {
                Input emailInput = new Input(new Text("\nEmail: "), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 6, 225, "A-Za-z0-9_.-@", "", false));
                email = emailInput.Display();
                if (email == null)
                {
                    return;
                }
                else if (!Regex.IsMatch(email, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                {
                    Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red);
                    error.Display();

                }

                Input nameInput = new Input(new Text("\nName: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z "));
                name = nameInput.Display();
                if (name == null)
                {
                    return;
                }
            }
            else
            {
                name = Program.account.name;
                email = Program.account.email;
            }

            string creditCard;

            while (true)
            {
                Input creditInput = new Input(new Text("\nCredit card:"), new Text("\nPlease enter a credit card!", ConsoleColor.Red), new InputSettings(false, 13, 16, "0-9", "", false));
                creditCard = creditInput.Display();

                if (Payment.ValidateCreditCard(creditCard))
                {
                    break;
                }
                else
                {
                    Text error = new Text("\nPlease enter a valid Credit card.", ConsoleColor.Red);
                    error.Display();
                    continue;
                }
            }
            Reservation reservation = new Reservation(creditCard, "Takeaway", "", Program.account.role == 1 ? "" : Program.account.id, name, email);

            Reservation[] temp = new Reservation[reservations.Length + 1];
            for (int i = 0; i < reservations.Length; i++)
            {
                temp[i] = reservations[i];
            }
            temp[^1] = reservation;
            reservations = temp;
            SaveReservation();
        }
           
        private static void ViewReservation()
        {
            foreach (Reservation option in FilterReservations())
            {

                Text optionName = new Text("\n" + "Name: " + option.userName, ConsoleColor.Green);
                optionName.Display();
                ReservationOptions foundTable = null;
                foreach (ReservationOptions table in tables)
                {
                    if (table.id == option.tableID)
                    {
                        foundTable = table;
                        break;
                    }
                }
                if (option.orderType == "Table reservation")
                {
                    Text optionTable = new Text("\n" + "Table name: " + foundTable.name, ConsoleColor.Green);
                    optionTable.Display();
                }
                Text optionType = new Text("\n" + "Type: " + option.orderType, ConsoleColor.Green);
                optionType.Display();

                Text optionorderID = new Text("\n" + "Reservation Code: " + option.orderID, ConsoleColor.Green);
                optionorderID.Display();

                Console.WriteLine("\n");
            }
            Menu menu = new Menu(new Text("Press enter to go back"));
            menu.Display();
        }

        private static Reservation[] FilterReservations()
        {
            Reservation[] results;
            if (Program.account.role != 0)
            {
                int count = 0;
                foreach (Reservation option in reservations)
                {

                    if(option.userID == Program.account.id)
                    {
                        count++;
                    }
                }
                results = new Reservation[count];
                int index = 0;
                
                foreach (Reservation option in reservations)
                {
                    if (option.userID == Program.account.id)
                    {
                        results[index] = option;
                        index++;
                    }
                }
            }
            else
            {
                string email;
                while (true)
                {
                    Input emailInput = new Input(new Text("\nEmail: "), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 6, 225, "A-Za-z0-9_.-@", "", false));
                    email = emailInput.Display();
                    if (!Regex.IsMatch(email, "^[A-Za-z0-9_.-]{1,64}@[A-Za-z-]{1,255}.(com|net|nl|org)$"))
                    {
                        Text error = new Text("\nPlease enter a valid email.", ConsoleColor.Red); error.Display();
                        continue;
                    }
                    else { break; }
                }
                Program.ClearConsole();
                int count = 0;
                foreach (Reservation option in reservations)
                {
                    if (option.userEmail == email)
                    {
                        count++;
                    }
                }
                results = new Reservation[count];
                int index = 0;

                foreach (Reservation option in reservations)
                {
                    if (option.userEmail == email)
                    {
                        results[index] = option;
                        index++;
                    }
                }
            }
            return results;   
        }
        public static void CancelReservation()
        {
            LoadReservation();

            if (reservations.Length == 0) { Text line01 = new Text("\nThere are no current reservations.\n", ConsoleColor.Red); line01.Display(); Menu menu = new Menu(new Text("Press enter to go back")); menu.Display(); return; }
            Input orderIDInput = new Input(new Text("\nEnter the unique ID of the reservation you want to cancel: "), new Text("\nPlease enter a valid ID!", ConsoleColor.Red), new InputSettings(false, 8, 8, "a-z0-9"));
            string orderID = orderIDInput.Display();
            for (int i = 0; i < reservations.Length; i++)
            {
                if (reservations[i].orderID == orderID) 
                {
                    //Display order?
                    Text reservation = new Text("\nDo you want to cancel this order?");
                    Text[] options = new Text[]
                    {
                    new Text("Yes"),
                    new Text("No"),
                    };

                    Menu setupReservation = new Menu(reservation, options);
                    int response = setupReservation.Display();
                    if (response == 0) {
                        Reservation[] temp = new Reservation[reservations.Length - 1];
                        int index = 0;
                        for (int j = 0; j < reservations.Length; j++)
                        {
                            if (j != i)
                            {
                                temp[index] = reservations[j];
                                index++;
                            }
                        }
                        reservations = temp;
                        SaveReservation();
                    }
                    return;
                }
                else if (i == reservations.Length-1 ) {      
                    Text line01 = new Text("\nYour reservation was not found!\n", ConsoleColor.Red); line01.Display();
                    Menu menu = new Menu(new Text("Press enter to go back"));
                    menu.Display();
                    return;
                }
            }
        }
    }
}