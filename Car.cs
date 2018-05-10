using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingArea
{
    class Car
    {
        readonly int id;
        public int Id
        {
            get { return id; }
        } //обработка исключений(добавить)

        double cash;
        public double Cash 
        {
            get { return cash;  }
            set { cash = value; }
        } //Обработка исключений(добавить)

        readonly CarType category;
        public CarType Category 
        {
            get { return category; }
        } //Обработка исключений(добавить)

        public Car(int id, double cash, string category)
        {
            this.id = id;
            this.cash = cash;

            object container = Enum.Parse(typeof(CarType), category);
            this.category = (CarType)container;
        }

        public void AddCash()
        {
            Console.WriteLine("Введите суму пополнения: ");
            double cash = Convert.ToInt32(Console.ReadLine());

            this.cash += cash;
        }

        public static void ShowTypeCar()
        {
            Array car = Enum.GetValues(typeof(CarType));
            Array colorList = Enum.GetValues(typeof(ConsoleColor));
            for(int i = 0; i < car.Length; i++)
            {
                Console.ForegroundColor = (ConsoleColor)colorList.GetValue(i+3);
                Console.Write("[{0}] ", car.GetValue(i));
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    enum CarType
    {
        Passenger = 1,
        Truck,
        Bus,
        Motorcycle
    }
}