namespace ServerConfig;

public class Program
{
    static void Main()
    {
        Console.WriteLine("Введите количество игроков: "); 
        int players = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Введите количество оперативной памяти: ");
        int RAM = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Сервер публичный? (true/false): ");
        bool isPublic = bool.Parse(Console.ReadLine());
        
        Console.WriteLine("Есть ли пароль? (true/false): ");
        bool hasPassword = bool.Parse(Console.ReadLine());
        
        string result = CheckConfiguration(players, RAM, isPublic, hasPassword);
        Console.WriteLine(result);
    }

    public static string CheckConfiguration(int players, int RAM, bool isPublic, bool hasPassword)
    {
        if (players <= 0)
        {
            return "Запуск невозможен: количество игроков должно быть больше нуля.";
        }

        if (RAM <= 1)
        {
            return "Запуск невозможен: серверу недостаточно оперативной памяти.";
        }
        else if (players > 50 && RAM <= 4)
        {
            return
                "Запуск возможен с предупреждением: для такого количества игроков рекомендуется больше оперативной памяти.";
        }
        else if (isPublic && hasPassword)
        {
            return "Запуск возможен с предупреждением: публичный сервер защищён паролем.";
        }
        else
        {
            return "Сервер готов к запуску.";
        }    
    }
}