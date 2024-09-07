using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopPlayers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();

            storage.Run();
        }
    }

    public class Product
    {
        public Product(string name, int expirationDate, int issueDate)
        {
            Name = name;
            ExpirationDate = expirationDate;
            IssueDate = issueDate;
        }

        public string Name { get; private set; }
        public int IssueDate { get; private set; }
        public int ExpirationDate { get; private set; }
       

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name}\nСрок годности - {ExpirationDate} лет\nГод производства - {IssueDate}");
        }
    }

    public class Storage
    {
        private List<Product> _products;
        private int _maxExpirationDate;
        private int _minExpirationDate;
        private int _minIssueDate;
        private int _currentYear;

        public Storage()
        {
            _maxExpirationDate = 10;
            _minExpirationDate = 5;
            _minIssueDate = 2015;
            _currentYear = 2024;

            _products = new List<Product>()
            {
                new Product("Тушенка", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Каша", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Килька", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Селедка", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Горошек", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Томаты", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Молоко", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Йогурт", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate),Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Кукуруза", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Чеснок", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
                new Product("Греча", Utils.GetRandomNumber(_minExpirationDate, _maxExpirationDate), Utils.GetRandomNumber(_minIssueDate, _currentYear)),
            };
        }

        public void Run()
        {
            const string CommandShowAllProducts = "1";
            const string CommandShowExpiredProducts = "2";
            const string CommandExitProgram = "3";

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine($"\n{CommandShowAllProducts} - показать все продукты" +
                    $"\n{CommandShowExpiredProducts} -  показать просрочку" +
                    $"\n{CommandExitProgram} - выйти");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowAllProducts:
                        ShowAllProducts();
                        break;

                    case CommandShowExpiredProducts:
                        ShowAllExpiredProducts();
                        break;

                    case CommandExitProgram:
                        isWorking = false;
                        break;
                }
            }
        }

        private void ShowAllProducts()
        {
            Console.Clear();

            ShowContentInList(_products);
        }

        private void ShowAllExpiredProducts()
        {
            Console.Clear();

            List<Product> expiredProducts = new List<Product>();

            expiredProducts = _products.Where(product => _currentYear - product.IssueDate > product.ExpirationDate).ToList();

            ShowContentInList(expiredProducts);
        }

        private void ShowContentInList(List<Product> players)
        {
            foreach (Product player in players)
            {
                player.ShowInfo();
                Console.WriteLine();
            }
        }
    }

    public static class Utils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => s_random.Next(minValue, maxValue);

        public static int GetRandomNumber(int maxValue) => s_random.Next(maxValue);
    }
}
