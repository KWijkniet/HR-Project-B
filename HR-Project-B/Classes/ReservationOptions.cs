using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class ReservationOptions
    {
        public string id;
        public double fee;
        public string name;
        public int chairsPairTable;
        public int tables;


        public ReservationOptions(double fee, string name, int chairsPairTable, int tables)
        {
            this.id = Guid.NewGuid().ToString();
            this.fee = fee;
            this.name = name;
            this.chairsPairTable = chairsPairTable;
            this.tables = tables;
            
        }

        public ReservationOptions(dynamic data)
        {
            this.id = data.id;
            this.fee = data.fee;
            this.name = data.name;
            this.chairsPairTable = data.chairsPairTable;
            this.tables = data.tables;

        }

    }
}
