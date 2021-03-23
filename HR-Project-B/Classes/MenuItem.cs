using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class MenuItem
    {
        public string id;
        public string categoryId;
        public string name;
        public string description;
        public double price;

        public MenuItem(string categoryId, string name, string description, double price)
        {
            this.id = Guid.NewGuid().ToString();
            this.categoryId = categoryId;
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public MenuItem(dynamic data)
        {
            this.id = data.id;
            this.categoryId = data.categoryId;
            this.name = data.name;
            this.description = data.description;
            this.price = data.price;
        }
    }
}
