using System;
using System.Collections.Generic;

namespace ParkingSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> menuItems = new List<string>() {
                "one",
                "two",
                "Exit"
            };
 
            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = Menu.drawMenu(menuItems);
                if (selectedMenuItem == "one")
                {
                    Console.Clear();
                    Console.WriteLine("HELLO one!"); Console.Read();
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}