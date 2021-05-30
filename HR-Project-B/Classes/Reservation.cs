using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class Reservation
    {
        public string creditcardNumber;
        public string orderType; //take-away || table reservation
        public string orderID; //unique
        public string tableID;
        public string userID;
        public string userName;
        public string userEmail;
        public string date;
        public string status;

        public Reservation(string creditcardNumber, string orderType, string tableID, string userID, string userName, string userEmail, string date)
        {
            this.creditcardNumber = creditcardNumber;
            this.orderType = orderType;

            this.orderID = Guid.NewGuid().ToString().Substring(0,8);

            this.tableID = tableID;
            this.userID = userID;
            this.userName = userName;
            this.userEmail = userEmail;
            this.date = date;
            this.status = "Open";
        }

        public Reservation(dynamic data)
        {
            this.creditcardNumber = data.creditcardNumber;
            this.orderType = data.orderType;
            this.orderID = data.orderID;
            this.tableID = data.tableID;
            this.userID = data.userID;
            this.userName = data.userName;
            this.userEmail = data.userEmail;
            this.date = data.date;
            this.status = data.status;
        }
    }
}


