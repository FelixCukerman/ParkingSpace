using System;
using System.Collections.Generic;
 
namespace ParkingSpace
{
    static class Menu
    {
        static Parking park = Parking.Create; 
        private static int index = 0;
  
        private static string drawMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
 
                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }
 
            ConsoleKeyInfo ckey = Console.ReadKey();
 
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {
                    //index = 0; //Remove the comment to return to the topmost item in the list
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    //index = menuItem.Count - 1; //Remove the comment to return to the item in the bottom of the list
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }
 
            Console.Clear();
            return "";
        }

        public static void Start()
        {
            List<string> menuItems = new List<string>() {
                "Add new car",
                "Remove car",
                "Show car",
                "Add cash to car",
                "Show pricelist",
                "Show current profit",
                "Remaining count",
                "Show all transactions",
                "Preview minute transactions",
                "Exit"
            };
 
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = Menu.drawMenu(menuItems);

                switch(selectedMenuItem)
                {
                    case "Add new car":
                    {
                        Console.Clear();
                        park.AddCar();
                        break;
                    }
                    
                    case "Remove car":
                    {
                        Console.Clear();
                        park.RemoveCar();
                        break;
                    }

                    case "Show car":
                    {
                        Console.Clear();
                        park.ShowCar();
                        break;
                    }

                    case "Show pricelist":
                    {
                        Console.Clear();
                        park.ShowPrice();
                        break;
                    }

                    case "Show current profit":
                    {
                        Console.Clear();
                        park.ShowProfit();
                        break;
                    }

                    case "Show all transactions":
                    {
                        Console.Clear();
                        park.ShowAllTransactions();
                        break;
                    }

                    case "Preview minute transactions":
                    {
                        Console.Clear();
                        park.PrevMinuteTransactions();
                        break;
                    }

                    case "Remaining count":
                    {
                        Console.Clear();
                        park.RemainingCount();
                        break;
                    }

                    case "Add cash to car":
                    {
                        Console.Clear();
                        park.AddCash();
                        break;
                    }

                    case "Exit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                }
            }
        }
    }
}