using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_number_44
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Wagon
    {
        public Wagon(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; private set; }
    }

    class Train
    {
        private List<Wagon> _wagons = new List<Wagon>();

        public int NumberWagons => _wagons.Count();
        public string StartCity { get; private set; }
        public string EndCity { get; private set; }

        public void SetDestination(string startCity, string endCity)
        {
            StartCity = startCity;
            EndCity = endCity;
        }

        public void AddWagons(List<Wagon> wagons)
        {
            _wagons = wagons;
        }
    }

    class Station
    {
        private Train _train = new Train();

        private int _numberTicketsSold;
        private bool _isReadyForDeparture;

        public void PrepareTrainDispatch()
        {
            CreateDestination();

            FormWagon(GetNumberTicketsSold());

            _isReadyForDeparture = true;
        }

        public void ShowInfo()
        {
            ShowMessage($"Досье\n" +
                        $"Место отправки поезда: {_train.StartCity}\n" +
                        $"Место прибытия поезда: {_train.EndCity}\n" +
                        $"Количество вагонов в поезде:{_train.NumberWagons}\n" +
                        $"Количество проданных Белетов:{_numberTicketsSold}\n");

            if (_isReadyForDeparture == true)
            {
                ShowMessage("Поезд готов к отправке!", ConsoleColor.Green);
            }
            else
            {
                ShowMessage("Поезд не готов к отправке!", ConsoleColor.Red);
            }
        }

        public void SendTrain()
        {
            if (_isReadyForDeparture == true)
            {
                _train = new Train();

                _isReadyForDeparture = false;
            }
            else
            {
                ShowMessage("К сожалению поест не готов к отправке!", ConsoleColor.Red);
            }
        }

        private void CreateDestination()
        {
            ShowMessage("Укажите с какого города поезд должен отправится");
            string startCity = Console.ReadLine();

            ShowMessage("Укажите город в который должен прибыть поезд");
            string endCity = Console.ReadLine();

            _train.SetDestination(startCity, endCity);
        }

        private int GetNumberTicketsSold()
        {
            Random random = new Random();

            int minNumberTicketsSold = 10;
            int MaxNumberTicketsSold = 100;

            return random.Next(minNumberTicketsSold, MaxNumberTicketsSold);
        }

        private void FormWagon(int passengerCount)
        {
            List<Wagon> wagons = new List<Wagon>();

            int wagonCapacity = 30;
            int numberWagon = (int)Math.Ceiling((double)passengerCount / wagonCapacity);

            for (int i = 0; i < numberWagon; i++)
            {
                wagons.Add(new Wagon(wagonCapacity));
            }

            _train.AddWagons(wagons);
        }

        private void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Blue)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
