using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HospitalWard hospital = new HospitalWard();

            hospital.Run();
        }
    }

    public class Patient
    {
        public Patient(string name, int age, string disease)
        {
            Name = name;
            Age = age;
            Disease = disease;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {Name}\nВозраст - {Age}\nБолезнь - {Disease}");
        }
    }

    public class HospitalWard
    {
        private List<Patient> _patients;
       
        public HospitalWard()
        {
            _patients = new List<Patient>()
            {
                new Patient("Arthur Simpson", 28, "flu"),
                new Patient("Joseph O’Brien", 28, "sore throat"),
                new Patient("Claude Patterson", 37, "runny nose"),
                new Patient("Peter Ward", 82, "runny nose"),
                new Patient("Mitchell Parker", 43, "flu"),
                new Patient("Dominic Webb", 92, "runny nose"),
                new Patient("Aysha Knox", 65, "sore throat"),
                new Patient("Rose Summers", 23, "flu"),
                new Patient("Aishah Sykes", 43, "sore throat"),
                new Patient("Montgomery Tanner", 32, "flu"),
                new Patient("Ayrton Hendrix", 21, "sore throat"),
            };
        }

        public void Run()
        {
            const string CommandSortByName = "1";
            const string CommandSortByAge = "2";
            const string CommandSortByDisease = "3";
            const string CommandExitProgram = "4";

            bool isWorking = true;

            ShowAllPatients();

            while (isWorking)
            {
                Console.WriteLine($"\n{CommandSortByName} - сортировать по имени" +
                    $"\n{CommandSortByAge} - сортировать по возрасту" +
                    $"\n{CommandSortByDisease} - искать по болезни" +
                    $"\n{CommandExitProgram} - выйти");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSortByName:
                        SortByName();
                        break;

                    case CommandSortByAge:
                        SortByAge();
                        break;

                    case CommandSortByDisease:
                        SsearchByDisease();
                        break;

                    case CommandExitProgram:
                        isWorking = false;
                        break;
                }
            }
        }
        
        private void ShowAllPatients()
        {
            ShowContent(_patients);
        }

        private void SortByName()
        {
            Console.Clear();

            var sortedByName = _patients.OrderBy(patient => patient.Name).ToList();

            ShowContent(sortedByName);
        }

        private void SortByAge()
        {
            Console.Clear();

            var sortedByAge = _patients.OrderBy(patient => patient.Age).ToList();

            ShowContent(sortedByAge);
        }

        private void SsearchByDisease()
        {
            Console.Clear();
            Console.WriteLine("Введите болезнь");

            string disease = Console.ReadLine().ToLower();

            var sortedByDisease = _patients.Where(patient => patient.Disease.Equals(disease)).ToList();

            ShowContent(sortedByDisease);
        }

        private void ShowContent(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                patient.ShowInfo();
                Console.WriteLine();
            }
        }
    }
}
