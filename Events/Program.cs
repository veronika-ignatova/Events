namespace TestP
{
    public delegate void CarIsSold(object obj, string msg);
    public enum CarStatus
    {
        ForSell,
        Sold
    }

    public class Car
    {
        private CarStatus status;
        public event CarIsSold? CarIsSold;
        public string? Model { get; set; }
        public CarStatus Status { get => status; set => SetCarStatus(value); }

        private CarStatus SetCarStatus(CarStatus status)
        {
            if (status == CarStatus.ForSell)
            {
                CarIsSold?.Invoke(this, $"{Model} for sale");
                return CarStatus.ForSell;
            }
            else
            {
                CarIsSold?.Invoke(this, $"{Model} Sold");
                return CarStatus.Sold;
            }
        }

    }

    public class User
    {
        private Car? userCar;
        public event CarIsSold? CarSold;
        public string? Name { get; set; }
        public Car? Car { get => userCar; set => userCar = GetCarStatus(value); }

        private Car? GetCarStatus(Car? car)
        {
            if (this.userCar != null)
            {
                if(car != null)
                {
                    CarSold?.Invoke(this, $"Buyer can't but the {car.Model} because he has the car {userCar.Model}");
                    return userCar;
                }
                else
                {
                    CarSold?.Invoke(this, $"Buyer sold the {this.userCar.Model}");
                    userCar.Status = CarStatus.Sold;
                }
            }
            else
            {
                if (car != null)
                {
                    CarSold?.Invoke(this, $"Buyer bought the {car.Model}");
                    car.Status = CarStatus.ForSell;
                }
                else
                {
                    CarSold?.Invoke(this, $"Buyer try to buy the car");
                }
            }
            return car;
        }
    }

    class EventTest
    {
        static void Main(string[] args)
        {
            Car car = new Car() { Model = "Audi", Status = CarStatus.ForSell };
            car.CarIsSold += Foo;
            User buyer = new User() { Name = "Veronika", Car = car};
            buyer.CarSold += Foo;
            buyer.Car = car;
            buyer.Car = car;
            buyer.Car = null;
            buyer.Car = null;
            buyer.Car = car;


            ////User seller = new User() { Name = "Anya" };
            //buyer.CarIsSold += Foo;
            //seller.CarIsSold += Foo;
            //seller.SetOnSale(car);
            //buyer.BuyCar(car);
            //buyer.BuyCar(car);


        }

        static void Foo(object obj, string msg)
        {
            Console.WriteLine(msg);
        }
    }
}