using System;
using System.Collections.Generic;
using System.Text;

namespace HR_Project_B
{
    class MenuManager
    {
        private static MenuCategory[] categories;
        private static FileManager fm = new FileManager("Menu.json", new string[] { "Data" });

        public static void Start()
        {
            LoadMenu();
            while (true)
            {
                Console.Clear();
                string title = "Menu Manager:";
                string[] options = new string[] { "View Menu", "Create Category", "Edit Category", "Back" };
                OptionMenu menu = new OptionMenu(title, options);
                int index = menu.Display();

                Console.Clear();
                switch (index)
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
                TextTool.TextColor(category.name, ConsoleColor.Green, true);
                TextTool.TextColor(category.description, ConsoleColor.Green, true);
                Console.WriteLine();
                int length = category.GetItemNames().Length;
                for(int i = 0; i < length; i++)
                {
                    MenuItem item = category.GetItem(i);
                    TextTool.TextColor("- " + item.name + " (€" + item.price + "):", ConsoleColor.White, true);
                    TextTool.TextColor("  " + item.description, ConsoleColor.White, true);
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("> Done");
            Console.ReadKey();
            Console.Clear();
            return;
        }

        private static void CreateCategory()
        {
            string name = "";
            string description = "";

            bool validInput = false;
            while (!validInput)
            {
                TextTool.TextColor("Category name:", ConsoleColor.Green, false);
                name = Console.ReadLine();
                if (name.Length > 0)
                {
                    validInput = true;
                }
                else
                {
                    TextTool.TextColor("Please enter a valid name!", ConsoleColor.Red, true);
                }
            }

            validInput = false;
            while (!validInput)
            {
                TextTool.TextColor("Category description:", ConsoleColor.Green, false);
                description = Console.ReadLine();
                if (description.Length > 0)
                {
                    validInput = true;
                }
                else
                {
                    TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                }
            }

            AddCategory(new MenuCategory(name, description));
            SaveMenu();
        }

        private static void ShowEditCategories()
        {
            while (true)
            {
                Console.Clear();
                string title = "Select a category to edit:";
                string[] options = CategoryNames();
                string[] temp = new string[options.Length + 1];
                for (int i = 0; i < options.Length; i++)
                {
                    temp[i] = options[i];
                }
                temp[^1] = "Back";
                options = temp;

                OptionMenu menu = new OptionMenu(title, options);
                int index = menu.Display();

                Console.Clear();
                if (index == options.Length - 1) { return; }
                MenuCategory category = categories[index];

                EditCategory(category);
            }
        }

        private static void EditCategory(MenuCategory category)
        {
            while (true)
            {
                Console.Clear();
                string title = "Menu Manager:";
                string[] options = new string[] { "Edit name", "Edit description", "Add Item", "Edit Item", "Delete", "Done" };
                OptionMenu menu = new OptionMenu(title, options);
                int index = menu.Display();

                Console.Clear();
                switch (index)
                {
                    case 0:
                        //Edit name
                        while (true)
                        {
                            TextTool.TextColor("Category name:", ConsoleColor.Green, false);
                            string name = Console.ReadLine();
                            if (name.Length > 0)
                            {
                                category.name = name;
                                SaveMenu();
                                break;
                            }
                            else
                            {
                                TextTool.TextColor("Please enter a valid name!", ConsoleColor.Red, true);
                            }
                        }
                        break;
                    case 1:
                        //Edit description
                        while (true)
                        {
                            TextTool.TextColor("Category description:", ConsoleColor.Green, false);
                            string description = Console.ReadLine();
                            if (description.Length > 0)
                            {
                                category.description = description;
                                SaveMenu();
                                break;
                            }
                            else
                            {
                                TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                            }
                        }
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
                        //Done
                        return;
                    default:
                        break;
                }
            }
        }

        private static void CreateItem(MenuCategory category)
        {
            Console.Clear();
            string name = "";
            string description = "";
            double price = 0.0;

            bool validInput = false;
            while (!validInput)
            {
                TextTool.TextColor("Item name:", ConsoleColor.Green, false);
                name = Console.ReadLine();
                if (name.Length > 0)
                {
                    validInput = true;
                }
                else
                {
                    TextTool.TextColor("Please enter a valid name!", ConsoleColor.Red, true);
                }
            }

            validInput = false;
            while (!validInput)
            {
                TextTool.TextColor("Item description:", ConsoleColor.Green, false);
                description = Console.ReadLine();
                if (description.Length > 0)
                {
                    validInput = true;
                }
                else
                {
                    TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                }
            }

            validInput = false;
            while (!validInput)
            {
                TextTool.TextColor("Item Price:", ConsoleColor.Green, false);
                string temp = Console.ReadLine();
                if (temp.Length > 0)
                {
                    try
                    {
                        price = double.Parse(temp);
                        validInput = true;
                    }
                    catch (Exception)
                    {
                        TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                    }
                }
                else
                {
                    TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                }
            }

            MenuItem item = new MenuItem(category.id, name, description, price);
            category.AddItem(item);

            SaveMenu();
        }

        private static void ShowEditItem(MenuCategory category)
        {
            while (true)
            {
                string title = "Select an item to edit:";
                string[] options = category.GetItemNames();
                string[] temp = new string[options.Length + 1];
                for (int i = 0; i < options.Length; i++)
                {
                    temp[i] = options[i];
                }
                temp[^1] = "Back";
                options = temp;

                OptionMenu menu = new OptionMenu(title, options);
                int mainIndex = menu.Display();

                Console.Clear();
                if (mainIndex == options.Length - 1) { return; }
                MenuItem menuItem = category.GetItem(mainIndex);

                EditItem(mainIndex, category, menuItem);
            }
        }

        private static void EditItem(int mainIndex, MenuCategory category, MenuItem menuItem)
        {
            while (true)
            {
                Console.Clear();
                string title = "Item Manager:";
                string[] options = new string[] { "Edit name", "Edit description", "Edit price", "Delete", "Done" };
                OptionMenu menu = new OptionMenu(title, options);
                int index = menu.Display();

                Console.Clear();
                switch (index)
                {
                    case 0:
                        //Edit name
                        while (true)
                        {
                            TextTool.TextColor("Item name:", ConsoleColor.Green, false);
                            string name = Console.ReadLine();
                            if (name.Length > 0)
                            {
                                menuItem.name = name;
                                category.UpdateItem(mainIndex, menuItem);
                                SaveMenu();
                                break;
                            }
                            else
                            {
                                TextTool.TextColor("Please enter a valid name!", ConsoleColor.Red, true);
                            }
                        }
                        break;
                    case 1:
                        //Edit description
                        while (true)
                        {
                            TextTool.TextColor("Item description:", ConsoleColor.Green, false);
                            string description = Console.ReadLine();
                            if (description.Length > 0)
                            {
                                menuItem.description = description;
                                category.UpdateItem(mainIndex, menuItem);
                                SaveMenu();
                                break;
                            }
                            else
                            {
                                TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                            }
                        }
                        break;
                    case 2:
                        //Edit price
                        while (true)
                        {
                            TextTool.TextColor("Item price:", ConsoleColor.Green, false);
                            string tempPrice = Console.ReadLine();
                            if (tempPrice.Length > 0)
                            {
                                try
                                {
                                    menuItem.price = double.Parse(tempPrice);
                                    category.UpdateItem(mainIndex, menuItem);
                                    SaveMenu();
                                    break;
                                }
                                catch (Exception)
                                {
                                    TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                                }
                            }
                            else
                            {
                                TextTool.TextColor("Please enter a valid description!", ConsoleColor.Red, true);
                            }
                        }
                        break;
                    case 3:
                        //Delete
                        category.RemoveItem(menuItem);
                        SaveMenu();
                        return;
                    case 4:
                        //Done
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
    }
}
