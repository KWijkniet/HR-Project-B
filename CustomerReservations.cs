using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class Class1
    {
        public static ReservationOptions[] reservationOptions;
        public static void Start()
        {
            LoadReservation();

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



        private static void CreateReservation()
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



            AddReservationOptions(new ReservationOptions(, name, chairsPairTableInt, tablesInt));
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


        public static void ViewReservations()
        {
            foreach (ReservationOptions option in reservationOptions)
            {

                Text optionFee = new Text("\n" + "Fee: " + option.fee, ConsoleColor.Green);
                optionFee.Display();

                Text optionName = new Text("\n" + "Name: " + option.name, ConsoleColor.Green);
                optionName.Display();

                Text optionChairsPairTable = new Text("\n" + "Chairspairtable: " + option.chairsPairTable, ConsoleColor.Green);
                optionChairsPairTable.Display();

                Text optionTables = new Text("\n" + "Tables: " + option.tables, ConsoleColor.Green);
                optionTables.Display();

                Console.WriteLine("\n");
            }
            Menu menu = new Menu(new Text("Press enter to go back"));
            menu.Display();

        }








    }
}