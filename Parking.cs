using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingSpace
{
    class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static int currentId = 1;
        private double Profit { get; set; }
        private List<Car> area;
        private List<Transaction> transaction;

        private uint timeout;
        private int parkingSpace;
        private double fine;

        protected Parking()
        {
            area = new List<Car>();
            transaction = new List<Transaction>();

            timeout = Settings.Timeout;
            parkingSpace = Settings.ParkingSpace;
            fine = Settings.Fine;
        }

        public static Parking Create
        {
            get
            {
                return lazy.Value;
            }
        }

        public void RemainingCount()
        {
            Console.WriteLine(new string('-', 38));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Осталось мест      : {0}", parkingSpace - area.Count);
            Console.WriteLine("Занято мест        : {0}", area.Count);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}