using System;

namespace Homework_number_44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandPrepareTrainDispatch = "1";
            const string CommandSendTrain = "2";
            const string CommandExit = "3";

            Station station = new Station();

            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                station.ShowInfo();

                Console.WriteLine($"\n\nДля того что бы подготовить поезд к отправке нажмите: {CommandPrepareTrainDispatch}\n" +
                                  $"Для  того что бы отправить поезда нажмите:{CommandSendTrain}\n" +
                                  $"Для того что бы выйти нажмите: {CommandExit}\n");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandPrepareTrainDispatch:
                        station.PrepareTrainDispatch();
                        break;

                    case CommandSendTrain:
                        station.SendTrain();
                        break;

                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Такой комады нет в списке команд!");
                        break;
                }

                Console.WriteLine("Для продолжения ведите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Train
    {
        public int QuantityWagons { get; private set; }
        public string StartCity { get; private set; }
        public string EndCity { get; private set; }

        public void SetDestination(string startCity, string endCity)
        {
            StartCity = startCity;
            EndCity = endCity;
        }

        public void SetWagons(int quantityWagons)
        {
            QuantityWagons = quantityWagons; 
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

            _numberTicketsSold = GetQuantityTicketsSold();

            FormWagon(_numberTicketsSold);

            _isReadyForDeparture = true;
        }

        public void ShowInfo()
        {
            ShowMessage($"Досье\n" +
                        $"Место отправки поезда: {_train.StartCity}\n" +
                        $"Место прибытия поезда: {_train.EndCity}\n" +
                        $"Количество вагонов в поезде:{_train.QuantityWagons}\n" +
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
                _numberTicketsSold = 0;

                ShowMessage("Поезд успешно отправлен!", ConsoleColor.Magenta);
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

        private int GetQuantityTicketsSold()
        {
            Random random = new Random();

            int minNumberTicketsSold = 10;
            int maxNumberTicketsSold = 250;

            return random.Next(minNumberTicketsSold, maxNumberTicketsSold);
        }

        private void FormWagon(int passengerCount)
        {
            int wagonCapacity = 30;

            int numberWagon = (int)Math.Ceiling((double)passengerCount / wagonCapacity);

           _train.SetWagons(numberWagon);
        }

        private void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Blue)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}