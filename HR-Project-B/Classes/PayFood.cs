﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B.Classes
{
    class PayFood
    {
        public string orderID;
        public ShoppingBasket shoppingBasket;

        public PayFood(string orderID, ShoppingBasket shoppingBasket) 
        {
            this.orderID = orderID;
            this.shoppingBasket = shoppingBasket;
        }

        public PayFood(dynamic data)
        {
            this.orderID = data.orderID;
            this.shoppingBasket = data.shoppingBasket;
        }

    }



}
