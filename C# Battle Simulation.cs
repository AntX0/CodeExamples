using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soldier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Barrack barrack = new Barrack();

            barrack.Run();
        }
    }

    public class Soldier 
    {
        public Soldier(string name, int timeOnDuty, string rank, string weapon) 
        {
            Name = name;
            TimeOnDuty = timeOnDuty;
            Rank = rank;
            Weapon = weapon;
        }

        public string Name { get; private set; }
        public int TimeOnDuty { get; private set; }
        public string Rank { get; private set; }
        public string Weapon { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} - {Rank}. Вооружение - {Weapon}. Срок службы - {TimeOnDuty} месяцев");
            Console.WriteLine();
        }
    }

    public class Barrack
    {
        private List<Soldier> _soldiers;

        public Barrack()
        {
            _soldiers = new List<Soldier>()
            {
                new Soldier("Thomas", 6, "Private", "Assault Rifle"),
                new Soldier("Jack", 12, "Private", "Assault Rifle"),
                new Soldier("Hoken", 32, "Lieutnant", "Semi Automatic Rifle"),
                new Soldier("Josh", 18, "Sergant", "Sniper Rifle"),
                new Soldier("Robbert", 2, "Private", "Assault Rifle"),
            };
        }

        public void Run()
        {
            const string CommandShowAllSoldiers = "1";
            const string CommandShowSoldiersRanks = "2";
            const string CommandExitProgram = "3";

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine($"\n{CommandShowAllSoldiers} - показать всех солдат" +
                    $"\n{CommandShowSoldiersRanks} -  показать солдат по званию" +
                    $"\n{CommandExitProgram} - выйти");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandShowAllSoldiers:
                        ShowAllSoldiers();
                        break;

                    case CommandShowSoldiersRanks:
                        ShowSoldiersRanks();
                        break;

                    case CommandExitProgram:
                        isWorking = false;
                        break;
                }
            }
        }

        private void ShowAllSoldiers()
        {
            foreach (Soldier soldier in _soldiers)
            {
                soldier.ShowInfo();
            }
        }

        private void ShowSoldiersRanks()
        {
            var soldiersRanks = _soldiers.Select(soldier => new { soldier.Name, soldier.Rank });

            foreach (var soldier in soldiersRanks)
            {
                Console.WriteLine($"{soldier.Name}.{soldier.Rank}");
            }
        }
    }
}
