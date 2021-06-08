using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HR_Project_B.Components;
using System.Text.RegularExpressions;
//We tried to make a overvieuw page were you can see what items are orderd and how much the profit is. But it didn't work because we did get whole time errors or it didn't save the items and show it in to json file.

/*
namespace HR_Project_B
{
    class overvieuw

    {
        public static ReservationOptions[] tables;
        public static Reservation[] reservations;
        
            
            

        public static void Start()
        {
            LoadReservation();
            
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
}*/
