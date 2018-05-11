using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpace
{
    static class Settings
    {
        readonly public static Dictionary<CarType, int> priceList;

        readonly static private uint timeout;
        static public uint Timeout
        {
            get { return timeout; }
        }

        readonly static private int parkingSpace;
        static public int ParkingSpace
        {
            get { return parkingSpace; }
        }

        readonly static private double fine;
        static public double Fine
        {
            get { return fine; }
        }

        static Settings()
        {
            priceList = new Dictionary<CarType, int>()
            {
                { CarType.Motorcycle, 1},
                { CarType.Bus,        2},
                { CarType.Truck,      3},
                { CarType.Passenger,  5}
            };

            int t = new Random(DateTime.Now.Millisecond).Next(5, 10) * 1000;
            Settings.timeout = (uint)t;
            Settings.parkingSpace = new Random(DateTime.Now.Millisecond).Next(15, 30);
            Settings.fine = new Random(DateTime.Now.Millisecond).Next(1, 3) * 0.1456;
        }
    }
}
