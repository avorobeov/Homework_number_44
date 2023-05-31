using System;
using System.Collections.Generic;
using System.Linq;

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
        public Train(string startCity, string endCity, int numberWagons)
        {
            StartCity = startCity;
            EndCity = endCity;
            NumberWagons = numberWagons;
        }

        public int NumberWagons { get; private set; }
        public string StartCity { get; private set; }
        public string EndCity { get; private set; }
    }

    class Station
    {
        private bool _isReadyForDeparture;
        private int _numberTicketsSold;
        private int _numberWagon;
        private string _startCity;
        private string _endCity;

        public void PrepareTrainDispatch()
        {
            CreateDestination();

            _numberTicketsSold = GetNumberTicketsSold();

            FormWagon(_numberTicketsSold);

            _isReadyForDeparture = true;
        }

        public void ShowInfo()
        {
            ShowMessage($"Досье\n" +
                        $"Место отправки поезда: {_startCity}\n" +
                        $"Место прибытия поезда: {_endCity}\n" +
                        $"Количество вагонов в поезде:{_numberWagon}\n" +
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
                Train train = new Train(_startCity,_endCity,_numberWagon);

                _isReadyForDeparture = false;
                _numberTicketsSold = 0;
                _numberWagon = 0;
                _startCity = "";
                _endCity = "";

                train = null;

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
            _startCity = Console.ReadLine();

            ShowMessage("Укажите город в который должен прибыть поезд");
            _endCity = Console.ReadLine();
        }

        private int GetNumberTicketsSold()
        {
            Random random = new Random();

            int minNumberTicketsSold = 10;
            int maxNumberTicketsSold = 250;

            return random.Next(minNumberTicketsSold, maxNumberTicketsSold);
        }

        private void FormWagon(int passengerCount)
        {
            int wagonCapacity = 30;
            _numberWagon = (int)Math.Ceiling((double)passengerCount / wagonCapacity);
        }

        private void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Blue)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}