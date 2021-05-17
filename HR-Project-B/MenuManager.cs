using System;
using HR_Project_B.Components;

namespace HR_Project_B
{
    class MenuManager
    {
        private static MenuCategory[] categories;
        private static FileManager fm = new FileManager("Menu.json");
        private static ShoppingBasket basket = new ShoppingBasket();

        public static void Start()
        {
            LoadMenu();

            if (Program.account.role <= 1 || Program.account.role == 3)
            {
                ViewMenu();
                return;
            }

            while (true)
            {
                Program.ClearConsole();

                Text message = new Text("Menu Manager:");
                Text[] messages = new Text[]
                {
                    new Text("View menu"),
                    new Text("Create Category"),
                    new Text("Edit Category"),
                    new Text("Back"),
                };

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();
                switch (selected)
                {
                    case 0:
                        //View Menu
                        ViewMenu();
                        break;
                    case 1:
                        //Create Category
                        CreateCategory();
                        break;
                    case 2:
                        //Edit Category
                        ShowEditCategories();
                        break;
                    case 3:
                        //back
                        return;
                    default:
                        break;
                }
            }
        }

        private static void ViewMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            foreach (MenuCategory category in categories)
            {
                Text categoryText = new Text("\n" + category.name, ConsoleColor.Green);
                categoryText.Display();

                if(category.description.Length > 0)
                {
                    Text categoryDescription = new Text("\n" + category.description, ConsoleColor.Green);
                    categoryDescription.Display();
                }

                int length = category.GetItemNames().Length;
                for(int i = 0; i < length; i++)
                {
                    MenuItem item = category.GetItem(i);

                    Text itemText = new Text("\n- " + item.name + " (€" + item.price + "):");
                    itemText.Display();

                    if (item.description.Length > 0)
                    {
                        Text itemDescription = new Text("\n  " + item.description);
                        itemDescription.Display();
                    }
                }
                Console.WriteLine("\n");
            }

            Text title = new Text("Select an option below:");
            Text[] options = new Text[]
            {
                new Text("Pay"),
                new Text("Back")
            };
            Menu menu = new Menu(title, options);
            int selected = menu.Display();

            switch (selected)
            {
                case 0:
                    PayMenu();
                    break;
                case 1:
                    return;
                default:
                    break;
            }
        }

