﻿using System;
using System.Linq;

namespace PraktikantAndPratsivnik
{
    // Базовий клас "Практикант"
    class Praktikant
    {
        public string Prizvyshche { get; set; }
        public string Imya { get; set; }
        public string Vuz { get; set; }

        // Віртуальний метод для введення даних
        public virtual void ZadatyDani()
        {
            Console.WriteLine("Введіть прізвище практиканта:");
            Prizvyshche = Console.ReadLine();
            Console.WriteLine("Введіть ім'я практиканта:");
            Imya = Console.ReadLine();
            Console.WriteLine("Введіть назву навчального закладу:");
            Vuz = Console.ReadLine();
            Console.WriteLine("Метод ZadatyDani викликано в класі Praktikant.");
        }

        // Віртуальний метод для перевірки симетричності прізвища
        public virtual bool ChiSimetrichnePrizvyshche()
        {
            string reversedPrizvyshche = string.Join("", Prizvyshche.ToCharArray().Reverse());
            bool isSymmetric = Prizvyshche.Equals(reversedPrizvyshche, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"Метод ChiSimetrichnePrizvyshche викликано в класі Praktikant: {isSymmetric}");
            return isSymmetric;
        }

        // Невіртуальний метод для демонстрації відсутності поліморфізму
        public void NevyrtualnyMethod()
        {
            Console.WriteLine("Це невіртуальний метод класу Praktikant.");
        }
    }

    // Похідний клас "ПрацівникФірми"
    class PratsivnikFirmy : Praktikant
    {
        public DateTime DataPryjomu { get; set; }
        public string Posada { get; set; }
        public string ZakincheneVuz { get; set; }

        // Перевизначення методу для введення даних
        public override void ZadatyDani()
        {
            base.ZadatyDani();  // Викликаємо базовий метод
            Console.WriteLine("Метод ZadatyDani викликано в класі PratsivnikFirmy.");
            Console.WriteLine("Введіть дату прийому на роботу (в форматі рік-місяць-день):");
            DataPryjomu = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введіть посаду працівника:");
            Posada = Console.ReadLine();
            Console.WriteLine("Введіть навчальний заклад, який закінчив працівник:");
            ZakincheneVuz = Console.ReadLine();
        }

        // Перевизначення методу для перевірки симетричності прізвища
        public override bool ChiSimetrichnePrizvyshche()
        {
            bool result = base.ChiSimetrichnePrizvyshche();  // Викликаємо базовий метод
            Console.WriteLine($"Метод ChiSimetrichnePrizvyshche викликано в класі PratsivnikFirmy: {result}");
            return result;
        }

        // Метод для визначення стажу роботи
        public int StazhRoboti()
        {
            int stazh = DateTime.Now.Year - DataPryjomu.Year;
            if (DateTime.Now < DataPryjomu.AddYears(stazh))
                stazh--;
            return stazh;
        }

        // Невіртуальний метод для демонстрації
        public new void NevyrtualnyMethod()
        {
            Console.WriteLine("Це віртуальний метод класу PratsivnikFirmy.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Динамічний вибір типу
            Praktikant praktikant;
            Console.WriteLine("Виберіть тип об'єкта для створення (1 - Praktikant, 2 - PratsivnikFirmy):");
            string choice = Console.ReadLine();

            if (choice == "2")
                praktikant = new PratsivnikFirmy();
            else
                praktikant = new Praktikant();

            // Виклик віртуальних методів
            praktikant.ZadatyDani();  // Віртуальний метод
            Console.WriteLine($"Прізвище симетричне? {praktikant.ChiSimetrichnePrizvyshche()}");  // Віртуальний метод

            // Демонстрація невіртуальних методів
            praktikant.NevyrtualnyMethod();

            // Якщо це PratsivnikFirmy, визначити стаж роботи
            if (praktikant is PratsivnikFirmy pratsivnik)
            {
                Console.WriteLine($"Стаж роботи: {pratsivnik.StazhRoboti()} років");
                pratsivnik.NevyrtualnyMethod();
            }
        }
    }
}
