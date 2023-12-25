namespace FinalBusTruck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите транспорт: ");
            Console.WriteLine("1. Автобус: ");
            Console.WriteLine("2. Грузовик: ");

            Console.Write("Ваш выбор: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch(choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали Автобус!\n");
                    Bus bus = new Bus();
                    bus.InfoInput();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Вы выбрали Грузовик!\n");
                    Truck truck = new Truck();
                    truck.InfoInput();
                    break;
            }
        }
    }
}
