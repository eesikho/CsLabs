namespace PlayerPassport;

class Program
{
    public static void Main( )
    {
        Console.WriteLine("Создание персонажа");
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введи класс (например: Рыцарь, Чародей, Нищий): ");
        string role =  Console.ReadLine();
        Console.WriteLine("Распределение характеристик");
        Console.Write("Введи уровень силы: ");
        string buffer =  Console.ReadLine();
        int strength = Convert.ToInt32(buffer);
        Console.Write("Введи уровень ловкости: ");
        string buffer1 =  Console.ReadLine();
        int dexterity = Convert.ToInt32(buffer1);
        Console.Write("Введи уровень интеллекта: ");
        string buffer2 =  Console.ReadLine();
        int intelligence = Convert.ToInt32(buffer2);
        Console.Write("Введи уровень веры: ");
        string buffer3 =  Console.ReadLine();
        int faith = Convert.ToInt32(buffer3);
        Console.WriteLine("===============[Профиль игрока]===============");
        Console.WriteLine($"Имя: {name}");
        Console.WriteLine($"Роль: {role}");
        Console.WriteLine($"Сила: {strength}");
        Console.WriteLine($"Ловкость: {dexterity}");
        Console.WriteLine($"Интеллект: {intelligence}");
        Console.WriteLine($"Вера: {faith}");
        Console.WriteLine("==============================================");        
    }
}