        private static void CreateCategory()
        {
            Program.ClearConsole();
            if (Program.account.role <= 2) { return; }

            Input nameInput = new Input(new Text("\nCategory name: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings());
            string name = nameInput.Display();
            if(name == null)
            {
                return;
            }

            Input descriptionInput = new Input(new Text("\nCategory description: "), new Text("\nPlease enter a valid description!", ConsoleColor.Red), new InputSettings(false, 0));
            string description = descriptionInput.Display();
            if (name == null)
            {
                return;
            }

            AddCategory(new MenuCategory(name, description));
            SaveMenu();
        }

        private static void ShowEditCategories()
        {
            while (true)
            {
                Program.ClearConsole();
                string[] options = CategoryNames();

                Text message = new Text("Select a category to edit:");
                Text[] messages = new Text[options.Length + 1];

                for (int i = 0; i < options.Length; i++)
                {
                    messages[i] = new Text(options[i]);
                }
                messages[^1] = new Text("Back");

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                if (selected == messages.Length - 1) { return; }
                MenuCategory category = categories[selected];

                EditCategory(category);
            }
        }

        private static void EditCategory(MenuCategory category)
        {
            while (true)
            {
                Program.ClearConsole();

                Text message = new Text("Select a category to edit:");
                Text[] messages = new Text[]
                {
                    new Text("Edit name"),
                    new Text("Edit description"),
                    new Text("Add Item"),
                    new Text("Edit Item"),
                    new Text("Delete"),
                    new Text("Back"),
                };

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                switch (selected)
                {
                    case 0:
                        //Edit name
                        Input nameInput = new Input(new Text("\nCategory name: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings());
                        string name = nameInput.Display();
                        if(name == null)
                        {
                            return;
                        }

                        category.name = name;
                        SaveMenu();
                        break;
                    case 1:
                        //Edit description
                        Input descriptionInput = new Input(new Text("\nCategory description: "), new Text("\nPlease enter a valid description!", ConsoleColor.Red), new InputSettings(false, 0));
                        string description = descriptionInput.Display();
                        if (description == null)
                        {
                            return;
                        }

                        category.description = description;
                        SaveMenu();
                        break;
                    case 2:
                        CreateItem(category);
                        break;
                    case 3:
                        ShowEditItem(category);
                        break;
                    case 4:
                        //Delete
                        RemoveCategory(category);
                        SaveMenu();
                        return;
                    case 5:
                        //Back
                        return;
                    default:
                        break;
                }
            }
        }

        private static void CreateItem(MenuCategory category)
        {
            Program.ClearConsole();
            if (Program.account.role <= 2) { return; }

            Input nameInput = new Input(new Text("\nItem name: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings());
            string name = nameInput.Display();
            if(name == null)
            {
                return;
            }

            Input descriptionInput = new Input(new Text("\nItem description: "), new Text("\nPlease enter a valid description!", ConsoleColor.Red), new InputSettings());
            string description = descriptionInput.Display();
            if (description == null)
            {
                return;
            }

            while (true)
            {
                Input priceInput = new Input(new Text("\nItem price: "), new Text("\nPlease enter a valid price!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9,."));
                string price = priceInput.Display();
                if (price == null)
                {
                    return;
                }

                try
                {
                    var parts = price.Split(".");
                    price = string.Join(",", parts);
                    double result = double.Parse(price);

                    MenuItem item = new MenuItem(category.id, name, description, result);
                    category.AddItem(item);
                    SaveMenu();
                    return;
                }
                catch (Exception)
                {
                    Text error = new Text("\nPlease enter a valid price!", ConsoleColor.Red);
                    error.Display();
                }
            }
        }

        private static void ShowEditItem(MenuCategory category)
        {
            while (true)
            {
                Program.ClearConsole();
                string[] options = category.GetItemNames();

                Text message = new Text("Select an item to edit:");
                Text[] messages = new Text[options.Length + 1];

                for (int i = 0; i < options.Length; i++)
                {
                    messages[i] = new Text(options[i]);
                }
                messages[^1] = new Text("Back");

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                if (selected == messages.Length - 1) { return; }
                MenuItem menuItem = category.GetItem(selected);

                EditItem(selected, category, menuItem);
            }
        }

        private static void EditItem(int mainIndex, MenuCategory category, MenuItem menuItem)
        {
            while (true)
            {
                Program.ClearConsole();

                Text message = new Text("Item Manager:");
                Text[] messages = new Text[]
                {
                    new Text("Edit name"),
                    new Text("Edit description"),
                    new Text("Edit price"),
                    new Text("Delete"),
                    new Text("Back"),
                };

                Menu menu = new Menu(message, messages);
                int selected = menu.Display();

                Program.ClearConsole();

                switch (selected)
                {
                    case 0:
                        //Edit name
                        Input nameInput = new Input(new Text("\nItem name: "), new Text("\nPlease enter a valid name!", ConsoleColor.Red), new InputSettings());
                        string name = nameInput.Display();
                        if (name == null)
                        {
                            break;
                        }

                        menuItem.name = name;
                        category.UpdateItem(mainIndex, menuItem);
                        SaveMenu();
                        break;
                    case 1:
                        //Edit description
                        Input descriptionInput = new Input(new Text("\nItem description: "), new Text("\nPlease enter a valid description!", ConsoleColor.Red), new InputSettings());
                        string description = descriptionInput.Display();
                        if (description == null)
                        {
                            break;
                        }

                        menuItem.description = description;
                        category.UpdateItem(mainIndex, menuItem);
                        SaveMenu();
                        break;
                    case 2:
                        //Edit price
                        if(Program.account.role <= 2)
                        {
                            break;
                        }

                        while (true)
                        {
                            Input priceInput = new Input(new Text("\nItem price: "), new Text("\nPlease enter a valid price!", ConsoleColor.Red), new InputSettings(false, 1, 999, "0-9,."));
                            string price = priceInput.Display();
                            if (price == null)
                            {
                                break;
                            }

                            try
                            {
                                var parts = price.Split(".");
                                price = string.Join(",", parts);
                                double result = double.Parse(price);

                                menuItem.price = result;
                                category.UpdateItem(mainIndex, menuItem);
                                SaveMenu();
                                return;
                            }
                            catch (Exception)
                            {
                                Text error = new Text("\nPlease enter a valid price!", ConsoleColor.Red);
                                error.Display();
                            }
                        }
                        break;
                    case 3:
                        //Delete
                        category.RemoveItem(menuItem);
                        SaveMenu();
                        return;
                    case 4:
                        //Back
                        return;
                    default:
                        break;
                }
            }
        }

        private static void LoadMenu()
        {
            dynamic[] found = fm.ReadJSON();
            categories = new MenuCategory[found.Length];
            for (int i = 0; i < found.Length; i++)
            {
                MenuCategory item = new MenuCategory(found[i]);

                categories[i] = item;
            }
        }

        private static void SaveMenu()
        {
            dynamic[] found = new dynamic[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                found[i] = categories[i];
            }

            fm.WriteJSON(found);
        }

        private static void AddCategory(MenuCategory category)
        {
            MenuCategory[] temp = new MenuCategory[categories.Length + 1];
            for (int i = 0; i < categories.Length; i++)
            {
                temp[i] = categories[i];
            }
            temp[^1] = category;

            categories = temp;
        }

        private static void RemoveCategory(MenuCategory category)
        {
            MenuCategory[] temp = new MenuCategory[categories.Length - 1];
            int index = 0;
            for (int i = 0; i < categories.Length; i++)
            {
                if(categories[i].id != category.id)
                {
                    temp[index] = categories[i];
                    index++;
                }
            }

            categories = temp;
        }

        private static string[] CategoryNames()
        {
            string[] temp = new string[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                temp[i] = categories[i].name;
            }
            return temp;
        }

        private static void PayMenu()
        {
            while (true)
            {
                Program.ClearConsole();
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                int count = 0;

                for (int i = 0; i < categories.Length; i++)
                {
                    count += categories[i].items.Count;
                }

                Text title = new Text("Select an option below:");
                Text[] options = new Text[count + 2];

                int index = 0;
                for (int i = 0; i < categories.Length; i++)
                {
                    for (int j = 0; j < categories[i].items.Count; j++)
                    {
                        MenuItem item = categories[i].items[j];
                        Text tmp = new Text((index + 1) + ". " + item.name + " (€" + item.price + ")");

                        options[index] = tmp;
                        index++;
                    }
                }
                options[^1] = new Text("Back");
                options[^2] = new Text("Next step");

                Menu menu = new Menu(title, options);
                int selected = menu.Display();

                if (selected == options.Length - 1)
                {
                    //back
                    return;
                }
                else if (selected == options.Length - 2)
                {
                    //next step
                    Payment.Start(basket, categories);
                    title = new Text("Select an option below:");
                    options = new Text[]
                    {
                        new Text("Confirm"),
                        new Text("Back")
                    };

                    menu = new Menu(title, options);
                    selected = menu.Display();

                    if(selected == 0)
                    {
                        Program.ClearConsole();
                        Payment.Start(basket, categories);
                        Payment.Pay();
                    }
                }
                else
                {
                    //select amount
                    PayMenuItemOptions(selected);
                }
            }
        }

        private static void PayMenuItemOptions(int selectedItem)
        {
            while (true)
            {
                Program.ClearConsole();
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Text title = new Text("Select an option below:");
                Text[] options = new Text[]
                {
                    new Text("Add to basket"),
                    new Text("Remove from basket"),
                    new Text("Back")
                };

                Menu menu = new Menu(title, options);
                int selected = menu.Display();

                if (selected != 2)
                {
                    Input amountInput = new Input(new Text("How many?"), new Text("Please enter an invalid amount.", ConsoleColor.Red), new InputSettings(true));
                    string result = amountInput.Display();
                    if (result == null)
                    {
                        continue;
                    }

                    try
                    {
                        int amount = int.Parse(result);

                        int index = 0;
                        string id = "";
                        for (int i = 0; i < categories.Length; i++)
                        {
                            for (int j = 0; j < categories[i].items.Count; j++)
                            {
                                if (index == selectedItem)
                                {
                                    id = categories[i].items[j].id;
                                    break;
                                }
                                index++;
                            }

                            if (id.Length > 0)
                            {
                                break;
                            }
                        }

                        //add to basket
                        if(selected == 0)
                        {
                            basket.AddToBasket(id, amount);
                        }
                        else
                        {
                            basket.RemoveFromBasket(id, amount);
                        }
                    }
                    catch (Exception)
                    {
                        Text error = new Text("\nPlease enter a valid amount!", ConsoleColor.Red);
                        error.Display();
                    }
                }
                return;
            }
        }
    }
}
