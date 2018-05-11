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

        public void AddCar()
        {
            Console.WriteLine(new string('-', 38));
            Console.WriteLine("Определите тип автомобиля: ");
            Car.ShowTypeCar();

            Random r = new Random(DateTime.Now.Millisecond);
            double money = r.Next(70, 200);

            try
            {
                if (currentId > parkingSpace)
                    throw new SpaceOverflowException();

                string category = Console.ReadLine();
                area.Add(new Car(currentId, money, category));
                Task t = Controller(currentId-1);
                currentId++;
            }
            catch(ArgumentException)
            {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такой тип транспорта недопустим!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch(SpaceOverflowException ex)
            {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            finally
            {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Press any key...");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void RemoveCar(int id)
        {
            try
            {
                if(area[id - 1].Cash < 0)
                    throw new PayException();

                area.RemoveAt(id - 1);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Машина с id{0} изгнана!", id);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine(new string('-', 48));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невозможно удалить машину за пределами парковки!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch(PayException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            finally
            {
                Console.WriteLine(new string('-', 48));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Press any key...");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public async Task Controller(int id)
        {

            while(true)
            {
                if (area[id].Cash < 0)
                    area[id].Cash = await Fine(id);
                else
                    area[id].Cash = await Payment(id);
            }
        }

        Task<double> Payment(int id)
        {
            return Task.Run(() =>
            {
                Thread.Sleep((int)timeout);
                Profit += Settings.priceList[(CarType)area[id].Category];
                return area[id].Cash - Settings.priceList[(CarType)area[id].Category];
            });
        }

        Task<double> Fine(int id)
        {
            return Task.Run(() =>
            {
                Thread.Sleep((int)timeout);
                Profit += Settings.priceList[(CarType)area[id].Category] * fine;
                return area[id].Cash - (Settings.priceList[(CarType)area[id].Category] * fine);
            });
        }
    }
}