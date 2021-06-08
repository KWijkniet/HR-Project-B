using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class ShoppingBasketItem
    {
        public string id;
        public int amount;

        public ShoppingBasketItem(string id, int amount)
        {
            this.id = id;
            this.amount = amount;
        }

        public ShoppingBasketItem(dynamic data)
        {
            this.id = data.id;
            this.amount = data.amount;
        }
    }
}
