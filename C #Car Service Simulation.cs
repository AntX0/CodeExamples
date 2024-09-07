using System;
using System.Collections.Generic;
using System.Linq;

namespace CarService1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandShowNextCar = "1";
            const string CommandFixCurrentCar = "2";
            const string CommandExitProgram = "3";

            bool isWorking = true;
            Service service = new Service();

            while (isWorking)
            {
                service.ShowEarnings();
                Console.WriteLine();
                service.ShowCar();

                Console.WriteLine($"\n{CommandShowNextCar} - следующая машина\n" +
                   $"{CommandFixCurrentCar} - починить машину\n" +
                   $"{CommandExitProgram} - выйти");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowNextCar:
                        service.GenerateNewCar();
                        break;

                    case CommandFixCurrentCar:
                        service.Fix();
                        break;

                    case CommandExitProgram:
                        isWorking = false;
                        break;
                }
            }
        }
    }

    public class Car
    {
        private List<Detail> _details;

        public Car()
        {
            _details = new List<Detail>() { new Wheel(), new Engine(), new GearBox(), new FuelTank() };
            BreakRandomDetail();
        }

        public Detail BrokenDetail { get; private set; }

        public void ShowInfo()
        {
            foreach (Detail detail in _details)
            {
                detail.ShowStatus();

                if (detail.IsBroken)
                    Console.WriteLine($"Стоимость замены {detail.Name} - {detail.RepairCost}");
            }

            Console.WriteLine();
            Console.WriteLine(new string('*', 25));
        }

        public bool HasBrokenDetails()
        {
            foreach (Detail detail in _details)
            {
                if (detail.IsBroken == true)
                    return true;
            }

            return false;
        }

        private void BreakRandomDetail()
        {
            int randomIndex = Utils.GetRandomNumber(_details.Count);
            _details[randomIndex].ChangeStatusToBroken();
            BrokenDetail = _details[randomIndex];
        }
    }

    public class Service
    {
        private Storage _storage;
        private List<int> _successValues;
        private int _earnedMoney;
        private int _serviceCost;
        private int _penalty;
        private Car _car;

        public Service()
        {
            _car = new Car();
            _successValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            _earnedMoney = 0;
            _serviceCost = 15;
            _penalty = 10;
            _storage = new Storage();
            _storage.ShowAvailableDetails();
        }

        public void Fix()
        {
            Console.Clear();

            int maxValue = 14;

            if (_car.HasBrokenDetails())
            {
                if (_storage.IsAvailable(_car.BrokenDetail.Index) && _successValues.Contains(Utils.GetRandomNumber(maxValue)))
                {
                    _car.BrokenDetail.ChangeStatusToFixed();
                    _earnedMoney += _serviceCost + _car.BrokenDetail.RepairCost;
                }
                else
                {
                    _earnedMoney -= _penalty;
                    Console.WriteLine("На складе нет такой детали или была заменена неправильная деталь");
                    Console.WriteLine($"Вы оштрафованы на {_penalty}");
                }
            }
            else
            {
                Console.WriteLine("Машина в полном порядке");
            }
        }

        public void GenerateNewCar()
        {
            _car = new Car();
            Console.Clear();
            _storage.ShowAvailableDetails();
        }

        public void ShowCar()
        {
            _car.ShowInfo();
        }

        public void ShowEarnings()
        {
            Console.WriteLine("Денег заработано - " + _earnedMoney);
        }
    }

    public class Storage
    {
        private Dictionary<Detail, int> _detailsAvailable;
        private int _wheelsAmount;
        private int _enginesAmount;
        private int _gearBoxesAmount;
        private int _fuelTanksAmount;

        public Storage()
        {
            _wheelsAmount = 3;
            _enginesAmount = 2;
            _gearBoxesAmount = 4;
            _fuelTanksAmount = 5;

            _detailsAvailable = new Dictionary<Detail, int>()
            {
                { new Wheel(), _wheelsAmount },
                { new Engine(), _enginesAmount },
                { new GearBox(), _gearBoxesAmount },
                { new FuelTank(), _fuelTanksAmount },
            };
        }

        public void ShowAvailableDetails()
        {
            foreach (KeyValuePair<Detail, int> detail in _detailsAvailable)
            {
                Console.WriteLine($"{detail.Key.Name} осталось {detail.Value}");
            }
        }

        public bool IsAvailable(int index)
        {
            for (int i = 0; i < _detailsAvailable.Count; i++)
            {
                if (_detailsAvailable.ElementAt(i).Key.Index == index && _detailsAvailable.ElementAt(i).Value > 0)
                {
                    _detailsAvailable[_detailsAvailable.ElementAt(i).Key] -= 1;
                    return true;
                }
            }

            return false;
        }
    }

    public abstract class Detail
    {
        public Detail()
        {
            IsBroken = false;
        }

        public string Name { get; protected set; }
        public int RepairCost { get; protected set; }
        public bool IsBroken { get; protected set; }
        public int Index { get; protected set; }

        public void ChangeStatusToFixed()
        {
            IsBroken = false;
        }

        public void ChangeStatusToBroken()
        {
            IsBroken = true;
        }

        public void ShowStatus()
        {
            if (IsBroken == true)
                Console.WriteLine($"Деталь нуждается в замене - {Name}");
            else
                Console.WriteLine($"Деталь исправна - {Name}");
        }
    }

    public class Wheel : Detail
    {
        public Wheel()
        {
            RepairCost = 25;
            Name = "Колесо";
            Index = 0;
        }
    }

    public class Engine : Detail
    {
        public Engine()
        {
            RepairCost = 100;
            Name = "Двигатель";
            Index = 1;
        }
    }

    public class GearBox : Detail
    {
        public GearBox()
        {
            RepairCost = 75;
            Name = "Коробка передач";
            Index = 2;
        }
    }

    public class FuelTank : Detail
    {
        public FuelTank()
        {
            RepairCost = 30;
            Name = "Бензобак";
            Index = 3;
        }
    }

    public static class Utils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => s_random.Next(minValue, maxValue);

        public static int GetRandomNumber(int maxValue) => s_random.Next(maxValue);
    }
}
