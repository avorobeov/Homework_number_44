﻿using System;
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

        public void AddWagons(Wagon wagon)
        {
            _wagons.Add(wagon);
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

            _numberTicketsSold = GetNumberTicketsSold();

            FormWagon(_numberTicketsSold);

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

        private int GetNumberTicketsSold()
        {
            Random random = new Random();

            int minNumberTicketsSold = 10;
            int maxNumberTicketsSold = 250;

            return random.Next(minNumberTicketsSold, maxNumberTicketsSold);
        }

        private void FormWagon(int passengerCount)
        {
            const int minWagonCapacity = 10;
            const int defaultWagonCapacity = 30;

            int numberWagon = (int)Math.Ceiling((double)passengerCount / defaultWagonCapacity);

            for (int i = 0; i < numberWagon; i++)
            {
                if (passengerCount > minWagonCapacity)
                {
                    _train.AddWagons(new Wagon(defaultWagonCapacity));

                    passengerCount -= defaultWagonCapacity;
                }
                else
                {
                    _train.AddWagons(new Wagon(minWagonCapacity));
                }
            }
        }

        private void ShowMessage(string text, ConsoleColor consoleColor = ConsoleColor.Blue)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}