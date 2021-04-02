using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class MenuCategory
    {
        public string id;
        public string name;
        public string description;
        public List<MenuItem> items;

        public MenuCategory(string name, string description)
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.description = description;
            this.items = new List<MenuItem>();
        }

        public MenuCategory(dynamic data)
        {
            this.id = data.id;
            this.name = data.name;
            this.description = data.description;
            this.items = new List<MenuItem>();

            foreach (dynamic item in data.items)
            {
                AddItem(item);
            }
        }

        //Add MenuItem to items
        public void AddItem(MenuItem item)
        {
            items.Add(item);
        }

        //Create and add MenuItem to items from dynamic
        public void AddItem(dynamic dynamicItem)
        {
            MenuItem item = new MenuItem(dynamicItem);
            AddItem(item);
        }

        //Remove MenuItem from items
        public void RemoveItem(MenuItem item)
        {
            items.Remove(item);
        }

        //Find and remove MenuItem from items by id
        public void RemoveItem(string id)
        {
            foreach (MenuItem item in items)
            {
                if(item.id == id)
                {
                    RemoveItem(item);
                    return;
                }
            }
        }

        //Returns all menu item names
        public string[] GetItemNames()
        {
            string[] temp = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                temp[i] = items[i].name;
            }

            return temp;
        }

        //Get a item based on index
        public MenuItem GetItem(int index)
        {
            return items[index];
        }

        public void UpdateItem(int index, MenuItem item)
        {
            items[index] = item;
        }
    }
}
