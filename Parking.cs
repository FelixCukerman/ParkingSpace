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

        static void Pause()
        {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Press any key...");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Gray;
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
                Pause();
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
                Pause();
            }
        }

        void AbstractShow(IEnumerable<Car> carList)
        {
            int i = -1;
            var enumerator = carList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Id        : {0}", enumerator.Current.Id);
                Console.WriteLine("Current   : {0}", enumerator.Current.Category);
                Console.WriteLine("Cash      : {0}$", Math.Round(enumerator.Current.Cash, 2));
                Console.ForegroundColor = ConsoleColor.Gray;
                i++;
            }
            if (i == -1)
            {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Парковка пуста!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public void ShowCar()
        {
            this.AbstractShow(area);
            Pause();
        }

        public void AddCash()
        {
            Console.Write("Введите id машины: ");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                area[id - 1].AddCash();
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Машины с id:{0} не существует", id);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            finally
            {
                Pause();
            }
        }

        public void ShowPrice()
        {
            foreach (var v in Settings.priceList)
            {
                Console.WriteLine(v);
            }
        }

        public void ShowProfit()
        {
            System.Console.WriteLine(new string('-', 38));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Current profit: {0}$", Math.Round(Profit, 2));
            Console.ForegroundColor = ConsoleColor.Gray;
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
                int price = Settings.priceList[(CarType)area[id].Category];

                Thread.Sleep((int)timeout);
                Profit += price;
                transaction.Add(new Transaction(id, price));
                return area[id].Cash - price;
            });
        }

        Task<double> Fine(int id)
        {
            return Task.Run(() =>
            {
                int price = Settings.priceList[(CarType)area[id].Category];

                Thread.Sleep((int)timeout);
                Profit += price * fine;
                transaction.Add(new Transaction(id, price));
                return area[id].Cash - (price * fine);
            });
        }
    }
}