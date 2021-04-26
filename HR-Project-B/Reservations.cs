using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class Reservations
    {
        public static ReservationOptions[] reservationOptions;
        public static void Start()
        {
            LoadReservation();

            if (Program.account.role <= 1 )
            {
                ViewReservations();
                return;
            }

            while (true){
                Program.ClearConsole();
                Text reservation = new Text("Reservation options");
                Text[] options = new Text[]
                {
                    new Text("View"),
                    new Text("Create"),
                    new Text("Edit"),
                    new Text("Back")
                };

                Menu setupReservation = new Menu(reservation, options);
                int response = setupReservation.Display();

                Program.ClearConsole();
                switch (response)
                {
                    case 0:
                        //View Menu
                        ViewReservations();
                        break;
                    case 1:
                        //Create Category
                        CreateReservationOptions();
                        break;
                    case 2:
                        //Edit Category
                        ReservationShowEditCategories();
                        break;
                    case 3:
                        //back
                        return;
                    default:
                        break;
                }


            
            
           
            }



            
        }

        public static void ViewReservations()
        {
            foreach (ReservationOptions option in reservationOptions)
            {
                
                Text optionFee = new Text("\n"+ "Fee: " + option.fee, ConsoleColor.Green);
                optionFee.Display();

                Text optionName= new Text("\n" + "Name: "+ option.name, ConsoleColor.Green);
                optionName.Display();

                Text optionChairsPairTable = new Text("\n"+ "Chairspairtable: " + option.chairsPairTable, ConsoleColor.Green);
                optionChairsPairTable.Display();

                Text optionTables = new Text("\n"+ "Tables: " + option.tables, ConsoleColor.Green);
                optionTables.Display();

                Console.WriteLine("\n");
            }
            Menu menu = new Menu(new Text("Press enter to go back"));
            menu.Display();

        }

        // Load accounts from the file
        private static void LoadReservation()
        {
            FileManager fm = new FileManager("ReservationOptions.json");

            dynamic[] foundOptions = fm.ReadJSON();
            reservationOptions = new ReservationOptions[foundOptions.Length];
            for (int i = 0; i < foundOptions.Length; i++)
            {
                ReservationOptions newReservationOptions = new ReservationOptions(foundOptions[i]);

                reservationOptions[i] = newReservationOptions;


            }
        }

        // Save accounts to the file
        private static void SaveReservation()
        {
            FileManager fm = new FileManager("ReservationOptions.json");

            dynamic[] foundOptions = new dynamic[reservationOptions.Length];
            for (int i = 0; i < reservationOptions.Length; i++)
            {
                foundOptions[i] = reservationOptions[i];
            }

            fm.WriteJSON(foundOptions);
        }

        private static void CreateReservationOptions()
        {
            Input nameInput = new Input(new Text("\n Enter name: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings());
            string name = nameInput.Display();
            if (name == null)
            {
                return;
            }

            double feedouble;
            while (true)
            
            {
                Input feeInput = new Input(new Text("\nCost of fee: "), new Text("\nPlease enter a valid cost of fee!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9,."));
                string fee = feeInput.Display();
                if (fee == null)
                {
                    return;
                }

                try
                {
                    var parts = fee.Split(".");
                    fee = string.Join(",", parts);
                    feedouble = double.Parse(fee);

                    break;
                }
                catch (Exception)
                {
                    Text error = new Text("\nPlease enter a valid cost of fee!", ConsoleColor.Red);
                    error.Display();
                }
            }

            int tablesInt;
            while (true)

            {
                Input tablesInput = new Input(new Text("\nNumber of tables: "), new Text("\nPlease enter a valid number of tables!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9"));
                string tables = tablesInput.Display();
                if (tables == null)
                {
                    return;
                }

                try
                {

                    tablesInt = int.Parse(tables);

                    break;
                }
                catch (Exception)
                {
                    Text error = new Text("\nPlease enter a valid number of tables!", ConsoleColor.Red);
                    error.Display();
                }
            }
            int chairsPairTableInt;
            while (true)

            {
                Input chairsPairTableInput = new Input(new Text("\nChairs pair table: "), new Text("\nPlease enter a valid chairs pair table!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9"));
                string chairsPairTable = chairsPairTableInput.Display();
                if (chairsPairTable == null)
                {
                    return;
                }

                try
                {

                    chairsPairTableInt = int.Parse(chairsPairTable);

                    break;
                }
                catch (Exception)
                {
                    Text error = new Text("\nPlease enter a valid chairs pair table!", ConsoleColor.Red);
                    error.Display();
                }
            }



            AddReservationOptions(new ReservationOptions(feedouble, name, chairsPairTableInt, tablesInt));
            SaveReservation();
        }

        private static void AddReservationOptions(ReservationOptions reservation)
        {
            ReservationOptions[] temp = new ReservationOptions[reservationOptions.Length + 1];
            for (int i = 0; i < reservationOptions.Length; i++)
            {
                temp[i] = reservationOptions[i];
            }
            temp[^1] = reservation;

            reservationOptions = temp;
        }

        private static void RemoveReservationOptions(ReservationOptions reservation)
        {
            ReservationOptions[] temp = new ReservationOptions[reservationOptions.Length - 1];
            int index = 0;
            for (int i = 0; i < reservationOptions.Length; i++)
            {
                if (reservationOptions[i].id != reservation.id)
                {
                    temp[index] = reservationOptions[i];
                    index++;
                }
            }

            reservationOptions = temp;
        }

        private static string[] ReservationNames()
        {
            string[] temp = new string[reservationOptions.Length];
            for (int i = 0; i < reservationOptions.Length; i++)
            {
                temp[i] = reservationOptions[i].name;
            }
            return temp;
        }

        private static void ReservationShowEditCategories()
        {
            while (true)
            {
                Program.ClearConsole();
                string[] options = ReservationNames();

                Text message = new Text("Select a category to edit:");
                Text[] messages = new Text[options.Length + 1];

                for (int i = 0; i < options.Length; i++)
                {
                    messages[i] = new Text(options[i]);
                }
                messages[^1] = new Text("Back");

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                if (selected == messages.Length - 1) { return; }
                ReservationOptions reservation = reservationOptions[selected];

                ReservationEditCategory(reservation);
            }
        }

        private static void ReservationEditCategory(ReservationOptions reservation)
        {
            while (true)
            {
                Program.ClearConsole();

                Text message = new Text("Select a reservation to edit:");
                Text[] messages = new Text[]
                {
                    new Text("change name"),
                    new Text("change fee"),
                    new Text("change chairs pair table"),
                    new Text("change Table"),
                    new Text("Delete"),
                    new Text("Back"),
                };

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                switch (selected)
                {
                    case 0:
                        //Edit name
                        Input nameInput = new Input(new Text("\nChange name: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings());
                        string name = nameInput.Display();
                        if (name == null)
                        {
                            return;
                        }
                        reservation.name = name;
                        SaveReservation();
                        break;
                    case 1:
                        // fee
                        double feedouble;
                        while (true)

                        {
                            Input feeInput = new Input(new Text("\n Change cost of fee: "), new Text("\nPlease enter a valid cost of fee!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9,."));
                            string fee = feeInput.Display();
                            if (fee == null)
                            {
                                return;
                            }

                            try
                            {
                                var parts = fee.Split(".");
                                fee = string.Join(",", parts);
                                feedouble = double.Parse(fee);
                                
                                reservation.fee = feedouble;
                                SaveReservation();
                                break;
                            }
                            catch (Exception)
                            {
                                Text error = new Text("\nPlease enter a valid cost of fee!", ConsoleColor.Red);
                                error.Display();
                            }
                        }

                        break;
                    case 2:
                        // chairs pair table
                        int chairsPairTableInt;
                        while (true)

                        {
                            Input chairsPairTableInput = new Input(new Text("\n Change chairs pair table: "), new Text("\nPlease enter a valid chairs pair table!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9"));
                            string chairsPairTable = chairsPairTableInput.Display();
                            if (chairsPairTable == null)
                            {
                                return;
                            }

                            try
                            {

                                chairsPairTableInt = int.Parse(chairsPairTable);
                                reservation.chairsPairTable = chairsPairTableInt;
                                SaveReservation();
                                break;
                            }
                            catch (Exception)
                            {
                                Text error = new Text("\nPlease enter a valid chairs pair table!", ConsoleColor.Red);
                                error.Display();
                            }
                        }
                        break;
                    case 3:
                        // tables
                        int tablesInt;
                        while (true)

                        {
                            Input tablesInput = new Input(new Text("\nNumber of tables: "), new Text("\nPlease enter a valid number of tables!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9"));
                            string tables = tablesInput.Display();
                            if (tables == null)
                            {
                                return;
                            }

                            try
                            {

                                tablesInt = int.Parse(tables);
                                reservation.tables = tablesInt;
                                SaveReservation();
                                break;
                            }
                            catch (Exception)
                            {
                                Text error = new Text("\nPlease enter a valid number of tables!", ConsoleColor.Red);
                                error.Display();
                            }
                        }
                        break;

                    case 4:
                        //Delete
                        RemoveReservationOptions(reservation);
                        SaveReservation();
                        return;
                    case 5:
                        //Back
                        return;
                    default:
                        break;
                }
            }
        }


    }
}
