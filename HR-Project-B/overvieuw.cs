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
    class overvieuw

    {
        public static ReservationOptions[] tables;
        public static Reservation[] reservations;
        public static 
            
            

        public static void Start()
        {
            LoadReservation();
            LoadTables();
        }
        //reservation data
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
        private static void SaveallData()
        {
            FileManager fm = new FileManager("allData.json");

            dynamic[] foundOptions = new dynamic[reservations.Length];
            for (int i = 0; i < reservations.Length; i++)
            {
                foundOptions[i] = reservations[i];
            }

            fm.WriteJSON(foundOptions);
        }
    }
}
