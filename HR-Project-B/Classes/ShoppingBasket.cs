using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class ShoppingBasket
    {
        private Dictionary<string, int> items = null;

        public void AddToBasket(string id, int amount)
        {
            if (items.ContainsKey(id))
            {
                items[id] += amount;
            }
            else
            {
                items.Add(id, amount);
            }
        }

        public void RemoveFromBasket(string id, int amount)
        {
            if (items.ContainsKey(id))
            {
                items[id] -= amount;

                if(items[id] <= 0)
                {
                    items.Remove(id);
                }
            }
        }

        public Dictionary<string, int> GetBasket()
        {
            return items;
        }
    }
}
