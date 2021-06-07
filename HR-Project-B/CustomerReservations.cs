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
        public static void SaveReservation()
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

            string date = "";
            while (true)
            {
                Input dateInput = new Input(new Text("\nEnter the date for the reservation (DD/MM/YYYY HH:MM):"), new Text("\nPlease enter a valid date!", ConsoleColor.Red), new InputSettings(false, 16, 16, "0-9/: "));
                date = dateInput.Display();
                if (date == null)
                {
                    return;
                }

                if (!ValidateDate(date))
                {
                    Text error = new Text("\nPlease enter a valid date!", ConsoleColor.Red);
                    error.Display();
                    continue;
                }

                if (!ReservationAvailable(date, table))
                {
                    Text error = new Text("\nThis table has already been reserved for this date!", ConsoleColor.Red);
                    error.Display();
                    continue;
                }
                break;
            }

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

            Reservation reservation = new Reservation(creditCard, "Reservation", table.id, Program.account.role == 1 ? "" : Program.account.id, name, email, date);
           
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

            string date = "";
            while (true)
            {
                Input dateInput = new Input(new Text("\nEnter the date for the reservation (DD/MM/YYYY HH:MM):"), new Text("\nPlease enter a valid date!", ConsoleColor.Red), new InputSettings(false, 3, 15, "0-9/: "));
                date = dateInput.Display();
                if (date == null)
                {
                    return;
                }

                if (!ValidateDate(date))
                {
                    Text error = new Text("\nPlease enter a valid date!", ConsoleColor.Red);
                    error.Display();
                    continue;
                }
                break;
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
            Reservation reservation = new Reservation(creditCard, "Takeaway", "", Program.account.role == 1 ? "" : Program.account.id, name, email, date);

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
            //Loop through all reservations (after applying a filter to the list)
            foreach (Reservation option in FilterReservations())
            {
                //Display the info about the reservation
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
                if (option.orderType == "Reservation")
                {
                    Text optionTable = new Text("\n" + "Table name: " + foundTable.name, ConsoleColor.Green);
                    optionTable.Display();
                }
                Text optionType = new Text("\n" + "Type: " + option.orderType, ConsoleColor.Green);
                optionType.Display();

                Text optionorderID = new Text("\n" + "Reservation Code: " + option.orderID, ConsoleColor.Green);
                optionorderID.Display();

                Text optionDate = new Text("\n" + "Date: " + option.date, ConsoleColor.Green);
                optionDate.Display();

                Text optionStatus = new Text("\n" + "Status: " + option.status, ConsoleColor.Green);
                optionStatus.Display();

                Console.WriteLine("\n");
            }
            Menu menu = new Menu(new Text("Press enter to go back"));
            menu.Display();
        }

        private static Reservation[] FilterReservations()
        {
            Reservation[] results;
            //if the user is logged in return the users reservations
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
            //If the user is a guest ask for credentials and then return all connected reservations
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
            //Load all reservations from the json
            LoadReservation();

            //Check if there are any reservations
            if (reservations.Length == 0)
            {
                //Display error and return
                Text line01 = new Text("\nThere are no current reservations.\n", ConsoleColor.Red);
                line01.Display();
                Menu menu = new Menu(new Text("Press enter to go back")); menu.Display();
                return;
            }
            
            //Request the unique id that belongs to that reservation
            Input orderIDInput = new Input(new Text("\nEnter the unique ID of the reservation you want to cancel: "), new Text("\nPlease enter a valid ID!", ConsoleColor.Red), new InputSettings(false, 8, 8, "a-z0-9"));
            string orderID = orderIDInput.Display();
            
            //Find the correct reservation
            for (int i = 0; i < reservations.Length; i++)
            {
                //Check if the reservation data matches the input and is still valid
                if (reservations[i].orderID == orderID && reservations[i].status != "Canceled") 
                {
                    //Check if it has expired (you are too late)
                    if (reservations[i].status == "Expired" || CheckExpired(reservations[i]))
                    {
                        //Show error
                        Text expiredText = new Text("\nThis reservation has expired", ConsoleColor.Red);
                        expiredText.Display();
                        
                        //Update reservation status if needed
                        reservations[i].status = "Expired";
                        //Save reservations
                        SaveReservation();

                        return;
                    }

                    //Cancel order?
                    Text reservation = new Text("\nDo you want to cancel this order?");
                    Text[] options = new Text[]
                    {
                        new Text("Yes"),
                        new Text("No"),
                    };

                    //If yes then set status to canceled
                    Menu setupReservation = new Menu(reservation, options);
                    int response = setupReservation.Display();
                    if (response == 0) {
                        //Change status to canceled
                        reservations[i].status = "Canceled";
                        SaveReservation();
                    }
                    return;
                }
                //If none can be found display an error
                else if (i == reservations.Length-1 ) {
                    Text line01 = new Text("\nYour reservation was not found!\n", ConsoleColor.Red); line01.Display();
                    Menu menu = new Menu(new Text("Press enter to go back"));
                    menu.Display();
                    return;
                }
            }
        }

        private static bool ValidateDate(string date)
        {
            //Check if the given string is a valid datetime
            try
            {
                DateTime dateTime = DateTime.Parse(date);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ReservationAvailable(string date, ReservationOptions table)
        {
            //Get the count of tables that are available
            int count = table.tables;

            //Check how many tables have been reserved for that time slot
            foreach (Reservation reservation in reservations)
            {
                //Check if a reservation has expired
                if (CheckExpired(reservation))
                {
                    reservation.status = "Expired";
                }

                //Check if its the correct table, if the status is correct and if it is in the same time slot
                if(reservation.tableID == table.id && reservation.status == "Open" && reservation.orderType == "Reservation" && DateInRange(reservation.date, date))
                {
                    count--;
                }
            }
            //Save possible changes
            SaveReservation();

            //If all tables have been reserved return false
            if (count <= 0)
            {
                return false;
            }
            //There are tables available so return true
            return true;
        }

        private static bool DateInRange(string reservation, string date)
        {
            //Convert string to datetime
            DateTime reservationDate = DateTime.Parse(reservation);

            //Create the range by adding and removing 2 hours from the datetime
            DateTime rangeMin = reservationDate.AddHours(-2);
            DateTime rangeMax = reservationDate.AddHours(2);

            //Convert string to datetime
            DateTime selectedDate = DateTime.Parse(date);

            //Check if the datetime is in the 2 hour range
            return selectedDate > rangeMin && selectedDate < rangeMax;
        }

        private static bool CheckExpired(Reservation reservation)
        {
            //Convert string to datetime
            DateTime date = DateTime.Parse(reservation.date.ToString());
            //Get current datetime
            DateTime now = DateTime.Now;

            //set reservation date to the time the reservation ends
            date.AddHours(2);

            //has expired
            if (now >= date) { return true; }

            return false;
        }
    }
}