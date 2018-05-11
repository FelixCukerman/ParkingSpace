using System;

namespace ParkingSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking park = Parking.Create;

            while(true)
            {
                park.AddCar();
                park.ShowCar();
                park.ShowProfit();
                park.RemainingCount();
                park.PrevMinuteTransactions();
            }
        }
    }
}