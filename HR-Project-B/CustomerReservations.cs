﻿using System;
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


        private static void LoadReservation()
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
                Input emailInput = new Input(new Text("\nEmail:"), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z0-9_.-@"));
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
            //make resevation here
            Program.ClearConsole();

            Text message = new Text("Select a table:");
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

            Reservation reservation = new Reservation("", "Table reservation", table.id,Program.account.role == 1 ? "": Program.account.id, name, email);

            Reservation[] temp = new Reservation[reservations.Length + 1];
            for (int i = 0; i < reservations.Length; i++)
            {
                temp[i] = reservations[i];
            }
            temp[^1] = reservation;
            reservations = temp;
            SaveReservation();


        }

        private static void VieuwReservation()
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

                Text optionTable = new Text("\n" + "Table name: " + foundTable.name, ConsoleColor.Green);
                optionTable.Display();

                Text optionorderID = new Text("\n" + "Order id: " + option.orderID, ConsoleColor.Green);
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
                    Input emailInput = new Input(new Text("\nEmail:"), new Text("\nPlease enter a valid email!", ConsoleColor.Red), new InputSettings(false, 3, 15, "A-Za-z0-9_.-@", "", false));
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










    }
}