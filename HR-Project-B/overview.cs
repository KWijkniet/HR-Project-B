using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HR_Project_B.Components;
using System.Text.RegularExpressions;
//We tried to make an overview page where you can see what items are ordered and what the profit is, but it didn't work due to numerous errors we weren't able to fix, such as saving the items and sending it to a json file.

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
