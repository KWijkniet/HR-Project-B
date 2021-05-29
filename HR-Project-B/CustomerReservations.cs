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
                        //Vieuw Category
                        VieuwReservation();
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
                Input emailInput = new Input(new Text("\nEmail:"), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 6, 225, "A-Za-z0-9_.-@", "", false));
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

                Input nameInput = new Input(new Text("\nName:"), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z "));
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
                    Text error = new Text("\nPlease enter a valid date!LOL", ConsoleColor.Red);
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
           
        private static void VieuwReservation() //VIEWreservation
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

            if (reservations.Length == 0)
            {
                Text line01 = new Text("\nThere are no current reservations.\n", ConsoleColor.Red);
                line01.Display();
                Menu menu = new Menu(new Text("Press enter to go back")); menu.Display();
                return;
            }
            
            Input orderIDInput = new Input(new Text("\nEnter the unique ID of the reservation you want to cancel: "), new Text("\nPlease enter a valid ID!", ConsoleColor.Red), new InputSettings(false, 8, 8, "a-z0-9"));
            string orderID = orderIDInput.Display();
            
            for (int i = 0; i < reservations.Length; i++)
            {
                if (reservations[i].orderID == orderID && reservations[i].status != "Canceled") 
                {
                    if (reservations[i].status == "Expired" || CheckExpired(reservations[i]))
                    {
                        Text expiredText = new Text("\nThis reservation has expired", ConsoleColor.Red);
                        expiredText.Display();
                        
                        reservations[i].status = "Expired";
                        SaveReservation();

                        return;
                    }

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
                        //Reservation[] temp = new Reservation[reservations.Length - 1];
                        //int index = 0;
                        //for (int j = 0; j < reservations.Length; j++)
                        //{
                        //    if (j != i)
                        //    {
                        //        temp[index] = reservations[j];
                        //        index++;
                        //    }
                        //}
                        //reservations = temp;
                        reservations[i].status = "Canceled";
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

        private static bool ValidateDate(string date)
        {
            string[] dateTimeParts = date.Split(' ');

            if(dateTimeParts.Length != 2)
            {
                return false;
            }

            string[] dateParts = dateTimeParts[0].Split('/');
            string[] timeParts = dateTimeParts[1].Split(':');

            if(dateParts.Length != 3 || timeParts.Length != 2)
            {
                return false;
            }

            if (dateParts[0].Length != 2 || dateParts[1].Length != 2 || dateParts[2].Length != 4 || timeParts[0].Length != 2 || timeParts[1].Length != 2)
            {
                return false;
            }

            //check if the selected date is within opening hours
            if(int.Parse(dateParts[0]) < 12)
            {
                return false;
            }

            return true;
        }

        private static bool ReservationAvailable(string date, ReservationOptions table)
        {
            int count = table.tables;
            foreach (Reservation reservation in reservations)
            {
                if (CheckExpired(reservation))
                {
                    reservation.status = "Expired";
                }

                if(reservation.tableID == table.id && reservation.status == "Open" && reservation.orderType == "Reservation" && DateInRange(reservation.date, date))
                {
                    count--;
                }
            }
            SaveReservation();

            if (count <= 0)
            {
                return false;
            }
            return true;
        }

        private static bool DateInRange(string reservation, string date)
        {
            string[] reservationParts = reservation.Split(' ');
            string[] timePartsA = reservationParts[1].Split(':');

            string[] dateParts = date.Split(' ');
            string[] timePartsB = dateParts[1].Split(':');

            if (Math.Abs(int.Parse(timePartsA[0]) - int.Parse(timePartsB[0])) < 2)
            {
                return true;
            }

            return false;
        }

        private static bool CheckExpired(Reservation reservation)
        {
            string now = DateTime.Now.ToString("dd/MM/yyy hh:mm");
            string[] nowParts = now.Split(' ');
            string[] dateNowParts = nowParts[0].Split('/');
            string[] timeNowParts = nowParts[1].Split(':');

            string[] parts = reservation.date.Split(' ');
            string[] dateParts = parts[0].Split('/');
            string[] timeParts = parts[1].Split(':');

            //check date
            if(int.Parse(dateNowParts[0]) > int.Parse(dateParts[0]) || int.Parse(dateNowParts[1]) > int.Parse(dateParts[1]) || int.Parse(dateNowParts[2]) > int.Parse(dateParts[2]))
            {
                //Console.WriteLine("Date expired: " + now + " VS " + reservation.date);
                return true;
            }

            //check time
            if (int.Parse(timeNowParts[0]) > int.Parse(timeParts[0]))
            {
                //Console.WriteLine("Time expired: " + now + " VS " + reservation.date);
                return true;
            }

            return false;
        }
    }
}