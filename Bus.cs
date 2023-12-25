using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBusTruck
{
    class Bus : Auto
    {
        protected int peopleCount;
        protected int weight;
        protected int avgWeight = 80;
        protected int cost = 60;
        protected int sum;
        public new void InfoInput()  //заполнение информации об автобусе
        {
            base.InfoInput();

            Console.Write("Введите максимальный объем бака(от 90 до 160 л): ");
            maxFuelVolume = int.Parse(Console.ReadLine());
            while (maxFuelVolume < 90 || maxFuelVolume > 160)
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

            Console.Write("Введите количество пассажиров: ");
            peopleCount = int.Parse(Console.ReadLine());
            sum += Cost(peopleCount);  //посчитали стоимость поездки
            weight = Weight(peopleCount); //посчитали вес

            Console.Clear();

            Move();
        }
        protected void Speed(ref double speed, ref double rashod) //как меняется скорость в зависимости от веса груза
        {
            if (weight >= 2000)
            {
                speed = start_speed / 1.5; //если вес груза больше 2к кг, то скорость уменьшается в 1.5 раза
                Console.WriteLine($"Вес составляет {weight} кг, скорость уменьшена в 1.5 раза и равна {Math.Round(speed, 3)}");
            }
            else if (weight >= 3500) //если вес груза >= 3500кг, то скорость уменьшается в 2 раза
            {
                speed = start_speed / 2;
                Console.WriteLine($"Вес составляет {weight} кг, скорость уменьшена в 1.5 раза и равна {Math.Round(speed, 3)}");
            }
            else
            {
                speed = start_speed;
                Console.WriteLine($"Вес составляет {weight} кг (меньше 2000 кг), скорость остается прежней и равна {start_speed}\"");
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
        protected override void Move()
        {
            Random rnd = new Random();
            alldistance = rnd.Next(100, 3000);
            Speed(ref speed, ref rashod);  //изменяем скорость
            InfoOutput();
            base.Move();
        }
        protected int Cost(int peopleCount)
        {
            sum = peopleCount * cost;
            return sum;
        }

        protected override void InfoOutput()  //изменяем вывод информации
        {
            base.InfoOutput();
            Console.WriteLine($"\tСкорость: {speed} км/ч");
            Console.WriteLine($"\tПройдено: {Math.Round(distanceTraveled, 3)} км");
            Console.WriteLine($"\tНеобходимо проехать: {Math.Round(alldistance, 3)} км");
            Console.WriteLine($"\tМаксимальный объем бензина в баке: {maxFuelVolume} км");
            Console.WriteLine($"\tОстаток бензина в баке: {Math.Round(fuelVolume, 3)}/{maxFuelVolume} л");
            Console.WriteLine($"\tЧисло пассажиров: {peopleCount}");
            Console.WriteLine($"\tВес: {weight} кг");
            Console.WriteLine($"\tЗаработок за сегодняшний день: {sum} руб");
            Console.WriteLine($"\tПробег: {probeg}");

            if (fuelVolume == 0 && distanceTraveled != 0)
            {
                Stop();
            }
        }

        private int Weight(int peopleCount)  //рассчитываем вес груза
        {
            weight = peopleCount * avgWeight;
            return weight;
        }

        private void Stop() //доехали до остановки
        {
            Random rnd = new Random();

            Console.WriteLine("\nВы доехали до остановки");

            int leave = rnd.Next(1, peopleCount);
            peopleCount -= leave; //кто-то мог выйти на остановке

            int addpeople = rnd.Next(1, peopleCount);
            peopleCount += addpeople; //добавили пассажиров

            sum += Cost(addpeople);
            weight = Weight(peopleCount);

            Console.WriteLine($"На остановке вышли {leave} людей и вошли {addpeople}");
            Console.WriteLine($"\nТеперь в автобусе {peopleCount} людей");

            Console.WriteLine("\nПродолжаем ехать");
        }
    }
}
