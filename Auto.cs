using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBusTruck
{
    class Auto //родительский класс Авто
    {
        protected double speed;            //скорость движения
        protected double rashod;           //стандартный расход при скорости 100 км/ч
        protected double start_rashod;
        protected double speedRashod;      //как будет расход меняться при текущей скорости
        protected double fuelVolume;       //текущий объем бака
        protected double maxFuelVolume;    //максимальный объем бака
        protected double alldistance;      //все расстояние
        protected double distanceTraveled; //пройденное расстояние. Каждая поездка при возможном объеме бака = расстояние до след остановки
        protected double canDrive;         //сколько сможем проехать при текущем объеме бака
        protected double start_speed;      //изнеачальная скорость
        protected double probeg;           //пробег

        protected virtual void InfoInput()  //топливо для грузовика и автобуса разное, поэтому они будут помещены в Override
        {
            Console.Write("Введите расход топлива на 100 км (от 4 до 15): ");
            start_rashod = Convert.ToDouble(Console.ReadLine()); 
            while (start_rashod < 4 || start_rashod > 15)   //проверочка на дурака
            {
                Console.Write("Введите расход в диапазоне от 5 до 15 л (расход берется на скорость 100 км/ч): ");
                start_rashod = Convert.ToDouble(Console.ReadLine());
            }

            Console.Write("Введите скорость (от 60 до 150 км/ч): ");
            start_speed = Convert.ToInt32(Console.ReadLine());
            while (start_speed > 150 || start_speed <= 0)   //проверочка на дурака
            {
                Console.Write("Введите верную скорость: ");
                start_speed = Convert.ToInt32(Console.ReadLine());
            }

            rashod = Rashod();
        }

        protected virtual void Move() //метод езда
        { 
            Random rnd = new Random();

            distanceTraveled = 0;  //пройденное расстояние изначально равно нулю
            while (distanceTraveled < alldistance)
            {
                canDrive = HowMuchCanDrive();
                Console.WriteLine($"При объеме бака в {fuelVolume} л вы можете проехать {Math.Round(canDrive, 3)} км");  //round

                Console.ReadKey();
                Console.Clear();

                if (canDrive + distanceTraveled >= alldistance) //если можем проехать больше чем надо
                {
                    canDrive = alldistance;
                    distanceTraveled = canDrive;

                    fuelVolume = 0;
                    Console.WriteLine($"Вы проехали необходимое расстояние - {alldistance} - км\n");
                    probeg = Probeg(alldistance);  //обновили значение пробега

                    InfoOutput();

                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("Желаете продолжить?\n1. Да\n2. Нет");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1) //желаем
                    {
                        fuelVolume = fuelFilling();   //заполняем бак
                        distanceTraveled = 0;

                        speed = start_speed;
                        rashod = start_rashod;
                        Console.WriteLine();

                        Move();         //заново запускаем езду и генерируем расстояние
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }

                else if (canDrive < alldistance) //если не хватит проехать все расстояние
                {
                    distanceTraveled += canDrive;
                    if (distanceTraveled >= alldistance)
                    {
                        Console.WriteLine($"Вы проехали необходимое расстояние - {alldistance} - км\n");
                        probeg = Probeg(alldistance);  //обновили значение пробега 

                        Console.ReadKey();
                        Console.Clear();

                        Console.WriteLine("Желаете продолжить?\n1. Да\n2. Нет");
                        int user_choice = Convert.ToInt32(Console.ReadLine());

                        if (user_choice == 1) //желаем
                        {
                            fuelVolume = fuelFilling();   //заполняем бак

                            Console.WriteLine();
                            speed = start_speed;
                            rashod = start_rashod;
                            distanceTraveled = 0;
                            Move();         //заново запускаем езду и генерируем расстояние
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }

                    Console.WriteLine($"Вы проехали {Math.Round(distanceTraveled, 3)}"); //round
                    fuelVolume = 0;

                    InfoOutput();

                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("Ваш бак пуст. Желаете заправиться?\n1. Да\n2. Нет");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1) //желаем
                    {
                        fuelVolume = fuelFilling();   //заполняем бак
                    }
                    else
                    {
                        Environment.Exit(0);  //выходим
                    }
                }
            }
        }

        protected virtual double fuelFilling()  //заправляемся
        {
            Console.Write($"Укажите, на сколько хотите заправиться (максимум {maxFuelVolume}): ");
            double add = Convert.ToInt32(Console.ReadLine());
            while (add <= 0 || add > maxFuelVolume)
            {
                Console.Write("Введите верное значение: ");
                add = Convert.ToInt32(Console.ReadLine());
            }
            fuelVolume += add;
            return fuelVolume;
        }

        protected virtual double Rashod()  //изменение расхода в зависимости от скорости
        {
            if (start_speed <= 60)
            {
                rashod = start_rashod * 2;
                Console.WriteLine("Скорость низкая, расход увеличен в 2 раза");
            }
            else if (start_speed >= 90)
            {
                rashod = start_rashod * 2;
                Console.WriteLine("Скорость высокая, расход увеличен в 2 раза");
            }
            return rashod;
        }

        protected virtual void InfoOutput()  //вывод информации
        {
            Console.WriteLine("Информация о транспорте: ");
            Console.WriteLine($"\tРасход: {rashod} л/100км");
            //Console.WriteLine($"\tПройдено: {distanceTraveled} км");
            //Console.WriteLine($"\tНеобходимо проехать: {distanceTraveled} км");
            //Console.WriteLine($"\tМаскимальный объем бензина в баке: {maxFuelVolume} км");
            //Console.WriteLine($"\tОстаток бензина в баке: {Math.Round(fuelVolume, 3)}/{maxFuelVolume} л");
        }

        protected double Probeg(double alldistance) //высчитываем пробег для транспорта
        {
            probeg += alldistance;
            return probeg;
        }

        protected double HowMuchCanDrive()  //расчитываем сколько можем проехать
        {
            canDrive = fuelVolume / (start_rashod / 100);
            return canDrive;
        }
    }
}
