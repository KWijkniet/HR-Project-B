using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class ShoppingBasket
    {
        private ShoppingBasketItem[] items = new ShoppingBasketItem[0];

        public void AddToBasket(string id, int amount)
        {
            foreach (ShoppingBasketItem item in items)
            {
                if(item.id == id)
                {
                    item.amount += amount;
                    return;
                }
            }

            ShoppingBasketItem[] tmp = new ShoppingBasketItem[items.Length + 1];
            for (int i = 0; i < items.Length; i++)
            {
                tmp[i] = items[i];
            }
            tmp[^1] = new ShoppingBasketItem(id, amount);
            items = tmp;
        }

        public void RemoveFromBasket(string id, int amount)
        {
            int index = -1;
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i].id == id)
                {
                    items[i].amount -= amount;
                    if(items[i].amount <= 0)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if(index >= 0)
            {
                ShoppingBasketItem[] tmp = new ShoppingBasketItem[items.Length - 1];
                int newIndex = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if(i != index)
                    {
                        tmp[newIndex] = items[newIndex];
                        newIndex += 1;
                    }
                }
                items = tmp;
            }
        }

        public ShoppingBasketItem[] GetBasket()
        {
            return items;
        }

        public void SetBasket(ShoppingBasketItem[] items)
        {
            this.items = items;
        }
    }
}
