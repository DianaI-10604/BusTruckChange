using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBusTruck
{
    class Truck : Auto
    {
        protected double goodsWeight;  //вес груза

        public new void InfoInput() //переопределенный метод заполнения информации
        {
            base.InfoInput();
            Console.Write("Введите максимальный объем бака(от 160 до 300 л): ");
            maxFuelVolume = int.Parse(Console.ReadLine());
            while (maxFuelVolume < 160 || maxFuelVolume > 300)
            {
                Console.Write("Введите верное значение: ");
                maxFuelVolume = int.Parse(Console.ReadLine());
            }

            Console.Write("Введите текущий объем бака: ");
            fuelVolume = int.Parse(Console.ReadLine());
            while (fuelVolume < 0 || fuelVolume > maxFuelVolume)
            {
                Console.Write("Введите верное значение: ");
                fuelVolume = int.Parse(Console.ReadLine());
            }

            Console.Write("Укажите вес груза: ");
            goodsWeight = Convert.ToDouble(Console.ReadLine());

            Console.Clear();
            Move();
        }
        protected override void Move()  //метод езда для грузовика
        {
            Random rnd = new Random();
            alldistance = rnd.Next(1000, 10000);
            Speed(ref speed, ref rashod);  //изменяем скорость
            InfoOutput();
            base.Move();
        }
        protected void Speed(ref double speed, ref double rashod) //как меняется скорость в зависимости от веса груза
        {
            if (goodsWeight >= 2000)
            {
                speed = start_speed / 1.5; //если вес груза больше 2к кг, то скорость уменьшается в 1.5 раза
                Console.WriteLine($"Вес груза составляет {goodsWeight}, скорость уменьшена в 1.5 раза и равна {speed}");
            }
            else if (goodsWeight >= 3500) //если вес груза >= 3500кг, то скорость уменьшается в 2 раза
            {
                speed = start_speed / 2;
                Console.WriteLine($"Вес груза составляет {goodsWeight}, скорость уменьшена в 1.5 раза и равна {speed}");
            }
            else
            {
                speed = start_speed;
                Console.WriteLine($"Вес груза составляет {goodsWeight}, скорость остается прежней и равна {speed}\"");
            }

            if (speed <= 60) //если слишком низкая или слишком высокая скорость, то расход увеличивается вдвое
            {
                rashod = start_rashod * 2;
            }
            else if (speed >= 90)
            {
                rashod = start_rashod * 2;
            }
            else
            {
                Console.WriteLine("Вы движетесь с оптимальной скоростью, расход не изменился");
            }
        }

        protected void DropGruz()
        {
            Console.WriteLine("\nВыберитe действие: \n1. Разгрузка\n2. Погрузка");
            Console.Write("\nВаш выбор: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write($"\nУкажите, сколько груза хотите убрать (доступно: {goodsWeight}): ");
                int gruz = Convert.ToInt32(Console.ReadLine());
                goodsWeight -= gruz;
                Console.WriteLine($"Вес груза: {goodsWeight}");
            }
            else
            {
                Console.Write($"\nУкажите, сколько груза хотите добавить: ");
                int gruz = Convert.ToInt32(Console.ReadLine());
                goodsWeight += gruz;
                Console.WriteLine($"Вес груза: {goodsWeight}");
            }
        }

        protected override void InfoOutput()
        {
            base.InfoOutput();
            Console.WriteLine($"\tСкорость: {speed} км/ч");
            Console.WriteLine($"\tВес груза: {goodsWeight}");
            Console.WriteLine($"\tПройдено: {Math.Round(distanceTraveled, 3)} км");
            Console.WriteLine($"\tНеобходимо проехать: {Math.Round(alldistance, 3)} км");
            Console.WriteLine($"\tМаксимальный объем бензина в баке: {maxFuelVolume} км");
            Console.WriteLine($"\tОстаток бензина в баке: {Math.Round(fuelVolume, 3)}/{maxFuelVolume} л");
            Console.WriteLine($"\tПробег: {probeg}");

            if (fuelVolume == 0 && distanceTraveled != 0)
            {
                DropGruz();
            }
        }
    }
}
